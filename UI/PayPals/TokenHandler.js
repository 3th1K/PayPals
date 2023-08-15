import AsyncStorage from '@react-native-async-storage/async-storage';
import jwtDecode from 'jwt-decode';
import { CreateLogger } from './Logger';

const log = CreateLogger("TokenHandler");

export const SetUser = async () =>{
    log.info("setting user");
    try{
        const token = await GetToken();
        const decodedToken = jwtDecode(token);
        log.info("User has been set - UserId : "+decodedToken.userId);
        return decodedToken.userId;
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
  
