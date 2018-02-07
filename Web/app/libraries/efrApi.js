/****************************************************************************
 * 
 *->Name: efrApi.js 
 *->Purpose: An object which contains api calls to interact with the e4r server.
 *
*****************************************************************************/

"use strict";
import "babel-polyfill";
import Axios from "axios";
import iAuth from "./iAuth";
import iCookie from "./iCookie";

const efrApi = (()=>{
    let _host = new WeakMap();
    let _port = new WeakMap();
    let _protocol = new WeakMap();
    let _gamePath = new WeakMap();
    let _renewPath = new WeakMap();
    let _loginPath = new WeakMap();
    let _signupPath = new WeakMap();
    let _logoutPath = new WeakMap();

	class efrApi {
		constructor() {
            _host.set(this, "localhost");
            _port.set(this, 8080);
            _protocol.set(this, "http");
            _gamePath.set(this, undefined);
            _renewPath.set(this, undefined);
            _loginPath.set(this, undefined);
            _signupPath.set(this, undefined);
            _logoutPath.set(this, undefined);
            iAuth.config({
                host: _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this),
                timeout: 10000
            });
		}
        config({host="localhost", port=8080, protocol="http", gamePath=undefined, 
                renewPath=undefined, loginPath=undefined, timeout=1000, headers=undefined,
                signupPath=undefined, logoutPath=undefined}) {
            _host.set(this, host);
            _port.set(this, port);
            _protocol.set(this, protocol);
            _gamePath.set(this, gamePath);
            _renewPath.set(this, renewPath);
            _loginPath.set(this, loginPath);
            _signupPath.set(this, signupPath);
            _logoutPath.set(this, logoutPath);
            iAuth.config({
                host: _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this),
                timeout: timeout,
                headers: headers
            });
		}
		createJSON({username=undefined, email=undefined, first_name=undefined, last_name=undefined, charity_name=undefined,
			subject_id=undefined, subject_name=undefined, difficulty=undefined, completed_blocks=undefined}) {
			return Promise.resolve(
                {
                userobject: {
                    user_data: {
                        username: username,
                        email: email,
                        first_name: first_name,
                        last_name: last_name,
                        charity_name: charity_name,
                        },
                    game_data: {
                        subject_id: subject_id,
                        subject_name: subject_name,
                        difficulty: difficulty,
                        completed_blocks: completed_blocks
                    }
                }
            });
        }
        login(user) {
            return iAuth.Authenticate(user, _loginPath.get(this));
        }
        signup(user) {
            return iAuth.Register(user, _signupPath.get(this));
        }
        logout() {
            return iAuth.Deauthenticate(iAuth.getUserFromCookie(), _logoutPath.get(this));
        }
        getQuestions(user) {
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _gamePath.get(this);
            return Axios.put(apiRoute, {user});
        }
        renewSession(user) {
            const session = iCookie.get("session");
            const objToSent = {user: {session: session, userobject: user}};
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _renewPath.get(this);
			return Axios.put(apiRoute, objToSent);
        }
	}
	return efrApi;
})();

export default new efrApi();
