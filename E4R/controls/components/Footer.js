import React from 'react';

export default class Footer extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div id="footer">
                <div id="t-heads">
                    <span id="title">Education For Revitalization</span>
                    <span className="bod">©&nbsp;2017</span>
                </div>
            </div>
        )
    }
};