/****************************************************************************
 * 
 *->Name: Routes.js 
 *->Purpose: React Component containing Routers for different sections of page
 *->Note: Will update to React router v4 with dynamic routing.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import {
	Route, // eslint-disable-line no-unused-vars
	Switch // eslint-disable-line no-unused-vars
} from "react-router-dom";
import Home from "../Home/Home";
import Login from "../Login/Login";
import Styles from "./style.css"; // eslint-disable-line no-unused-vars
import Contact from "../Contact/contact";

export default function Routes(props) { // eslint-disable-line no-unused-vars
	return (
		<section>
			<Switch>
				<Route exact path='/' component={Home}/>
				<Route path='/contact' component={Contact}/>
				<Route path='/login' component={Login}/>
			</Switch>
		</section>
	);
}