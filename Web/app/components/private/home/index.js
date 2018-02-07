import React from "react";
import Styles from "./style.css";
import { connect } from "react-redux";
import { Line } from "rc-progress";
import Proptypes from "prop-types";

class Home extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const percent = (parseFloat(((this.props.index) / this.props.questions.length)) * 100).toString();
        return (
            <div className={Styles.home}>
                <div className={Styles.recentactivity}>
                    <h1 className={Styles.heading}>User Status</h1>
                    <div className={Styles.activities}>
                        <div className={Styles.activity}>
                            <span className={Styles.records}>Current Level</span>
                            <div className={Styles.recordcontent}><i className={["fa", "fa-star", Styles.star].join(" ")}/><span className={Styles.number}>{parseInt(this.props.user.game_data.difficulty) + 1}</span></div>
                        </div>
                        <div className={Styles.activity}>
                            <span className={Styles.records}>Questions Solved</span>
                            <div className={Styles.recordcontent}><i className={["fa", "fa-trophy", Styles.trophy].join(" ")}/><span className={Styles.number}>1</span></div>
                        </div>
                        <div className={Styles.activity}>
                            <span className={Styles.records}>Total Donations</span>
                            <div className={Styles.recordcontent}><i className={["fa", "fa-heart", Styles.heart].join(" ")}/><span className={Styles.number}>$1.00</span></div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
export default connect(
	(state) => ({user: state.user, questions: state.questions.questions, index: state.questions.index})
)(Home);