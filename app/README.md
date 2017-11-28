## Getting Started with a RESTful API

```
GET: Retrieve Information
POST: Insert Information
PUT: Update Information
DELETE: Remove Information
```

## Using Education for Revitalization Api

```
SERVER PUBLIC IP: 34.208.210.218
SERVER PUBLIC HTTP PORT: 3002
SERVER PUBLIC HTTPS/SSL PORT: 3003

API Calls:
          API Welcome -> hostname:port/api -> ALL
          Log In -> hostname:port/api/login -> POST
          Log Out -> hostname:port/api/logout -> PUT
          Sign Up -> hostname:port/api/signup -> POST
          Renew Session -> hostname:port/api/renew -> PUT

Debugging API Calls:
          Display Users Information -> hostname:port/test/display -> GET


```
## JSON Formats:

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
    "type": "GET",
    "code": 200,
    "action": "RENEW_SESSION",
    "session_id": {session_token}
}

On invalid/expired session:
{
    "response": "Failed",
    "type": "GET",
    "code": 403,
    "action": "LOGOUT",
    "reason": "User's session token is invalid."
}

On non-existant session:
{
    "response": "Failed",
    "type": "GET",
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
