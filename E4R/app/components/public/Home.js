import React from 'react';
import {NavLink} from 'react-router-dom';

import Footer from './Footer'

import img1 from "../../assets/latest/event3.jpg";
import img2 from "../../assets/latest/event2.jpg";
import img3 from "../../assets/latest/even1.jpg";
import img4 from "../../assets/latest/event4.jpg";
import img5 from "../../assets/latest/event5.jpg";

import c1 from '../../assets/charities/american-red-cross.png';
import c2 from '../../assets/charities/united-way.png';
import c3 from '../../assets/charities/taskforce.png';
import c4 from '../../assets/charities/feedingamerica.png';
import c5 from '../../assets/charities/salvationarmy.png';
import c6 from '../../assets/charities/directrelief.png';
import c7 from '../../assets/charities/wounded.png';
import c8 from '../../assets/charities/leukemia.png';

const Top = props => {
    return (
        <div className='content-top'>
        <h1 className='welcome'>BE A <span id='good'>BETTER</span> PERSON</h1>
        <h3 className='project-description'>A PROJECT DESIGNED TO EDUCATE AND CONTRIBUTE</h3>
        <NavLink id='join-btn' to='/login'>JOIN US</ NavLink>
        <a id='follow-btn' href='https://twitter.com' target='_blank'></a>
    </div>
    )
}

const DescriptionCells = props => {
    return (
        <div className='h-cells-container'>
            <div className='h-cells'>
            <h1><span className='h-d-icons icon-1'></span>Mission</h1>
                <p>We are a non-profit organization who
                    seeks to provide a platform for those
                    who seek education and donation.</p>
            </div>
            <div className='h-cells'>
                <h1><span className='h-d-icons icon-2'></span>Education</h1>
                <p>We offer a wide range of subjects for
                     users to solve and learn.</p>
            </div>
            <div className='h-cells'>
            <h1><span className='h-d-icons icon-3'></span>Donation</h1>
                <p>We will donated the revenue made 
                     from ads to a user selected reputable
                     charity.</p>
            </div>
        </div>
    )
}

const SupportingCharities = props => {
    return (
        <div className='h-support-charities-container'>
            <div className='h-support-charities-title'><h2>Helping millions of those in need.</h2></div>
            <div className='h-support-charities h-support-1'>
                <img src={c1} alt="" width='150px' height='50px'/>
                <img src={c2} alt="" width='150px' height='100px'/>
                <img src={c3} alt="" width='100px' height='60px'/>
                <img src={c4} alt="" width='140px' height='80px'/>  
            </div>
            <div className='h-support-charities h-support-2'>
                <img src={c5} alt="" width='60px' height='70px'/>
                <img src={c6} alt="" width='170px' height='50px'/>
                <img src={c7} alt="" width='100px' height='90px'/>
                <img src={c8} alt="" width='150px' height='60px'/>
            </div>
        </div>
    );
}

const LatestNews = props => {
    return (
        <div className='h-latest'>
            <h1>URGENT Events</h1>
            <div className='h-latest-container'>
                <div className='h-latest-modules'>
                    <div className='h-latest-module'>
                        <img src={img1} alt="event1" width='270px' height='184x' />
                        <p>United way is asking
                            everyone to donate
                            their unwanted foods
                            to the poor.</p>
                        <span className='h-learn-btn'>LEARN MORE</span>
                    </div>
                    <div className='h-latest-module'>
                        <img src={img2} alt="event2" width='270px' height='184x'/>
                        <p>American Red Cross
                             is inviting everyone
                             to attend their blood
                             giving fair.</p>
                        <span className='h-learn-btn'>LEARN MORE</span>
                    </div>
                    <div className='h-latest-module'>
                        <img src={img3} alt="event3" width='270px' height='184x'/>
                        <p>Direct Relief Disaster 
                             Fair to raise funds for
                             the recent hurricane
                             attack.</p>
                        <span className='h-learn-btn'>LEARN MORE</span>
                    </div>
                    <div className='h-latest-module'>
                        <img src={img4} alt="event4" width='270px' height='184x'/>
                        <p>This is Jacky Chan
                             because Jacky Chan.
                             because he kicks butt.
                             Don’t question this.</p>
                        <span className='h-learn-btn'>LEARN MORE</span>
                    </div>
                    <div className='h-latest-module'>
                        <img src={img5} alt="event5" width='270px' height='184x'/>
                        <p>Salving has no season.
                             Donate your money
                             now to people
                             who needs it.</p>
                        <span className='h-learn-btn'>LEARN MORE</span>
                    </div>
                    <div className='h-latest-module'>
                        <img src={img1} alt="event6" width='270px' height='184x'/>
                        <p>Direct Relief Disaster 
                             Fair to raise funds for
                             the recent hurricane
                             attack.</p>
                        <span className='h-learn-btn' >LEARN MORE</span>
                    </div>
                    <div className='h-latest-module'>
                        <img src={img1} alt="event7" width='270px' height='184x'/>
                        <p>Direct Relief Disaster 
                             Fair to raise funds for
                             the recent hurricane
                             attack.</p>
                        <span className='h-learn-btn'>LEARN MORE</span>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default class Home extends React.Component {
    constructor(props) {
        super(props);
        this.top = true;
        this.navAppear = this.navAppear.bind(this);
    }
    componentWillMount() {
        const header = document.getElementsByClassName('header')[0];
        const nav = document.getElementById('mobile-nav');
        if (header) {
            nav.style.background = 'white';
            header.style = 'background: transparent; color: white; padding-top: 20px;';
        }
        document.addEventListener('scroll', this.navAppear);
    }
    componentWillUnmount() {
        document.removeEventListener('scroll', this.navAppear);
    }
    navAppear() {
        if (document.body.scrollTop <= 30 && !this.top) {
            const header = document.getElementsByClassName('header')[0];
            const nav = document.getElementById('mobile-nav');
            if (header) {
                header.style = 'background: transparent; color: white; padding-top: 20px;';
                nav.style.background = 'white';
                this.top = true;
            }
        }
        else if (document.body.scrollTop >= 31 && this.top){
            const header = document.getElementsByClassName('header')[0];
            const nav = document.getElementById('mobile-nav');
            if (header) {
                header.style = 'background: white; color: #262E30; position: fixed; padding: 0;';
                nav.style.background = '#262E30';
                this.top = false;
            }
        }
    }
    render() {
        return(
            <section className='Home'>
                <div className='h-parallex'></div>
                <Top/>
                <div className='h-content'>
                    <DescriptionCells/>
                    <SupportingCharities/>
                    <LatestNews/>
                </div>
                <Footer/>
            </section>
        );
    }
}