import React from 'react';
import Footer from './Footer';
import img1 from '../../assets/contacts/github.png';
import img2 from '../../assets/contacts/email.png';
import img3 from "../../assets/contacts/facebook.png";
import img4 from "../../assets/contacts/location.png";

function ContactInfo(props) {
    return (
        <div className='available'>
            <div className='contactinfo'>
                <img src={img4} alt="Location" width='20px' height='20px'/>
                <h4><a href="">Klamath Falls, OR</a></h4>
            </div>
            <hr className='c-sep'/>
            <div className='contactinfo'>
                <img src={img1} alt="Location" width='20px' height='20px'/>
                <h4><a href="">E4R Wiki</a></h4>
            </div >
            <hr className='c-sep'/>
            <div className='contactinfo'>
                <img src={img3} alt="Location" width='20px' height='20px'/>
                <h4><a href="">Education for Revitalization</a></h4>
            </div>
            <hr className='c-sep'/>
            <div className='contactinfo'>
                <img src={img2} alt="Location" width='20px' height='20px'/>
                <h4><a href="">admin@e4r.com</a></h4>
            </div>
        </div>
    );
};

function InquiriesForm(props) {
    return (
        <div id='form'>
            <input type="text" className='c-input c-name' name='name' placeholder='NAME'/>
            <input type="email" className='c-input c-email' name='email' placeholder='EMAIL'/>
            <textarea name="message" className='c-msg' id="msg" cols="30" rows="10" placeholder='MESSAGE'></textarea>
            <button id='submit-btn'>SUBMIT</button>
        </div>
    );
};

function Header(props) {
    return (
        <div>
            <h1>CONTACT US</h1>
            <hr className='c-hcep'/>
            <h3>Here at Educatoin for Revitalization, We love to help.</h3>
            <h3 id='c-lasth'>Contact us with the following ways and we will respond as soon as possible.</h3>
            <InquiriesForm/>
        </div>
    );
};

export default class Contact extends React.Component {
    constructor(props) {
        super(props);
    }
    componentDidMount() {
        const header = document.getElementsByClassName('header')[0];
        const nav = document.getElementById('mobile-nav');
        if (header) {
            nav.style.background = '#262E30';
            header.style = 'background: white; color: #262E30; position: fixed; padding: 0;';
        }
    }
    render() {
        return (
            <section id='Contacts'>
                <Header/>
                <ContactInfo/>
            </section>
        );
    }
};