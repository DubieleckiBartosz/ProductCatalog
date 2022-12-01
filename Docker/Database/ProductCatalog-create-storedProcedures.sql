CREATE OR ALTER PROCEDURE product_createNewProduct_I
	@price DECIMAL,
	@code VARCHAR(MAX),
	@name VARCHAR(50)
AS
BEGIN
	INSERT INTO Products([Name], Price, Code)
	VALUES(@name, @price, @code)
END
GO


CREATE OR ALTER PROCEDURE product_getProductByName_S 
	@name VARCHAR(50)
AS
BEGIN 
	SELECT TOP(1) Id, Code, [Name], Price 
	FROM Products WHERE [Name] = @name
END
GO

CREATE OR ALTER PROCEDURE product_getProductsBySearch_S
	@price DECIMAL = NULL,
	@code VARCHAR(MAX) = NULL,
	@name VARCHAR(50) = NULL,
	@sortModelType VARCHAR(10) = NULL,
	@sortModelName VARCHAR(MAX) = NULL,
	@pageNumber INT = NULL,
	@pageSize INT = NULL
AS
BEGIN
	SELECT Id, Code, [Name], Price 
	FROM Products 
	WHERE (@name IS NULL OR [Name] = @name)
	AND (@price IS NULL OR Price = @price)
	AND (@code IS NULL OR Code = @code)
	ORDER BY 

	CASE WHEN @sortModelName = 'Id' AND @sortModelType = 'asc' THEN Id END ASC,  
	CASE WHEN @sortModelName = 'Id' AND @sortModelType = 'desc' THEN Id END DESC,

	CASE WHEN @sortModelName = 'Price' AND @sortModelType = 'asc' THEN Price END ASC,  
	CASE WHEN @sortModelName = 'Price' AND @sortModelType = 'desc' THEN Price END DESC,

	CASE WHEN @sortModelName = 'Name' AND @sortModelType = 'asc' THEN [Name] END ASC,  
	CASE WHEN @sortModelName = 'Name' AND @sortModelType = 'desc' THEN [Name] END DESC,

	CASE WHEN @sortModelName = 'Created' AND @sortModelType = 'asc' THEN Created END ASC,  
	CASE WHEN @sortModelName = 'Created' AND @sortModelType = 'desc' THEN Created END DESC

	OFFSET (@pageNumber - 1)* @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY
END
GO

CREATE OR ALTER PROCEDURE product_createProducts_I
	@tvp ProductsTableType READONLY
AS
BEGIN
	MERGE Products AS trg
	USING (SELECT * FROM @tvp) AS src
		ON (trg.[Name] = src.[Name])  
		WHEN NOT MATCHED THEN
		INSERT ([Name], Price, Code) VALUES (src.[Name], src.Price, src.Code);
END
GO