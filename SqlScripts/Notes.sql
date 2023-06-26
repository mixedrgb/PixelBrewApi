use PixelBrew
GO

drop table Notes
;

CREATE TABLE Notes (
    NoteId INT IDENTITY(1,1) NOT NULL,
    PreferredBeverage VARCHAR(1024) NOT NULL,
    OtherNotes VARCHAR(2048),
    CONSTRAINT PK_Notes PRIMARY KEY (NoteId)
)

INSERT INTO Notes (
    PreferredBeverage,
    OtherNotes
)
VALUES (
    'Frappe',
    'Here is where I'\'''d put a trophy... IF I HAD ONE!!'
)
;

-- TODO: escape the apostrophe