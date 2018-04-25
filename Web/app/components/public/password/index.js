import React from "react";
import Styles from "./style.css";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import Proptypes from "prop-types";
import { handlePasswordRest } from "../../../redux/actions/user";
import url from "url";

class Password extends React.Component {
	constructor(props) {
		super(props);
		this.VerifyID = undefined;
		this.reset = this.reset.bind(this);
	}
	componentWillMount() {
		const queryData = new URLSearchParams(window.location.search);
		this.VerifyID = queryData.get("id");
	}
	reset(event) {
		event.preventDefault();
		if (event.target.npw.value === event.target.cnpw.value) {
			this.props.handlePasswordRest(this.VerifyID, event.target.cnpw.value);
		}
		else {
			confirm("Passwords do not match!");
		}
	}
	render() {
		if (!this.VerifyID) {
			return (
				<div className={Styles.pwreset}>
					<span>Verification ID Not Found!</span>
				</div>
			);
		}
		return (
			<div className={Styles.pwreset}>
				<form className={Styles.pwform} action="javascript:void(0);" onSubmit={this.reset}>
					<h1 className={Styles.pwheader}>Password Reset</h1>
					{this.props.error === "INVALID_ID" && <span className={Styles.invalid}>Invalid Verification ID</span> }
					<span>New Password</span>
					<input name="npw" type="password" required/>
					<span>Confirm New Password</span>
					<input name="cnpw" type="password" required/>
					<input id={Styles.resetbtn} type="submit" value="RESET MY PASSWORD"/>
				</form>
			</div>
		)
	}
}

export default connect(
	(state) => ({user: state.user, error: state.state.error}),
	(dispatch) => bindActionCreators({handlePasswordRest}, dispatch)
)(Password);