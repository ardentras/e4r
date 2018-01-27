import React from "react";
import { NavLink } from "react-router-dom";
import Styles from "./style.css";
import Routes from "../routes";

const Dashboard = props => (
    <div className={Styles.dashboard}>
        {Routes.map((elem, index)=>(
            <NavLink key={index} exact={elem.exact} to={elem.path} className={Styles.dlinks} activeClassName={Styles.dlinkactive}>{elem.label}</NavLink>
        ))}
        <span onClick={props.func.logout}>Logout</span>
    </div>
);

export default Dashboard;
