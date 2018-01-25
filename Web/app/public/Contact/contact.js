import React from "react";
import Styles from "./style.css";

function ContactInfo(props) {
    return (
        <div className={Styles.available}>
            <div className={Styles.contactinfo}>
                <h4><a href="">Klamath Falls, OR</a></h4>
            </div>
            <div className={Styles.contactinfo}>
                <h4><a href="">E4R Wiki</a></h4>
            </div >
            <div className={Styles.contactinfo}>
                <h4><a href="">Education for Revitalization</a></h4>
            </div>
            <div className={Styles.contactinfo}>
                <h4><a href="">admin@e4r.com</a></h4>
            </div>
        </div>
    );
};

function InquiriesForm(props) {
    return (
        <div id={Styles.form}>
            <input type="text" className={[Styles.cinput, Styles.cname].join(" ")} name='name' placeholder='NAME'/>
            <input type="email" className={[Styles.cinput, Styles.cemail].join(" ")} name='email' placeholder='EMAIL'/>
            <textarea name="message" className={Styles.cmsg} id={Styles.msg} cols="30" rows="10" placeholder='MESSAGE'></textarea>
            <button id={Styles.submitbtn}>SUBMIT</button>
        </div>
    );
};

function Header(props) {
    return (
        <div>
            <h1>CONTACT US</h1>
            <hr className={Styles.chcep}/>
            <h3>Here at Educatoin for Revitalization, We love to help.</h3>
            <h3 id={Styles.clasth}>Contact us with the following ways and we will respond as soon as possible.</h3>
            <InquiriesForm/>
        </div>
    );
};

export default class Contact extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <section id={Styles.Contacts}>
                <div className={Styles.clearfix}></div>
                <Header/>
                <ContactInfo/>
            </section>
        );
    }
};