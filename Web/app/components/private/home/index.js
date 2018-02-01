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
                    <div className={Styles.progresstonext}>
                        <span className={Styles.progressheading} >Progress to next block:</span>
                        <Line className={Styles.progressbar} percent={percent} strokeWidth="4" strokeColor="#88FF95" trailWidth="4"/>
                    </div>
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
                    <div>
                        <div className={Styles.rowheaders}>
                            <div>
                                <h1 className={Styles.heading}>Feeds</h1>
                                <div className={Styles.feeds}>
                                    <span className={Styles.feed}>KevjXu just donated $.25</span>
                                    <span className={Styles.feed}>User12356 just donated $.25</span>
                                    <span className={Styles.feed}>Jacob just donated $.25</span>
                                    <span className={Styles.feed}>Kelcey just donated $.25</span>
                                    <span className={Styles.feed}>Jimmy just donated $.25</span>
                                </div>
                            </div>
                            <div>
                                <h1 className={Styles.heading}>Discussions</h1>
                                <div className={Styles.discussions}>
                                    <span className={Styles.msg}>KevjXu: Hey guys!</span>
                                    <span className={Styles.msg}>User12356: Who am i?</span>
                                    <span className={Styles.msg}>Jacob: I just donated $.25!</span>
                                    <span className={Styles.msg}>Kelcey: Unicorns and cats</span>
                                    <span className={Styles.msg}>Jimmy: I made brownies.</span>
                                    <input className={Styles.msginput} type="text"/>
                                </div>
                            </div>
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