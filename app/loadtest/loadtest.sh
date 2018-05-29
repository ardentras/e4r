#!/bin/bash

echo "//========================================================================================//"
echo ""
echo "Running loadtest on /api/check_username endpoint."
echo ""
echo "Sending username and email."
echo ""
echo "# of concurrent connections: 10"
echo "# of requests per second:    500"
echo "Total runtime for this test: 20s"
echo ""
echo "//========================================================================================//"

loadtest -c 10 -t 20 --rps 500 -k -m POST -T 'application/json' --data '{"user":{"username":"shaunrasmusen","email":"shaunrasmusen@gmail.com"}}' http://34.216.143.255:3002/api/check_username

echo "//========================================================================================//"
echo ""
echo "Running loadtest on /api/login endpoint."
echo ""
echo "Sending username and password."
echo ""
echo "# of concurrent connections: 10"
echo "# of requests per second:    250"
echo "Total runtime for this test: 20s"
echo ""
echo "//========================================================================================//"

loadtest -c 10 -t 20 --rps 250 -k -m POST -T 'application/json' --data '{"user":{"username":"shaunrasmusen","password":"blahblah"}}' http://34.216.143.255:3002/api/login

echo "//========================================================================================//"
echo ""
echo "Running loadtest on /api/update_uo endpoint."
echo ""
echo "Sending session_id token and user object."
echo ""
echo "# of concurrent connections: 10"
echo "# of requests per second:    250"
echo "Total runtime for this test: 20s"
echo ""
echo "//========================================================================================//"

loadtest -c 10 -t 20 --rps 250 -k -m PUT -T 'application/json' --data '{"user":{"session":"9c9e03a4-f951-46da-a1d8-5e0584877727","userobject":{"user_data":{"username":"shaunrasmusen","email":"shaunrasmusen@gmail.com","first_name":"","last_name":"","selected_charity":"","favorite_charities":[""]},"game_data":{"subject_name":"Science","subject_id":3,"difficulty":0,"totalQuestions":39,"totalDonated":0,"blocksRemaining":7,"completed_blocks":[60,5,164]}, "timestamp":"2018-01-24T02:06:58+00:00"}}}' http://34.216.143.255:3002/api/update_uo

echo "//========================================================================================//"
echo ""
echo "Running loadtest on /api/q/request_block endpoint."
echo ""
echo "Sending session_id token, user object, and total donated."
echo ""
echo "# of concurrent connections: 10"
echo "# of requests per second:    250"
echo "Total runtime for this test: 20s"
echo ""
echo "//========================================================================================//"

loadtest -c 10 -t 20 --rps 250 -k -m PUT -T 'application/json' --data '{"user":{"session":"9c9e03a4-f951-46da-a1d8-5e0584877727","userobject":{"user_data":{"username":"shaunrasmusen","email":"shaunrasmusen@gmail.com","first_name":"","last_name":"","selected_charity":"","favorite_charities":[""]},"game_data":{"subject_name":"Science","subject_id":3,"difficulty":0,"totalQuestions":39,"totalDonated":0,"blocksRemaining":7,"completed_blocks":[60,5,164]}, "timestamp":"2018-01-24T02:06:58+00:00"}, "donated": "24"}}' http://34.216.143.255:3002/api/q/request_block
