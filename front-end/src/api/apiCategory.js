import callAPI from './apiService';

export const GET_CATEGORY = () => callAPI.get("category");

export const GET_CATEGORY_BY_ID = (id) => callAPI.get("category/" + id);

export const DELETE_CATEGORY = (id) => callAPI.delete("category/" + id);

export const POST_CATEGORY = (body) => callAPI.post("category", body);

export const PUT_CATEGORY = (id, body) => callAPI.put("category/" + id, body);