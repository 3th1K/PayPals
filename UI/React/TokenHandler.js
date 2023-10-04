import AsyncStorage from '@react-native-async-storage/async-storage';
import jwtDecode from 'jwt-decode';
import { CreateLogger } from './Logger';

const log = CreateLogger("TokenHandler");

export const SetToken = async (token) =>{
    //const { setUserId } = useUser();
    log.info("Setting Token");
    try{
        await AsyncStorage.setItem('token', token);
        log.success("Token Set Successfull");
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
    log.success("Removed Token");
};

export const CheckToken = async () => {
    const token = await AsyncStorage.getItem('token');
    if(token){
        try {
            const decodedToken = jwtDecode(token);
            const currentTime = Date.now() / 1000; // Convert current time to seconds
            if (decodedToken.exp && decodedToken.exp < currentTime) {
              log.error("Token has expired");
              return false
            }
            else{
                return true;
            }
        } 
        catch (error) {
            log.error("Failed to decode token", error);
            return false;
        }
    }
    else{
        return false;
    }
    
}
  
