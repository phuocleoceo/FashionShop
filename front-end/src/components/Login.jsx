import { Link as RouterLink } from 'react-router-dom';
import { Box, Button, Container, Grid, Link, TextField, Typography } from '@material-ui/core';
import FacebookIcon from '../assets/icon/Facebook';
import GoogleIcon from '../assets/icon/Google';

const Login = () => {
	return (
		<>
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
					<form>
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
							label="Username"
							margin="normal"
							name="username"
							type="text"
							variant="outlined"
						/>
						<TextField
							fullWidth
							label="Password"
							margin="normal"
							name="password"
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
		</>
	);
};

export default Login;
