import React from "react";
import Styles from "./style.css";
import Spinner from "../../loading";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { handleSaveButton, handleGameRestart, handlePasswordResetButton } from "../../../redux/actions/user";

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
            this.props.handleGameRestart(this.props.user.userobject);
        }
    }
    reset() {
        const fname = document.getElementById(Styles.fname);
        const lname = document.getElementById(Styles.lname);
        const charity = document.getElementById(Styles.charity);
        if(fname && lname && charity) {
            fname.value = this.props.user.userobject.user_data.first_name;
            lname.value = this.props.user.userobject.user_data.last_name;
            charity.innerHTML = this.props.user.userobject.user_data.charity_name ? this.props.user.userobject.user_data.charity_name : "None";
        }
    }
    resetPassword() {
        if (confirm("Are you sure?")) {
           this.props.handlePasswordResetButton(this.props.user.userobject.user_data.username, this.props.user.userobject.user_data.email);
        }
    }
    save() {
        const fname = document.getElementById(Styles.fname).value;
        const lname = document.getElementById(Styles.lname).value;
        if (fname && lname) {
            if (fname !== this.props.user.userobject.user_data.first_name || lname !== this.props.user.userobject.user_data.last_name) {
                this.props.handleSaveButton(fname, lname, this.props.user.userobject);
            }
        }
    }
    render() {
        let initials = this.props.user.userobject.user_data.username;
        if (this.props.user.userobject.user_data.first_name && this.props.user.userobject.user_data.first_name !== "" && this.props.user.userobject.user_data.last_name && this.props.user.userobject.user_data.last_name !== "") {
            initials = this.props.user.userobject.user_data.first_name[0] + "." + this.props.user.userobject.user_data.last_name[0];
        }
        return (
            <div className={Styles.settings}>
                <div className={Styles.initials}>{initials}</div>
                <div className={Styles.level}>{"Level " + (parseInt(this.props.user.userobject.game_data.difficulty) + 1)}</div>
                <div className={[Styles.fnamefield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>FIRST NAME</span>
                    <input id={Styles.fname} className={Styles.fieldinput} type="text" defaultValue={this.props.user.userobject.user_data.first_name}/>
                </div>
                <div className={[Styles.lnamefield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>LAST NAME</span>
                    <input id={Styles.lname} className={Styles.fieldinput} type="text" defaultValue={this.props.user.userobject.user_data.last_name}/>
                </div>
                <div className={[Styles.emailfieled, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>EMAIL</span>
                    <span className={Styles.fieldinput}>{this.props.user.userobject.user_data.email}</span>
                </div>
                <div className={[Styles.charityfield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>CHARITY</span>
                    <span id={Styles.charity} className={Styles.fieldinput}>{this.props.user.userobject.user_data.selected_charity !== "" ? this.props.user.userobject.user_data.selected_charity : "None" }</span>
                </div>
                <div className={[Styles.tokenfield, Styles.fields].join(" ")}>
                    <span className={Styles.fieldhead}>SESSION</span>
                    <span id={Styles.token} className={Styles.fieldinput}>{this.props.user.token}</span>
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
	(state) => ({user: state.user, states: state.state}),
	(dispatch) => bindActionCreators({ handleSaveButton, handleGameRestart, handlePasswordResetButton }, dispatch)
)(Settings);
