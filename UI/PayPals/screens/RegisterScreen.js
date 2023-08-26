import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet } from 'react-native';
import { CreateLogger } from '../Logger';
import Icon from 'react-native-vector-icons/FontAwesome';
import { CreateUser } from '../services/UserService';

import { useNavigation } from '@react-navigation/native';

const log = CreateLogger("RegisterScreen");
const RegisterScreen = () => {
  const [username, setUsername] = useState('');
  const [userExists, setUserExists] = useState(false);
  const [isUsernameValid, setIsUsernameValid] = useState(false);
  const [email, setEmail] = useState('');
  const [isEmailValid, setIsEmailValid] = useState(false);
  const [password, setPassword] = useState('');
  const [isPasswordValid, setIsPasswordValid] = useState(false);
  const [retypePassword, setRetypePassword] = useState('');
  const [isRetypePasswordValid, setIsRetypePasswordValid] = useState(false);
  const navigator = useNavigation();

  useEffect(() => {
    setIsRetypePasswordValid(isPasswordValid && retypePassword === password);
  }, [retypePassword, isPasswordValid, password]);

  const handleUsernameChange = (text) => {
    setUsername(text);
    setIsUsernameValid(text.length >=5 );
  }
  const handleEmailChange = (text) => {
    setEmail(text);
    const validateEmail = (email) => {
      // Regular expression for basic email validation
      const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
      return emailPattern.test(email);
    };
    setIsEmailValid(validateEmail(email));
  }
  const handlePasswordChange = (text) => {
    setPassword(text);
    setIsPasswordValid(text.length >=8 );
  }
  const handleRetypePasswordChange = (text) => {
    setRetypePassword(text);
  }
  const handleRegister = async () => {
    if(!isUsernameValid){
      log.warn("Provided username is invalid, please provide username of length 5 to 50");
    }
    if(!isEmailValid){
      log.warn("Please provide a valid email")
    }
    if(!isPasswordValid){
      log.warn("Please provide a valid password");
    }
    if(!retypePassword){
      log.warn("Provided passwords dosent matches");
    }
    if(isEmailValid && isPasswordValid && isUsernameValid && isRetypePasswordValid){
      try{
        const data = {
          username: username,
          email: email,
          password: password
        }
        var user =  await CreateUser(data);
        setUserExists(false);
        log.success("Register Successfull");
        log.info("Navigating to Login");
        navigator.navigate("Login");
      }catch(error){
        const errorCode = error.response.status;
        const errorMessage = error.response.data;
        const userAlreadyExistsError = "user already exists";
        if(errorCode == '400' && errorMessage.includes(userAlreadyExistsError)){
          log.error("User already exists, please try different username, or log in");
          setUserExists(true);
        }
      }
      
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Register</Text>

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          placeholder="Username"
          onChangeText={handleUsernameChange}
          value={username}
        />
        {isUsernameValid && (
          <Icon style={styles.iconContainer} name="check-circle" size={20} color={userExists? "red":"green" }/>
        )}
      </View>

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          placeholder="Email"
          onChangeText={handleEmailChange}
          value={email}
          keyboardType="email-address"
        />
        {isEmailValid && (
          <Icon style={styles.iconContainer} name="check-circle" size={20} color="green" />
        )}
      </View>

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          placeholder="Password"
          onChangeText={handlePasswordChange}
          value={password}
          secureTextEntry
        />
        {isPasswordValid && (
          <Icon style={styles.iconContainer} name="check-circle" size={20} color="green" />
        )}
      </View>

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          placeholder="Retype Password"
          onChangeText={handleRetypePasswordChange}
          value={retypePassword}
          secureTextEntry
        />
        {isRetypePasswordValid && (
          <Icon style={styles.iconContainer} name="check-circle" size={20} color="green" />
        )}
      </View>
      <TouchableOpacity style={styles.button} onPress={handleRegister}>
        <Text style={styles.buttonText}>Register</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
  },
  inputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
  },
  iconContainer:{
    position: 'absolute',
    right: 10, 
    alignItems: 'center',
    paddingBottom: 6
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
  },
  input: {
    width: '60%',
    padding: 10,
    marginBottom: 10,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5
  },
  button: {
    backgroundColor: 'blue',
    padding: 10,
    borderRadius: 5,
    marginTop: 10,
  },
  buttonText: {
    color: 'white',
    fontSize: 16,
    textAlign: 'center',
  },
});

export default RegisterScreen;
