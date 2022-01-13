CREATE PROC SearchByValue
@Value NVARCHAR(50)
AS
SELECT *
FROM BookingLite
WHERE FirstName LIKE '%' +@Value +'%' OR LastName LIKE '%' +@Value +'%' OR Email LIKE '%' + @Value +'%'