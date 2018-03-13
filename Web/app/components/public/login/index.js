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
import { handlerAuth, handlerRegister } from "../../../redux/actions/auth";
import { ifSignUp, setSignUpSuccessful } from "../../../redux/actions/state";
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
	login(event) {
		event.preventDefault();
		this.props.handlerAuth({username: event.target.username.value, password: event.target.password.value});
	}
	signup(event) {
		event.preventDefault();
		this.props.setSignUpSuccessful(false);
		this.props.ifSignUp(!this.props.states.IS_SIGNUP);
	}
	register(event) {
		event.preventDefault();
		this.props.handlerRegister({email: event.target.email.value, username: event.target.username.value, password: event.target.password.value});
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
	(dispatch) => bindActionCreators({handlerAuth,handlerRegister,ifSignUp,setSignUpSuccessful}, dispatch)
)(Login);