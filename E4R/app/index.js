/****************************************************************************
 * 
 *->Name: index.js 
 *->Purpose: Root of React components.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import { Provider } from "react-redux"; // eslint-disable-line no-unused-vars
import ReactDOM from "react-dom";
import App from "./public/All/App"; // eslint-disable-line no-unused-vars
import store from "./redux/store";

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
