/****************************************************************************
 * 
 *->Name: Nav.js 
 *->Purpose: React Component of the Navagation options.
 *
*****************************************************************************/

import React from "react"; // eslint-disable-line no-unused-vars
import { NavLink } from "react-router-dom"; // eslint-disable-line no-unused-vars
import { connect } from "react-redux";
import Proptypes from "prop-types";
import Styles from "./style.css";

const Cat = props => 
	<img className={Styles.cat} src="/static/cat.ico" alt="X"/>;

class Nav extends React.Component {
	constructor(props) {
		super(props);
	}
	render() {
		return (
			<div className={Styles.head}>
				<div className={Styles.testbanner}>
					<span className={Styles.bannertext}>The current website is for testing only...</span> 
					<Cat/>
				</div>
				<div className={Styles.navcontainer}>
					<div>
						<img src="/static/logo/logo.png" alt="X" id={Styles.logo}/>
						<span>Education for Revitalization</span>
					</div>
					<ul className={Styles.navagations}>
						<li className={Styles.navagation}><NavLink activeClassName="active" to='/'>HOME</NavLink></li>
						<li className={Styles.navagation}><NavLink activeClassName="active" to='/contact'>CONTACT US</NavLink></li>
						<li className={Styles.navagation}><NavLink activeClassName="active" to='/dashboard'>{this.props.states.IS_AUTH ? "DASHBOARD" : "LOG IN"}</NavLink></li>
					</ul>
				</div>
			</div>
		);
	}
}
Nav.propTypes = {
	states: Proptypes.object
};

export default connect(
	(state) => ({states: state.state}))(Nav);