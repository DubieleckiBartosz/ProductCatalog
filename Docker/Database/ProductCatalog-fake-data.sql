IF NOT EXISTS(SELECT 1 FROM Products)
BEGIN
	DECLARE @counter INT; 
	SET @counter = 1;

	WHILE ( @counter <= 100)
	BEGIN
		PRINT 'The counter value is : ' + CONVERT(VARCHAR, @counter);

		DECLARE @price DECIMAL(14, 2) = CONVERT(DECIMAL(14, 2) , 10 + (1000-1) * RAND(CHECKSUM(NEWID())));

		PRINT 'Price : ' + CONVERT(VARCHAR, @price);

		INSERT INTO Products([Name], Price, Code) 
		VALUES('Product ' + CONVERT(VARCHAR, @counter), @price, CAST(NEWID() AS VARCHAR(MAX)))


		SET @counter = @counter + 1;
	END
END 