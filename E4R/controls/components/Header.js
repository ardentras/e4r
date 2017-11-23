import React from "react";

export default class Header extends React.Component{
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <nav id="navagations">
                <h1 className="initials">E.4.R</h1>
                <h6 className="motto">Be a Good Person</h6>
                <h6 className="motto" id="sun">☀</h6>
                <ul className="navs">
                    <li>
                        <a href="/">Home</a>
                    </li>
                    <li>
                        <a href="#">Contact Us</a>
                    </li>
                    <li>
                        <a href="#">Log In</a>
                    </li>
                </ul>
            </nav>
        )
    }
};