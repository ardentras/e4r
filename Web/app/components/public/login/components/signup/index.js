import React from "react";
import Styles from "./style.css";


const Signup = props => (
    <form className={Styles.signup} action="javascript:void(0);" onSubmit={props.func.register}>
        <h1 className={Styles.headers}>Join Us</h1>
        {props.error === "REG_FAIL" && <span className={Styles.warning}>Username/email already exist</span> }
        {props.error === "REG_ERROR" && <span className={Styles.error}>Cannot connect to server</span> }
        <span>email:</span>
        <input type="email" className={Styles.inputs} name="email" placeholder="email" required/>
        <span>username:</span>
        <input className={Styles.inputs} type="text" name="username" placeholder="username" required/>
        <span>password:</span>
        <input className={Styles.inputs}  type="password" name="password" placeholder="password" required/>
        <input className={Styles.sbtn}  type="submit" value="Sign Up"/>
        <span onClick={props.func.signup} className={Styles.returnbtn}>already have an account?</span>
    </form>
);

export default Signup;