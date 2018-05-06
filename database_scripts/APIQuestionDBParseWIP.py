import requests as requests
import random as random

responce = requests.get("https://opentdb.com/api.php?amount=50&category=23&type=multiple")
data = responce.json()
##print(data)

import csv
##with open('history.csv', 'w', newline='') as csvfile:
##    datawriter = csv.writer(csvfile, delimiter=' ', quotechar=' ', quoting=csv.QUOTE_MINIMAL)
##            datawriter.writerow(write["question"])
            
##datawriter.writerow(write["question"])
            
with open('history.csv', 'w', newline='') as csvfile:
    fieldnames = ['question', 'correct_answer', 'incorrect_answer_one', 'incorrect_answer_two', 'incorrect_answer_three', 'difficulty']
    writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
    writer.writeheader()
    for x in range(0, 49):
        write = data["results"][x]
        if write["type"] == "multiple":
            writer.writerow({'question': write['question'].encode('utf8'), 'correct_answer': write['correct_answer'].encode('utf8'),
            'incorrect_answer_one': write['incorrect_answers'][0].encode('utf8'),
            'incorrect_answer_two': write['incorrect_answers'][1].encode('utf8'),
            'incorrect_answer_three': write['incorrect_answers'][2].encode('utf8'),
            'difficulty': write['difficulty'].encode('utf8')})

    
##count = block_id + ((x - error_count) / 10)
    
    block_id = 14
    count = block_id
    error_count = 0
with open('dbcall.txt', 'a') as file:
    for x in range(0, 40):
        write = data["results"][x]
        if write["type"] == "multiple":
            if (x - error_count) % 10 == 0:
                count = count + 1
            list = write['incorrect_answers']
            list.append(write['correct_answer'])
            random.shuffle(list)
            string = "INSERT INTO EFRQuest.Questions (QuestionText, QuestionOne, QuestionTwo, QuestionThree, QuestionFour, CorrectAnswer, QuestionBlockID, HelpID) VALUES ("
            string = string + '\'' + write['question'] + '\', '
            string = string + '\'' + list[0] + '\', '
            string = string + '\'' + list[1] + '\', '
            string = string + '\'' + list[2] + '\', '
            string = string + '\'' + list[3] + '\', '
            string = string + '\'' + write['correct_answer'] + '\', '
            string = string + str(count) + ', '
            string = string + str(4) + ');\n'
            try:
                file.write(string)
            except:
                error_count = error_count + 1