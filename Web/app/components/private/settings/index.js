import React from "react";
import Styles from "./style.css";
import Spinner from "../../loading";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { handleCompleteReset } from "../../../redux/actions/questions";

class Settings extends React.Component {
    constructor(props) {
        super(props);
        this.restart = this.restart.bind(this);
    }
    restart() {
        if (confirm("Are you sure?")) {
            this.props.handleCompleteReset(this.props.user);
        }
    }
    render() {
        return (
            <div className={Styles.settings}>
                <div className={Styles.userinfos}>
                    <div className={[Styles.infos, Styles.labels].join(" ")}>
                        <span>First Name:</span>
                        <span>Last Name: </span>
                        <span>Charity:</span>
                        <span>Token:</span>
                    </div>
                    <div className={[Styles.infos, Styles.inputs].join(" ")}>
                        <input type="text" name="fname" defaultValue={this.props.user.user_data.first_name}/>
                        <input type="text" name="lname" defaultValue={this.props.user.user_data.last_name}/>
                        <span>{this.props.user.user_data.charity_name ? this.props.user.user_data.charity_name : "None"}</span>
                        <span>{this.props.user.token}</span>
                    </div>
                    <button className={Styles.restartbtn} onClick={this.restart}>Restart</button>
                </div>
                <div className={Styles.infobtns}>
                    <button className={Styles.savebtn}>Save</button>
                    <button className={Styles.resetbtn}>Reset</button>
                </div>
            </div>
        );
    }
}

export default connect(
	(state) => ({user: state.user, questions: state.questions.questions}),
	(dispatch) => bindActionCreators({ handleCompleteReset }, dispatch)
)(Settings);
