import React from "react";
import Styles from "./style.css";

class Settings extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={Styles.settings}>
                <div>This is Settings.</div>
            </div>
        );
    }
}

export default Settings;