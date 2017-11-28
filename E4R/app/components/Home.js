import React from 'react';
import {NavLink} from 'react-router-dom';

const Top = props => {
    return (
        <div className='content-top'>
        <h1 className='welcome'>BE A <span id='good'>GOOD</span> PERSON</h1>
        <h3 className='project-description'>A PROJECT DESIGNED TO EDUCATE AND CONTRIBUTE</h3>
        <NavLink id='join-btn' to='/login'>JOIN US</ NavLink>
        <a id='follow-btn' href='https://twitter.com' target='_blank'></a>
    </div>
    )
}

const DescriptionCells = props => {
    return (
        <div className='d-cells'>
        <div className='cells'>
            <h2>MISSION</h2>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam tempor, magna id interdum varius, libero est porta lorem, pellentesque egestas mi tortor sit amet ante.</p>
            <span className="cell-btn">LEARN MORE</span>
        </div>
        <div className='cells'>
            <h2>SOLVE</h2>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam tempor, magna id interdum varius, libero est porta lorem, pellentesque egestas mi tortor sit amet ante.</p>
            <span className="cell-btn">LEARN MORE</span>
        </div>
        <div className='cells'>
            <h2>LEARN</h2>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam tempor, magna id interdum varius, libero est porta lorem, pellentesque egestas mi tortor sit amet ante.</p>
            <span className="cell-btn">LEARN MORE</span>
        </div>
        <div className='cells'>
            <h2>DONATE</h2>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam tempor, magna id interdum varius, libero est porta lorem, pellentesque egestas mi tortor sit amet ante.</p>
            <span className="cell-btn">LEARN MORE</span>
        </div>
    </div>
    )
}

const LatestNews = props => {
    return (
        <div className='latest-news'>
        <h1 id="latest-title">Latest <span>News</span></h1>
        <div className='latest-contents'>
            <div className='latest-cells cell1'>
                <span className="latest-news-content"></span>
                <span className='latest-overlay'></span>
                <span className="latest-nc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nisi convallis, accumsan est non, tempor eros. Vivamus cursus velit eros.</span>
            </div>
            <div className='latest-cells cell2'>
                <span className="latest-news-content"></span>
                <span className='latest-overlay'></span>
                <span className="latest-nc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nisi convallis, accumsan est non, tempor eros. Vivamus cursus velit eros.</span>
            </div>
            <div className='latest-cells cell3'>
                <span className="latest-news-content"></span>
                <span className='latest-overlay'></span>
                <span className="latest-nc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nisi convallis, accumsan est non, tempor eros. Vivamus cursus velit eros.</span>
            </div>
            <div className='latest-cells cell4'>
                <span className="latest-news-content"></span>
                <span className='latest-overlay'></span>
                <span className="latest-nc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nisi convallis, accumsan est non, tempor eros. Vivamus cursus velit eros.</span>
            </div>
            <div className='latest-cells cell5'>
                <span className="latest-news-content"></span>
                <span className='latest-overlay'></span>
                <span className="latest-nc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nisi convallis, accumsan est non, tempor eros. Vivamus cursus velit eros.</span>
            </div>
            <div className='latest-cells cell6'>
                <span className="latest-news-content"></span>
                <span className='latest-overlay'></span>
                <span className="latest-nc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sit amet nisi convallis, accumsan est non, tempor eros. Vivamus cursus velit eros.</span>
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
                <div className='parallex'></div>
                <Top/>
                <div className='content'>
                    <DescriptionCells/>
                    <LatestNews/>
                </div>
            </section>
        );
    }
}