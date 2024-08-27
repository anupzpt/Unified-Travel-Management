CREATE TABLE Setting.PackageDetails(
		RowId					BIGINT   NOT NULL	IDENTITY(1,1),
		PackageCode				VARCHAR(150),
		PackageName				VARCHAR(150),
		PackageType				VARCHAR(150),
		PackageAverageCost		MONEY,
		Country					VARCHAR(150),
		Duration				VARCHAR(150),
		BestSeason				VARCHAR(150),
		[Group]					VARCHAR(80),
		MinimumPerson			BIGINT,
		MaximumPerson			BIGINT,
		MaxAltiude				VARCHAR(150),
		Accommodation			VARCHAR(150),
		Transportation			VARCHAR(150),
		PackageCount			BIGINT,
		Description				NVARCHAR(MAX),
		GuideCode				VARCHAR(150),
		Status					VARCHAR(10),
		RouteImage				VARCHAR(Max),
		Image					VARCHAR(Max),
		DocumentCode			VARCHAR(150),
		ReviewCode				VARCHAR(150),
		CreatedBy				VARCHAR(150),
		CreatedDate				DATETIME2,
		ModifiedBy				VARCHAR(150),
		ModifiedDate			DATETIME2
)

create SCHEMA Package
CREATE TABLE Package.PackageItineraryDetails(
		RowId					BIGINT   NOT NULL	IDENTITY(1,1),
		PackageCode				VARCHAR(150),
		Title					VARCHAR(MAX),
		Accomodation			VARCHAR(150),
		Meals					VARCHAR(150),
		Transport				VARCHAR(150),
		Description				VARCHAR(150),
		Notes					VARCHAR(150),
		Status					VARCHAR(10),
		CreatedBy				VARCHAR(150),
		CreatedDate				DATETIME2,
		ModifiedBy				VARCHAR(150),
		ModifiedDate			DATETIME2
)

CREATE TABLE Package.PackageAvailabilityDetails(
		RowId					BIGINT   NOT NULL	IDENTITY(1,1),
		PackageCode				VARCHAR(150),
		Year					VARCHAR(30),
		Month					VARCHAR(50),
		AvailableDate			DATETIME2,
		PackageCost				MONEY,
		Status					VARCHAR(10),
		CreatedBy				VARCHAR(150),
		CreatedDate				DATETIME2,
		ModifiedBy				VARCHAR(150),
		ModifiedDate			DATETIME2
)

CREATE TABLE Package.InclusionExcludesPackageDetails(
		RowId					BIGINT   NOT NULL	IDENTITY(1,1),
		PackageCode				VARCHAR(150),
		IncludeExclude			VARCHAR(150),
		Description				VARCHAR(MAX),
		Status					VARCHAR(10),
		CreatedBy				VARCHAR(150),
		CreatedDate				DATETIME2,
		ModifiedBy				VARCHAR(150),
		ModifiedDate			DATETIME2
)