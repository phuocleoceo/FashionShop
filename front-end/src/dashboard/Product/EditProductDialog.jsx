import React, { useState, useEffect } from 'react';
import { useForm } from "react-hook-form";
import { PUT_PRODUCT, SAVE_PHOTO } from '../../api/apiProduct';
import { GET_CATEGORY } from '../../api/apiCategory';
import { toast } from 'react-toastify';
import {
	Dialog, DialogActions, DialogContent, Select, InputLabel, Button,
	DialogTitle, TextField, IconButton, Grid, MenuItem, FormControl
} from '@material-ui/core';
import CancelIcon from '@material-ui/icons/Cancel';
import EditIcon from '@material-ui/icons/Edit';
import PhotoCamera from '@material-ui/icons/PhotoCamera';
import { PHOTO_PATH_URL } from '../../extension/AppURL';

export default function EditProductDialog(props) {
	const { open, handleClose, onReload, currentPrd } = props;
	const { register, handleSubmit, reset } = useForm();
	const [ctg, setCtg] = useState([]);
	const [image, setImage] = useState("");
	const [selectedImage, setSelectedImage] = useState();

	useEffect(() => {
		setImage(PHOTO_PATH_URL + currentPrd.ImagePath);
		reset(currentPrd); //Fix defaultValue in react-hook-form
	}, [reset, currentPrd]);

	useEffect(() => getCtg(), []);

	const getCtg = async () => {
		const response = await GET_CATEGORY();
		if (response.status === 200) {
			setCtg(response.data);
		}
	};

	const onSubmit = async (data) => {
		const infor = {
			...data,
			price: parseFloat(data.price),
			imagePath: selectedImage ? selectedImage.name : currentPrd.ImagePath
		};
		const check = await PUT_PRODUCT(currentPrd.Id, infor);
		if (check.status === 204) {
			toast.success("Edit Product Successfully");
			reset();  // Reset Registered TextField
			handleClose();
			onReload();
			if (selectedImage) await savePhoto(selectedImage);
		}
		else {
			toast.error("Edit Product Failure !");
		}
	};

	const savePhoto = async (photo) => {
		const formData = new FormData();
		formData.append("myFile", photo, photo.name);
		await SAVE_PHOTO(formData);
	};

	const handleFileSelected = (e) => {
		e.preventDefault();
		setSelectedImage(e.target.files[0]);
		setImage(URL.createObjectURL(e.target.files[0]));
	};

	return (
		<Dialog open={open} onClose={handleClose} fullWidth maxWidth="sm">
			<form onSubmit={handleSubmit(onSubmit)}>
				<DialogTitle>Edit Product</DialogTitle>
				<DialogContent style={{ height: "37vh" }}>
					<Grid container spacing={5}>
						<Grid item xs={12} sm={6}>
							<TextField
								required
								autoFocus
								margin="dense"
								defaultValue={currentPrd.Name}
								{...register("name")}
								label="Name"
								type="text"
								fullWidth
							/>
							<TextField
								required
								margin="dense"
								defaultValue={currentPrd.Price}
								{...register("price")}
								label="Price"
								type="text"
								fullWidth
							/>
							<TextField
								margin="dense"
								defaultValue={currentPrd.Description}
								{...register("description")}
								label="Description"
								type="text"
								fullWidth
							/>

							<FormControl style={{ minWidth: "100%" }}>
								<InputLabel>Category</InputLabel>
								<Select {...register("categoryId")} defaultValue={currentPrd.CategoryId}>
									{
										ctg.map(c => (
											<MenuItem value={c.Id}>{c.Name}</MenuItem>
										))
									}
								</Select>
							</FormControl>
						</Grid>

						<Grid item xs={12} sm={6}>
							<img src={image} width="170vw" height="auto" alt="upload"
								style={{ marginLeft: "2vw" }} />
							<p></p>

							<input accept="image/*" type="file" style={{ display: "none" }}
								id="contained-button-file" onChange={handleFileSelected} multiple />

							<label htmlFor="contained-button-file">
								<Button variant="contained" color="primary"
									style={{ marginLeft: "4vw" }} component="span"
									startIcon={<PhotoCamera />}
								>
									Upload
								</Button>
							</label>
						</Grid>
					</Grid>
				</DialogContent>
				<DialogActions>
					<IconButton onClick={handleClose} color="secondary">
						<CancelIcon fontSize="large" />
					</IconButton>
					<IconButton type="submit" color="primary">
						<EditIcon fontSize="large" />
					</IconButton>
				</DialogActions>
			</form>
		</Dialog>
	)
}
