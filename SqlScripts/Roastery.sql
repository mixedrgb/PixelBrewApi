use PixelBrew
GO

drop table Roastery

CREATE TABLE Roastery (
    RoasteryId INT IDENTITY(1,1) NOT NULL,
    RoasteryName VARCHAR(64) NOT NULL,
    StreetAddress VARCHAR(128),
    City VARCHAR(128),
    USAState VARCHAR(64),
    FoundingDate VARCHAR(64),
)

INSERT INTO Roastery (
    RoasteryName,
    StreetAddress,
    City,
    USAState,
    FoundingDate
)
VALUES
(
    'PixelBrew',
    '1234 Main St',
    'Anytown',
    'CA',
    '2017-01-01'
),
(
    'Big Cuppa',
    '1234 Main St',
    'Morrilton',
    'Arkansas',
    '2017/05/01'
)
;
