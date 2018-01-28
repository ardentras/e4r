import React from "react";
import Styles from "./style.css";

class Question extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={Styles.question}>
                <div className={Styles.header}>
                    <h1>All Questions</h1>
                    <div className={Styles.progresscontainer}>
                        <span>Current Progress:</span>
                        <div className={Styles.progress}>
                            <div className={Styles.progressbar} role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100">
                                70%
                            </div>
                        </div>
                    </div>
                </div>
                <div className={Styles.questioncontents}>
                    <div className={Styles.subjects}>
                        <h2>Subjects</h2>
                        <span className={Styles.subject}><i className={["fa", "fa-times", Styles.remove].join(" ")} aria-hidden="true"/>Math</span>
                        <span className={Styles.subject}><i className={["fa", "fa-times", Styles.remove].join(" ")} aria-hidden="true"/>Science</span>
                        <span className={Styles.subject}><i className={["fa", "fa-times", Styles.remove].join(" ")} aria-hidden="true"/>History</span>
                        <span className={Styles.subject}><i className={["fa", "fa-times", Styles.remove].join(" ")} aria-hidden="true"/>Add...</span>
                    </div>
                    <div className={Styles.selection}>
                        <div className={Styles.topic}>
                            <h1>Question 1:</h1>
                            <p>Find the antiderivative of f(x)=4x.</p>
                            <span>(use C as constant arbitrary)</span>
                        </div>
                        <div className={Styles.choices}>
                            <span>Answers: </span>
                            <span className={Styles.choice}>A) 2x^2 + C</span>
                            <span className={Styles.choice}>B) 5x^2 + C</span>
                            <span className={Styles.choice}>C) x^3 + C</span>
                            <span className={Styles.choice}>D) None of the above</span>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Question;