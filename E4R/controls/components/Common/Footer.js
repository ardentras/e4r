import React from 'react';

function Platforms(props) {
    return(
        <div>
            <h4>Platform</h4>
            <h5>Android</h5>
            <h5>Chrome</h5>
            <h5>Safari</h5>
            <h5>Mozilla</h5>
        </div>
    );
};

function CopyRight(props) {
    return (
        <div>
            <h4>EDUCATION FOR REVITALIZATION</h4>
            <h5>©2017</h5>
        </div>
    );
};

function Organization(props) {
    return (
        <div>
            <h4>Organization</h4>
            <h5>About Us</h5>
            <h5>Twitter</h5>
            <h5>Facebook</h5>
            <h5>Our Team</h5>
        </div>
    );
};

function Resources(props) {
    return(
        <div>
            <h4>Resources</h4>
            <h5>Help</h5>
            <h5>Contact Us</h5>
        </div>
    );
};

export default class Footer extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <footer>
                <div className="f-resources">
                    <CopyRight/>
                    <Platforms/>
                    <Organization/>
                    <Resources/>
                </div>
            </footer>
        )
    }
};