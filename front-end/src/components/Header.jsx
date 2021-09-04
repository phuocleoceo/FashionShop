import React from 'react';
import { Link } from 'react-router-dom';
import { AppBar, Toolbar, IconButton, Badge } from '@material-ui/core';
import HomeIcon from '@material-ui/icons/Home';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import AccountCircle from '@material-ui/icons/AccountCircle';

export default function Header() {

	return (
		<AppBar>
			<Toolbar>
				<Link to="/">
					<IconButton>
						<HomeIcon />
					</IconButton>
				</Link>

				<Link to="/cart" style={{ flexGrow: 1 }}>
					<IconButton>
						<Badge badgeContent="0" color="secondary">
							<ShoppingCartIcon />
						</Badge>
					</IconButton>
				</Link>

				<Link to="/login">
					<IconButton>
						<AccountCircle fontSize="large" color="inherit" />
					</IconButton>
				</Link>
			</Toolbar>
		</AppBar >
	)
}