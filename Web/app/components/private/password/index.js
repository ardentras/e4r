import React from "react";
import Styles from "./style.css";
import { connect } from "react-redux";
import Proptypes from "prop-types";

class Password extends React.Component {
	constructor(props) {
		super(props);
	}
	render() {
		return (
			<div className={Styles.pwreset}>
				<form className={Styles.pwform} action="javascript:void(0);" onSubmit={null}>
					<h1 className={Styles.pwheader}>Password Reset</h1>
					<span>Current Password</span>
					<input type="password" required/>
					<span>New Password</span>
					<input type="password" required/>
					<span>Confirm New Password</span>
					<input type="password" required/>
					<input id={Styles.resetbtn} type="submit" value="RESET MY PASSWORD"/>
				</form>
			</div>
		)
	}
}

export default connect(
	(state) => ({user: state.user})
)(Password);