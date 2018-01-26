import React from "react";
import Styles from "./style.css";
import { connect } from "react-redux";
import Proptypes from "prop-types";
import iAuth from "../../libraries/iAuth";
import { bindActionCreators } from "redux";
import { 
	handlerUserDeAuth } from "../../redux/actions/auth/auth";

class Home extends React.Component {
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
    }
    logout() {
        console.log("hey");
        const user = iAuth.getUserFromCookie();
        console.log(user);
        this.props.handlerUserDeAuth(user);
    }
    render() {
        return (
            <div className={Styles.dashboard}>
                <div className={Styles.dmain}>
                    This is Home.
                </div>
            </div>
        );
    }
}

export default connect(
	(state) => ({user: state.user, states: state.state}),
	(dispatch) => bindActionCreators({
		handlerUserDeAuth }, dispatch)
)(Home);