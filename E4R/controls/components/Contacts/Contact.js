import React from 'react';
import InquiriesForm from './InquiriesForm';
import {setNavClass} from '../events/events';
import Footer from '../Common/Footer';

function ContactInfo(props) {
    return (
        <div>
            <h2>Contact Us</h2>
            <h4>Klamath Falls, OR</h4>
            <h4>+1 (123)456-7890</h4>
            <h4>administrator@e4r.us</h4>
        </div>
    );
};

export default class Contact extends React.Component {
    constructor(props) {
        super(props);
    }
    componentDidMount() {
        setNavClass();
    }
    render() {
        return (
            <section>
                <div id ="contact-wrapper">
                    <div className="contact-info">
                        <ContactInfo/>
                        <InquiriesForm/>
                    </div>
                </div>
                <Footer/>
            </section>
        );
    }
};