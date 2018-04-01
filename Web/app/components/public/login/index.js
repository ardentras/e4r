/****************************************************************************
 * 
 *->Name: Login.js 
 *->Purpose: React Component of Login Section.
 *
*****************************************************************************/

"use strict";
import "babel-polyfill";
import React from "react";
import PropTypes from "prop-types";
import { Redirect, Link } from 'react-router-dom'
import { bindActionCreators } from "redux";
import { connect } from "react-redux";

import { handleAuthentication, handleSignUp } from "../../../redux/actions/user"; 
import { SignUp, setTheme } from "../../../redux/actions/state";

import iCookie from "../../../libraries/iCookie";
import Visit from "./components/visit";
import Signup from "./components/signup";
import Return from "./components/return";
import Styles from "./style.css";

class Login extends React.Component {
	constructor(props) {
		super(props);
		this.login = this.login.bind(this);
		this.signup = this.signup.bind(this);
		this.register = this.register.bind(this);
	}
	componentWillMount() {
		const savedTheme = iCookie.getStorage("theme");
		if (savedTheme && this.props.states.THEME !== savedTheme) {
			this.props.setTheme(savedTheme);
		}
	}
	login(event) {
		event.preventDefault();
		this.props.handleAuthentication(event.target.username.value, event.target.password.value);
	}
	signup(event) {
		event.preventDefault();
		this.props.SignUp(!this.props.states.IS_SIGNUP);
	}
	register(event) {
		event.preventDefault();
		this.props.handleSignUp({email: event.target.email.value, username: event.target.username.value, password: event.target.password.value});
	}
	render() {
		const { redirectToRefer } = this.props.states;
		if (redirectToRefer) {
			return (
				<Redirect to="/dashboard"/>
			);
		}
		return (
			<div>
				<div className={Styles.formcontainer}>
					<div className={Styles.forms}>	
						<Visit func={{signup: this.signup}}/>
						<hr/>
						{ !this.props.states.IS_SIGNUP && <Return error={this.props.states.error} AUTHING={this.props.states.AUTHING} func={{auth: this.login, signup: this.signup}} SIGN_SUC={this.props.states.SIGNUP_SUCCESSFUL}/>}
						{ this.props.states.IS_SIGNUP && <Signup error={this.props.states.error} func={{register: this.register, signup: this.signup}}/>}
					</div>
				</div>
			</div>
		);
	}
}

Login.propTypes = {
	states: PropTypes.object,
};

export default connect(
	(state) => ({states: state.state, user: state.user}),
	(dispatch) => bindActionCreators({ 
		SignUp, 
		handleAuthentication,
		handleSignUp,
		setTheme }, dispatch)
)(Login);