import React, { useState, useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Get_Cart, Plus_Cart, Minus_Cart, Remove_Cart } from '../redux/slices/cartSlice';
import {
	Container, Table, TableContainer, Paper,
	TableHead, TableRow, TableCell, TableBody,
	styled, IconButton, Typography, TablePagination
} from '@material-ui/core';
import { PHOTO_PATH_URL } from '../extension/AppURL';
import { numberWithCommas } from '../extension/DataHandle';
import DeleteIcon from '@material-ui/icons/Delete';
import IndeterminateCheckBoxIcon from '@material-ui/icons/IndeterminateCheckBox';
import AddBoxIcon from '@material-ui/icons/AddBox';

const Image = styled("img")({
	width: "10vh",
	height: "auto"
});
const TableHeadBlack = styled(TableHead)({
	backgroundColor: "black"
});
const TableCellWhite = styled(TableCell)({
	color: "white"
});
const TableCellBold = styled(TableCell)({
	fontWeight: "bold",
	fontSize: "17px"
});

export default function Cart() {
	const [total, setTotal] = useState("");
	const [page, setPage] = useState(0);
	const [rowsPerPage, setRowsPerPage] = useState(5);
	const userId = useSelector(state => state.authentication.Id);
	const cart = useSelector(state => state.cart);
	const dispatch = useDispatch();

	useEffect(() => {
		let sum = 0;
		cart.forEach(c => {
			sum += c.Total;
		});
		setTotal(numberWithCommas(sum));
	}, [cart]);

	const handleMinus = async (cartId) => {
		const check = await dispatch(Minus_Cart(cartId));
		if (check.payload) {
			dispatch(Get_Cart(userId));
		}
	};
	const handlePlus = async (cartId) => {
		const check = await dispatch(Plus_Cart(cartId));
		if (check.payload) {
			dispatch(Get_Cart(userId));
		}
	};
	const handleRemove = async (cartId) => {
		const check = await dispatch(Remove_Cart(cartId));
		if (check.payload) {
			dispatch(Get_Cart(userId));
		}
	};

	const handleChangePage = (e, newPage) => {
		setPage(newPage);
	};

	const handleChangeRowsPerPage = (e) => {
		setRowsPerPage(parseInt(e.target.value, 10)); //decimal
		setPage(0);
	};

	return (
		<Container style={{ marginTop: "12vh" }}>
			{
				cart.length > 0 ?
					<Paper>
						<TableContainer>
							<Table>
								<TableHeadBlack>
									<TableRow>
										<TableCellWhite>Product</TableCellWhite>
										<TableCellWhite>Price</TableCellWhite>
										<TableCellWhite>Image</TableCellWhite>
										<TableCellWhite>Quantity</TableCellWhite>
										<TableCellWhite>Total Price</TableCellWhite>
										<TableCellWhite>Action</TableCellWhite>
									</TableRow>
								</TableHeadBlack>

								<TableBody>
									{
										cart.slice(page * rowsPerPage, (page + 1) * rowsPerPage)
											.map(c => (
												<TableRow key={c.Id}>
													<TableCell>{c.Product}</TableCell>
													<TableCell>{c.Price} VNĐ</TableCell>
													<TableCell>
														<Image src={PHOTO_PATH_URL + c.ImagePath} alt="productIMG" />
													</TableCell>
													<TableCell>
														<IconButton onClick={() => handleMinus(c.Id)}>
															<IndeterminateCheckBoxIcon color="secondary" />
														</IconButton>
														{c.Count}
														<IconButton onClick={() => handlePlus(c.Id)}>
															<AddBoxIcon color="primary" />
														</IconButton>
													</TableCell>
													<TableCell>{c.Total} VNĐ</TableCell>
													<TableCell>
														<IconButton onClick={() => handleRemove(c.Id)}>
															<DeleteIcon color="secondary" />
														</IconButton>
													</TableCell>
												</TableRow>
											))
									}
									<TableRow>
										<TableCellBold>TOTAL</TableCellBold>
										<TableCell></TableCell>
										<TableCell></TableCell>
										<TableCell></TableCell>
										<TableCellBold>{total} VNĐ</TableCellBold>
									</TableRow>
								</TableBody>
							</Table>
						</TableContainer>

						<TablePagination
							rowsPerPageOptions={[5, 10, 25]}
							component="div"
							count={cart.length}
							rowsPerPage={rowsPerPage}
							page={page}
							onPageChange={handleChangePage}
							onRowsPerPageChange={handleChangeRowsPerPage}
						/>
					</Paper>
					:
					<Typography style={{ fontSize: "20px" }}>
						Shopping Cart is Empty Now !
					</Typography>
			}
		</Container>
	)
}