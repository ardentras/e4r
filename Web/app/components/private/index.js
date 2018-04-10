

import React from "react";
import { Route, Redirect } from "react-router-dom";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { handleDeAuthentication } from "../../redux/actions/user"; 
import { getQuestions } from "../../redux/actions/questions";
import Routes from "./routes";
import Dashboard from "./dashboard";
import Styles from "./style.css";
import FooterStyle from "./footer/style.css";
import NewDash from "./newDash";
import ChatBox from "./chatBox";
import Footer from "./footer";
import SpinnerStyle from "../loading/style.css";

class Private extends React.Component {
    constructor(props) {
        super(props);
        this.logout = this.logout.bind(this);
    }
    componentDidMount() {
        if (!this.props.questions || this.props.questions.length <= 0) {
            this.props.getQuestions(this.props.user.token, this.props.user.userobject);
        }
        this.resize();
    }
    resize() {
        if (window.innerWidth <= 740) {
            const Footer = document.getElementsByClassName(FooterStyle.footer)[0].clientHeight;
            document.getElementsByClassName(Styles.private)[0].style.paddingBottom = Footer + "px";
        }
        else {
            const Footer = document.getElementsByClassName(FooterStyle.footer)[0].clientHeight;
            document.getElementsByClassName(Styles.private)[0].style.paddingBottom = 0;
        }
    }
    logout() {
        this.props.handleDeAuthentication(this.props.user.token, this.props.user.userobject);
    }
    hideModal() {
        const modal = document.getElementsByClassName(Styles.modal)[0];
        modal.style.transform = "translateY(-1000px)";
    }
    render() {
        const { redirectToRefer } = this.props.states;
        if (!redirectToRefer) {
            <Redirect to="/login"/>
        }
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
                <div className={[Styles.contents, (this.props.states.THEME === "Light" ? null : Styles.darkcontents)].join(" ")}>
                    <NewDash func={{handleLogOut: this.logout}}/>
                    {Routes.map((elem, index)=>(
                        <Route key={index} exact={elem.exact} path={elem.path} component={elem.component}/>
                    ))}
                    <ChatBox/>
                </div>
                <Footer/>
            </div>
        );
    }
}

export default connect(
	(state) => ({states: state.state, user: state.user, questions: state.questions.questions}),
	(dispatch) => bindActionCreators({ handleDeAuthentication,getQuestions }, dispatch)
)(Private);
