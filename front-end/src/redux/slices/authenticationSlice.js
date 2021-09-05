import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { LOGIN, REGISTER } from '../../api/apiAuthentication';
import { SET_USER, GET_USER, REMOVE_USER } from '../../extension/LocalStorageService';

export const RegisterAction = createAsyncThunk(
	"authentication/RegisterAction",
	async (body) => {
		try {
			const response = await REGISTER(body);
			return response.status === 201;
		}
		catch { return false; }
	}
);

export const LoginAction = createAsyncThunk(
	"authentication/LoginAction",
	async (body) => {
		try {
			const response = await LOGIN(body);
			return {
				Accepted: response.status === 200,
				ResponseData: response.data
			};
		}
		catch { return { Accepted: false }; }
	}
);

export const authenticationSlice = createSlice({
	name: 'authentication',
	initialState: {},
	reducers: {
		CheckLoggedIn: (state, action) => {
			const user = GET_USER();
			if (user) return user.User;
			return {};
		},
		Logout: (state, action) => {
			REMOVE_USER();
			return {};
		}
	},
	extraReducers: {
		[LoginAction.fulfilled]: (state, action) => {
			if (action.payload.Accepted) {
				SET_USER(action.payload.ResponseData);
				return action.payload.ResponseData.User;
			}
			return {};
		}
	}
})

export const { CheckLoggedIn, Logout } = authenticationSlice.actions

export default authenticationSlice.reducer