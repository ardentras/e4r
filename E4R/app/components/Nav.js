import React from 'react';
import ReactRouter from 'react-router-dom';
import {
    BrowserRouter as Router,
    Route,
    NavLink
  } from 'react-router-dom';

export default class Nav extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            mobile: false
        };
        this.updateMobile = this.updateMobile.bind(this);
    }
    updateMobile(state) {
        this.setState(()=>{
            return {
                mobile: state
            }
        })
    }
    render() {
        return (
              <div>
                  <ul className='header'>
                      <span onClick={this.updateMobile.bind(null, !this.state.mobile)} id='mobile-nav'></span>
                      <li id="title">Education For Revitalization</li>
                      <ul style={this.state.mobile === true ? {transform: 'translateX(0)'} : null} className='navagations'>
                          <li><NavLink exact className="n-item n-but" activeClassName="active" to="/">HOME</NavLink></li>
                          <li><NavLink exact className="n-item n-but" activeClassName="active" to="/contact">CONTACT US</NavLink></li>   
                          <li><NavLink exact className="n-item n-but" activeClassName="active" to="/login">LOG IN</NavLink></li>   
                      </ul> 
                  </ul>
              </div>
        );
    }
};
