import React from "react";
import { NavLink } from "react-router-dom";
import Styles from "./style.css";
import Routes from "../routes";

class Dashboard extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div className={Styles.dashboard}>
                {Routes.map((elem, index)=>(
                    <NavLink key={index} exact={elem.exact} to={elem.path} className={Styles.dlinks} activeClassName={Styles.dlinkactive}>{elem.label}</NavLink>
                ))}
            </div>
        );
    }
}

export default Dashboard;