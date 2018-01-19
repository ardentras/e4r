/****************************************************************************
 * 
 *->Name: Returning.js 
 *->Purpose: React Component displaying the Returning side of the login section.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import PropTypes from "prop-types";
import Styles from "./style.css";

const Returning = props => 
	<div className={[Styles.formcontainer, Styles.cons].join(" ")}>
		<h1 className={Styles.heading}>Returning</h1>
		<form className={Styles.userform} action="javascript:void(0);" onSubmit={!props.IS_AUTH ? props.Authenticate : props.Logout} method="POST">
			{props.SIGNUP_SUCCESSFUL && <div className={[Styles.ssuccessful, Styles.warning].join(" ")}><h1>Success!</h1><span>You have successfully registered for EFR.</span></div>}
			{props.error === "AUTH_FAIL" && <span className={[Styles.lfail, Styles.warning].join(" ")}>Incorrect username/password!</span>}
			{props.AUTHING && <span className={[Styles.ssuccessful, Styles.warning].join(" ")}>Authenticating...</span>}
			<span>Username:</span>
			<input className={Styles.fields} type="text" name="username" placeholder = "username/email" required/>
			<span>Password:</span>
			<input className={Styles.fields} type="password" name="password" placeholder ="password" required/>
			<a className={Styles.forgot} href="#">forgot password?</a>
			{!props.IS_AUTH && <input className={Styles.btn} type="submit" value="LOG IN"/>}
			{ props.IS_AUTH && <input className={Styles.btn} type="submit" value="LOG OUT"/> }
		</form>
	</div>;

Returning.propTypes = {
	error: PropTypes.string,
	SIGNUP_SUCCESSFUL: PropTypes.bool,
	AUTHING: PropTypes.bool,
	IS_AUTH: PropTypes.bool,
	Authenticate: PropTypes.func,
	Logout: PropTypes.func
};

export default Returning;
