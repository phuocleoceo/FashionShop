import React, { useEffect, useState } from 'react';
import { PHOTO_PATH_URL } from '../extension/AppURL';
import { GET_PRODUCT } from '../api/apiProduct';
import {
	Grid, Card, CardActionArea,
	CardActions, CardContent, CardMedia,
	Typography, Container, Button
} from '@material-ui/core';

const ImgCardStyle = {
	width: "100%",
	height: "200px",
	objectFit: "cover"
};

export default function Home() {
	const [listProduct, setListProduct] = useState([]);

	useEffect(() => {
		const getPrd = async () => {
			const response = await GET_PRODUCT();
			if (response.status === 200) {
				setListProduct(response.data);
			}
		};
		getPrd();
	}, []);

	return (
		<Container style={{ marginTop: "12vh" }}>
			<Grid container spacing={5}>
				{
					listProduct.map(prd => (
						<Grid item xs={6} sm={4} md={3} key={prd.Id}>
							<Card style={{ maxWidth: 340 }}>
								<CardActionArea>
									<CardMedia
										component="img"
										alt="ProductImage"
										style={ImgCardStyle}
										image={PHOTO_PATH_URL + prd.ImagePath}
										title="ProductImage"
									/>
								</CardActionArea>
								<CardContent>
									<Typography gutterBottom variant="h5" component="h2">
										{prd.Name}
									</Typography>
									<Typography variant="body2" color="textSecondary" component="p">
										{prd.Price} VNĐ
									</Typography>
								</CardContent>
								<CardActions>
									<Button variant="contained" color="primary">
										Add To Cart
									</Button>
								</CardActions>
							</Card>
						</Grid>
					))
				}
			</Grid >
		</Container>
	)
}
