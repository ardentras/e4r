Object.defineProperty(exports, "__esModule", {
    value: true
});

import axios from 'axios'; 

// const user = this.user;

const iAuth = (()=>{
    let isAuthorized = new WeakMap();
    let isFailed = new WeakMap();
    let callbacks = new WeakMap();
    let protocol = new WeakMap();
    let authenticateRoute = new WeakMap();
    let deauthenticateRoute = new WeakMap();;
    let apiPort = new WeakMap();
    class iAuth {
        constructor() {
            callbacks.set(this, null);
            protocol.set(this, 'http');
            authenticateRoute.set(this, 'localhost');
            deauthenticateRoute.set(this, 'localhost');
            apiPort.set(this, '8080');
            isAuthorized.set(this, false);
            isFailed.set(this, false);
        }
        config({useCallback=null, use='http', authRoute='', deAuthRoute='', port=8080}) {
            callbacks.set(this, useCallback);
            protocol.set(this, use);
            authenticateRoute.set(this, authRoute);
            deauthenticateRoute.set(this, deAuthRoute);
            apiPort.set(this, port.toString());
        }
        defaultAuthenticateCallBack() {
            console.log('Log In');
        }
        defaultDeAuthenticateCallBack() {
            console.log('Log Out');
        }
        authenticate(user, e) {
            let self = this;
            axios.post("https://34.208.210.218:3003/api/login", {user})
            .then(function (response) {
                if (response.data.response === "Success") {
                    isAuthorized.set(self, true);
                    e.setState(()=>{
                        return {
                            redirectToReferrer: true,
                            failed: false
                        }
                    });
                }
                else {
                    isFailed.set(self, true);
                    e.setState(()=>{
                        return {
                            redirectToReferrer: false,
                            failed: true
                        }
                    });
                }
            })
            .catch(function (error) {
                console.log(error);
            });
        }
        deauthenticate(callbacks) {
            isAuthorized.set(this, false);
            setTimeout(callbacks);
            setTimeout(()=>{
                window.location.reload();
            }, 1);
        }
        signup(user, e) {
            let self = this;
            axios.post("https://34.208.210.218:3003/api/signup", {user})
            .then(function (response) {
                if (response.data.response === "Succeed") {
                    isAuthorized.set(self, false);
                    e.setState(()=>{
                        return {
                            redirectToReferrer: false,
                            failed: false,
                            signup: false
                        }
                    });
                }
                else {
                    e.setState(()=>{
                        return {
                            redirectToReferrer: false,
                            failed: true,
                            signup: false
                        }
                    });
                }
            })
            .catch(function (error) {
                console.log(error);
            });
        }
        reset() {
            isFailed.set(this, false);
            isAuthorized.set(this, false);
        }
        ifFailed() {
            return (isFailed.get(this));
        }
        ifAuthorized() {
            return (isAuthorized.get(this));
        }
    }
    return iAuth;
})();

exports.default = new iAuth();