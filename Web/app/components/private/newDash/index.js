import React from "react";
import { connect } from "react-redux";
import { NavLink } from "react-router-dom";
import Routes from "../routes";
import Style from "./style.css";

class Dashboard extends React.Component {
	constructor(props) {
		super(props);
	}
	render() {
		return (
			<div className={Style.dashboard}>
				<div className={Style.selectors}>
					{Routes.map((elem, index)=>{
						if (elem.label !== "Password Reset") {
							return <NavLink key={index} exact={elem.exact} to={elem.path} className={Style.selector} activeClassName={Style.activeselector}>{elem.icon}<span className={Style.linktext}>{elem.label}</span></NavLink>
						}
					})}
					<div className={Style.logout} onClick={this.props.func.handleLogOut}><i className={["fa", "fa-sign-out", Style.icons].join(" ")} aria-hidden="true"/><span className={Style.linktext}>Logout</span></div>
				</div>
			</div>
		)
	}
}

export default connect(
	(state) => ({user: state.user}),
	null,
	null,
	{pure: false}
)(Dashboard);