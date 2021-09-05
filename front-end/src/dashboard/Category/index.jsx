import React, { useState, useEffect } from 'react';
import { GET_CATEGORY } from '../../api/apiCategory';
import {
	Container, Table, TableContainer, Paper,
	TableHead, TableRow, TableCell, TableBody,
	styled, IconButton, TablePagination, Grid
} from '@material-ui/core';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';

const TableHeadBlack = styled(TableHead)({
	backgroundColor: "black"
});
const TableCellWhite = styled(TableCell)({
	color: "white"
});

export default function Category() {
	const [listCategory, setListCategory] = useState([]);
	const [page, setPage] = useState(0);
	const [rowsPerPage, setRowsPerPage] = useState(5);
	useEffect(() => {
		const getCtg = async () => {
			const response = await GET_CATEGORY();
			if (response.status === 200) {
				setListCategory(response.data);
			}
		};
		getCtg();
	}, []);

	const handleChangePage = (e, newPage) => {
		setPage(newPage);
	};

	const handleChangeRowsPerPage = (e) => {
		setRowsPerPage(parseInt(e.target.value, 10)); //decimal
		setPage(0);
	};

	return (
		<Container style={{ marginTop: "12vh" }} maxWidth="md">
			<Paper>
				<TableContainer>
					<Table>
						<TableHeadBlack>
							<TableRow>
								<TableCellWhite>Name</TableCellWhite>
								<TableCellWhite>Action</TableCellWhite>
							</TableRow>
						</TableHeadBlack>

						<TableBody>
							{
								listCategory
									.slice(page * rowsPerPage, (page + 1) * rowsPerPage)
									.map(ctg => (
										<TableRow key={ctg.Id}>
											<TableCell>{ctg.Name}</TableCell>

											<TableCell>
												<IconButton>
													<EditIcon color="primary" />
												</IconButton>
												<IconButton>
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
							count={listCategory.length}
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
