## Getting Started with RESTFUL Api

```
GET: Retrieve Information
POST: Insert Information
PUT: Update Information
Delete: Remove Information
```

## Using Education for Revitalization Api

```
SERVER PUBLIC IP: 34.208.210.218
SERVER PUBLIC PORT: 3002

API Calls: 
          API Welcome -> server:port/api -> ALL
          Log In -> server:port/api/login -> POST
          Log Out -> server:port/api/logout -> PUT
          Sign Up -> server:port/api/signup -> POST
          Renew Session -> server:port/api/renew -> PUT

TEST API Calls:
          Display Users Information -> server:port/test/display -> GET
          

```
## JSON Formats: 

#### LOG IN/SIGN UP:
```
          {
	   "user": {
	        "username": "test1",
	        "email": "test@test.com",
	        "password": "testpassword"
	   }
          }
```

#### LOG IN RESPONSE:
```
          {
            "response": "Success",
            "type": "GET",
            "code": 200,
            "action": "LOGIN",
            "session_id": "{session_token}",
            "user_object": "{}"
          }
          
          OR
          
          {
            "response": "Failed",
            "type": "GET",
            "code": 403,
            "reason": "User not found",
            "result": null
          }
```    
#### SIGN UP RESPONSE:
```
          {
            "response": "Succeed",
            "type": "POST",
            "code": 201,
            "action": "SIGNUP"
          }

          OR
          
          {
            "response": "Failed",
            "type": "GET",
            "code": 403,
            "reason": "User not found",
            "result": null
          }
```
