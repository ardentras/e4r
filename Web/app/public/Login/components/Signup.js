/****************************************************************************
 * 
 *->Name: Signup.js 
 *->Purpose: React Component displaying the Signup side of the login section.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import PropTypes from "prop-types";
import Styles from "./style.css";

const Signup = props => 
	<div className={[Styles.formcontainer, Styles.cons].join(" ")}>
		<h1 className={Styles.heading}>Sign Up</h1>
		<form className={Styles.userform} action="javascript:void(0);" onSubmit={props.signUp} method="POST">
			{props.error === "SIGNUP_FAIL" && <span className={[Styles.lfail, Styles.warning].join(" ")}>Email/Username already exist!</span>}
			{props.error === "INVALID_EMAIL" && props.EMAIL_INVALID && <span className={[Styles.lfail, Styles.warning].join(" ")}>Email invalid!</span>}
			{props.error === "CONN_ERR" && <span className={[Styles.lfail, Styles.warning].join(" ")}>Cannot connect to server!</span>}
			<span>Email:</span>
			<input className={Styles.fields} type="email" name="email" placeholder = "email" required/>
			<span>Username:</span>
			<input className={Styles.fields} type="text" name="username" placeholder = "username" required/>
			<span>Password:</span>
			<input className={Styles.fields} type="password" name="password" placeholder ="password" required/>
			<input className={Styles.btn} type="submit" value="Sign Up"/>
		</form>
	</div>;

Signup.propTypes = {
	signUp: PropTypes.func,
	SIGNUP_SUCCESSFUL: PropTypes.bool,
	error: PropTypes.string
};

export default Signup;
