const mssql = require('mssql');
const nodemailer = require('nodemailer');
const shajs = require('sha.js');

const fromEmail = "e4rtesting@gmail.com";

const USERNAME_LENGTH = 50;
const EMAIL_LENGTH = 100;
const PASSWORD_LENGTH = 1000;

class TDatabase {
	// Creates the connection to the database given the passed parameters.
    constructor(db_host="localhost", db_port="1433", db_user="root", db_pw="root", db_name="") {
		var config = {
			user: db_user,
			password: db_pw,
			server: db_host,
			database: db_name,
			port: db_port,
			options: {
				encrypt: true
			}
		};

        this.db = new mssql.ConnectionPool(config);
      	this.db.connect((err) => {
            if (err) {
                printErrorDetails(err);
            }
            else {
                console.log('Connected to database' + (db_name == '' ? ' ' : ' ' + db_name + ' ') + 'at ' + db_host + ':' + db_port);
            }
        });
    }

	// Prints details given the passed error.
	printErrorDetails(err) {
		console.log("ErrorNo: " + err.number);
		console.log("Code: " + err.state);
		console.log("Call: " + err.procname);
		console.log("Class: " + err.class);
	}

	// Sends a confirmation email to the user to confirm the email used to create their account.
    confirmationEmail (data) {
        var transporter = nodemailer.createTransport({
            service: 'Gmail',
            auth: {
                user: fromEmail,
                pass: 'xiaozhu541'
            }
            });
        let HelperOptions = {
            from: "Education For Revitalization <" + fromEmail + ">",
            to: data.email,
            subject: "Account Confirmation",
            html: "<p>Hello " + data.name + ",</p>" +
                  "<p style='margin-left: 20px'>Thanks for signing up for Education for Revitalization.</p>" +
                  "<p style='margin-left: 20px'>Please click on the following verify address to activate your account: </p>" +
                  "<p style='margin-left: 20px'>Email: <span style='margin-left: 100px'>" + data.email + "</span></p>" +
                  "<p style='margin-left: 20px'>Verify: <span style='margin-left: 100px'><a href='https://google.com'>Google</a></span></p>" +
                  "<p style='margin-left: 20px'>If you haven't already, install our app for <span style='color: #15c'>Android</span></p>" +
                  "<p style='margin-left: 20px'>If you have any questions, please <span style='color: #15c'>Contact Us</span>.</p>" +
                  "<p>Sincerely,</p>" +
                  "<p style='font-size: 90%;'>The E4R Team</p>"
        };
        transporter.sendMail(HelperOptions, (err, response)=>{
            if(err) {
                console.log('Confirmation Email failed: ' + data.email);
                return false;
            }
        });
        console.log("Confirmation Email Sent: " + data.email);
    }

