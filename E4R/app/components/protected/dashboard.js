import React from 'react';
import {
    withRouter
} from 'react-router-dom';
import iAuth from '../libraries/iAuth';
import avatar from '../../assets/avatar/avatar.jpg';

const LogoutButton = withRouter(({ history }) => (
    iAuth.ifAuthorized() ? ( <button className='d-selections d-logout' onClick={() => {
          iAuth.deauthenticate(() => history.push('/login'))
        }}>LOG OUT</button>
    ) : (
        history.push('/login')
    )
  ))

export default class Dashboard extends React.Component {
    constructor(props) {
        super(props);
    }
    componentWillUnmount() {
        const header = document.getElementsByClassName('header')[0];
        if (header) {
            header.style = 'display: flex';
        }
    }
    componentDidMount() {
        const header = document.getElementsByClassName('header')[0];
        if (header) {
            header.style = 'display: none';
        }
    }
    render() {
        return(
            <section id='dashboard'>
                <div className='d-menu'>
                    <img id='avatar' src={avatar} alt='Avatar'></img>
                    <hr/>
                    <div className='statistics'>
                        <span>Donated: $5.25</span>
                        <span>Solved: 6</span>
                    </div>
                    <hr/>
                    <div className='d-menu-selections'>
                        <a href="#" className='d-selections'>Profile</a>
                        <a href="#" className='d-selections'>Charities</a>
                        <a href="#" className='d-selections'>Rankings</a>
                        <a href="#" className='d-selections'>Friends</a>
                        <a href="#" className='d-selections'>Settings</a>
                        <LogoutButton/>
                    </div>
                </div>
                <div className='d-content'>
                    <div className='d-header'>
                        <h1>Welcome Back!</h1>
                    </div>    
                </div>
            </section>
        )
    }
};