const express = require('express');
const app = express();
const cors = require('cors');
const bodyParser = require('body-parser');
const TDatabase = require('./TDatabase');

const HOST = "apptest.co4ebczraatr.us-west-2.rds.amazonaws.com";
const DB_PORT = "3306";
const DB_USER = "kevinjxu";
const DB_PW = "apptest1";
const DB = "er";

const db = new TDatabase(
    HOST, 
    DB_PORT, 
    DB_USER, 
    DB_PW, 
    DB);

//BASIC REST API
//GET - List/Retrieve
//PUT - Replace/Update
//POST - Create New
//DELETE - Removal/Erase

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use(cors());

app.get('/', (req, res)=>{
    console.log('Welcome to API');
});
app.post('/api/login', (req,res) => {
    db.LogIn(res, req.body.user);
});
app.post('/api/signup', (req, res) => {
    db.AccountCreation(res, req.body.user);
});
app.get('/api/test/display', (req, res) => {
    db.Display(res);
});

app.listen(2000);
console.log("Running on Port 2000");