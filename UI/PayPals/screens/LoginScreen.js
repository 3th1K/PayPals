import { View, Text, SafeAreaView, TouchableOpacity } from 'react-native'
import { useState, useEffect } from 'react'
import React from 'react'
import { useNavigation } from '@react-navigation/native'
import { getToken } from '../services/IdentityService'

const LoginScreen = () => {
    const navigation = useNavigation();
    // const [requestData, setRequestData] = useState("lol");
    // useEffect(() => {
    //     console.log(requestData);
    //     const fetchToken = async () => {
    //         try {
    //             const token = await getToken(requestData);
    //             console.log(token);
    //         } catch (error) {
    //             console.log(error);
    //         } 
    //     };
    //     fetchToken();
    //   }, [requestData]);
    const doLogin = async (data) => {
        try {
            const token = await getToken(data);
            console.log(token);
        } catch (error) {
            console.log(error);
        } 

    }

    return (
        <SafeAreaView>
            <View>
                <TouchableOpacity onPress={() => doLogin({username: "mehedi1@gmail.com", password: "pass"})}><Text>login</Text></TouchableOpacity>
                <TouchableOpacity onPress={() => navigation.navigate("Register")}><Text>Sign Up</Text></TouchableOpacity>
            </View>
        </SafeAreaView>
    )
}

export default LoginScreen