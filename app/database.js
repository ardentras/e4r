const mssql = require('mssql');

const config = {
	user: "cooluser",
	password: "coolpassword",
	server: "e4rdb.cz5nhcw7ql0u.us-west-2.rds.amazonaws.com",
	database: ""
};

module.exports = {
	login: function () {
		const pool = mssql.connect(config);
	}
};