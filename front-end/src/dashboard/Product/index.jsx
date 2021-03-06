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
import AddProductDialog from './AddProductDialog';
import EditProductDialog from './EditProductDialog';

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
	const [addDialogShow, setAddDialogShow] = useState(false);
	const [editDialogShow, setEditDialogShow] = useState(false);
	const [currentPrd, setCurrentPrd] = useState({
		Id: 0, Name: "", Price: 0, Description: "", CategoryId: 0, ImagePath: ""
	});
	const [reload, setReload] = useState(false);

	useEffect(() => getPrd(), [reload]);

	const getPrd = async () => {
		const response = await GET_PRODUCT();
		if (response.status === 200) {
			setListProduct(response.data);
		}
	};

	const handleEditProduct = (prd) => {
		setCurrentPrd(prd);
		setEditDialogShow(true);
	}

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

	const reloadPage = () => setReload(!reload);

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
											<TableCell>{prd.Price} VN??</TableCell>
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
												<IconButton onClick={() => handleEditProduct(prd)}>
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
						<IconButton style={{ marginTop: "2px", marginLeft: "10px" }}
							onClick={() => setAddDialogShow(true)}>
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
			<AddProductDialog
				open={addDialogShow}
				handleClose={() => setAddDialogShow(false)}
				onReload={reloadPage}
			/>

			<EditProductDialog
				open={editDialogShow}
				handleClose={() => setEditDialogShow(false)}
				onReload={reloadPage}
				currentPrd={currentPrd}
			/>
		</Container>
	)
}
