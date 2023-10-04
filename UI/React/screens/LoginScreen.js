import { View, Text, SafeAreaView, TouchableOpacity, StatusBar, TextInput, StyleSheet } from 'react-native'
import { useState, useEffect } from 'react'
import React from 'react'
import { useNavigation, useFocusEffect } from '@react-navigation/native'
import { RecieveToken, HealthCheck } from '../services/IdentityService'
import { CreateLogger } from '../Logger'
import { SetToken, CheckToken, GetIdFromToken, GetToken } from '../TokenHandler'
import { useDispatch } from 'react-redux'
import { SetUser } from '../redux/UserAction'
import LoadingOverlay from '../helpers/LoadingOverlay'
const log = CreateLogger("LoginScreen");

const LoginScreen = () => {
    const navigation = useNavigation();
    const [username, setUsername] = useState('');
    const [usernameError, setUsernameError] = useState(false);
    const [password, setPassword] = useState('');
    const [passwordError, setPasswordError] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [warningMessage, setWarningMessage] = useState(null);
    const [loginSuccess, setLoginSuccess] = useState(false);

    const dispatch = useDispatch();
    useFocusEffect(
        React.useCallback(() => {
            setUsername('');
            setPassword('');
            setPasswordError(false);
            setUsernameError(false);
            setWarningMessage(null);
            setLoginSuccess(false);
        }, [])
    );
    useEffect(() => {
        async function checkToken(){
            setIsLoading(true);
            log.info("Checking if user already logged in");
            const tokenPresent = await CheckToken();
            if(tokenPresent){
                log.success("User is already logged in");
                const token = await GetToken();
                const userId = await GetIdFromToken(token);
                dispatch(SetUser(userId))
                log.info("Navigating to Main");
                navigation.navigate("Main");
            }
            else{
                log.warn("User is not logged in, please log in");
            }
            
            setIsLoading(false);
        }
        
        checkToken();
    }, []);
    const doLogin = async (data) => {
        setIsLoading(true);
        setUsernameError(false);
        setPasswordError(false);
        try{
            log.info("Requesting Token")
            const tokenData = await RecieveToken(data);
            const token = tokenData.data;
            log.success(`Recieved Token`);
            await SetToken(token);
            const userId = await GetIdFromToken(token);
            dispatch(SetUser(userId))
            setLoginSuccess(true);
            setWarningMessage('');
            log.info("Navigating to Main");
            navigation.navigate("Main");
        }
        catch(error){
            setIsLoading(false);
            if(error.code == "ECONNABORTED"){
                log.error("SERVER UNREACHABLE, PLEASE TRY AGAIN AFTER SOME TIME");
                setWarningMessage("Server is unreachable. Please try again later.");
            }
            else if(error.code == "ERR_BAD_REQUEST"){
                const errorCode = error.response.status.toString();
                if(errorCode == "404"){
                    log.warn("USER NOT FOUND");
                    setWarningMessage("Please Provide Valid Username");
                    setUsernameError(true);
                    setUsername('');
                    setPassword('');
                }
                else if(errorCode == "401"){
                    log.warn("INCORRECT PASSWORD");
                    setWarningMessage("Incorrect Password");
                    setPasswordError(true);
                    setPassword('');
                }
                else{
                    log.warn("UNEXPECTED ERROR");
                    setWarningMessage("Unexpected Error");
                }
            }
        }
        finally {
            setIsLoading(false);
        }
    } 

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.innerContainer}>
            {warningMessage && <Text style={styles.warningText}>{warningMessage}</Text>}

                <TextInput
                    style={usernameError? styles.errorInput : loginSuccess? styles.successInput : styles.input}
                    placeholder="Username"
                    value={username}
                    onChangeText={text => setUsername(text)}
                />
                <TextInput
                    style={passwordError? styles.errorInput : loginSuccess? styles.successInput : styles.input}
                    placeholder="Password"
                    value={password}
                    onChangeText={text => setPassword(text)}
                    secureTextEntry
                />
                <TouchableOpacity style={styles.button} onPress={() => doLogin({ username, password })}>
                    <Text style={styles.buttonText}>Login</Text>
                </TouchableOpacity>
                <TouchableOpacity onPress={() => {log.info("Navigating to Register Screen");navigation.navigate("Register")}}>
                    <Text style={styles.signUpText}>Sign Up</Text>
                </TouchableOpacity>
            </View>
            <StatusBar style="auto" />
            <LoadingOverlay isVisible={isLoading} />
        </SafeAreaView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'white',
    },
    innerContainer: {
        width: '80%',
    },
    input: {
        height: 40,
        borderColor: 'gray',
        borderWidth: 1,
        marginBottom: 10,
        paddingLeft: 10,
    },
    errorInput: {
        height: 40,
        borderColor: 'red',
        borderWidth: 2,
        marginBottom: 10,
        paddingLeft: 10,
    },
    successInput: {
        height: 40,
        borderColor: 'green',
        borderWidth: 2,
        marginBottom: 10,
        paddingLeft: 10,
    },
    button: {
        backgroundColor: 'blue',
        borderRadius: 5,
        padding: 10,
        alignItems: 'center',
    },
    buttonText: {
        color: 'white',
        fontWeight: 'bold',
    },
    signUpText: {
        marginTop: 10,
        color: 'blue',
        textAlign: 'center',
    },
    warningText: {
        color: 'red',
        marginTop: 10,
        textAlign: 'center',
    },
});

export default LoginScreen