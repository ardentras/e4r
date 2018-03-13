import React from "react";
import Styles from "./style.css";
import Spinner from "../../loading";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { handlerDeAuth } from "../../../redux/actions/auth";
import { handleCompleteReset } from "../../../redux/actions/questions";
import { handleNames, resetPWRequest } from "../../../redux/actions/user";

class Settings extends React.Component {
    constructor(props) {
        super(props);
        this.restart = this.restart.bind(this);
        this.reset = this.reset.bind(this);
        this.save = this.save.bind(this);
        this.resetPassword = this.resetPassword.bind(this);
    }
    restart() {
        if (confirm("Are you sure?")) {
            this.props.handleCompleteReset(this.props.user);
        }
    }
    reset() {
        const fname = document.getElementById(Styles.fname);
        const lname = document.getElementById(Styles.lname);
        const charity = document.getElementById(Styles.charity);
        if(fname && lname) {
            fname.value = this.props.user.user_data.first_name;
            lname.value = this.props.user.user_data.last_name;
            charity.innerHTML = this.props.user.user_data.charity_name ? this.props.user.user_data.charity_name : "None";
        }
    }
    resetPassword() {
        if (confirm("Are you sure?")) {
            this.props.resetPWRequest({username: this.props.user.user_data.username, email: this.props.user.user_data.email});
            this.props.handlerDeAuth(this.props.user);
        }
    }
    save() {
        const fname = document.getElementById(Styles.fname).value;
        const lname = document.getElementById(Styles.lname).value;
        if (fname && lname) {
            if (fname !== this.props.user.user_data.first_name || lname !== this.props.user.user_data.last_name) {
                this.props.handleNames(fname, lname, this.props.user);
            }
        }
    }
    render() {
        console.log(this.props.user);
        return (
            <div className={Styles.settings}>
                <div className={Styles.initials}>K.X</div>
                <div className={Styles.level}>Level 1</div>
                <div className={[Styles.fnamefield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>FIRST NAME</span>
                    <input id={Styles.fname} className={Styles.fieldinput} type="text" defaultValue={this.props.user.user_data.first_name}/>
                </div>
                <div className={[Styles.lnamefield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>LAST NAME</span>
                    <input id={Styles.lname} className={Styles.fieldinput} type="text" defaultValue={this.props.user.user_data.last_name}/>
                </div>
                <div className={[Styles.emailfieled, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>EMAIL</span>
                    <span className={Styles.fieldinput}>{this.props.user.user_data.email}</span>
                </div>
                <div className={[Styles.charityfield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>CHARITY</span>
                    <span id={Styles.charity} className={Styles.fieldinput}>{this.props.user.user_data.selected_charity !== "" ? this.props.user.user_data.selected_charity : "None" }</span>
                </div>
                <div className={[Styles.tokenfield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>SESSION</span>
                    <span id={Styles.token} className={Styles.fieldinput}>awdawdawdawdawd-adwdawdawd-awdawdawd23</span>
                </div>
                <div className={[Styles.pwfield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>PASSWORD</span>
                    <span onClick={this.resetPassword} className={Styles.pwreset}>RESET</span>
                </div>
                <div className={[Styles.infofield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>CHANGES</span>
                    <span onClick={this.reset} className={Styles.resetbtn}>DEFAULT</span>
                </div>
                <div className={[Styles.gamefield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>GAME</span>
                    <span onClick={this.restart} className={Styles.restartbtn}>RESTART</span>
                </div>
                <span onClick={this.save} className={Styles.savebtn}>SAVE</span>
            </div>
        );
    }
}

export default connect(
	(state) => ({user: state.user}),
	(dispatch) => bindActionCreators({ handleCompleteReset, handleNames, resetPWRequest, handlerDeAuth }, dispatch)
)(Settings);
