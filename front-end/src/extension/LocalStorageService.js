export const SET_USER = (data) => {
	localStorage.setItem("fs_user", JSON.stringify(data));
}

export const GET_USER = () => {
	return JSON.parse(localStorage.getItem("fs_user"));
}

export const REMOVE_USER = () => {
	localStorage.removeItem("fs_user");
}

export const GET_ACCESS_TOKEN = () => {
	const user = GET_USER();
	return user ? user.AccessToken : "";
}

export const GET_REFRESH_TOKEN = () => {
	const user = GET_USER();
	return user ? user.RefreshToken : "";
}

export const GET_USER_INFOR = () => {
	const user = GET_USER();
	return user ? user.User : null;
}