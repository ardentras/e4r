/****************************************************************************
 * 
 *->Name: Visitor.js 
 *->Purpose: React Component displaying the Visitor side of the login section.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import PropTypes from "prop-types";
import Styles from "./style.css";
import iAuth from "../../../libraries/iAuth";

const Visitor = props =>  // eslint-disable-line no-unused-vars
	<div className={[Styles.visitorContainer, Styles.cons].join(" ")}>
		<h1 className={Styles.heading}>Just Visiting?</h1>
		<p className={Styles.description} >Start exploring our application as a visitor.</p>
		<button className={Styles.vbtn} onClick={iAuth.ifPersist}>Continue</button>
		<span className={Styles.or}>OR</span>
		<button onClick={ props.signUp.bind(null,(!props.IS_SIGNUP ? true : false))} className={Styles.vbtn}>{!props.IS_SIGNUP ? "Join Us" : "Log In" }</button>
		<button className={Styles.needhelp}>need help?</button>
	</div>;


Visitor.propTypes = {
	signUp: PropTypes.func,
	IS_SIGNUP: PropTypes.bool
};


export default Visitor;
