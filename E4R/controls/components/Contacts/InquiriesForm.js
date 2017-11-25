import React from 'react';

function FormInputs(props) {
    return(
        <form action="mailto:admin@e4r.com" method="post" encType="text/plain">
            <input id="name" type="text" name="name" placeholder="NAME"/>
            <input id="email" type="text" name="email" placeholder="EMAIL"/>
            <textarea name="message" id="msg" cols="30" rows="10" placeholder="MESSAGE"></textarea>
            <button id="submit" type="submit">Submit</button>
        </form>
    );
};

export default class InquiriesForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showForm: false
        }; 
        this.clickSendInquiry = this.clickSendInquiry.bind(this);
    }
    shouldComponentUpdate(nextProps, nextState) {
        return (this.state.showForm !== nextState.showForm);
    }
    clickSendInquiry(state) {
        this.setState(()=>{
            return {
                showForm: state
            }
        });
    }
    render() {
        return(
            <div className="inquiries">
                <h3>General Inquiries &#38; Questions</h3>
                <span onClick={this.clickSendInquiry.bind(null,!this.state.showForm)}>{!this.state.showForm ? "Send Inquiry" : "Close Inquiry"}</span>
                <div id="email_form">{this.state.showForm && <FormInputs/>}</div>
            </div> 
        );
    }
};