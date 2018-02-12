import React from "react";
import Styles from "./style.css";

const Spinner = props => (
    <div className={Styles.spinnercontainer}>
        <div className={Styles.spinner}/>
    </div>
);

export default Spinner;