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
import Testbanner from "./components/testbanner";
import Routes from "../routes";
import Styles from "./style.css";

class Navagation extends React.Component {
	constructor(props) {
		super(props);
	}
	render() {
		return (
			<div className={Styles.navagation}>
				<Testbanner/>
				<div className={Styles.header}>
					<div className={Styles.brand}>
						<img src="/static/logo/logo.png" alt="X" id={Styles.logo}/>
						<span>Education for Revitalization</span>
					</div>
					<div className={Styles.navagations}>
						{Routes.map((elem,index)=>(
							<NavLink key={index} exact={elem.exact} to={elem.path} className={Styles.navselector} activeClassName={Styles.activelink}>{elem.label}</NavLink>
						))}
					</div>
				</div>
			</div>
		);
	}
}

export default Navagation;
// Navagation.propTypes = {
// 	states: Proptypes.object
// };

// export default connect(
// 	(state) => ({states: state.states}))(Navagation);