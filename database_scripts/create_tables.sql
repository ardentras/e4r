CREATE SCHEMA EFRAcc;
CREATE SCHEMA EFRQuest;

CREATE TABLE EFRAcc.PasswordSalts
(SaltID                INT            NOT NULL IDENTITY PRIMARY KEY,
 Salt               NVARCHAR(500)  NOT NULL
);

CREATE TABLE EFRAcc.Users
(UserID            INT               NOT NULL IDENTITY PRIMARY KEY,
 Username          NVARCHAR(50)   NOT NULL,
 EmailAddr         NVARCHAR(100)  NOT NULL,
 PasswordHash      NVARCHAR(1000) NOT NULL,
 UserObject        VARBINARY(MAX) NOT NULL,
 SaltID            INT            FOREIGN KEY REFERENCES
                                   EFRAcc.PasswordSalts(SaltID)
);

CREATE TABLE EFRAcc.Charities
(CharityID    INT    NOT NULL PRIMARY KEY,
 CharityName    NVARCHAR(100) NOT NULL,
 CharityLink    NVARCHAR(100) NOT NULL
);

CREATE TABLE EFRAcc.CharityUsers
(UserID    INT    NOT NULL FOREIGN KEY REFERENCES EFRAcc.Users(UserID),
 CharityID    INT    NOT NULL FOREIGN KEY REFERENCES EFRAcc.Charities(CharityID),
 PRIMARY KEY(UserID, CharityID)
);

CREATE TABLE EFRAcc.PasswordRecovery
 (RecoveryID        INT            NOT NULL IDENTITY PRIMARY KEY,
  UserID            INT            FOREIGN KEY REFERENCES
                                   EFRAcc.Users(UserID)
);

CREATE TABLE EFRAcc.Sessions
 (SessionID         VARCHAR(36)    NOT NULL PRIMARY KEY,
  ExpirationTime    DATETIME       NOT NULL,
  UserID            INT            FOREIGN KEY REFERENCES
                                   EFRAcc.Users(UserID)
);

-- CREATE TRIGGER SessionsExpDate_TRIG ON testdb.EFRAcc.Sessions
--     AFTER INSERT, UPDATE
-- AS
--     UPDATE EFRAcc.Sessions
--     SET ExpirationTime = DATEADD(DAY, 1, ExpirationTime)
--     WHERE SessionID IN (SELECT SessionID FROM Inserted);

CREATE TABLE EFRAcc.Achievements
 (AchievementID     INT            NOT NULL IDENTITY PRIMARY KEY,
  AchievementName   NVARCHAR(100)  NOT NULL,
  Description       NVARCHAR(500)  NOT NULL,
  EXPVALUE          INT            NOT NULL
);

CREATE TABLE EFRAcc.AchievementList
 (AchievementID     INT            FOREIGN KEY REFERENCES
                                   EFRAcc.Achievements(AchievementID),
  UserID            INT            FOREIGN KEY REFERENCES
                                   EFRAcc.Users(UserID)
);

CREATE TABLE EFRQuest.Subjects
 (SubjectID         INT            NOT NULL IDENTITY PRIMARY KEY,
  SubjectName       NVARCHAR(100)  NOT NULL
);

CREATE TABLE EFRQuest.QuestionsDB
 (QuestionBlockID   INT            NOT NULL IDENTITY PRIMARY KEY,
  Difficulty        INT            NOT NULL,
  SubjectID         INT            FOREIGN KEY REFERENCES
                                   EFRQuest.Subjects
);

CREATE TABLE EFRQuest.Questions
 (QuestionID        INT            NOT NULL IDENTITY PRIMARY KEY,
  QuestionText      NVARCHAR(250)  NOT NULL,
  QuestionOne       NVARCHAR(250)  NOT NULL,
  QuestionTwo       NVARCHAR(250)  NOT NULL,
  QuestionThree     NVARCHAR(250)  NOT NULL,
  QuestionFour      NVARCHAR(250)  NOT NULL,
  CorrectAnswer     NVARCHAR(250)  NOT NULL,
  StatsOne          INT            NOT NULL DEFAULT 0,
  StatsTwo          INT            NOT NULL DEFAULT 0,
  StatsThree        INT            NOT NULL DEFAULT 0,
  StatsFour         INT            NOT NULL DEFAULT 0,
  QuestionBlockID   INT            FOREIGN KEY REFERENCES
                                   EFRQuest.QuestionsDB(QuestionBlockID)
);

CREATE TABLE EFRQuest.QuestionHelp
 (HelpID            INT            NOT NULL IDENTITY PRIMARY KEY,
  HelpInfo          NVARCHAR(500)  NOT NULL,
  QuestionID        INT            FOREIGN KEY REFERENCES
                                   EFRQuest.Questions(QuestionID)
);
