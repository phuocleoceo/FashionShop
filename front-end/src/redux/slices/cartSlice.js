import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { GET_CART, ADD_TO_CART, PLUS_CART, MINUS_CART, REMOVE_CART } from '../../api/apiCart';

export const Get_Cart = createAsyncThunk(
	"cart/Get_Cart",
	async (userId) => {
		const response = await GET_CART(userId);
		return response.data;
	}
);

export const Add_To_Cart = createAsyncThunk(
	"cart/Add_To_Cart",
	async (body) => {
		const response = await ADD_TO_CART(body);
		return response.status === 201;
	}
);

export const Plus_Cart = createAsyncThunk(
	"cart/Plus_Cart",
	async (cartId) => {
		const response = await PLUS_CART(cartId);
		return response.status === 204;
	}
);

export const Minus_Cart = createAsyncThunk(
	"cart/Minus_Cart",
	async (cartId) => {
		const response = await MINUS_CART(cartId);
		return response.status === 204;
	}
);

export const Remove_Cart = createAsyncThunk(
	"cart/Remove_Cart",
	async (cartId) => {
		const response = await REMOVE_CART(cartId);
		return response.status === 204;
	}
);

export const cartSlice = createSlice({
	name: 'cart',
	initialState: [],
	reducers: {},
	extraReducers: {
		[Get_Cart.fulfilled]: (state, action) => {
			return action.payload;
		}
	}
})

//export const {  } = cartSlice.actions

export default cartSlice.reducer