/****************************************************************************
 * 
 *->Name: iCookie.js 
 *->Purpose: An object which interacts with document cookies.
 *
*****************************************************************************/

"use strict";

/**
 * @param { string } cookie - Define the cookie to be stored
 */

const iCookie = (()=>{
	class iCookie {
		constructor() {}
		set(cookie) {
			document.cookie = cookie;
		}
		add(name, value) {
			const expire = "expires=" + this.time();
			const cookie = name + "=" + value + ";" + expire + ";path=/";
			this.set(cookie);
		}
		get(key) {
			let match = document.cookie.match(new RegExp(key + "=([^;]+)"));
			return match ? match[1] : undefined;
		}
		time() {
			const date = new Date();
			date.setTime(date.getTime() + (7 * 24 * 60 * 60 * 1000));
			return date.toUTCString();
		}
		reset() {
			document.cookie.split(";").forEach(function(c) { 
				document.cookie = c.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/"); 
			});
		}
	}
	return iCookie;
})();

export default new iCookie();