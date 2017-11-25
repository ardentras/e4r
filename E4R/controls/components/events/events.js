import React from 'react';
import ReactDOM from 'react-dom';


export function setNavClass(check = false) {
    let currentName = "nav-wrapper";
    let nextName = "nav-scroll";
    if (check === true) {
        currentName = "nav-scroll";
        nextName = "nav-wrapper"
    }
    const navs = document.getElementsByClassName(currentName)[0];
    if (navs !== undefined) {
        navs.className = nextName;
    }
};