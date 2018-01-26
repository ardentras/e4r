import React from "react";
import Styles from "./style.css";

class Leaderboard extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={Styles.leaderboard}>
                <div>This is Leaderboard.</div>
            </div>
        );
    }
}

export default Leaderboard;