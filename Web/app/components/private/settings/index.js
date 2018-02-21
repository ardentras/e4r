import React from "react";
import Styles from "./style.css";
import Spinner from "../../loading";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { handleCompleteReset } from "../../../redux/actions/questions";
import { handleNames } from "../../../redux/actions/user";

class Settings extends React.Component {
    constructor(props) {
        super(props);
        this.restart = this.restart.bind(this);
        this.reset = this.reset.bind(this);
        this.save = this.save.bind(this);
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
        return (
            <div className={Styles.settings}>
                <div className={Styles.userinfos}>
                    <div className={[Styles.infos, Styles.labels].join(" ")}>
                        <div className={Styles.info}>
                            <span>First name: </span>
                            <input id={Styles.fname} type="text" name="fname" defaultValue={this.props.user.user_data.first_name}/>
                        </div>
                        <div className={Styles.info}>
                            <span>Last name: </span>
                            <input id={Styles.lname} type="text" name="lname" defaultValue={this.props.user.user_data.last_name}/>
                        </div>
                        <div className={Styles.info}>
                            <span>Charity:</span>
                            <span id={Styles.charity}>{this.props.user.user_data.charity_name ? this.props.user.user_data.charity_name : "None"}</span>
                        </div>
                    </div>
                    <div className={Styles.btns}>
                        <div className={Styles.infobtns}>
                            <button className={Styles.savebtn} onClick={this.save}>Save</button>
                            <button className={Styles.resetbtn} onClick={this.reset}>Reset</button>
                        </div>
                        <button className={Styles.restartbtn} onClick={this.restart}>Restart</button>
                    </div>
                </div>
            </div>
        );
    }
}

export default connect(
	(state) => ({user: state.user, questions: state.questions.questions}),
	(dispatch) => bindActionCreators({ handleCompleteReset, handleNames }, dispatch)
)(Settings);