	// Verifies a user's email via regex.
    isEmail (data) {
        let check = false;
        const format = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|edu)\b/;
        if (format.test(data)) {
            check = true;
        }
        return check;
    }

	// Verifies an email has no special characters
    hasSpecialChar (data) {
        let check = false;
        const format = /[ !#$%^&*()_+\-=\[\]{};':"\\|,<>\/?]+/;
        if (format.test(data)) {
            check = true;
        }
        return check;
    }

	// Sanitizes user input.
    sanitizeInput (data) {
        let check = false;
        const specialChar = this.hasSpecialChar(data);
        const verifyEmail = this.isEmail(data);
        if (specialChar === false && verifyEmail === true) {
            check = true;
        }
        return check;
    }

	// Attempts to log the user in given a certain username and Password
	//
	// Example:
	// curl -XPOST localhost:3002/api/login -H 'Content-Type: application/json' -d '{"user":{"username":"shaunrasmusen","password":"defaultpass"}}'
    attemptLogin (client, data) {
        this.db.request().input('username', mssql.NVarChar(EMAIL_LENGTH), data.username)
				.query("SELECT * FROM EFRAcc.Users WHERE EmailAddr=@username OR Username=@username", (err, users) => {
            if (err) {
				printErrorDetails();
				console.log("LOGIN Fail");
                client.json({response: "Failed", type: "GET" ,code: 500, reason: "Unknown database error"});
            }
            else {
                if (users.rowsAffected > 0) {
					//TODO Update this with a call to the salt table
					let salt = "qoi43nE5iz0s9e4?309vzE()FdeaB420"
					let hashedPassword = shajs('sha256').update(data.password + salt).digest('hex');

                    if (users.recordsets[0][0].PasswordHash === hashedPassword) {
						this.db.request().input('username', mssql.NVarChar(USERNAME_LENGTH), users.recordsets[0][0].Username)
										.query("SELECT CAST(UserObject AS VARCHAR) AS UserObject FROM EFRAcc.Users WHERE Username=@username", (err, res) => {
											console.log(res);
											client.json({response: "Success", type: "GET", code: 200, action: "LOGIN", result: res.recordsets[0][0].UserObject});
										});
                    }
                    else {
						client.json({response: "Failed", type: "GET", code: 403, reason: "Invalid Password"});
                    }
                }
                else {
                    client.json({response: "Failed", type: "GET", code: 403, reason: "User not found", result: err});
                }
            }
        });
    }

	// Attempts to create an account with the given username, email, and password
	//
	// Example:
	// curl -XPOST localhost:3002/api/signup -H 'Content-Type: application/json' -d '{"user":{"username":"shaunrasmusen","email":"shaunrasmusen@gmail.com","password":"defaultpass"}}'
    createAccount(client, data) {
        const sanitized = this.sanitizeInput(data.email);
        console.log("SIGNUP Request");
        if (sanitized === true) {
            this.db.request().input('email', mssql.NVarChar(EMAIL_LENGTH), data.email)
							.input('username', mssql.NVarChar(USERNAME_LENGTH), data.username)
							.query("SELECT * FROM EFRAcc.Users WHERE EmailAddr = @email OR Username = @username;", (err, users) => {
                if (err) {
					printErrorDetails();
					console.log("SIGNUP Error");
                    client.json({response: "Failed", type: "GET" ,code: 500, reason: "Search User error"});
                }
                else {
                    if (users.rowsAffected > 0) {
                        client.json({response: "Failed", type: "GET",code: 100, reason: "User already exists"});
                    }
                    else {
						//TODO Update this with a call to the salt table
						let salt = "qoi43nE5iz0s9e4?309vzE()FdeaB420"
						let hashedPassword = shajs('sha256').update(data.password + salt).digest('hex');

                        this.db.request().input('username', mssql.NVarChar(USERNAME_LENGTH), data.username)
								.input('email', mssql.NVarChar(EMAIL_LENGTH), data.email)
								.input('password', mssql.NVarChar(PASSWORD_LENGTH), hashedPassword)
								.query("INSERT INTO EFRAcc.Users VALUES (@username, @email, @password, CAST('{}' AS VARBINARY(MAX)), NULL);", (err, res) => {
                            if (err) {
								printErrorDetails();
                                console.log("SIGNUP Error");
                                client.json({response: "Failed", type: "POST", code: 500, reason: "Create User error", result: err});
                            }
                            else {
                                console.log('SIGNUP SUCCEED Email: ' + data.email );
                                client.json({response: "Succeed", type: "POST", code: 200, action: "SIGNUP"});
                                //this.confirmationEmail(data);
                            }
                        });
                    }
                }
            });
        }
        else {
            client.json({response: "Rejected", Code: 500, reason: "Invalid Email"});
        }
    }

	// Displays all current user accounts from the database.
	// TODO Remove in production.
	//
	// curl -XGET localhost:3002/api/test/display
    displayUsers(client) {
        this.db.request().query("SELECT * FROM EFRAcc.Users", (err, result) => {
            if (err) {
				printErrorDetails();
                console.log("GET Error");
                client.json({response: "Failed", type: "GET", code: 404, reason: err});
            }
            else {
                client.json({response: 'Successful', type: "GET" ,code: 200, action: "DISPLAY", userCount: result.length, result});
            }
        });
    }

	// Ensures that the database connection is closed on object destruction.
	gracefulShutdown() {
		this.db.close();
	}
}

module.exports = TDatabase;
