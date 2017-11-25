import React from 'react';
import ReactDOM from 'react-dom';
import {BrowserRouter as Router,
    NavLink,
    Route} from 'react-router-dom';

import {setNavClass} from '../events/events';

import LatestNews from './LatestNews';
import DescriptinCells from './DescriptionCells';
import Footer from '../Common/Footer';

function Background(props) {
    return(
        <div>
            <img id="background" src='../../assets/temp.jpg' alt='Background' width="100%" height="800px"></img>
            <a href='https://twitter.com' target="_blank">
                <img id="follow-but" src='../../assets/Follow.png' alt='Follow'></img>
            </a>
        </div>
    );
}

function Heading(props) {
    return (
        <div className="content-top">
            <Background/>
            <div id="descriptions">
                <h1>BE A GOOD PERSON</h1>
                <h2>A PROJECT DEISNGED TO RAISE FUNDS FOR CHARITIES</h2>
                <JoinButton/>
            </div>
        </div>
    );
};

function JoinButton(props) {
    return (
        <NavLink className="join-us" activeClassName="active" to="/login">
        JOIN US
    </NavLink>
    );
};

export default class Home extends React.Component {
    constructor(props) {
        super(props);
        this.atTop = true;
        this.latestViewed = false;
        this.scrollPosition = 0;
        this.transformNavagation = this.transformNavagation.bind(this);
    }
    componentWillMount() {
        document.addEventListener('scroll', this.transformNavagation, false);
    }
    componentWillUnmount() {
        this.latestViewed = false;
        document.removeEventListener('scroll', this.transformNavagation, false);
    }
    componentDidMount() {
        setNavClass(true);
    }
    transformNavagation() {
        this.scrollPosition = document.body.scrollTop;
        if (this.scrollPosition <= 37 && !this.atTop) {
            let navs = document.getElementsByClassName("nav-scroll")[0];
            if (navs !== undefined) {
                navs.className = "nav-wrapper";
            }
            this.atTop = true;
        }
        else if (this.scrollPosition > 40 && this.atTop){
            let navs = document.getElementsByClassName("nav-wrapper")[0];
            if (navs !== undefined) {
                navs.className = "nav-scroll";
            }
            this.atTop = false;
        }
        if (this.scrollPosition >= 450 && !this.latestViewed) {
            let latest = document.getElementsByClassName("news");
            if(latest !== undefined) {
                Array.from(latest).map((elem, index)=>{
                    setTimeout(()=>{
                        elem.className = "news-view";
                    }, 500 * index);
                });
                this.latestViewed = true;
            }
        }
    }
    render() {
        return (
            <section>
                <Heading/>
                <div className="content-center">
                    <DescriptinCells/>
                    <LatestNews/>
                </div>
                <Footer/>
            </section>
        );
    }
}