const dbconn = require("./database");

module.exports = { // eslint-disable-line no-undef
	getstuff: function () {
		dbconn.login();
		dbconn.query();
	}
};