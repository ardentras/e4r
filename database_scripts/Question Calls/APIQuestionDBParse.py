import requests as requests
import random as random


## Math API:        19
## Blocks: 60, 70, 80

## Mythology API:   20
## Blocks: 90, 100, 110

## Anime/Manga API: 31
## Blocks: 120, 130, 140

## Computer API:    18
## Blocks: 180, 190, 200

## Sports API:      21
## Blocks: 210, 220, 230

## Animals API:     27
## Blocks: 240, 250, 260

## Film API:        11
## Blocks: 270, 280, 290

## Geography API:   22
## Blocks: 300, 310, 320

## General API:     9
## Blocks: 330, 340, 350

## Video Games API: 15
## Blocks: 360, 390, 420

## History API:     23
## Blocks: 450, 470, 490

## Television API:  14
## Blocks: 510, 520, 530

## Science API:     17
## Blocks: 540, 550, 560

## Celebrities API: 26
## Blocks: 570, 580, 590

## Board Games API: 16
## Blocks: 600, 610, 620

## Vehicles API:    28
## Blocks: 630, 640, 650

## Update when a new API call is added
######################################
category = "Vehicles"
api = 28
amount = 20
easy_block_id = 630
medium_block_id = 640
hard_block_id = 650
######################################

added_questions = []
easy_questions = []
medium_questions = []
hard_questions = []

easy_count = 0
medium_count = 0
hard_count = 0

error_count = 0

difficulty = ""
for i in range(0,100):
    responce = requests.get("https://opentdb.com/api.php?amount=20&category=" + str(api))
    data = responce.json()
    with open('testwrite.txt', 'w') as file:
        for x in range(0, amount):
            write = data["results"][x]
            if write["type"] == "multiple":
                if (write['question'] in added_questions) == False:
                    difficulty = write['difficulty']
                    if difficulty == "easy":
                        easy_count += 1
                        if easy_count % 10 == 0:
                            easy_block_id += 1
                    if difficulty == "medium":
                        medium_count += 1
                        if medium_count % 10 == 0:
                            medium_block_id += 1
                    if difficulty == "hard":
                        hard_count += 1
                        if hard_count % 10 == 0:
                            hard_block_id += 1
                    added_questions.append(write['question'])
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
                    if difficulty == "easy":
                        string = string + str(easy_block_id) + ', '
                    if difficulty == "medium":
                        string = string + str(medium_block_id) + ', '
                    if difficulty == "hard":
                        string = string + str(hard_block_id) + ', '
                    string = string + str(4) + ');\n'
                    try:
                        file.write(string)
                        if difficulty == "easy":
                            easy_questions.append(string)
                        if difficulty == "medium":
                            medium_questions.append(string)
                        if difficulty == "hard":
                            hard_questions.append(string)
                    except:
                        error_count = error_count + 1
                        if difficulty == "easy":
                            easy_count -= 1
                            if easy_count % 10 == 9:
                                easy_block_id -= 1
                        if difficulty == "medium":
                            medium_count -= 1
                            if medium_count % 10 == 9:
                                medium_block_id -= 1
                        if difficulty == "hard":
                            hard_count -= 1
                            if hard_count % 10 == 9:
                                hard_block_id -= 1
                        print("Write error found with: " + string)
                        
name = str(len(easy_questions)) + "-" + "Easy" + category + "Questions.txt"
with open(name, 'w') as file:
    for x in range(0, len(easy_questions) - 1):
        file.write(easy_questions[x])
        
name = str(len(medium_questions)) + "-" + "Medium" + category + "Questions.txt"
with open(name, 'w') as file:
    for x in range(0, len(medium_questions) - 1):
        file.write(medium_questions[x])
        
name = str(len(hard_questions)) + "-" + "Hard" + category + "Questions.txt"
with open(name, 'w') as file:
    for x in range(0, len(hard_questions) - 1):
        file.write(hard_questions[x])

        
print("Finished with: " + str(len(added_questions)))