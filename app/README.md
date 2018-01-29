## Getting Started with a RESTful API

```
GET: Retrieve Information
POST: Insert Information
PUT: Update Information
DELETE: Remove Information
```

## Using Education for Revitalization Api

```
SERVER PUBLIC IP: 35.163.221.182
SERVER PUBLIC HTTP PORT: 3002
SERVER PUBLIC HTTPS/SSL PORT: 3003

API Calls:
          API Welcome -> hostname:port/api -> ALL
          Log In -> hostname:port/api/login -> POST
          Log Out -> hostname:port/api/logout -> PUT
          Sign Up -> hostname:port/api/signup -> POST
          Renew Session -> hostname:port/api/renew -> PUT
          Check Username -> hostname:port/api/check_username -> GET
          Request Question Block -> hostname:port/api/q/request_block -> GET

Debugging API Calls:
          Display Users Information -> hostname:port/test/display -> GET


```
## JSON Formats:

#### User Object Definition:
```
"userobject": {
    "user_data": {
        "username": "test1",
        "email": "test@test.com",
        "first_name": "John",
        "last_name": "Doe",
        "charity_name": "ACME Charity, LLC"
    },
    "game_data": {
        "subject_name": "Math",
        "subject_id": "1",
        "difficulty": "0",
        "completed_blocks": [1, 3, 4]
    },
    "timestamp":"2018-01-24T02:06:58+00:00"
}
```

#### SIGN UP REQUEST:
```
{
    "user": {
      "username": "test1",
      "email": "test@test.com",
      "password": "testpassword"
    }
}
```
#### SIGN UP RESPONSE:
```
On successful signup:
{
  "response": "Succeed",
  "type": "POST",
  "code": 201,
  "action": "SIGNUP"
}

On user already exists:
{
  "response": "Failed",
  "type": "GET",
  "code": 100,
  "reason": "User already exists",
}
```
#### LOG IN REQUEST:
```
{
    "user": {
        "username": "test1 OR test@test.com",
        "password": "testpassword"
    }
}
```
#### LOG IN RESPONSE:
```
On successful login:
{
  "response": "Success",
  "type": "GET",
  "code": 200,
  "action": "LOGIN",
  "session_id": "{session_token}",
  "user_object": "{}"
}

On invalid username/email:
{
  "response": "Failed",
  "type": "GET",
  "code": 403,
  "reason": "User not found",
  "result": null
}

On invalid password:
{
    "response": "Failed",
    "type": "GET",
    "code": 403,
    "reason": "Invalid Password"
}
```
#### SESSION RENEW REQUEST:
```
{
    "user": {
        "session": "{session_token}"
    }
}
```
#### SESSION RENEW RESPONSE:
```
On successful renewal:
{
    "response": "Success",
    "type": "PUT",
    "code": 200,
    "action": "RENEW_SESSION",
    "session_id": {session_token}
    "user_object": {user_object}
}

On invalid/expired session:
{
    "response": "Failed",
    "type": "PUT",
    "code": 403,
    "action": "LOGOUT",
    "reason": "User's session token is invalid."
}

On non-existant session:
{
    "response": "Failed",
    "type": "PUT",
    "code": 403,
    "action": "LOGOUT",
    "reason": "User's session token was not found."
}
```
#### LOGOUT REQUEST:
```
{
    "user": {
        "session": "{session_token}",
        "userobject": "{user_object}"
    }
}
```
#### LOGOUT RESPONSE:
```
On successful logout:
{
    "response": "Success",
    "type": "PUT",
    "code": 200,
    "reason": "User successfully logged out."
}

On invalid session:
{
    "response": "Failed",
    "type": "PUT",
    "code": 500,
    "reason": "Session invalid. User object could not be saved"
}
```
## Misc Requests:
#### CHECK_USERNAME REQUEST:
```
{
    "user": {
      "username": "test1",
      "email": "test@test.com",
    }
}
```
#### CHECK_USERNAME RESPONSE:
```
On user not found:
{
  "response": "Succeed",
  "type": "GET",
  "code": 200,
  "reason": "User not found"
}

On user already exists:
{
  "response": "Failed",
  "type": "GET",
  "code": 100,
  "reason": "User already exists",
}
```
## Game Requests:
#### REQUEST_BLOCK REQUEST:
```
{
    "user": {
        "session":"{session_id}",
        "userobject": {user_object}
    }
}
```
#### REQUEST_BLOCK RESPONSE:
```
[
    {
        "QuestionID":{id},
        "QuestionText":"{text}",
        "QuestionOne":"{answer1}",
        "QuestionTwo":"{answer2}",
        "QuestionThree":"{answer3}",
        "QuestionFour":"{answer4}",
        "CorrectAnswer":"{correct_answer}",
        "QuestionBlockID":"{block_id}"
    },
    {...},
]
```
