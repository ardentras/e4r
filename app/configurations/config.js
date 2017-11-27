//configurations
const DB_CONFIG = {
	user: "cooluser",
	password: "coolpassword",
	server: "e4rdb.cz5nhcw7ql0u.us-west-2.rds.amazonaws.com",
	database: "testdb",
	port: "1433",
	options: {
		encrypt: true
	}
};

const DB_USER_CONFIG = {
		USERNAME_LENGTH: 50,
		EMAIL_LENGTH: 100,
		PASSWORD_LENGTH: 1000
};

const EMAIL_CONFIG = {
    username: "e4rtesting@gmail.com",
    password: "xiaozhu541"
};

module.exports = {
	DB_CONFIG: DB_CONFIG,
	DB_USER_CONFIG: DB_USER_CONFIG,
	EMAIL_CONFIG: EMAIL_CONFIG
}
