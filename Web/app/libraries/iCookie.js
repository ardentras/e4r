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
		encrypt(string) {
			return btoa(encodeURIComponent(string).replace(/%([0-9A-F]{2})/g, (match, p1)=>{
				return String.fromCharCode("0x" + p1);
			}));
		}
		decrypt(string) {
			return decodeURIComponent(Array.prototype.map.call(atob(string), (c)=>{
				return "%" + c.charCodeAt(0).toString(16);
			}).join(""));
		}
		setStorage(name, value, secure) {
			secure ? localStorage.setItem(this.encrypt(name), this.encrypt(JSON.stringify(value))) : localStorage.setItem(name, value);
		}
		getStorage(name, secure=false) {
			let value = JSON.parse(secure ? this.decrypt(localStorage.getItem(this.encrypt(name))) : localStorage.getItem(name));
			return value;
		}
		removeStorage(name) {
			localStorage.removeItem(name);
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