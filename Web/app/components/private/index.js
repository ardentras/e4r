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
import DashboardStyles from "./dashboard/style.css";

class Private extends React.Component {
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
    }
    componentWillMount() {
        if (!this.props.questions || this.props.questions.length <= 0) {
            this.props.getQuestions(this.props.user);
        }
    }
    componentDidMount() {
        window.addEventListener("resize", this.resize);
        if (window.innerWidth <= 740) {
            const dashboardheight = document.getElementsByClassName(DashboardStyles.dashboard)[0].clientHeight;
            document.getElementsByClassName(Styles.private)[0].style.paddingBottom = dashboardheight + "px";
        }
    }
    componentWillUnmount() {
        window.removeEventListener("resize",this.resize);
    }
    resize() {
        if (window.innerWidth <= 740) {
            const dashboardheight = document.getElementsByClassName(DashboardStyles.dashboard)[0].clientHeight;
            document.getElementsByClassName(Styles.private)[0].style.paddingBottom = dashboardheight + "px";
        }
        else {
            const dashboardheight = document.getElementsByClassName(DashboardStyles.dashboard)[0].clientHeight;
            document.getElementsByClassName(Styles.private)[0].style.paddingBottom = 0;
        }
    }
    hideModal() {
        const modal = document.getElementsByClassName(Styles.modal)[0];
        modal.style.transform = "translateY(-1000px)";
    }
    logout() {
        this.props.handlerDeAuth(this.props.user);
    }
    render() {
        return (
            <div className={Styles.private}>
                <div className={Styles.modal}>
                    <div className={Styles.levelup}>
                        <i className={["fa", "fa-trophy", Styles.trophy].join(" ")}/>
                        <span className={Styles.modalheader}>Congratulation</span>
                        <p className={Styles.modalcontent}>You made it to the next level!</p>
                        <button onClick={this.hideModal} className={Styles.modalbtn}>OK</button>
                    </div>
                </div>
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
