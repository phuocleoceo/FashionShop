import React, { useEffect, useState } from 'react';
import { PHOTO_PATH_URL } from '../extension/AppURL';
import { useHistory } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { Get_Cart, Add_To_Cart } from '../redux/slices/cartSlice';
import { GET_PRODUCT } from '../api/apiProduct';
import { toast } from 'react-toastify';
import AddShoppingCartIcon from '@material-ui/icons/AddShoppingCart';
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
	const userId = useSelector(state => state.authentication.Id);
	const dispatch = useDispatch();
	const history = useHistory();

	useEffect(() => {
		const getPrd = async () => {
			const response = await GET_PRODUCT();
			if (response.status === 200) {
				setListProduct(response.data);
			}
		};
		getPrd();
	}, []);

	const handleAddToCart = async (productId) => {
		if (userId) {
			const newCartInfor = {
				userId, productId, count: 1
			};
			const check = await dispatch(Add_To_Cart(newCartInfor));
			if (check.payload) {
				toast.success("Added to cart");
				dispatch(Get_Cart(userId));
			}
		}
		else history.push("/login");
	}

	const RedirectToDetail = (productId) => history.push("/" + productId);

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
										onClick={() => RedirectToDetail(prd.Id)}
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
									<Button variant="contained" color="primary"
										startIcon={<AddShoppingCartIcon />}
										onClick={() => handleAddToCart(prd.Id)}>
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
