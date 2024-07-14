INSERT INTO Users (Email, [Password], UserType, InternalId)
VALUES
('john.doe@example.com', 'password123', 1, 'INT12345'),
('jane.smith@example.com', 'password123', 2, 'INT12346'),
('bob.jones@example.com', 'password123', 1, 'INT12347'),
('alice.brown@example.com', 'password123', 2, 'INT12348');

INSERT INTO ShiftTypes ([Name], [Description], Price, NightPrice, HolidayPrice)
VALUES
('Day Shift', 'Regular day shift', 100.00, 120.00, 150.00),
('Night Shift', 'Regular night shift', 150.00, 180.00, 200.00),
('Holiday Shift', 'Shift during holidays', 200.00, 250.00, 300.00);

INSERT INTO Places ([Name])
VALUES
('New York'),
('Los Angeles'),
('Chicago'),
('Houston');

INSERT INTO Shifts (CreationDate, StartDate, EndDate, PricePerUser, ShiftTypeId, CreatorUserId, MaxUsers, TotalPrice, Status)
VALUES
('2024-06-01', '2024-06-10 08:00:00', '2024-06-10 16:00:00', 100.00, 1, 1, 5, 500.00, 1),
('2024-06-02', '2024-06-11 20:00:00', '2024-06-12 04:00:00', 150.00, 2, 2, 3, 450.00, 1),
('2024-06-03', '2024-06-15 08:00:00', '2024-06-15 16:00:00', 200.00, 3, 3, 4, 800.00, 1);

INSERT INTO ShiftPlaces (ShiftId, PlaceId)
VALUES
(1, 1),
(1, 2),
(2, 3),
(3, 4);


INSERT INTO AppliedUsers (ShiftId, UserId)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(3, 1),
(3, 2);


INSERT INTO ApprovedUsers (ShiftId, UserId)
VALUES
(1, 1),
(2, 3),
(3, 2);

