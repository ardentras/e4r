import React from "react";
import { Link } from 'react-router-dom'
import Styles from "./style.css";

const Return = props =>(
    <form className={Styles.returning} action="javascript:void(0);" onSubmit={props.func.auth}>
        <h1 className={Styles.headers}>Returning</h1>
        {props.error === "AUTH_FAIL" && <span className={Styles.warning}>Incorrect username/password</span> }
        {props.error === "AUTH_ERROR" && <span className={Styles.error}>Cannot connect to server</span> }
        {props.SIGN_SUC && <span className={Styles.auth}>Thank you for joining us!</span>}
        {props.AUTHING && <span className={Styles.auth}>Authenicating...</span> }
        <span>username:</span>
        <input className={Styles.inputs} type="text" name="uid" placeholder="username/email" required/>
        <span>password:</span>
        <input className={Styles.inputs}  type="password" name="pw" placeholder="password" required/>
        <Link to="/contacts">forgot password?</Link>
        <input className={Styles.sbtn}  type="submit" value="Log In"/>
    </form>
);

export default Return;