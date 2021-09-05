import React from 'react';
import { Link } from 'react-router-dom';
import { AppBar, Toolbar, IconButton, Badge } from '@material-ui/core';
import HomeIcon from '@material-ui/icons/Home';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import AccountCircle from '@material-ui/icons/AccountCircle';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import { useSelector, useDispatch } from 'react-redux';
import { Logout } from '../redux/slices/authenticationSlice';
import { useHistory } from 'react-router-dom';

export default function Navigation() {
	const isLoggedIn = useSelector(state => state.authentication.isLoggedIn);
	const cart = useSelector(state => state.cart);
	const dispatch = useDispatch();
	const history = useHistory();

	const handleLogout = () => {
		dispatch(Logout());
		history.push("/");
	};

	return (
		<AppBar>
			<Toolbar>
				{
					!isLoggedIn ?
						<>
							<Link to="/" style={{ flexGrow: 1 }}>
								<IconButton>
									<HomeIcon fontSize="large" />
								</IconButton>
							</Link>
							<Link to="/register">
								<IconButton>
									<PersonAddIcon fontSize="large" />
								</IconButton>
							</Link>

							<Link to="/login">
								<IconButton>
									<AccountCircle fontSize="large" />
								</IconButton>
							</Link>
						</>
						:
						<>
							<Link to="/">
								<IconButton>
									<HomeIcon fontSize="large" />
								</IconButton>
							</Link>
							<Link to="/cart" style={{ flexGrow: 1 }}>
								<IconButton>
									<Badge color="secondary"
										badgeContent={cart.length > 0 ? cart.length : "0"}>
										<ShoppingCartIcon fontSize="large" />
									</Badge>
								</IconButton>
							</Link>
							<IconButton onClick={handleLogout}>
								<ExitToAppIcon fontSize="large" />
							</IconButton>
						</>
				}
			</Toolbar>
		</AppBar >
	)
}