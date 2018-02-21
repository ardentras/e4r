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
                        <div className={Styles.info}>
                            <span>First name: </span>
                            <input type="text" name="fname" defaultValue={this.props.user.user_data.first_name}/>
                        </div>
                        <div className={Styles.info}>
                            <span>Last name: </span>
                            <input type="text" name="lname" defaultValue={this.props.user.user_data.last_name}/>
                        </div>
                        <div className={Styles.info}>
                            <span>Charity:</span>
                            <span>{this.props.user.user_data.charity_name ? this.props.user.user_data.charity_name : "None"}</span>
                        </div>
                    </div>
                    <div className={Styles.btns}>
                        <div className={Styles.infobtns}>
                            <button className={Styles.savebtn}>Save</button>
                            <button className={Styles.resetbtn}>Reset</button>
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
	(dispatch) => bindActionCreators({ handleCompleteReset }, dispatch)
)(Settings);
