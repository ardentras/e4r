/****************************************************************************
 * 
 *->Name: index.js 
 *->Purpose: Root of React components.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import ReactDOM from "react-dom";
import { Provider } from "react-redux"; // eslint-disable-line no-unused-vars
import store from "./redux/store";
import App from "./components/public"; // eslint-disable-line no-unused-vars
import iAuth from "./libraries/iAuth";
import "./style.css";

iAuth.config({
	host: "http://35.163.221.182:3002",
	universalPath: "/api",
	loginPath: "/login",
	logoutPath: "/logout",
	registerPath: "/signup",
	gamePath: "/q/request_block"
});
/****************************************************************************
 * 
 *->React Name: render 
 *->Purpose: Render the App component, also wrapping it with the provider component
 *           Allowing linking between redux tree with the components.
 *
*****************************************************************************/
ReactDOM.render(
	<Provider store={store}>
		<App/>
	</Provider>,
	document.getElementById("app")
);
