import React from "react";
import { NavLink } from "react-router-dom";
import Styles from "./style.css";
import Routes from "../routes";

const Dashboard = props => (
    <div className={Styles.dashboard}>
        <div className={Styles.selectors}>
            {Routes.map((elem, index)=>(
                <NavLink key={index} exact={elem.exact} to={elem.path} className={Styles.dlinks} activeClassName={Styles.dlinkactive}>{elem.icon}{elem.label}</NavLink>
            ))}
            <span className={Styles.logout} onClick={props.func.logout}><i className={["fa", "fa-sign-out", Styles.icons].join(" ")} aria-hidden="true"/>Logout</span>
        </div>
    </div>
);

export default Dashboard;
