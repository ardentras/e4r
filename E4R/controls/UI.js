import React from "react";
import ReactDom from "react-dom";

import Home from "./components/Home/Home";
import Navs from "./components/Common/Navs";
import errorPath from "./components/Common/errorPath";
import Contact from "./components/Contacts/Contact";
import Login from "./components/Login/Login";

import {
    BrowserRouter as Router,
    Route,
    Switch
  } from 'react-router-dom';

import "../css/style.css";
import "../css/error.css";
import "../css/mobile.css";

function UI(props) {
    return (
        <Router>
            <div>
                <div id="wrapper">
                    <div className="nav-wrapper">
                        <Navs/>
                    </div>
                </div>
                <Switch>
                    <Route exact path='/' component={Home}/>
                    <Route path='/contacts' component={Contact}/>
                    <Route path='/login' component={Login}/>
                    <Route component={errorPath}/>
                </Switch>
            </div>
        </Router>  
    )
};

ReactDom.render (
    <UI/>,
    document.getElementById("app")
)