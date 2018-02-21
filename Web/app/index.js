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
import Spinner from "./components/loading";
import efrApi from "./libraries/efrApi";
import SpinnerStyle from "./components/loading/style.css";

efrApi.config({
    host: "35.163.221.182", 
    port: 3002,
    protocol: "http", 
    gamePath: "/api/q/request_block",
    renewPath: "/api/renew",
    loginPath: "/api/login",
    signupPath: "/api/signup",
    logoutPath: "/api/logout",
    updatePath: "/api/update_uo"
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

function windowLoaded() {
    const Loading = document.getElementsByClassName(SpinnerStyle.spinnercontainer)[0];
    Loading.style.display = "none";
};

window.onload = () => {
    setTimeout(windowLoaded, 300);
};