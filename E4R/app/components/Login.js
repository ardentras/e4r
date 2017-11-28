import React from 'react';
import axios from 'axios';

const Style= {
    color: 'white',
    background: 'rgb(240, 143, 143)',
    display: 'block',
    width: '250px',
    margin: '10px auto'
}

const StyleGood = {
    color: 'white',
    background: 'rgb(62, 255, 111)',
    display: 'block',
    width: '250px',
    margin: '10px auto'
}

export default class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            signup: false,
            errorCode: 0
        };
        this.user = {
            username: '',
            email: '',
            password: ''
        }
        this.update = this.update.bind(this);
        this.updateUser = this.updateUser.bind(this);
        this.postUser = this.postUser.bind(this);
        this.updateError = this.updateError.bind(this);
    }
    update(data) {
        const self = this;
        this.setState(()=>{
            return {
                signup: data,
                errorCode: 0
            }
        });
    }
    updateError(code) {
        const self = this;
        this.setState(()=>{
            return {
                signup: self.state.signup,
                errorCode: code
            }
        })
    }
    updateUser(data) {
        if (data.target.type === 'email') {
            this.user.email = data.target.value;
        }
        else if (data.target.type === 'password'){
            this.user.password = data.target.value;
        } 
        else {
            this.user.username = data.target.value;
        }
    }
    postUser() {
        const self = this;
        if (this.user.username !== '' && this.user.password !== '' && this.user.username.length > 5 && this.user.password.length >= 8) {
            if (this.state.signup === true) {
                if (this.user.email.length >= 3) {
                    const user = this.user;
                    axios.post("https://34.208.210.218:3003/api/signup", {user})
                            .then(function (response) {
                                if (response.data.response === "Succeed") {
                                    self.updateError(200);
                                }
                                else {
                                    self.updateError(205);
                                }
                            })
                            .catch(function (error) {
                                self.updateError(404);
                            });
                }else {
                    this.updateError(302)
                }
            } else {
                const user = this.user;
                axios.post("https://34.208.210.218:3003/api/login", {user})
                            .then(function (response) {
                                if (response.data.response === "Success") {
                                    self.updateError(200);
                                }
                                else {
                                    self.updateError(205);
                                }
                            })
                            .catch(function (error) {
                                self.updateError(404);
                            });
            }
        }
        else {
            if (this.user.username.length <= 0 && this.user.password.length <= 0) {
                this.updateError(400);
            }
            else if (this.user.username.length < 5) {
                this.updateError(301);
            }
            else {
                this.updateError(300);
            }
        }
    }
    componentDidMount() {
        const header = document.getElementsByClassName('header')[0];
        const nav = document.getElementById('mobile-nav');
        if (header) {
            nav.style.background = '#439899';
            header.style = 'background: white; color: #262E30; position: fixed; padding: 0;';
        }
    }
    render() {
        return (
            <section id='login'>
                <div id='l-form'>
                    {this.state.signup === false ? <h1>LOG IN</h1> : <h1>SIGN UP</h1>}
                    {this.state.errorCode === 300 ? <h3 style={Style}>Password Must be atleast 8 characters</h3> :
                    this.state.errorCode === 301 ? <h3 style={Style}>Username Must be Valid length</h3> :
                    this.state.errorCode === 302 ? <h3 style={Style}>Email Must be Valid length</h3> :
                    this.state.errorCode === 400 ? <h3 style={Style}>Email/Password Must not be empty!</h3> : 
                    this.state.errorCode === 200 ? <h3 style={StyleGood}>{this.state.signup === true ? 'SIGN UP SUCCESSFUL' : 'LOG IN SUCCESSFUL'}</h3> : 
                    this.state.errorCode === 205 ? <h3 style={Style}> {this.state.signup === false ? 'Username/Password Incorrect' : 'USER ALREADY EXIST'}</h3> :
                    this.state.errorCode === 404 ? <h3 style={Style}>ERROR OCCURRED</h3>: null}
                    {this.state.signup === true && <input type="email" id='log-email' name='email' placeholder='EMAIL' onChange={this.updateUser.bind(null)}/> }
                    <input type="username" id='log-user' name='username' placeholder='USERNAME' onChange={this.updateUser.bind(null)}/>
                    <input type='password' id='log-pw' name='password' placeholder='PASSWORD' onChange={this.updateUser.bind(null)}/>
                    <button id='l-btn' onClick={this.postUser.bind(null)}>{!this.state.signup === false ? 'SIGN UP' : 'LOG IN'}</button>
                    <button id='s-btn' onClick={this.update.bind(null, !this.state.signup)}>{!this.state.signup === false ? 'CANCLE' : 'SIGN UP'}</button>
                </div>
            </section>
        );
    }
};