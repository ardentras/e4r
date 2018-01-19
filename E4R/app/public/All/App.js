/****************************************************************************
 * 
 *->Name: App.js 
 *->Purpose: React Component root of routers and Navagation component.
 *
*****************************************************************************/
import React from "react"; // eslint-disable-line no-unused-vars
import {
	BrowserRouter as Router // eslint-disable-line no-unused-vars
} from "react-router-dom";
import Nav from "../Navagation/Nav"; // eslint-disable-line no-unused-vars
import Routes from "../All/Routes"; // eslint-disable-line no-unused-vars
import Styles from "./style.css";

export default function App(props) { // eslint-disable-line no-unused-vars
	return (
		<Router>
			<div className={Styles.container}>
				<div>
					<Nav/>
				</div>
				<Routes/>
			</div>
		</Router>  
	);
}
