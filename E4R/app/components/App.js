import React, { Component } from 'react';
import Nav from './public/Nav';
import Home from './public/Home';
import Contact from './public/Contact';
import ErrorPath from './public/ErrorPath';
import Login from './public/Login';
import iAuth from './libraries/iAuth';
import Dashboard from './protected/dashboard'

import {
    BrowserRouter as Router,
    Route,
    NavLink,
    Switch,
    Redirect
  } from 'react-router-dom';
import { setInterval } from 'timers';

  const PrivateRoute = ({ component: Component, ...rest }) => (
    <Route {...rest} render={props => (
        iAuth.ifAuthorized() ? (
        <Component {...props}/>
      ) : (
        <Redirect to={{
          pathname: '/login',
          state: { from: props.location }
        }}/>
      )
    )}/>
  )

export default class App extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <Router>
            <div>
                <div id="wrapper">
                    <div className="nav-wrapper">
                        <Nav/>
                        <Switch>
                            <Route exact path='/' component={Home}/>
                            <Route exact path='/contact' component={Contact}/>
                            <Route exact path={iAuth.ifAuthorized() === false ? '/login' : '/protected'} component={Login}/>
                            <PrivateRoute path='/protected' component={Dashboard}/>
                            <Route component={ErrorPath} />
                        </Switch>
                    </div>
                </div>
            </div>
        </Router>  
        );
    }
};