CREATE TABLE Setting.GuideDetails
(
    RowId					BIGINT NOT NULL IDENTITY(1, 1),
    GuideCode				VARCHAR(150) NULL,
    Name					VARCHAR(150) NULL,
    Age						VARCHAR(20) NULL,
    Address					VARCHAR(150) NULL,
    Experience				VARCHAR(50) NULL,
    PhoneNo					BIGINT NULL,
    CitizenshipNo			NVARCHAR(MAX) NULL,
    SpecializedRegion		NVARCHAR(150) NULL,
    Status					VARCHAR(150) NULL,
    Image					NVARCHAR(MAX) NULL,
    Description				NVARCHAR(MAX) NULL,
    CreatedBy				VARCHAR(150) NULL,
    CreatedDate				DATETIME2 NULL,
    ModifiedBy				VARCHAR(150) NULL,
    ModifiedDate			DATETIME2 NULL
);
