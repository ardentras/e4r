import React from 'react';
import Nav from './Nav';
import Home from './Home';
import Contact from './Contact';
import ErrorPath from './ErrorPath';
import Login from './Login';
import {
    BrowserRouter as Router,
    Route,
    NavLink,
    Switch
  } from 'react-router-dom';

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
                            <Route exact path='/login' component={Login}/>
                            <Route component={ErrorPath} />
                        </Switch>
                    </div>
                </div>
            </div>
        </Router>  
        );
    }
};