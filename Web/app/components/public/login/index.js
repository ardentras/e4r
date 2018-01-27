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
import Visit from "./components/visit";
import Return from "./components/return";
import Styles from "./style.css";

class Login extends React.Component {
	constructor(props) {
		super(props);
		this.login = this.login.bind(this);
		this.visitor = this.visitor.bind(this);
	}
	login(event) {
		event.preventDefault();
		this.props.handlerAuth({username: event.target.uid.value, password: event.target.pw.value});
	}
	visitor(event) {
		event.preventDefault();
		console.log(event.target);
		// this.props.handlerRegister();
	}
	render() {
		const { redirectToReferrer } = this.props.states;
		console.log(this.props.states);
		if (redirectToReferrer) {
			return (
				<Redirect to="/dashboard"/>
			);
		}
		return (
			<div>
				<div className={Styles.clearfix}/>
				<div className={Styles.formcontainer}>
					<div className={Styles.forms}>	
						<Visit func={{visitor: this.visitor}}/>
						<hr/>
						<Return error={this.props.states.error} AUTHING={this.props.states.AUTHING} func={{auth: this.login}}/>
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
	(state) => ({states: state.state}),
	(dispatch) => bindActionCreators({handlerAuth,handlerRegister}, dispatch)
)(Login);