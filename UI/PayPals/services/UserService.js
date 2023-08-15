import axiosInstance from '../helpers/Interceptor';
import { CreateLogger } from '../Logger'
import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { GetToken, SetUser } from '../TokenHandler';
const log = CreateLogger("UserService");

const API_URL = 'http://192.168.0.108:5000';


export const GetUsers = async () => {
    log.info("Retrieving All Users");
    try{
        const response = await axiosInstance.get('/users');
        log.success("All User Retrieved Successsfully From Server");
        log.debug(response.data);
        return response;
    }
    catch(error){
        log.error(error);
    }
};

export const GetUsersDetails = async () => {
    log.info("Retrieving All Users Details");
    try{
        const response = await axiosInstance.get('/users/details');
        log.success("All User Details Retrieved Successsfully From Server");
        log.debug(response.data);
        return response;
    }
    catch(error){
        log.error(error);
    }
};

export const GetUser = async () => {
    log.info("Retrieving User");
    try{
        const id = await SetUser();
        const response = await axiosInstance.get(`/users/${id}`);
        log.success("User Retrieved Successsfully From Server");
        log.debug(response.data);
        return response.data;
    }
    catch(error){
        log.error(error.response);
    }
};
