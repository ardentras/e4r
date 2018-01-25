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
	Switch, // eslint-disable-line no-unused-vars
	Redirect // eslint-disable-line no-unused-vars
} from "react-router-dom";
import Home from "../Home/Home";
import Login from "../Login/Login";
import Styles from "./style.css"; // eslint-disable-line no-unused-vars
import Private from "../../private";
import Contact from "../Contact/contact";
import PrivateRoute from "./privateroute";// eslint-disable-line no-unused-vars
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { setSessionToken } from "../../redux/actions/auth/auth";
import {Refer, setAuthenticateSuccess} from "../../redux/actions/state/state";
import "babel-polyfill";
import iAuth from "../../libraries/iAuth";
import iCookie from "../../libraries/iCookie";

class Routes extends React.Component {
	constructor(props) {
		super(props);
	}
	async componentWillMount() {
		if (!this.props.states.IS_AUTH) {
			const check = await iAuth.ifPersist();
			if (check && check.data.response != "Failed") {
				if (check.data.response) {
					const date = new Date();
					date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000));
					const expire = "expires=" + date.toUTCString();
					const cookie1 = "session=" + check.data.session_id + ";" + expire + ";path=/";
					iCookie.set(cookie1);
					this.props.setAuthenticateSuccess(true);
					this.props.Refer();
				}
			}
		}
	}
	render() {
		return (
			<section>
				<Switch>
					<Route exact path='/' component={Home}/>
					<Route path='/contact' component={Contact}/>
					<Route path='/login' component={Login}/>
					<PrivateRoute path="/dashboard" component={Private}/>
				</Switch>
			</section>
		);
	}
}

export default connect(
	(state) => ({states: state.state, user: state.user}),
	(dispatch) => bindActionCreators({Refer, setAuthenticateSuccess, setSessionToken}, dispatch),null,{pure: false}
)(Routes);