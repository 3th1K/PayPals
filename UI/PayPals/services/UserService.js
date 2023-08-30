import axiosInstance from '../helpers/Interceptor';
import axios from 'axios';
import { CreateLogger } from '../Logger'

const log = CreateLogger("UserService");


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

export const GetUser = async (id) => {
    log.info("Retrieving User");
    const response = await axiosInstance.get(`/users/${id}`);
    
    log.success("User Retrieved Successsfully From Server");
    log.debug(response.data);
    return response.data;
};

export const CreateUser = async (data) => {
    log.info("Creating User");
    const response = await axiosInstance.post(`/users/create`, data)
    log.success("User Created Successsfully");
    log.debug(response.data);
    return response.data;
}

export const GetUserGroups = async (id) => {
    log.info("Retrieving user groups");
    const response = await axiosInstance.get(`/users/${id}/groups`);
    
    log.success("User Groups Retrieved Successsfully From Server");
    log.debug(response.data);
    return response.data;
}
