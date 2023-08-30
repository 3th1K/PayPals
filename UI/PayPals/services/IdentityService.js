import axiosInstance from '../helpers/Interceptor';
import { CreateLogger } from '../Logger'
const log = CreateLogger("IdentityService");


export const HealthCheck = async () => {
    await axiosInstance.get(API_URL+'/healthcheck', {
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
    const response = await axiosInstance.post('/login', requestData);
    log.success("Response Recieved From Server");
    return response;
};
