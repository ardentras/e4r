import React from "react";
import Styles from "./style.css";

export default class Contact extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <section className={Styles.contacts}>
                <div>
                    <h1 className={Styles.header}>CONTACT US</h1>
                    <h3 className={Styles.company}>Education for Revitalization</h3>
                    <p>phone: (123)456-7890</p>
                    <p>email: info@efr.com</p>
                    <p className={Styles.location}>Location:</p>
                    <p>Klamath Falls, OR</p>
                    <form className={Styles.email} action="javascript:void(0);" onSubmit={null}>
                        <h2>Let's Talk</h2>
                        <p>Fill in the following form to ask or find answers to your questions.</p>
                        <div className={Styles.infos}>
                            <input type="text" name="name" id={Styles.name} placeholder="What's your mom call you?"/>
                            <input type="email" name="email" placeholder="Where can we email you?"/>
                        </div>
                        <textarea name="content" cols="30" rows="10" placeholder="Spill the beans..."/>
                        <input className={Styles.sendbtn} type="submit" value="Send"/>
                    </form>
                </div>
            </section>
        );
    }
};