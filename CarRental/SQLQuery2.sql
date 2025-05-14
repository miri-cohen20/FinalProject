INSERT INTO [dbo].[Car] ([Id], [seats], [cleanStatus], [properStatus], [FinalCleaning], [LastCorrection], [city], [street], [descriptionCleaning], [descriptionProper])
VALUES 
('1', 5, 1, 1, GETDATE(), GETDATE(), 'Tel Aviv', 'Rothschild Blvd', 'Clean', 'Proper'),
('2', 7, 1, 1, GETDATE(), GETDATE(), 'Jerusalem', 'Jaffa St', 'Clean', 'Proper'),
('3', 9, 0, 1, GETDATE(), GETDATE(), 'Haifa', 'Ben Gurion St', 'Clean', 'Proper'),
('4', 5, 1, 0, GETDATE(), GETDATE(), 'Beer Sheva', 'Hativat Yiftach St', 'Clean', 'Not Proper'),
('5', 7, 1, 1, GETDATE(), GETDATE(), 'Eilat', 'Shenkin St', 'Clean', 'Proper'),
('6', 9, 1, 0, GETDATE(), GETDATE(), 'Ashdod', 'HaPalmach St', 'Clean', 'Not Proper'),
('7', 5, 0, 1, GETDATE(), GETDATE(), 'Netanya', 'Hertzel St', 'Not Clean', 'Proper'),
('8', 7, 1, 1, GETDATE(), GETDATE(), 'Rishon Lezion', 'HaShalom St', 'Clean', 'Proper');
