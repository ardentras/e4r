/****************************************************************************
 * 
 *->Name: Nav.js 
 *->Purpose: React Component of the Navagation options.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import { NavLink } from "react-router-dom"; // eslint-disable-line no-unused-vars
import Styles from "./style.css";

const Cat = props => 
	<img className={Styles.cat} src="/app/assets/cat.ico" alt="Cat"/>;

export default function Nav (props) { // eslint-disable-line no-unused-vars
	return (
		<div className={Styles.head}>
			<div className={Styles.testbanner}>
				<span className={Styles.bannertext}>The current website is for testing only...</span> 
				<Cat/>
			</div>
			<div className={Styles.navcontainer}>
				<div>
					<img src="/app/assets/logo.png" alt="" id={Styles.logo}/>
					<span>Education for Revitalization</span>
				</div>
				<ul className={Styles.navagations}>
					<li className={Styles.navagation}><NavLink activeClassName="active" to='/'>HOME</NavLink></li>
					<li className={Styles.navagation}><NavLink activeClassName="active" to='/contact'>CONTACT US</NavLink></li>
					<li className={Styles.navagation}><NavLink activeClassName="active" to='/login'>LOG IN</NavLink></li>
				</ul>
			</div>
		</div>
	);
}