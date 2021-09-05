import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import {
	Box,
	Button,
	Container,
	Link,
	TextField,
	Typography,
	Grid
} from '@material-ui/core';
import { useHistory } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { useForm } from "react-hook-form";
import { toast } from 'react-toastify';
import { RegisterAction } from '../../redux/slices/authenticationSlice';

const Register = () => {
	const { register, handleSubmit } = useForm();
	const dispatch = useDispatch();
	const history = useHistory();

	const onSubmit = async (data) => {
		const regInfor = { ...data, role: "Customer" };
		const check = await dispatch(RegisterAction(regInfor));
		if (check.payload) {
			toast.success("Register Successfully !");
			history.push("/login");
		}
		else {
			toast.error("Register Failure !");
		}
	};

	return (
		<Box
			style={{ marginTop: "15vh" }}
			sx={{
				backgroundColor: 'background.default',
				display: 'flex',
				flexDirection: 'column',
				height: '100%',
				justifyContent: 'center'
			}}
		>
			<Container maxWidth="md">
				<form onSubmit={handleSubmit(onSubmit)}>
					<Box sx={{ mb: 3 }}>
						<Typography color="textPrimary" variant="h4">
							Create new account
						</Typography>
					</Box>

					<Grid container spacing={2}>
						<Grid item xs={12} sm={6}>
							<TextField
								fullWidth
								required
								label="Name"
								margin="normal"
								{...register("name")}
								variant="outlined"
							/>
						</Grid>

						<Grid item xs={12} sm={6}>
							<TextField
								fullWidth
								required
								label="Email Address"
								margin="normal"
								{...register("email")}
								type="email"
								variant="outlined"
							/>
						</Grid>

						<Grid item xs={12} sm={6}>
							<TextField
								fullWidth
								required
								label="UserName"
								margin="normal"
								{...register("userName")}
								variant="outlined"
							/>
						</Grid>

						<Grid item xs={12} sm={6}>
							<TextField
								fullWidth
								required
								label="Password"
								margin="normal"
								{...register("password")}
								type="password"
								variant="outlined"
							/>
						</Grid>

						<Grid item xs={12} sm={6}>
							<TextField
								fullWidth
								required
								label="Phone Numer"
								margin="normal"
								{...register("phoneNumber")}
								variant="outlined"
							/>
						</Grid>

						<Grid item xs={12} sm={6}>
							<TextField
								fullWidth
								required
								label="Address"
								margin="normal"
								{...register("address")}
								variant="outlined"
							/>
						</Grid>
					</Grid>
					<Box sx={{ py: 2 }}>
						<Button
							color="primary"
							fullWidth
							size="large"
							type="submit"
							variant="contained"
						>
							Sign up now
						</Button>
					</Box>

					<Typography color="textSecondary" variant="body1">
						Have an account?{' '}
						<Link component={RouterLink} to="/login" variant="h6" underline="hover">
							Sign in
						</Link>
					</Typography>
				</form>
			</Container>
		</Box >
	);
};

export default Register;
