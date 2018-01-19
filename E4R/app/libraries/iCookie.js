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
			return new Promise((resolve, reject)=>{
				document.cookie = cookie;
				if (!document.cookie) {
					reject(false);
				}
				else {
					resolve(true);
				}
			});
		}
		get(key) {
			let match = document.cookie.match(new RegExp(key + "=([^;]+)"));
			return match ? match[1] : undefined;
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