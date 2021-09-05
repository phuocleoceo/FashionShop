import React from 'react';
import NotFoundIMG from '../assets/img/NotFound.png';

export default function NotFound() {
	return (
		<div style={{ textAlign: "center", marginTop: "15vh" }}>
			<img width="50%" height="auto" src={NotFoundIMG} alt="notfound" />
			<h2>This page could not be found !</h2>
		</div >
	)
}
