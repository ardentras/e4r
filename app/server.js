const express = require('express');
const app = express();
const Router = express.Router();

const bodyParser = require('body-parser');
const http = require('http');
const TDatabase = require('./database');
const DB_CONFIG = require('./configurations/config').DB_CONFIG;
const USE_HTTPS = require('./configurations/config').USE_HTTPS;
const db = new TDatabase(DB_CONFIG);
const HTTPport = 3002;

function terminator(sig) {
	if (typeof sig === "string") {
		console.log('Received %s - terminating Node server ...', sig);
		db.gracefulShutdown();
		process.exit(1);
	};
	console.log('Node server stopped.');
};

function terminatorSetup(element, index, array) {
	process.on(element, function() { terminator(element); });
};

['SIGHUP', 'SIGINT', 'SIGQUIT', 'SIGILL', 'SIGTRAP', 'SIGABRT', 'SIGBUS', 'SIGFPE', 'SIGUSR1', 'SIGSEGV', 'SIGUSR2', 'SIGPIPE', 'SIGTERM'].forEach(terminatorSetup);

process.on('exit', function() { terminator(); });

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// Enable CORS
app.use((req, res, next) => {
	res.header("Access-Control-Allow-Origin", "*");
	res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
	res.header('Access-Control-Allow-Methods', 'PUT, POST, GET, DELETE');
	next();
});

Router.all('/', (req, res)=>{
	res.send('Welcome to the API');
});
// Tested
Router.post('/signup', (req, res) => {
    db.createAccount(res, req.body.user);
});
Router.put('/resend_verify', (req, res) => {
	db.resendVerify(res, req.body.user);
});
// Tested
Router.post('/login', (req, res) => {
	db.attemptLogin(res, req.body.user);
});
// Tested
Router.put('/renew', (req, res) => {
	db.renewSessionToken(res, req.body.user);
});
Router.put('/logout', (req, res) => {
	db.attemptLogout(res, req.body.user);
});
// Tested
Router.post('/check_username', (req, res) => {
	db.checkUsername(res, req.body.user);
});
Router.post('/reset_password', (req, res) => {
	db.resetPassword(res, req.body.user);
});
Router.put('/verify_password_reset', (req, res) => {
	db.verifyPasswordReset(res, req.body.user);
});
// Tested
Router.delete('/delete_user', (req, res) => {
	db.deleteUser(res, req.body.user);
});
// Writing tests
Router.put('/update_uo', (req, res) => {
	db.update_uo(res, req.body.user);
});
// Tested
Router.get('/verify_email/:VerifyID', (req, res) => {
	db.verifyEmail(res, req.params);
});
Router.get('/bubble_feed', (req, res) => {
	db.bubbleFeed(res);
});
Router.put('/q/request_block', (req, res) => {
	db.requestQuestionBlock(res, req.body.user, req.body.game);
});
Router.put('/q/request_help', (req, res) => {
	db.requestHelp(res, req.body);
});
Router.get('/test/display', (req, res) => {
    db.displayUsers(res);
});

app.use('/api', Router);

http.createServer(app).listen(HTTPport);
console.log("HTTP running on port " + HTTPport);

if (USE_HTTPS) {
	const fs = require('fs');
	const https = require('https');

	// Generate private key file and certificate file
	// openssl req -new -newkey rsa:2048 -nodes -out mydomain.csr -keyout private.key
	// openssl x509 -req -days 365 -in mydomain.csr -signkey private.key -out mydomain.crt
	const key = fs.readFileSync('./encryption/private.key');
	const cert = fs.readFileSync( '../.well-known/mydomain.crt');
	const options = {
		key: key,
		cert: cert,
	};
	const HTTPsport = 3003;

	https.createServer(options, app).listen(HTTPsport);
	console.log("HTTPs running on port " + HTTPsport);
}
