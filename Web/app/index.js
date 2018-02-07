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
import "./style.css";
import efrApi from "./libraries/efrApi";

efrApi.config({
    host: "35.163.221.182", 
    port: 3002,
    protocol: "http", 
    gamePath: "/api/q/request_block",
    renewPath: "/api/renew",
    loginPath: "/api/login",
    signupPath: "/api/signup",
    logoutPath: "/api/logout"
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
