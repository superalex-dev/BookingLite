CREATE PROC AddorEdit
@Id INT,
@FirstName NVARCHAR(50),
@LastName NVARCHAR(50),
@Phone NVARCHAR(50),
@Email NVARCHAR(50),
@Address NVARCHAR(50)
AS
IF @Id = 0
BEGIN
INSERT INTO BookingLite (FirstName, LastName, Phone,Email,Address)
VALUES (@FirstName,@LastName,@Phone,@Email,@Address)
END
ELSE
BEGIN
UPDATE BookingLite
SET
FirstName = @FirstName,
LastName = @LastName,
Phone = @Phone,
Email = @Email,
Address = @Address
WHERE Id = @Id
END