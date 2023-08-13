import AsyncStorage from '@react-native-async-storage/async-storage';
import jwtDecode from 'jwt-decode';
import { CreateLogger } from './Logger';

const log = CreateLogger("TokenHandler");
// Assuming 'token' is the stored JWT token
export const SetUser = async (token) =>{
    log.info("setting user");
    try{
        const decodedToken = jwtDecode(token);
        log.info(decodedToken);
    }
    catch(error){
        log.error(error);
    }
}
export const SetToken = async (token) =>{
    log.info("Setting Token");
    try{
        await AsyncStorage.setItem('token', token);
        log.info("Token Set Successfull");
    }
    catch(error){
        log.error("Token was not set : ",error);
    }
}

export const GetToken = async () =>{
    return await AsyncStorage.getItem('token');
}

export const RemoveToken = async () => {
    await AsyncStorage.removeItem('token');
};

export const CheckToken = async () => {
    const isToken = await AsyncStorage.getItem('token');
    return isToken !== null;
}
  
