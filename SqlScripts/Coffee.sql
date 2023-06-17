use PixelBrew
GO

drop table Coffee;

CREATE TABLE Coffee (
                CoffeeId INT IDENTITY(1,1) NOT NULL,
                CoffeeName VARCHAR(64) NOT NULL,
                Region VARCHAR(128),
                Processing VARCHAR(128),
                Varietal VARCHAR(64),
                RoastType VARCHAR(32),
                Weight VARCHAR(16),
                RoastDate VARCHAR(64),
                CONSTRAINT PK_Coffee PRIMARY KEY (CoffeeId)
)

INSERT INTO Coffee
(CoffeeName, Region, Processing, Varietal, RoastType, Weight, RoastDate)
VALUES
('Costa Rica', 'Tarrazu', 'Washed', 'Caturra', 'Medium', '12oz', '2017-01-01'),
('Ethiopia', 'Yirgacheffe', 'Swiss Water', 'Heirloom', 'Light', '5lb', '2017-01-01'),
('Guatamala/Sumatra',null, 'Natural', 'SL28', 'Espresso', '2lb', '06/15/2023')
;
