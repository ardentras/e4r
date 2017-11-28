const express = require('express');
const app = express();
const Router = express.Router();

//Generate private key file and certificate file
// openssl req -new -newkey rsa:2048 -nodes -out mydomain.csr -keyout private.key
// openssl x509 -req -days 365 -in mydomain.csr -signkey private.key -out mydomain.crt
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
app.use((req, res, next) => {
	res.header("Access-Control-Allow-Origin", "*");
	res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
	next();
});

Router.all('/', (req, res)=>{
	res.send('Welcome to the API');
});
Router.post('/signup', (req, res) => {
    db.createAccount(res, req.body.user);
});
Router.post('/login', (req, res) => {
	db.attemptLogin(res, req.body.user);
});
Router.put('/renew', (req, res) => {
	db.renewSessionToken(res, req.body.user);
});
Router.put('/logout', (req, res) => {
	db.attemptLogout(res, req.body.user);
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
	const key = fs.readFileSync('./encryption/private.key');
	const cert = fs.readFileSync( './encryption/mydomain.crt' );
	const options = {
		key: key,
		cert: cert,
	};
	const HTTPsport = 3003;

	https.createServer(options, app).listen(HTTPsport);
	console.log("HTTPs running on port " + HTTPsport);
}
