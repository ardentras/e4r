/****************************************************************************
 * 
 *->Name: iAuth.js 
 *->Purpose: An object which contains api calls to interact with the e4r server.
 *
*****************************************************************************/

"use strict";

import axios from "axios";
import iCookie from "./iCookie";
import "babel-polyfill";
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
	let m_onError = new WeakMap();
	let m_host = new WeakMap();
	let m_universalPath = new WeakMap();
	let m_http = new WeakMap();
	let m_login_path = new WeakMap();
	let m_logout_path = new WeakMap();
	let m_register_path = new WeakMap();
	class iAuth {
		constructor() {
			m_onError.set(this, undefined);
			m_host.set(this, undefined);
			m_universalPath.set(this, undefined);
			m_http.set(this, undefined);
			m_login_path.set(this, undefined);
			m_logout_path.set(this, undefined);
			m_register_path.set(this, undefined);
			this.ifPersist = this.ifPersist.bind(this);
		}
		config({ http=true, onError=undefined, // eslint-disable-line no-unused-vars
			host=undefined, universalPath=undefined,
			loginPath=undefined, logoutPath=undefined, registerPath=undefined}) {
			http ? m_http.set(this, http) : null;
			host ? m_host.set(this, host) : null;
			onError ? m_onError.set(this, onError) : null;
			universalPath ? m_universalPath.set(this, universalPath) : null;
			loginPath ? m_login_path.set(this, loginPath) : null;
			logoutPath ? m_logout_path.set(this, logoutPath) : null;
			registerPath ? m_register_path.set(this, registerPath) : null;
		}
		createJSON({username=undefined, email=undefined, first_name=undefined, last_name=undefined, charity_name=undefined,
			subject_id=undefined, subject_name=undefined, difficulty=undefined, completed_blocks=undefined}) {
			return Promise.resolve({
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
			});
		}
		getUserFromCookie() {
			return Promise.resolve({
				username: iCookie.get("username"),
				session: iCookie.get("session"),
				userobject: {}
			});
		}
		async ifPersist() {
			let result = false;
			if (document.cookie) {
				const session = iCookie.get("session");
				if (session) {
					result = await axios.put(m_host.get(this) + (m_universalPath.get(this) ? m_universalPath.get(this) : "") + "/renew", {user: {session: session}});
				}
			}
			return Promise.resolve(result);
		}
		async Authenticate(user, onSuccess, api=undefined) {
			let result = null;
			let error = null;
			try {
				result = await axios.post(m_host.get(this) + (m_universalPath.get(this) ? m_universalPath.get(this) : "") + (api ? api : m_login_path.get(this) ? m_login_path.get(this) : null), {user});
			}
			catch(err) {
				error = false;
			}
			return Promise.resolve(error ? error : result);
		}
		async Deauthenticate(user, onSuccess=undefined, api=undefined) {
			let result = null;
			let error = null;
			try {
				result = await axios.put(m_host.get(this) + (m_universalPath.get(this) ? m_universalPath.get(this) : "") + (api ? api : m_logout_path.get(this) ? m_logout_path.get(this) : null), {user})
			}
			catch(err) {
				error = false;
			}
			return Promise.resolve(error ? error : result);
		}
		async Register(user, onSuccess=undefined, api=undefined) {
			let result = null;
			let error = null;
			try {
				result = await axios.post(m_host.get(this) + (m_universalPath.get(this) ? m_universalPath.get(this) : "") + (api ? api : m_register_path.get(this) ? m_register_path.get(this) : null), {user});
			}
			catch(err) {
				error = false;
			}
			return Promise.resolve(error ? error : result);
		}
	}
	return iAuth;
})();

export default new iAuth();
