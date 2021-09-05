import React, { useEffect } from 'react';
import { useForm } from "react-hook-form";
import { PUT_CATEGORY } from '../../api/apiCategory';
import { toast } from 'react-toastify';
import {
	Dialog, DialogActions, DialogContent,
	DialogTitle, TextField, IconButton
} from '@material-ui/core';
import CancelIcon from '@material-ui/icons/Cancel';
import EditIcon from '@material-ui/icons/Edit';

export default function EditCategoryDialog(props) {
	const { open, handleClose, onReload, currentCtg } = props;
	const { register, handleSubmit, reset } = useForm();

	useEffect(() => {
		reset({ name: currentCtg.Name }); //Fix defaultValue in react-hook-form
	}, [reset, currentCtg]);

	const onSubmit = async (data) => {
		const check = await PUT_CATEGORY(currentCtg.Id, data);
		if (check.status === 204) {
			toast.success("Edit Category Successfully");
			reset();  // Reset Registered TextField
			handleClose();
			onReload();
		}
		else {
			toast.error("Edit Category Failure !");
		}
	};

	return (
		<Dialog open={open} onClose={handleClose} fullWidth maxWidth="xs">
			<form onSubmit={handleSubmit(onSubmit)}>
				<DialogTitle>Add Category</DialogTitle>
				<DialogContent>
					<TextField
						disabled
						autoFocus
						margin="dense"
						defaultValue={currentCtg.Id}
						label="Id"
						type="text"
						fullWidth
					/>
					<TextField
						required
						autoFocus
						margin="dense"
						defaultValue={currentCtg.Name}
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
						<EditIcon fontSize="large" />
					</IconButton>
				</DialogActions>
			</form>
		</Dialog>
	)
}
