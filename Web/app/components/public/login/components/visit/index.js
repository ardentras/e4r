import React from "react";
import { Link } from 'react-router-dom'
import Styles from "./style.css";

const Visit = props =>(
    <div className={Styles.visitor}>
		<h1 className={Styles.headers}>Visitor</h1>
		<span>Start exploring our application as a visitor.</span>
		<input className={Styles.inputs} type="button" onClick={null} value="Continue"/>
		<span>OR</span>
		<input className={Styles.inputs} type="button" onClick={props.func.signup} value="Join Us"/>
		<Link to="/contacts">need help?</Link>
	</div>
);

export default Visit;
