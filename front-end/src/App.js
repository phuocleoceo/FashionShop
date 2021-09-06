import React, { useEffect } from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import NotFound from './components/NotFound';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useSelector, useDispatch } from 'react-redux';
import { CheckLoggedIn } from './redux/slices/authenticationSlice';
import { Get_Cart } from './redux/slices/cartSlice';
import Navigation from './components/Navigation';
import Home from './components/Home';
import Login from './components/Authentication/Login';
import Register from './components/Authentication/Register';
import Cart from './components/Cart';
import Category from './dashboard/Category';
import Product from './dashboard/Product'
import ProductDetail from './components/ProductDetail';

function App() {
	const isLoggedIn = useSelector(state => state.authentication.Id); //if notLoggedIn Id will be undefined
	const userId = useSelector(state => state.authentication.Id);
	const dispatch = useDispatch();

	useEffect(() => {
		dispatch(CheckLoggedIn());
	}, [dispatch]);

	useEffect(() => {
		if (userId) dispatch(Get_Cart(userId));
	}, [dispatch, userId]);

	return (
		<>
			<BrowserRouter>
				<Navigation />

				<Switch>
					<Route path="/" exact component={Home} />
					<Route path="/login" component={Login} />
					<Route path="/register" component={Register} />
					{isLoggedIn && <Route path="/cart" component={Cart} />}
					{isLoggedIn && <Route path="/dashboard/category" component={Category} />}
					{isLoggedIn && <Route path="/dashboard/product" component={Product} />}
					<Route path="/:productId" children={<ProductDetail />} />
					<Route component={NotFound} />
				</Switch>
			</BrowserRouter>

			<ToastContainer
				position="top-center"
				autoClose={3000}
				hideProgressBar={false}
				newestOnTop={false}
				closeOnClick
				rtl={false}
				pauseOnFocusLoss
				draggable
				pauseOnHover
			/>
		</>
	);
}

export default App;
