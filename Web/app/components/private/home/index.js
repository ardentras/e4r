import React from "react";
import Styles from "./style.css";
import Proptypes from "prop-types";

class Home extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={Styles.dashboard}>
                <div className={Styles.dmain}>
                    This is Home.
                </div>
            </div>
        );
    }
}

export default Home;