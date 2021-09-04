import { BrowserRouter, Route, Switch } from 'react-router-dom';
import Header from './components/Header';
import Home from './components/Home';
import Login from './components/Login';
import NotFound from './components/NotFound';

function App() {
	return (
		<>
			<BrowserRouter>
				<Header />

				<Switch>
					<Route path="/" exact component={Home} />
					<Route path="/login" component={Login} />
					<Route component={NotFound} />
				</Switch>
			</BrowserRouter>
		</>
	);
}

export default App;
