const dbconn = require('./database');

module.exports = {
	getstuff: function () {
		dbconn.login();
		dbconn.query();
	}
};