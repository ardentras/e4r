import React from "react";
import { Link } from 'react-router-dom'
import error from "../../../../../redux/errorCodes";
import Styles from "./style.css";

const Return = props =>(
    <form className={Styles.returning} action="javascript:void(0);" onSubmit={props.func.auth}>
        <h1 className={Styles.headers}>Returning</h1>
        {props.error === error.LOGIN_FAIL && <span className={Styles.warning}>Incorrect username/password</span> }
        {props.error === error.TIME_OUT && <span className={Styles.warning}>Timeout, please try again...</span> }
        {props.error === error.PERSIST_TIMEOUT && <span className={Styles.warning}>Persist Login Timeout, please login again...</span> }
        {props.error === error.CONN_FAIL && <span className={Styles.error}>Cannot connect to server</span> }
        {props.SIGN_SUC && <span className={Styles.auth}>Please verify your email...</span>}
        {props.AUTHING && <span className={Styles.auth}>Authenicating...</span> }
        <span>username:</span>
        <input className={Styles.inputs} type="text" name="username" placeholder="username/email" required/>
        <span>password:</span>
        <input className={Styles.inputs}  type="password" name="password" placeholder="password" required/>
        <Link to="/contacts">forgot password?</Link>
        <input className={Styles.sbtn}  type="submit" value="Log In"/>
        <span onClick={props.func.signup} className={Styles.signupbtn}>Sign Up</span>
    </form>
);

export default Return;
