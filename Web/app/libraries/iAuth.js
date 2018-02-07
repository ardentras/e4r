/****************************************************************************
 * 
 *->Name: iAuth.js 
 *->Purpose: An object which contains api calls to interact with the e4r server.
 *
*****************************************************************************/

"use strict";
import "babel-polyfill";
import axios from "axios";
import iCookie from "./iCookie";
import { promisify } from "util";

/**
 * @param { boolean } http - Define if to use http request
 * @param { function } onError - Define the function to be call when error
 * @param { function } authSuccess - Define the function to be call when succeeded
 * @param { function } authFail - Define the function to be call when failed
 * @param { string } host - Define the host to send request to
 * @param { string } universalPath - Define the universal api path
 * @param { string } api - The api extension path
 * @param { Object } user - The user object to be saved
 **/

const iAuth = (()=>{
	let _request = new WeakMap();
	class iAuth {
		constructor() {
			_request.set(this, axios);
		}
		config({host, timeout, headers=undefined}) {
			_request.set(this, axios.create({
				baseURL: host,
				timeout: timeout
			}));
			headers ? _request.get(this).defaults.headers = headers : null;
		}
		getUserFromCookie() {
			return {
				session: iCookie.get("session"),
				userobject: {}
			};
		}
		Authenticate(user, path) {
			return _request.get(this).post(path, {user});
		}
		Deauthenticate(user, path) {
			return _request.get(this).put(path, {user});
		}
		Register(user, path) {
			return _request.get(this).post(path, {user});
		}
	}
	return iAuth;
})();

export default new iAuth();
