import React from "react";
import { Route } from "react-router-dom";
import { bindActionCreators } from "redux";
import { 
    handlerDeAuth } from "../../redux/actions/auth";
import { getQuestions } from "../../redux/actions/user";
import { connect } from "react-redux";
import Routes from "./routes";
import Dashboard from "./dashboard";
import Styles from "./style.css";
import efrApi from "../../libraries/efrApi";
efrApi.config({
    host: "35.163.221.182", 
    port: 3002,
    protocol: "http", 
    gameRoute: "/api/q/request_block",
    _renewRoute: "/api/renew"
});

class Private extends React.Component {
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
    }
    componentWillMount() {
        if (this.props.questions.length <= 0) {
			this.props.getQuestions(this.props.user);
		}
    }
    logout() {
        this.props.handlerDeAuth();
    }
    render() {
        return (
            <div className={Styles.private}>
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
	(state) => ({user: state.user, states: state.state, questions: state.questions.questions}),
	(dispatch) => bindActionCreators({ handlerDeAuth, getQuestions }, dispatch)
)(Private);
