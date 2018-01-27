import React from "react";
import { Link } from 'react-router-dom'
import Styles from "./style.css";

const Visit = props =>(
    <form className={Styles.visitor} action="javascript:void(0);" onSubmit={props.func.visitor}>
		<h1 className={Styles.headers}>Visitor</h1>
		<span>Start exploring our application as a visitor.</span>
		<input className={Styles.inputs} type="submit" name="continue" value="Continue"/>
		<span>OR</span>
		<input className={Styles.inputs} type="submit" name="signup" value="Join Us"/>
		<Link to="/contacts">need help?</Link>
	</form>
);

export default Visit;
