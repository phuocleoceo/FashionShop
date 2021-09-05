import { configureStore } from '@reduxjs/toolkit';
import authenticationSlice from '../redux/slices/authenticationSlice';

export const store = configureStore({
	reducer: {
		authentication: authenticationSlice
	},
});