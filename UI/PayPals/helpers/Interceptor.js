import axios from 'axios';
import { CreateLogger } from '../Logger';
import { CheckToken, GetToken } from '../TokenHandler';
const log = CreateLogger("Interceptor");

const axiosInstance = axios.create({
  baseURL: 'http://192.168.0.108:5000', // Your API base URL
});

axiosInstance.interceptors.request.use(async (config) => {
  if(! await CheckToken()){
    log.error("Token missing, please login again");
  }
  const token = await GetToken();
  if (token) {
    log.info("Token is present, adding it to header");
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
});

export default axiosInstance;
