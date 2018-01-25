import React from "react";
import Styles from "./style.css";

class Question extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={Styles.question}>
                <div>This is Questions.</div>
            </div>
        );
    }
}

export default Question;