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

const DEFAULT_USER_OBJECT = {
    "user_data": {
        "username": "",
        "email": "",
        "first_name": "",
        "last_name": "",
        "selected_charity": "",
		"favorite_charities": [""]
    },
    "game_data": {
        "subject_name": "",
        "subject_id": 1,
        "difficulty": 0,
        "totalQuestions": 0,
        "totalDonated": 0,
		"blocksRemaining": 0,
        "completed_blocks": []
    },
    "timestamp":""
}

const DB_USER_CONFIG = {
		USERNAME_LENGTH: 50,
		EMAIL_LENGTH: 100,
		PASSWORD_LENGTH: 1000,
		SESSION_TOKEN_LENGTH: 36,
};

const EMAIL_CONFIG = {
    username: "e4rtesting@gmail.com",
    password: "xiaozhu541"
};

const USE_HTTPS = false;

module.exports = {
	DB_CONFIG: DB_CONFIG,
	DB_USER_CONFIG: DB_USER_CONFIG,
	DEFAULT_USER_OBJECT: DEFAULT_USER_OBJECT,
	EMAIL_CONFIG: EMAIL_CONFIG,
	USE_HTTPS: USE_HTTPS
}
