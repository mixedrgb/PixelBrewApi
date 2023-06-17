use PixelBrew
GO

CREATE TABLE Equipment (
    GrinderId INT IDENTITY(1,1) NOT NULL,
    GrinderManufacturer VARCHAR(64) NOT NULL,
    GrinderType VARCHAR(64),
    GrinderModel VARCHAR(64) NOT NULL,
    GrinderSetting VARCHAR(64),
    CONSTRAINT PK_Grinder PRIMARY KEY (GrinderId)
)

INSERT INTO Equipment (
    GrinderManufacturer,
    GrinderType,
    GrinderModel,
    GrinderSetting
)
VALUES
('Baratza', 'Burr', 'Encore', '20'),
('Baratza', 'Burr', 'Virtuoso', '20'),
('Baratza', 'Burr', 'Vario', '22'),
('Baratza', 'Burr', 'Forte', '27'),
('Baratza', 'Burr', 'Sette', '69'),
('Baratza', 'Burr', 'Sette 270', '34'),
('Baratza', 'Burr', 'Sette 270W', '23'),
('Baratza', 'Burr', 'Sette 270Wi', '20'),
('Baratza', 'Burr', 'Sette 30 AP', '21'),
('Baratza', 'Burr', 'Sette 30 BG', '50'),
('Baratza', 'Burr', 'Sette 270 AP', '2')
;
