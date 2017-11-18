import React from 'react';
import Footer from "./Footer";

export default class Content extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <div id="contents">
                <div id="img-con">
                    <img id="testimg" src='../../assets/temp.jpg' alt='Test'></img>
                    <span id="lives" className="floating">Change Lives</span>
                    <span id="solving" className="floating">By Solving</span>
                </div>
                <div id="recent">
                    <span id="rec">RECENT</span>
                    <span id="eve">Events</span>
                    <div id ="rec-contents">
                        <div id="rec-kevin">
                            <h3 id="hur-kevin" className="rec-donate">Donate to Hurrican Kevin</h3>
                            <p>Everyone has the right to live on.  Due to the ongoing Hurricane,
                            people are suffering from his whining.</p>
                            <span className="join"><a href='#'>Join Us</a></span>
                            <span className="learn"><a href='#'>Learn More</a></span>
                        </div>
                        <div id="rec-sushi">
                            <h3 id="tsu-sushi" className="rec-donate">Donate to Tsunami Sushi</h3>
                            <p>Food is the essential element of life, and it should not be
                            anyone or anything’s right to take that away.</p>
                            <span className="join"><a href='#'>Join Us</a></span>
                            <span className="learn"><a href='#'>Learn More</a></span>
                        </div>
                    </div>
                    <div>
                        <canvas id="grid" width="1447px" height="800px"></canvas>
                        <Footer/>
                    </div>
                </div>
            </div>
        )
    }
};
