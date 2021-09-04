import React from 'react';
import NotFoundIMG from '../assets/img/NotFound.png';

export default function NotFound() {
	return (
		<div style={{ textAlign: "center" }}>
			<img width="450vw" height="auto" src={NotFoundIMG} alt="notfound" />
			<h2>This page could not be found !</h2>
		</div >
	)
}
