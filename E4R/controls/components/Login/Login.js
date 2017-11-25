import React from 'react';
import axios from 'axios';

import {setNavClass} from '../events/events';
import Footer from '../Common/Footer';

export default class Login extends React.Component {
    componentDidMount() {
        setNavClass();
    }
    render() {
        return(
            <section>
                content
                <Footer/>
            </section>
        );
    }
}

// export default class Login extends React.Component {
//     constructor(props) {
//         super(props);
//         this.state = {
//             signup: false,
//             signupstatus: "",
//             login: ""
//         };
//         this.signupform = {
//             name: "",
//             email: "",
//             password: "",
//             status: "waiting",
//         }
//         this.loginform = {
//             email: "",
//             password: ""
//         }
//         this.SignUp = this.SignUp.bind(this);
//         this.failed = false;
//         this.LogIn = this.LogIn.bind(this);
//         this.onClick = this.onClick.bind(this);
//         this.onChange = this.onChange.bind(this);
//         this.updateLogIn = this.updateLogIn.bind(this);
//         this.createButtonClick = this.createButtonClick.bind(this);
//         this.unhandleError = this.unhandleError.bind(this);
//     }
//     componentDidMount() {
//         setNavClass(0);
//     }
//     componentWillMount() {
//         window.addEventListener('unhandledrejection', this.unhandleError);
//     }
//     unhandleError(event) {
//         this.failed = true;
//     }
//     onChange(data) {
//         this.signupform[data.target.name] = data.target.value;
//     }
//     updateLogIn(check) {
//         this.setState(()=>{
//             return {
//                 signup: false,
//                 login: check
//             }
//         });
//     }
//     SignUp() {
//         let self = this;
//         const user = this.signupform;
//         axios.post("http://localhost:2000/api/signup", {user})
//             .then(function (response) {
//                 if (response.data.response === "Succeed") {
//                     self.state.signupstatus = response.data.response;
//                 }
//                 else {
//                     self.state.signupstatus = "Failed";
//                 }
//             })
//             .catch(function (error) {
//             });
//     }
//     LogIn() {
//         const user = this.loginform;
//         let self = this;
//         axios.post("http://localhost:2000/api/login", {user})
//         .then(function (response) {
//             if (response.data.response === true) {
//                 self.updateLogIn("successful");
//             }
//             else {
//                 self.updateLogIn("failed");
//             }
//         })
//         .catch(function (error) {
//             alert("UNEXPECTED ERROR OCCURRED");
//         });
//     }
//     onClick(data) {
//         if (data.username !== "" && data.password !== "")
//         {
//             if (this.state.signup === false) {
//                 this.LogIn();
//             }
//             else {
//                 this.SignUp();
//                 this.state.signup = false;
//             }
//         };
//     }
//     createButtonClick(data) {
//         this.setState(()=>{
//             return {
//                 signup: data
//             }
//         });
//     }
//     render() {
//         return (
//             <div>
//                 <section id="login">
//                     <div id="login-container">
//                         <div className="login-form">
//                             {this.failed === true && <h1>Error Happened!</h1>}
//                             {this.state.login === "failed" && this.state.signup === false && <h1>Email/Password Incorrect!</h1>}
//                             {this.state.login === "successful" && this.state.signup === false && <h1>Log In Successful</h1>}
//                             {this.state.signupstatus === "Succeed" && this.state.signup === true ? <h1>Register Succesful</h1> :
//                             this.state.signupstatus === "Failed" && this.state.signup === true > <h1>Email Already Used</h1>}
//                             {this.state.signup === true ? <h1 id="l-title">Sign Up</h1> : <h1 id="l-title">WELCOME</h1>}
//                             {this.state.signup === true ? <input className="user-in name" type="text" name="name" placeholder="NAME" onChange={this.onChange.bind(null)}/> :
//                             null}
//                             <input className="user-in email" type="text" name="email" placeholder="EMAIL" onChange={this.onChange.bind(null)}/>
//                             <input className="user-in pw" type="password" name="password" placeholder="PASSWORD" onChange={this.onChange.bind(null)}/>
//                             {this.state.signup === false ? <span id="submit-btn" onClick={this.onClick.bind(null,this.state)}>LOG IN</span> :
//                             <span id="submit-btn" onClick={this.onClick.bind(null,this.state)}>Register</span>}
//                             <span id="forgot">Forgot Password?</span>
//                             {this.state.signup === false ? <span id="create" onClick={this.createButtonClick.bind(null,true)}>Create an Account</span>
//                             : <span id="create" onClick={this.createButtonClick.bind(null,false)}>Already Register?</span>}
//                         </div>
//                     </div>
//                 </section>
//                 <Footer/>
//             </div>
           
//         );   
//     }
// };