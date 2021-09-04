import callAPI from './apiService';

export const GET_PRODUCT = () => callAPI.get("product");

export const GET_PRODUCT_BY_ID = (id) => callAPI.get("product/" + id);

export const DELETE_PRODUCT = (id) => callAPI.delete("product/" + id);

export const POST_PRODUCT = (body) => callAPI.post("product", body);

export const PUT_PRODUCT = (id, body) => callAPI.put("product/" + id, body);

export const SAVE_PHOTO = (body) => callAPI.post("product/savefile", body);