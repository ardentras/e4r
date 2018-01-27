import React from "react";
import Styles from "./style.css";

export default class Contact extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <section id={Styles.contacts}>
                <div className={Styles.clearfix}/>
                <div className={Styles.ccontents}>
                    <div className={Styles.cmain}>
                        <div className={Styles.info}>
                            <div className={[Styles.ways, Styles.topway].join(" ")}>
                                <h1 className={[Styles.phone, Styles.wayheader].join(" ")}>Phone</h1>
                                <span className={Styles.wayinfo}>(123)456-789</span>
                            </div>
                            <div className={[Styles.ways, Styles.mid].join(" ")}>
                                <h1 className={[Styles.email, Styles.wayheader].join(" ")}>Email</h1>
                                <span className={Styles.wayinfo}>e4r@admin.com</span>
                            </div>
                            <div className={[Styles.ways, Styles.bottomway].join(" ")}>
                                <h1 className={[Styles.location, Styles.wayheader].join(" ")}>Location</h1>
                                <span className={Styles.wayinfo}>Klamath Falls, OR</span>
                            </div>
                        </div>
                        <form className={Styles.form} action="javascript:void(0);" onSubmit={null}>
                            <h1>Send us an email!</h1>
                            <input type="text" name="name" placeholder="name"/>
                            <input type="email" name="email" placeholder="email"/>
                            <textarea name="emailcontent" cols="30" rows="10" placeholder="type your message..."/>
                            <input type="submit" value="Submit"/>
                        </form>
                    </div>
                </div>
            </section>
        );
    }
};