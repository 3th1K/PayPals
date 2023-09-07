import axios from 'axios';
import { CreateLogger } from '../Logger';
import { CheckToken, GetToken } from '../TokenHandler';
const log = CreateLogger("Interceptor");

const axiosInstance = axios.create({
  baseURL: 'http://192.168.0.106:5000',
  timeout: 10000
});

axiosInstance.interceptors.request.use(async (config) => {
  if(! await CheckToken()){
    log.error("Token missing, please login again");
  }
  else{
    const token = await GetToken();
    log.info("token -> "+token);
    if (token) {
      log.info("Token is present, adding it to header");
      config.headers['Authorization'] = `Bearer ${token}`;
      log.success("Token added to header");
    }
  }
  
  return config;
});

export default axiosInstance;
