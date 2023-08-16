import axios from "axios";
import { CreateLogger } from '../Logger'
const log = CreateLogger("IdentityService");

const API_URL = 'http://192.168.0.108:5000';


export const HealthCheck = async () => {
    await axios.get(API_URL+'/healthcheck', {
        timeout: 10000
    })
    .then(response => {
        log.success("Server Is Alive : "+response.status);
    })
    .catch(error => {
        if (error.code === 'ECONNABORTED') {
            log.warning('Request timed out');
        } else {
            console.log(error.message);
        }
    });
};


export const RecieveToken = async (requestData) => {
    log.info("Credentials : ",requestData);
    const response = await axios.post(API_URL+'/login', requestData, {timeout: 10000});
    log.success("Response Recieved From Server");
    return response;
};
