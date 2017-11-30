import React from 'react';
import axios from 'axios';
import {
    withRouter,
    Redirect
} from 'react-router-dom';
import iAuth from '../libraries/iAuth';



const JustVisiting = props => {
  const check = props.ifSignUp();
  return (
    <div className='visiting'>
      <h1>Just Visiting?</h1>
      <p>Start exploring our application as a visitor.</p>
      <button>Continue</button>
      <span>OR</span>
      <button onClick={props.onsignup}>{check ? 'Cancle' : 'Register'}</button>
      <a href="/contact">NEED HELP?</a>
    </div>
  );
}

class FormFields extends React.Component {
  constructor(props) {
    super(props);
    this.onchange = props.onchange;
    this.onsubmit = props.onsubmit;
    this.ifSignUp = props.ifSignUp;
  }
  render() {
    const check = this.ifSignUp();
    return (
      <div id='form-fields'>
        {iAuth.ifFailed() ? <span>Incorrect Username/Password</span> : null}
        {check && <span>Email:</span>}
        {check && <input type="email" name='email' placeholder='EMAIL' onChange={this.onchange}/>}
        <span>Username:</span>
        <input type="text" name='username' placeholder={check ? 'USERNAME' : 'USERNAME/EMAIL'} onChange={this.onchange}/>
        <span>Password:</span>
        <input className='pw' type="password" name="password" placeholder="PASSWORD" onChange={this.onchange}/>
        {!check && <a href="/contact">forgot password?</a>}
        <button onClick={this.onsubmit}>{check ? 'REGISTER' : 'LOG IN' }</button>
      </div>
    );
  }
}

const Returning = props => {
  const check = props.ifSignUp();
  return (
    <div className='returning'>
      <h1>{check? 'Register' : 'Returning'}</h1>
      <FormFields onchange={props.onchange} onsubmit={props.onsubmit} ifSignUp={props.ifSignUp}/>
    </div>
  );
}

const LoginForm = props => {
  return (
    <div className='l-form'>
      <JustVisiting onsignup={props.onsignup} ifSignUp={props.ifSignUp}/>
      <hr/>
      <Returning onchange={props.onchange} onsubmit={props.onsubmit} ifSignUp={props.ifSignUp}/>
    </div>
  );
}

export default class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            redirectToReferrer: false,
            failed: false,
            signup: false
        }
        this.user = {
          username: '',
          email: '',
          password: ''
        }
        this.login = this.login.bind(this);
        this.fillUser = this.fillUser.bind(this);
        this.signup = this.signup.bind(this);
        this.ifSignUp = this.ifSignUp.bind(this);
        this.enterKey = this.enterKey.bind(this);
      }
      componentWillMount() {
        document.addEventListener('keypress', this.enterKey);
      }
      componentWillUnmount() {
        document.removeEventListener('keypress', this.enterKey);
      }
    componentDidMount() {
        const header = document.getElementsByClassName('header')[0];
        const nav = document.getElementById('mobile-nav');
        if (header) {
            nav.style.background = '#262E30';
            header.style = 'background: white; color: #262E30; position: fixed; padding: 0;';
        }
    }
    enterKey(e) {
      let key = e.which || e.keyCode;
      if(key === 13) {
        this.login();
      }
    } 
    fillUser(e) {
      this.user[e.target.name] = e.target.value;
    }
  login() {
    if (!this.state.signup) {
      iAuth.authenticate(this.user, this);
    }
    else {
      iAuth.signup(this.user, this);
    }
    }
    signup() {
      this.setState(()=>{
        return {
          redirectToReferrer: this.state.redirectToReferrer,
          failed: this.state.failed,
          signup: !this.state.signup
        }
      });
      iAuth.reset();
    }
    ifSignUp() {
      if (this.state.signup) {
        return true;
      }
      else {
        return false;
      }
    }
    render() {
      const { from } = this.props.location.state || { from: { pathname: '/protected' } }
      const { redirectToReferrer } = this.state
      if (redirectToReferrer) {
        return (
          <Redirect to={from}/>
         )
      }
        return (
            <section id='login'>
                  <LoginForm onchange={this.fillUser} onsubmit={this.login} onsignup={this.signup} ifSignUp={this.ifSignUp}/>
              </section>
          )
      }
  }