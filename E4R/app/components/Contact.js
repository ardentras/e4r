import React from 'react';
import Footer from './Footer';

function ContactInfo(props) {
    return (
        <div className='available'>
            <h2>Available at</h2>
            <h4>Klamath Falls, OR</h4>
            <h4>+1 (123)456-7890</h4>
            <h4>administrator@e4r.us</h4>
        </div>
    );
};

function InquiriesForm(props) {
    return (
        <div id='form'>
            <input type="text" id='name' name='name' placeholder='NAME'/>
            <input type="text" id='email' name='email' placeholder='EMAIL'/>
            <textarea name="message" id="msg" cols="30" rows="10" placeholder='MESSAGE'></textarea>
            <button>SUBMIT</button>
        </div>
    );
}

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
            <section>
                <div id ="contact-wrapper">
                    <div className="contact-info">
                        <h1>CONTACT</h1>
                        <hr/>
                        <p>How may we help you</p>
                        <div className='inquries-form'>
                            <InquiriesForm/>
                            <ContactInfo/>
                        </div>
                    </div>
                </div>
            </section>
        );
    }
};