import AsyncStorage from '@react-native-async-storage/async-storage';
import jwtDecode from 'jwt-decode';
import { CreateLogger } from './Logger';

const log = CreateLogger("TokenHandler");

export const SetToken = async (token) =>{
    //const { setUserId } = useUser();
    log.info("Setting Token");
    try{
        await AsyncStorage.setItem('token', token);
        log.info("Token Set Successfull");
        // var token = await GetToken();
        // const decodedToken = jwtDecode(token);
        // if (decodedToken && decodedToken.userId) {
        //     setUserId(decodedToken.userId);
        // }
    }
    catch(error){
        log.error("Token was not set : ",error);
    }
}

export const GetIdFromToken = async (token) => {
    log.info("Extracting UserId From Token ...");
    const decodedToken = jwtDecode(token);
    if (decodedToken && decodedToken.userId) {
        log.success("Extracted UserId From Token");
        return (decodedToken.userId);
    }
    log.error("Cannot Extract UserId From Token");
    return null;
}

export const GetToken = async () =>{
    return await AsyncStorage.getItem('token');
}

export const RemoveToken = async () => {
    log.info("Removing Token");
    await AsyncStorage.removeItem('token');
    log.info("Removed Token");
};

export const CheckToken = async () => {
    const isToken = await AsyncStorage.getItem('token');
    return isToken !== null;
}
  
