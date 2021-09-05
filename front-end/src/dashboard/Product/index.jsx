import React, { useState, useEffect } from 'react';
import { GET_PRODUCT, DELETE_PRODUCT } from '../../api/apiProduct';
import {
	Container, Table, TableContainer, Paper,
	TableHead, TableRow, TableCell, TableBody,
	styled, IconButton, TablePagination, Grid
} from '@material-ui/core';
import { PHOTO_PATH_URL } from '../../extension/AppURL';
import { toast } from 'react-toastify';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';

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

export default function Product() {
	const [listProduct, setListProduct] = useState([]);
	const [page, setPage] = useState(0);
	const [rowsPerPage, setRowsPerPage] = useState(5);
	useEffect(() => getPrd(), []);

	const getPrd = async () => {
		const response = await GET_PRODUCT();
		if (response.status === 200) {
			setListProduct(response.data);
		}
	};

	const handleDeleteProduct = async (productId) => {
		if (window.confirm('Are you confirm to delete ?')) {
			const check = await DELETE_PRODUCT(productId);
			if (check.status === 204) {
				toast.success("Delete Product Successfully !");
			}
			else {
				toast.error("Delete Product Failure !");
			}
			getPrd();
		}
	}

	const handleChangePage = (e, newPage) => {
		setPage(newPage);
	};

	const handleChangeRowsPerPage = (e) => {
		setRowsPerPage(parseInt(e.target.value, 10)); //decimal
		setPage(0);
	};

	return (
		<Container style={{ marginTop: "12vh" }} maxWidth="lg">
			<Paper>
				<TableContainer>
					<Table>
						<TableHeadBlack>
							<TableRow>
								<TableCellWhite>Name</TableCellWhite>
								<TableCellWhite>Price</TableCellWhite>
								<TableCellWhite>Description</TableCellWhite>
								<TableCellWhite>Category</TableCellWhite>
								<TableCellWhite>Image</TableCellWhite>
								<TableCellWhite>Action</TableCellWhite>
							</TableRow>
						</TableHeadBlack>

						<TableBody>
							{
								listProduct
									.slice(page * rowsPerPage, (page + 1) * rowsPerPage)
									.map(prd => (
										<TableRow key={prd.Id}>
											<TableCell>{prd.Name}</TableCell>
											<TableCell>{prd.Price} VNĐ</TableCell>
											<TableCell>
												{prd.Description.length > 20
													? prd.Description.substring(0, 20) + "..."
													: prd.Description
												}
											</TableCell>
											<TableCell>{prd.Category}</TableCell>
											<TableCell>
												<Image src={PHOTO_PATH_URL + prd.ImagePath} alt="productIMG" />
											</TableCell>
											<TableCell>
												<IconButton>
													<EditIcon color="primary" />
												</IconButton>
												<IconButton onClick={() => handleDeleteProduct(prd.Id)}>
													<DeleteIcon color="secondary" />
												</IconButton>
											</TableCell>
										</TableRow>
									))
							}
						</TableBody>
					</Table>
				</TableContainer>

				<Grid container>
					<Grid item xs={12} sm={6}>
						<IconButton style={{ marginTop: "2px", marginLeft: "10px" }}>
							<AddCircleIcon color="primary" />
						</IconButton>
					</Grid>

					<Grid item xs={12} sm={6}>
						<TablePagination
							rowsPerPageOptions={[5, 10, 25]}
							component="div"
							count={listProduct.length}
							rowsPerPage={rowsPerPage}
							page={page}
							onPageChange={handleChangePage}
							onRowsPerPageChange={handleChangeRowsPerPage}
						/>
					</Grid>
				</Grid>
			</Paper>
		</Container>
	)
}
