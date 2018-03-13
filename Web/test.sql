select * from EFRAcc.Sessions WHERE UserID=(SELECT UserID FROM EFRAcc.Users WHERE EmailAddr='kevinjia.xu@gmail.com') ORDER BY ExpirationTime;
select * from EFRAcc.Users;
select * from EFRQuest.QuestionHelp;
select * from EFRQuest.Questions;


BEGIN TRANSACTION remove_kevin
BEGIN TRY
    DELETE FROM EFRAcc.Sessions
    WHERE UserID=(
        SELECT UserID
        FROM EFRAcc.Users
        WHERE EmailAddr='kevinjia.xu@gmail.com'
        );
    DELETE FROM EFRAcc.Users
    WHERE EmailAddr='kevinjia.xu@gmail.com';
COMMIT TRANSACTION remove_kevin
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION remove_kevin
END CATCH
