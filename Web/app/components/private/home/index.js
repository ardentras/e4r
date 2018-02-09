import React from "react";
import Styles from "./style.css";
import { connect } from "react-redux";
import ProgressBar from "progressbar.js";
import Proptypes from "prop-types";
import iCookie from "../../../libraries/iCookie";

class Home extends React.Component {
    constructor(props) {
        super(props);
    }
    componentDidMount() {
        const percentage = document.getElementById(Styles.percentage);
        const index = parseFloat(this.props.index)/10;
        let bar = new ProgressBar.Path("#heart-path", {
            easing: "easeInOut",
            duration: 800
        });
        bar.animate(index);
        percentage.innerHTML = (index*100) + "%";
    }
    render() {
        return (
            <div className={Styles.home}>
                <div className={Styles.recentactivity}>
                    <div className={Styles.userdata}>
                        <div className={Styles.uidcontainer}>
                            <span className={Styles.welcome}>Welcome!</span>
                            <span className={Styles.uid}>{this.props.user.user_data.username}</span>
                        </div>
                        <div id={Styles.heart}>
                            <svg xmlns="http://www.w3.org/2000/svg" version="1.1" x="0px" y="0px" viewBox="0 0 100 100">
                                <path fillOpacity="0" strokeWidth="1" stroke="#bbb" d="M81.495,13.923c-11.368-5.261-26.234-0.311-31.489,11.032C44.74,13.612,29.879,8.657,18.511,13.923  C6.402,19.539,0.613,33.883,10.175,50.804c6.792,12.04,18.826,21.111,39.831,37.379c20.993-16.268,33.033-25.344,39.819-37.379  C99.387,33.883,93.598,19.539,81.495,13.923z"/>
                                <path id="heart-path" fillOpacity="0" strokeWidth="3" stroke="#ED6A5A" d="M81.495,13.923c-11.368-5.261-26.234-0.311-31.489,11.032C44.74,13.612,29.879,8.657,18.511,13.923  C6.402,19.539,0.613,33.883,10.175,50.804c6.792,12.04,18.826,21.111,39.831,37.379c20.993-16.268,33.033-25.344,39.819-37.379  C99.387,33.883,93.598,19.539,81.495,13.923z"/>
                                <text id={Styles.percentage} x="40%" y="50%" position="absolute" fontFamily="inherit" fontSize="inherit"/>
                            </svg>
                        </div>
                    </div>
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