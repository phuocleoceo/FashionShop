import axios from 'axios';
import { API_URL } from '../extension/AppURL';

const callAPI = axios.create({
	baseURL: API_URL,
	headers: {
		'content-type': 'application/json'
	}
});

// callAPI.interceptors.request.use((config) => {
// 	// Add AccessToken to Headers

// }, (error) => {
// 	Promise.reject(error);
// });

// callAPI.interceptors.response.use((response) => {
// 	return response;
// }, (error) => {
// 	Promise.reject(error);
// }
// );

export default callAPI;