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
    let _updatePath = new WeakMap();
    let _resetPWPath = new WeakMap();
    let _verifyPW = new WeakMap();
    let _questionHelp = new WeakMap();
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
            _updatePath.set(this, undefined);
            _resetPWPath.set(this, undefined);
            _verifyPW.set(this, undefined);
            _questionHelp.set(this, undefined);
            iAuth.config({
                host: _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this),
                timeout: 5000
            });
            Axios.defaults.timeout = 5000;
        }
        config({host="localhost", port=8080, protocol="http", gamePath=undefined, 
                renewPath=undefined, loginPath=undefined, timeout=5000, headers=undefined,
                signupPath=undefined, logoutPath=undefined, updatePath=undefined, resetPWPath=undefined, verifyPW=undefined, questionHelp=undefined}) {
            _host.set(this, host);
            _port.set(this, port);
            _protocol.set(this, protocol);
            _gamePath.set(this, gamePath);
            _renewPath.set(this, renewPath);
            _loginPath.set(this, loginPath);
            _signupPath.set(this, signupPath);
            _logoutPath.set(this, logoutPath);
            _updatePath.set(this, updatePath);
            _resetPWPath.set(this, resetPWPath);
            _verifyPW.set(this, verifyPW);
            _questionHelp.set(this, questionHelp);
            iAuth.config({
                host: _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this),
                timeout: timeout,
                headers: headers
            });
        }
        validateUserData(user_data) {
            let check = false;
            if (typeof user_data === "object") {
                if (user_data.username && typeof user_data.username === "string" &&
                    user_data.email && typeof user_data.email === "string" &&
                    user_data.first_name && typeof user_data.first_name === "string" &&
                    user_data.last_name && typeof user_data.last_name === "string" &&
                    user_data.selected_charity && typeof user_data.selected_charity === "string" &&
                    user_data.favorite_charities && Array.isArray(user_data.favorite_charities)) {
                        check = true;
                    }
            }
            return check;
        }
        validateGameData(game_data) {
            let check = false;
            if (typeof game_data === "object") {
                if (game_data.subject_name  !== undefined&& typeof game_data.subject_name === "string" &&
                    game_data.subject_id !== undefined && typeof game_data.subject_id === "number" &&
                    game_data.difficulty !== undefined && typeof game_data.difficulty === "number" &&
                    game_data.totalQuestions !== undefined && typeof game_data.totalQuestions === "number" &&
                    game_data.totalDonated !== undefined && typeof game_data.totalDonated === "number" &&
                    game_data.blocksRemaining !== undefined && typeof game_data.blocksRemaining === "number" &&
                    game_data.completed_blocks !== undefined && Array.isArray(game_data.completed_blocks)) {
                        check = true;
                    }
            }
            return check;
        }
        ValidateObject(user) {
            let check = false;
            if (user) {
                if (user.user_data && this.validateUserData(user.user_data) &&
                    user.game_data && this.validateGameData(user.game_data) &&
                    user.timestamp && typeof user.timestamp === "string") {
                        check = true;
                    }
            }
            return true;
        }
        login(user) {
            return iAuth.Authenticate(user, _loginPath.get(this));
        }
        signup(user) {
            return iAuth.Register(user, _signupPath.get(this));
        }
        logout(user) {
            return iAuth.Deauthenticate({user}, _logoutPath.get(this));
        }
        getQuestions(user) {
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _gamePath.get(this);
            return Axios.put(apiRoute, {user});
        }
        updateUser(user) {
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _updatePath.get(this);
            return Axios.put(apiRoute, {user});
        }
        resetPWRequest(user) {
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _resetPWPath.get(this);
            return Axios.post(apiRoute, {user});
        }
        resetPW(user) {
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _verifyPW.get(this);
            return Axios.put(apiRoute, {user});
        }
        getHelp(qid) {
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _questionHelp.get(this);
            return Axios.put(apiRoute, {question_id: qid});
        }
        renewSession(token) {
            const objToSent = {user: {session: token}};
            const apiRoute = _protocol.get(this) + "://" + _host.get(this) + ":" + _port.get(this) + _renewPath.get(this);
			return Axios.put(apiRoute, objToSent);
        }
	}
	return efrApi;
})();

export default new efrApi();
