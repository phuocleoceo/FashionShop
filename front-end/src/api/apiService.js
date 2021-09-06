import axios from 'axios';
import { API_URL } from '../extension/AppURL';
import { GET_ACCESS_TOKEN, GET_REFRESH_TOKEN, SET_USER } from '../extension/LocalStorageService';

const callAPI = axios.create({
	baseURL: API_URL,
	headers: {
		'content-type': 'application/json'
	}
});

callAPI.interceptors.request.use((config) => {
	// Add AccessToken to Headers
	config.headers["Authorization"] = "Bearer " + GET_ACCESS_TOKEN();
	return config;
}, (error) => {
	return Promise.reject(error);
});

callAPI.interceptors.response.use((response) => {
	console.log("response accepted");
	return response;
}, async (error) => {
	console.log("response rejected");
	const originalRequest = error.config;
	if (error.response.status === 401 && !originalRequest._retry) {
		originalRequest._retry = true;
		// Get Current TokenAPI
		const tokenAPI = {
			accessToken: GET_ACCESS_TOKEN(),
			refreshToken: GET_REFRESH_TOKEN()
		};
		// RefreshToken
		const check = await callAPI.post("authentication/refresh", tokenAPI);
		if (check.status === 200) {
			SET_USER(check.data);
			// Recall API
			callAPI.defaults.headers.common["Authorization"] = "Bearer " + check.data.AccessToken;
			return callAPI(originalRequest);
		}
		//else await callAPI.post("authentication/revoke");

		return Promise.reject(error);
	}
})

export default callAPI;