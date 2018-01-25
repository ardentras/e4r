/****************************************************************************
 * 
 *->Name: Login.js 
 *->Purpose: React Component of Login Section.
 *
*****************************************************************************/

"use strict";
import React from "react";
import PropTypes from "prop-types";
import iAuth from "../../libraries/iAuth";
import "babel-polyfill";
import {Redirect} from 'react-router-dom'

import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { 
	setUserInformation,
	handlerUserAuth,
	handlerUserDeAuth,
	handlerRegister } from "../../redux/actions/auth/auth";
import { 
	ifSignUp, 
	setAuthenticateSuccess,
	Error } from "../../redux/actions/state/state";
import Returning from "./components/Returning"; // eslint-disable-line no-unused-vars
import Visitor from "./components/Visitor"; // eslint-disable-line no-unused-vars
import Signup from "./components/Signup"; // eslint-disable-line no-unused-vars
import Styles from "./style.css";

iAuth.config({  
	host: "http://35.163.221.182:3002",
	universalPath: "/api",
	loginPath: "/login",
	logoutPath: "/logout",
	registerPath: "/signup"
});

class Login extends React.Component {
	constructor(props) {
		super(props);
		this.authenticate = this.authenticate.bind(this);
		this.register = this.register.bind(this);
		this.update = this.update.bind(this);
		this.signUp = this.signUp.bind(this);
		this.state = {
			tryAuth: false,
			tryReg: false
		};
	}
	update({Auth=false, Reg=false}) {
		this.setState({ // eslint-disable-line react/no-set-state
			tryAuth: Auth,
			tryReg: Reg
		});
	}
	signUp(state) {
		this.props.ifSignUp(state);	
		this.props.Error(null);
		state === false ? this.props.setUserInformation({}) : null;
	}
	componentDidUpdate() {
		if (this.state.tryAuth) {
			this.props.handlerUserAuth(this.props.user);
			this.update({});
		}
	}
	register(event) {
		const user = {
			email: event.target.email.value,
			username: event.target.username.value,
			password: event.target.password.value,
		};
		this.update({Reg: true});
		this.props.handlerRegister(user);
	}
	authenticate(event) {
		if (!this.props.states.IS_AUTH) {
			this.update({Auth: true});
			this.props.setUserInformation({
				username: event.target.username.value,
				password: event.target.password.value,
			});
		}
	}
	render() {
		const { redirectToReferrer } = this.props.states;
		if (redirectToReferrer) {
			return (
				<Redirect to="/dashboard"/>
			);
		}
		return (
			<div>
				<div className={Styles.clearfix}></div>
				<div className={Styles.logincontainer}>
					<Visitor signUp={this.signUp}
						IS_SIGNUP={this.props.states.IS_SIGNUP}
						signUp={this.signUp}/>
					{this.props.states.IS_SIGNUP ? 
						<Signup 
							signUp={this.register}
							error={this.props.states.error} /> :
						<Returning 
							IS_AUTH={this.props.states.IS_AUTH} 
							Authenticate={this.authenticate} 
							Logout={this.Logout}
							SIGNUP_SUCCESSFUL={this.props.states.SIGNUP_SUCCESSFUL}
							error={this.props.states.error}
							AUTHING={this.props.states.AUTHING}/>
					}
				</div>
			</div>
		);
	}
}

Login.propTypes = {
	user: PropTypes.object,
	states: PropTypes.object,
	ifSignUp: PropTypes.func,
	setUserInformation: PropTypes.func,
	handlerUserAuth: PropTypes.func,
	handlerUserDeAuth: PropTypes.func,
	handlerRegister: PropTypes.func,
	setAuthenticateSuccess: PropTypes.func,
	Error: PropTypes.func
};

export default connect(
	(state) => ({user: state.user, states: state.state}),
	(dispatch) => bindActionCreators({
		handlerUserAuth,
		setUserInformation,
		handlerUserDeAuth,
		handlerRegister,
		ifSignUp,
		setAuthenticateSuccess,
		Error }, dispatch)
)(Login);