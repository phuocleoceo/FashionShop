import callAPI from './apiService';

export const GET_CART = (userId) => callAPI.get("cart/" + userId);

export const ADD_TO_CART = (body) => callAPI.post("cart", body);

export const PLUS_CART = (cartId) => callAPI.put("cart/plus/" + cartId);

export const MINUS_CART = (cartId) => callAPI.put("cart/minus/" + cartId);

export const REMOVE_CART = (cartId) => callAPI.delete("cart/remove/" + cartId);