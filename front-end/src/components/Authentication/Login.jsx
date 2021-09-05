import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import { Box, Button, Container, Grid, Link, TextField, Typography } from '@material-ui/core';
import FacebookIcon from '../../assets/icon/Facebook';
import GoogleIcon from '../../assets/icon/Google';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { toast } from 'react-toastify';
import { LoginAction } from '../../redux/slices/authenticationSlice';
import { useForm } from "react-hook-form";

const Login = () => {
	const dispatch = useDispatch();
	const history = useHistory();
	const { register, handleSubmit } = useForm();

	const onSubmit = async (data) => {
		const check = await dispatch(LoginAction(data));
		if (check.payload.Accepted) {
			toast.success("Login Successfully");
			history.push("/");
		}
		else {
			toast.error("Login Failure");
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
			<Container maxWidth="sm">
				<form onSubmit={handleSubmit(onSubmit)}>
					<Box sx={{ mb: 3 }}>
						<Typography color="textPrimary" variant="h4">
							Sign in
						</Typography>

						<Typography
							color="textSecondary"
							gutterBottom
							variant="body2"
						>
							Sign in on the internal platform
						</Typography>
					</Box>
					<Grid container spacing={3}>
						<Grid item xs={12} md={6}>
							<Button
								color="primary"
								fullWidth
								startIcon={<FacebookIcon />}
								size="large"
								variant="contained"
							>
								Login with Facebook
							</Button>
						</Grid>
						<Grid item xs={12} md={6}>
							<Button
								color="primary"
								fullWidth
								startIcon={<GoogleIcon />}
								size="large"
								variant="contained"
							>
								Login with Google
							</Button>
						</Grid>
					</Grid>
					<Box sx={{ pb: 1, pt: 3 }}						>
						<Typography
							align="center"
							color="textSecondary"
							variant="body1"
						>
							or login with username and password
						</Typography>
					</Box>
					<TextField
						fullWidth
						required
						label="Username"
						margin="normal"
						{...register("userName")}
						type="text"
						variant="outlined"
					/>
					<TextField
						fullWidth
						required
						label="Password"
						margin="normal"
						{...register("password")}
						type="password"
						variant="outlined"
					/>
					<Box sx={{ py: 2 }}>
						<Button
							color="primary"
							fullWidth
							size="large"
							type="submit"
							variant="contained"
						>
							Sign in now
						</Button>
					</Box>
					<Typography
						color="textSecondary"
						variant="body1"
					>
						Don&apos;t have an account?
						{' '}
						<Link component={RouterLink} to="/register" variant="h6" underline="hover">
							Sign up
						</Link>
					</Typography>
				</form>
			</Container>
		</Box>
	);
};

export default Login;
