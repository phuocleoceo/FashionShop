import React, { useEffect, useState } from 'react';
import { PHOTO_PATH_URL } from '../extension/AppURL';
import { useParams, useHistory } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { Get_Cart, Add_To_Cart } from '../redux/slices/cartSlice';
import { GET_PRODUCT_BY_ID } from '../api/apiProduct';
import { toast } from 'react-toastify';
import AddShoppingCartIcon from '@material-ui/icons/AddShoppingCart';
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import {
	Grid, TextField,
	Typography, Container, Button, Paper
} from '@material-ui/core';

export default function ProductDetail() {
	const { productId } = useParams();
	const [prd, setPrd] = useState({});
	const userId = useSelector(state => state.authentication.Id);
	const [quantity, setQuantity] = useState(1);
	const dispatch = useDispatch();
	const history = useHistory();

	useEffect(() => {
		const getPrd = async () => {
			const response = await GET_PRODUCT_BY_ID(productId);
			if (response.status === 200) {
				setPrd(response.data);
			}
		};
		getPrd();
	}, [productId]);

	const handleAddToCart = async (productId) => {
		if (userId) {
			const newCartInfor = {
				userId, productId, count: quantity
			};
			const check = await dispatch(Add_To_Cart(newCartInfor));
			if (check.payload) {
				toast.success("Added to cart");
				dispatch(Get_Cart(userId));
			}
		}
		else history.push("/login");
	}

	const handleChange = (e) => {
		setQuantity(e.target.value);
	}

	return (
		<Container style={{ marginTop: "12vh" }}>
			<Paper elevation={3}>
				<Grid container spacing={5}>
					<Grid item xs={12} sm={6}>
						<Typography variant="h3">{prd.Name}</Typography>
						<Typography variant="h5">{prd.Category}</Typography>
						<Typography>{prd.Price}</Typography>
						<Typography>{prd.Description}</Typography>
						<TextField
							required
							autoFocus
							onChange={handleChange}
							value={quantity}
							label="Quantity"
							type="text"
						/>
						<p></p>
						<Button variant="contained" color="primary"
							startIcon={<AddShoppingCartIcon />}
							onClick={() => handleAddToCart(prd.Id)}>
							Add To Cart
						</Button>
						<Button variant="contained" color="secondary"
							startIcon={<ArrowBackIcon />}
							style={{ marginLeft: "1vw" }}
							onClick={() => history.push("/")} >
							Back To Menu
						</Button>
					</Grid>

					<Grid item xs={12} sm={6}>
						<img src={PHOTO_PATH_URL + prd.ImagePath} alt="prdImg"
							width="300vw" height="auto" />
					</Grid>
				</Grid>
			</Paper>
		</Container >
	)
}
