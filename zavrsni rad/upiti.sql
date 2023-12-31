CREATE TABLE Residents (
    ResidentID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    RoomNumber INT,
    AdmissionDate DATETIME,
    MedicalCondition NVARCHAR(100)
);

INSERT INTO Residents (FirstName, LastName, RoomNumber, AdmissionDate, MedicalCondition)
VALUES
('Marko', 'Markić', 101, '2022-05-15', 'Diabetes'),
('Ivo', 'Ivić', 102, '2021-11-10', 'Hypertension'),
('Darko', 'Darkić', 103, '2023-02-20', 'Arthritis');

UPDATE Residents
SET RoomNumber = 105
WHERE FirstName = 'Marko' AND LastName = 'Markić';

DELETE FROM Residents
WHERE FirstName = 'Darko' AND LastName = 'Darkić';

SELECT * FROM Residents WHERE MedicalCondition = 'Diabetes';


