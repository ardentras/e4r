const mysql = require('mysql');
const nodemailer = require('nodemailer');
const fs = require('fs');

const fromEmail = "e4rtesting@gmail.com";

class TDatabase {
    constructor(db_host="localhost", db_port="8080", db_user="root", db_pw="root", db_name="") {
        this.db = mysql.createConnection({
            host: db_host,
            port: db_port,
            user: db_user,
            password: db_pw,
            database: db_name,
            timeout: 60000
          });
          this.db.connect((err)=>{
            if (err) {
                console.log("ErrorNo: " + err.errorno);
                console.log("Code: " + err.code);
                console.log("Call: " + err.syscall);
                console.log("Fatal: " + err.fatal);
            }
            else {
                console.log('Connected to Database: ' + db_name);
            }
          });
    }
    confirmationEmail(data) {
        var transporter = nodemailer.createTransport({
            service: 'Gmail',
            auth: {
                user: fromEmail,
                pass: 'xiaozhu541'
            }
            });
        let HelperOptions = {
            from: "Education For Revitalization <email@gmail.com>",
            to: "e4rtesting@gmail.com",
            subject: "Account Confirmation",
            html: "<p>Hello " + data.name + ",</p>" +
                  "<p style='margin-left: 20px'>Thanks for signing up for Education for Revitalization.</p>" +
                  "<p style='margin-left: 20px'>Please click on the following verify address to activate your account: </p>" +
                  "<p style='margin-left: 20px'>Email: <span style='margin-left: 100px'>" + data.email + "</span></p>" +
                  "<p style='margin-left: 20px'>Verify: <span style='margin-left: 100px'><a href='https://google.com'>Google</a></span></p>" +
                  "<p style='margin-left: 20px'>If you haven't already, install our app for <span style='color: #15c'>Android</span></p>" +
                  "<p style='margin-left: 20px'>If you have any questions, please <span style='color: #15c'>Contact Us</span>.</p>" +
                  "<p>sincerely,</p>" +
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
    isEmail(data) {
        let check = false;
        const format = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|edu)\b/;
        if (format.test(data)) {
            check = true;
        }
        return check;
    }
    ifSpecialChar(data) {
        let check = false;
        const format = /[ !#$%^&*()_+\-=\[\]{};':"\\|,<>\/?]+/;
        if (format.test(data)) {
            check = true;
        }
        return check;
    }
    sanitizeInput(data) {
        let check = false;
        const specialChar = this.ifSpecialChar(data);
        const verifyEmail = this.isEmail(data);
        if (specialChar === false && verifyEmail === true) {
            check = true;
        }
        return check;
    }
    LogIn(client, data) {
        this.db.query("SELECT * FROM Users WHERE Email=?", data.email,(err, users, res)=>{
            if (err) {
                client.json({response: "Failed", type: "GET" ,code: 500, reason: "Search User error"});
            }
            else {
                if (users && users.length) {
                    if (users[0].Password === data.password) {
                        client.json({response: true});
                    }
                    else {
                        client.json({response: false});
                    }
                }
                else {
                    client.json({response: false});
                }
            }
        });
        
    }
    AccountCreation(client, data) {
        const sanitized = this.sanitizeInput(data.email);
        console.log("SIGNUP Request");
        if (sanitized === true) {
            this.db.query("SELECT * FROM Users WHERE Email=?", data.email,(err, users, res)=>{
                if (err) {
                    client.json({response: "Failed", type: "GET" ,code: 500, reason: "Search User error"});
                }
                else {
                    if (users && users.length) {
                        client.json({response: "Failed", type: "GET",code: 100, reason: "User already exist"});
                    }
                    else {
                        this.db.query("INSERT INTO Users SET ?", data, (err,res)=>{
                            if (err) {
                                console.log("SIGNUP Error");
                                client.json({response: "Failed", type: "POST", code: 500, reason: "Create User error"});
                            }
                            else {
                                console.log('SIGNUP SUCCEED Email: ' + data.email );
                                client.json({response: "Succeed", type: "POST",code: 200, action: "SIGNUP"});
                                this.confirmationEmail(data);
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
    Display(client) {
        this.db.query("SELECT * FROM Users",(err, users, res)=>{
            if (err) {
                console.log("GET Error");
                client.json({response: "Failed", type: "GET", code: 404, reason: "Database error"});
            }
            else {
                client.json({response: 'Succcessful', type: "GET" ,code: 200, action:"DISPLAY", userCount: users.length, users});
            }
        });
    }
}

module.exports = TDatabase;