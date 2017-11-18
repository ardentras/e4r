import React from "react";
import ReactDom from "react-dom";
import Header from "./components/Header";
import Content from './components/Content';

import "../css/style.css";
import "./components/Draw";


function UI(props) {
    return (
        <div id="wrapper">
            <Header/>
            <div className="filler"></div>
            <Content/>
        </div>
    )
};

ReactDom.render (
    <UI/>,
    document.getElementById("app")
)