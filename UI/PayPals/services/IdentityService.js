import axios from "axios";

const API_BASE_URL = 'https://localhost:7230';

export const getToken = async (requestData) => {
  try {
    //const token = await axios.post(`${API_BASE_URL}/login`, requestData);
    console.log(requestData)
    var response = await axios(
        {
            url: 'http://192.168.0.104:5082/healthcheck',
            method: 'get',
            validateStatus: false
        });
    console.log("all ok "+response);
  } catch (error) {
    console.error('Error fetching token:', error);
    throw error;
  }

// fetch('http://192.168.0.104:5082/healthcheck')
// .then(response => {
//   if (response.ok) {
//     console.log('API is accessible');
//   } else {
//     console.log('API is not accessible');
//   }
// })
// .catch(error => {
//   console.log(error);
// });
};