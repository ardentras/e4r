import React from "react";
import { Route } from "react-router-dom";
import { bindActionCreators } from "redux";
import { 
    handlerDeAuth } from "../../redux/actions/auth";
import { connect } from "react-redux";
import Routes from "./routes";
import Dashboard from "./dashboard";
import Styles from "./style.css";

class Private extends React.Component {
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
    }
    logout() {
        this.props.handlerDeAuth();
    }
    render() {
        return (
            <div>
                <div className={Styles.clearfix}></div>
                <div className={Styles.contents}>
                    <Dashboard func={{logout: this.logout}}/>
                    {Routes.map((elem, index)=>(
                        <Route key={index} exact={elem.exact} path={elem.path} component={elem.component}/>
                    ))}
                </div>
            </div>
        );
    }
}

export default connect(
	(state) => ({user: state.user, states: state.state}),
	(dispatch) => bindActionCreators({ handlerDeAuth }, dispatch)
)(Private);
