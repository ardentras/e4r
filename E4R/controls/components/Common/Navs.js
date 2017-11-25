import React from 'react';
import {BrowserRouter as Router,
NavLink,
Route} from 'react-router-dom';

export default class Nav extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            visible: false
        };
    }
    update(state) {
        console.log(state);
        this.setState(()=>{
            return {
                visible: state
            }
        })
    }
    render() {
        return (
            <ul className="nav">
                <div onClick={this.update.bind(null, !this.state.visible)} className='mobile-nav'>
                    <span className='m-nav'>
                        <span id='good'>
                        </span>
                    </span>
                </div>
                <div style={this.state.visible === false ? {visibility: 'hidden'} : {visibility: 'visible', transform: 'translateX(200px)'}} id="nav-items-mobile">
                    <li>
                        <NavLink exact className="n-item n-but" activeClassName="active" to="/">
                            HOME
                        </NavLink>
                    </li>
                    <li>
                        <NavLink className="n-item n-but" activeClassName="active" to="/contacts">
                            CONTACT US
                        </NavLink>
                    </li>
                    <li>
                        <NavLink className="n-item n-but" activeClassName="active" to="/login">
                            LOG IN
                        </NavLink>
                    </li>
                </div>
                <li><h1 id="title">EDUCATION FOR REVITALIZATION</h1></li>
                <div id="nav-items">
                    <li>
                        <NavLink exact className="n-item n-but" activeClassName="active" to="/">
                            HOME
                        </NavLink>
                    </li>
                    <li>
                        <NavLink className="n-item n-but" activeClassName="active" to="/contacts">
                            CONTACT US
                        </NavLink>
                    </li>
                    <li>
                        <NavLink className="n-item n-but" activeClassName="active" to="/login">
                            LOG IN
                        </NavLink>
                    </li>
                </div>
            </ul>
        );
    }
}