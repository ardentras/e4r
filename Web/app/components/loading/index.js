import React from "react";
import {connect} from "react-redux";
import Styles from "./style.css";

const Spinner = props => (
    <div className={Styles.spinnercontainer}>
        <div className={Styles.spinner}/>
        <div className={Styles.text}><span>{props.states.AUTHING ? "Authenticating..." : props.states.SIGNINGUP ? "Registering..." : props.states.DEAUTHING ? "Signing Out..." : props.states.PERSIST ? "Persisting Login.." : props.states.SIGNINGUP ? "Signing up..." : "Loading..."}</span></div>
    </div>
);

export default connect(
	(state) => ({states: state.state})
)(Spinner);