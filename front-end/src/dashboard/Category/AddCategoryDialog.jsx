import React from 'react';
import { useForm } from "react-hook-form";
import { POST_CATEGORY } from '../../api/apiCategory';
import { toast } from 'react-toastify';
import {
	Dialog, DialogActions, DialogContent,
	DialogTitle, TextField, IconButton
} from '@material-ui/core';
import CancelIcon from '@material-ui/icons/Cancel';
import AddCircleIcon from '@material-ui/icons/AddCircle';

export default function AddCategoryDialog(props) {
	const { open, handleClose, onReload } = props;
	const { register, handleSubmit, reset } = useForm();

	const onSubmit = async (data) => {
		const check = await POST_CATEGORY(data);
		if (check.status === 201) {
			toast.success("Add Category Successfully");
			reset();  // Reset Registered TextField
			handleClose();
			onReload();
		}
		else {
			toast.error("Add Category Failure !");
		}
	};

	return (
		<Dialog open={open} onClose={handleClose} fullWidth maxWidth="xs">
			<form onSubmit={handleSubmit(onSubmit)}>
				<DialogTitle>Add Category</DialogTitle>
				<DialogContent>
					<TextField
						required
						autoFocus
						margin="dense"
						{...register("name")}
						label="Name"
						type="text"
						fullWidth
					/>
				</DialogContent>
				<DialogActions>
					<IconButton onClick={handleClose} color="secondary">
						<CancelIcon fontSize="large" />
					</IconButton>
					<IconButton type="submit" color="primary">
						<AddCircleIcon fontSize="large" />
					</IconButton>
				</DialogActions>
			</form>
		</Dialog>
	)
}
