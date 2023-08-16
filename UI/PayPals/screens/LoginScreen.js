import { View, Text, SafeAreaView, TouchableOpacity, StatusBar, TextInput, StyleSheet } from 'react-native'
import { useState, useEffect } from 'react'
import React from 'react'
import { useNavigation } from '@react-navigation/native'
import { RecieveToken, HealthCheck } from '../services/IdentityService'
import { CreateLogger } from '../Logger'
import { SetToken, CheckToken, GetToken } from '../TokenHandler'
const log = CreateLogger("LoginScreen");

const LoginScreen = () => {
    const navigation = useNavigation();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [token] = useState(null);
    useEffect(() => {
        async function checkToken(){
            log.info("Checking if user already logged in");
            const tokenPresent = await CheckToken();
            if(tokenPresent){
                log.success("User is already logged in");
                //await setToken(await GetToken());
                log.info("Navigating to Main");
                navigation.navigate("Main");
            }
        }
        checkToken();
    }, []);
    const doLogin = async (data) => {
        try{
            log.info("Requesting Token")
            const token = await RecieveToken(data);
            log.success(`Recieved Token`);
            await SetToken(token.data);
            log.info("Navigating to Main");
            navigation.navigate("Main");
        }
        catch(error){
            if(error.code == "ECONNABORTED"){
                log.error("SERVER UNREACHABLE, PLEASE TRY AGAIN AFTER SOME TIME");
            }
            else if(error.code == "ERR_BAD_REQUEST"){
                const errorCode = error.response.status.toString();
                if(errorCode == "404"){
                    log.warn("USER NOT FOUND");
                }
                else if(errorCode == "401"){
                    log.warn("INCORRECT PASSWORD");
                }
                else{
                    log.warn("UNEXPECTED ERROR");
                }
            }
        }
    } 

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.innerContainer}>
                <TextInput
                    style={styles.input}
                    placeholder="Username"
                    value={username}
                    onChangeText={text => setUsername(text)}
                />
                <TextInput
                    style={styles.input}
                    placeholder="Password"
                    value={password}
                    onChangeText={text => setPassword(text)}
                    secureTextEntry
                />
                <TouchableOpacity style={styles.button} onPress={() => doLogin({ username, password })}>
                    <Text style={styles.buttonText}>Login</Text>
                </TouchableOpacity>
                <TouchableOpacity onPress={() => navigation.navigate("Register")}>
                    <Text style={styles.signUpText}>Sign Up</Text>
                </TouchableOpacity>
            </View>
            <StatusBar style="auto" />
        </SafeAreaView>
    )
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
});

export default LoginScreen