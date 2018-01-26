import React from "react";
import { Route } from "react-router-dom";
import Routes from "./routes";
import Dashboard from "./dashboard";
import Styles from "./style.css";

class Private extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div>
                <div className={Styles.clearfix}></div>
                <div className={Styles.contents}>
                    <Dashboard/>
                    {Routes.map((elem, index)=>(
                        <Route key={index} exact={elem.exact} path={elem.path} component={elem.component}/>
                    ))}
                </div>
            </div>
        );
    }
}

export default Private;