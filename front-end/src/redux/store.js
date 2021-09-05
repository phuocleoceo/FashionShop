import { configureStore } from '@reduxjs/toolkit';
import authenticationSlice from '../redux/slices/authenticationSlice';
import cartSlice from '../redux/slices/cartSlice';

export const store = configureStore({
	reducer: {
		authentication: authenticationSlice,
		cart: cartSlice
	},
});