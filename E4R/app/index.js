import React from 'react';
import ReactDOM from 'react-dom';
import App from './components/App';
import Footer from './components/Footer';

import css from './css/style.css';

ReactDOM.render(
    <div>
    <App/>
    <Footer/>
</div>,
    document.getElementById('app')
)