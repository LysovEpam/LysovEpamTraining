USE [OnlineStore]
GO
/****** Object:  StoredProcedure [dbo].[Product_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_Delete]
   @IdEntity INT
AS
DELETE FROM Product WHERE Product.ProductId = @IdEntity;

SELECT @@ROWCOUNT AS 'rows delete'
GO
/****** Object:  StoredProcedure [dbo].[Product_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_Insert]
    @Price DECIMAL,
    @Status NVARCHAR(50),
    @ProductInformationId INT
AS

INSERT INTO Product(Price, Status, ProductInformation_ProductInformationId) 
VALUES(@Price, @Status, @ProductInformationId)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[Product_SelectAll]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_SelectAll]
   
AS

SELECT Product.ProductId, Product.Price, Product.Status, Product.ProductInformation_ProductInformationId
FROM Product

GO
/****** Object:  StoredProcedure [dbo].[Product_SelectById]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_SelectById]
    @IdEntity INT
AS

SELECT Product.ProductId, Product.Price, Product.Status, Product.ProductInformation_ProductInformationId
FROM Product
WHERE Product.ProductId = @IdEntity


GO
/****** Object:  StoredProcedure [dbo].[Product_Update]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Product_Update]
	@IdEntity INT,
    @Price DECIMAL,
    @Status NVARCHAR(50),
    @ProductInformationId INT
AS

UPDATE Product SET
Product.Price = @Price,
Product.Status = @Status,
Product.ProductInformation_ProductInformationId = @ProductInformationId
WHERE Product.ProductId = @IdEntity

SELECT @@ROWCOUNT AS 'rows update'

GO
/****** Object:  StoredProcedure [dbo].[ProductCategory_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductCategory_Delete]
   @IdEntity INT
AS
DELETE FROM ProductCategory WHERE ProductCategory.ProductCategoryId = @IdEntity;

SELECT @@ROWCOUNT AS 'rows delete'

GO
/****** Object:  StoredProcedure [dbo].[ProductCategory_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductCategory_Insert]
    @CategoryName NVARCHAR(50),
    @Description NVARCHAR(500)
AS
INSERT INTO ProductCategory(CategoryName, Description) 
VALUES(@CategoryName, @Description)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[ProductCategory_SelectAll]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductCategory_SelectAll]
	
AS
SELECT ProductCategory.ProductCategoryId, 
	ProductCategory.CategoryName, 
	ProductCategory.Description
FROM ProductCategory

GO
/****** Object:  StoredProcedure [dbo].[ProductCategory_SelectById]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductCategory_SelectById]
	@IdEntity INT
AS
SELECT ProductCategory.ProductCategoryId, 
	ProductCategory.CategoryName, 
	ProductCategory.Description
FROM ProductCategory 
WHERE ProductCategory.ProductCategoryId = @IdEntity

GO
/****** Object:  StoredProcedure [dbo].[ProductCategory_Update]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductCategory_Update]
	@IdEntity INT,
    @CategoryName NVARCHAR(50),
    @Description NVARCHAR(500)
AS
UPDATE ProductCategory SET 
	ProductCategory.CategoryName = @CategoryName,
	ProductCategory.Description = @Description

WHERE ProductCategory.ProductCategoryId = @IdEntity

SELECT @@ROWCOUNT AS 'rows update'

GO
/****** Object:  StoredProcedure [dbo].[ProductInformation_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductInformation_Delete]
    @IdEntity INT
AS
DELETE FROM ProductInformation WHERE ProductInformation.ProductInformationId = @IdEntity;

SELECT @@ROWCOUNT AS 'rows delete'

GO
/****** Object:  StoredProcedure [dbo].[ProductInformation_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductInformation_Insert]
    @ProductName NVARCHAR(50),
    @ImageLocalSource NVARCHAR(100),
	@Description NVARCHAR(500)
AS
INSERT INTO ProductInformation(ProductInformation.ProductName, 
ProductInformation.ImageLocalSource, ProductInformation.Description) 
VALUES(@ProductName, @ImageLocalSource, @Description)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[ProductInformation_SelectAll]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductInformation_SelectAll]

AS
SELECT ProductInformation.ProductInformationId,  
ProductInformation.ProductName,
ProductInformation.ImageLocalSource,
ProductInformation.Description,
ProductInformation_ProductCategory.ProductCategory_ProductCategoryId


FROM  ProductInformation
	INNER JOIN ProductInformation_ProductCategory 
	ON ProductInformation_ProductCategory.ProductInformation_ProductInformationId = ProductInformation.ProductInformationId
 
  
	ORDER BY ProductInformation_ProductCategory.ProductInformation_ProductInformationId
GO
/****** Object:  StoredProcedure [dbo].[ProductInformation_SelectById]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductInformation_SelectById]
    @IdEntity INT
AS

SELECT ProductInformation.ProductInformationId,  
ProductInformation.ProductName,
ProductInformation.ImageLocalSource,
ProductInformation.Description,
ProductInformation_ProductCategory.ProductCategory_ProductCategoryId


FROM ProductInformation_ProductCategory
  JOIN ProductInformation 
  ON ProductInformation_ProductCategory.ProductInformation_ProductInformationId = 
  ProductInformation.ProductInformationId
  WHERE ProductInformation.ProductInformationId = @IdEntity

  
  ORDER BY ProductInformation_ProductCategory.ProductInformation_ProductInformationId
GO
/****** Object:  StoredProcedure [dbo].[ProductInformation_Update]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductInformation_Update]
	@IdEntity INT,
	@ProductName NVARCHAR(50),
	@ImageLocalSource NVARCHAR(100),
	@Description NVARCHAR(500)
AS

UPDATE  ProductInformation SET 
ProductInformation.ProductName = @ProductName,
ProductInformation.ImageLocalSource = @ImageLocalSource,
ProductInformation.Description = @Description


WHERE ProductInformation.ProductInformationId = @IdEntity

SELECT @@ROWCOUNT AS 'rows update'


GO
/****** Object:  StoredProcedure [dbo].[ProductInformationProductCategory_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductInformationProductCategory_Delete]
    @IdProductInformation INT,
	@IdCategory INT
AS

DELETE FROM ProductInformation_ProductCategory 
WHERE ProductInformation_ProductCategory.ProductInformation_ProductInformationId = @IdProductInformation AND
ProductInformation_ProductCategory.ProductCategory_ProductCategoryId = @IdCategory;

SELECT @@ROWCOUNT AS 'rows delete'

GO
/****** Object:  StoredProcedure [dbo].[ProductInformationProductCategory_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProductInformationProductCategory_Insert]
    @ProductCategoryId INT,
    @ProductInformationId INT
AS
INSERT INTO ProductInformation_ProductCategory(
ProductInformation_ProductCategory.ProductCategory_ProductCategoryId,
ProductInformation_ProductCategory.ProductInformation_ProductInformationId)
VALUES(@ProductCategoryId, @ProductInformationId)



GO
/****** Object:  StoredProcedure [dbo].[UserAccess_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_Delete]
    @IdEntity INT

AS
DELETE FROM UserAccess WHERE UserAccess.UserAccessId = @IdEntity;

SELECT @@ROWCOUNT AS 'rows delete'

GO
/****** Object:  StoredProcedure [dbo].[UserAccess_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_Insert]
    @Login NVARCHAR(50),
    @PasswordHash NVARCHAR(50),
    @Status NVARCHAR(50),
    @Role NVARCHAR(50)
AS
INSERT INTO UserAccess(UserAccess.Login, UserAccess.PasswordHash, UserAccess.Status, UserAccess.Role)
VALUES(@Login, @PasswordHash, @Status, @Role)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectAll]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_SelectAll]
	
AS
SELECT UserAccess.UserAccessId, UserAccess.Login, UserAccess.PasswordHash, UserAccess.Status, UserAccess.Role 
FROM UserAccess 


GO
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectById]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_SelectById]
	@IdEntity INT
   
AS
SELECT UserAccess.UserAccessId, UserAccess.Login, UserAccess.PasswordHash, UserAccess.Status, UserAccess.Role 
FROM UserAccess 
WHERE UserAccess.UserAccessId = @IdEntity


GO
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectByLogin]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_SelectByLogin]
    @Login NVARCHAR(50)
    
AS
SELECT UserAccess.UserAccessId, UserAccess.Login, UserAccess.PasswordHash, UserAccess.Status, UserAccess.Role 
FROM UserAccess 
WHERE UserAccess.Login = @Login
GO
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectByLoginPassword]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_SelectByLoginPassword]
    @Login NVARCHAR(50),
    @PasswordHash NVARCHAR(50)

AS
SELECT UserAccess.UserAccessId, UserAccess.Login, UserAccess.PasswordHash, UserAccess.Status, UserAccess.Role 
FROM UserAccess 
WHERE UserAccess.Login = @Login AND UserAccess.PasswordHash = @PasswordHash
GO
/****** Object:  StoredProcedure [dbo].[UserAccess_Update]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_Update]
	@IdEntity INT,
    @Login NVARCHAR(50),
    @PasswordHash NVARCHAR(50),
    @Status NVARCHAR(50),
    @Role NVARCHAR(50)
AS
UPDATE UserAccess SET 
UserAccess.Login = @Login, UserAccess.PasswordHash = @PasswordHash, UserAccess.Status = @Status, UserAccess.Role = @Role
WHERE UserAccess.UserAccessId = @IdEntity

SELECT @@ROWCOUNT AS 'rows update'

GO
/****** Object:  StoredProcedure [dbo].[UserAuthorizationToken_CancelSessionKey]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorizationToken_CancelSessionKey]
	@UserId INT,
	@OldStatus NVARCHAR(50),
	@FinishSession DATETIME,
	@NewStatus NVARCHAR(50)

AS
UPDATE UserAuthorizationToken SET 
UserAuthorizationToken.FinishSession = @FinishSession,
UserAuthorizationToken.Status = @NewStatus

WHERE UserAuthorizationToken.User_UserId = @UserId AND UserAuthorizationToken.Status = @OldStatus
GO
/****** Object:  StoredProcedure [dbo].[UserAuthorizationToken_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorizationToken_Delete]
  @IdEntity INT
AS
DELETE FROM UserAuthorizationToken WHERE UserAuthorizationToken.UserAuthorizationId = @IdEntity;

SELECT @@ROWCOUNT AS 'rows delete'
GO
/****** Object:  StoredProcedure [dbo].[UserAuthorizationToken_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorizationToken_Insert]
    @StartSession DATETIME,
    @FinishSession DATETIME,
    @UserToken NVARCHAR(50),
	@Status NVARCHAR(50),
    @UserId INT
AS

INSERT INTO UserAuthorizationToken(
	UserAuthorizationToken.StartSession, UserAuthorizationToken.FinishSession,
	UserAuthorizationToken.UserToken, UserAuthorizationToken.Status, 
	UserAuthorizationToken.User_UserId) 
VALUES(@StartSession, @FinishSession, @UserToken, @Status, @UserId)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[UserAuthorizationToken_SelectAll]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorizationToken_SelectAll]

AS

SELECT 
UserAuthorizationToken.UserAuthorizationId,  
UserAuthorizationToken.StartSession, UserAuthorizationToken.FinishSession, 
UserAuthorizationToken.UserToken, UserAuthorizationToken.Status, UserAuthorizationToken.User_UserId
FROM UserAuthorizationToken




GO
/****** Object:  StoredProcedure [dbo].[UserAuthorizationToken_SelectById]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorizationToken_SelectById]
@IdEntity INT
   
AS

SELECT 
UserAuthorizationToken.UserAuthorizationId,  
UserAuthorizationToken.StartSession, UserAuthorizationToken.FinishSession, 
UserAuthorizationToken.UserToken, UserAuthorizationToken.Status, UserAuthorizationToken.User_UserId
FROM UserAuthorizationToken
WHERE UserAuthorizationToken.UserAuthorizationId = @IdEntity



GO
/****** Object:  StoredProcedure [dbo].[UserAuthorizationToken_SelectByToken]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorizationToken_SelectByToken]
    @UserToken NVARCHAR(50)
AS

SELECT UserAuthorizationToken.UserAuthorizationId, 
UserAuthorizationToken.StartSession, UserAuthorizationToken.FinishSession,
UserAuthorizationToken.UserToken, UserAuthorizationToken.Status,
UserAuthorizationToken.User_UserId FROM UserAuthorizationToken
GO
/****** Object:  StoredProcedure [dbo].[UserAuthorizationToken_Update]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorizationToken_Update]
    @IdEntity INT,
	@StartSession DATETIME,
	@FinishSession DATETIME,
	@UserToken NVARCHAR(50),
	@Status NVARCHAR(50),
	@UserId INT

AS
UPDATE UserAuthorizationToken SET 
UserAuthorizationToken.StartSession = @StartSession,
UserAuthorizationToken.FinishSession = @FinishSession,
UserAuthorizationToken.UserToken = @UserToken,
UserAuthorizationToken.Status = @Status,
UserAuthorizationToken.User_UserId = @UserId
WHERE UserAuthorizationToken.UserAuthorizationId = @IdEntity

SELECT @@ROWCOUNT AS 'rows update'
GO
/****** Object:  StoredProcedure [dbo].[UserOrder_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserOrder_Delete]
	 @IdEntity INT
AS
DELETE FROM UserOrder WHERE UserOrder.UserOrderId = @IdEntity;

SELECT @@ROWCOUNT AS 'rows delete'



GO
/****** Object:  StoredProcedure [dbo].[UserOrder_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserOrder_Insert]
    @DateOrder DATETIME,
    @Address NVARCHAR(500),
	@Status NVARCHAR(500),
    @UserId INT
AS

INSERT INTO UserOrder(UserOrder.DateOrder, UserOrder.Address, UserOrder.Status, UserOrder.User_UserId)
VALUES(@DateOrder, @Address, @Status, @UserId)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[UserOrder_SelectAll]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserOrder_SelectAll]
	
AS

SELECT UserOrder.UserOrderId,  
UserOrder.DateOrder,
UserOrder.Address,
UserOrder.Status,
UserOrder.User_UserId,
UserOrder_Product.Product_ProductId


FROM UserOrder_Product
  JOIN UserOrder 
  ON UserOrder_Product.UserOrder_UserOrderId = 
  UserOrder.UserOrderId
  
  ORDER BY UserOrder_Product.UserOrder_UserOrderId



GO
/****** Object:  StoredProcedure [dbo].[UserOrder_SelectById]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserOrder_SelectById]
	 @IdEntity INT
AS

SELECT UserOrder.UserOrderId,  
UserOrder.DateOrder,
UserOrder.Address,
UserOrder.Status,
UserOrder.User_UserId,
UserOrder_Product.Product_ProductId


FROM UserOrder_Product
  JOIN UserOrder 
  ON UserOrder_Product.UserOrder_UserOrderId = 
  UserOrder.UserOrderId
  WHERE UserOrder.UserOrderId = @IdEntity

  
  ORDER BY UserOrder_Product.UserOrder_UserOrderId



GO
/****** Object:  StoredProcedure [dbo].[UserOrder_Update]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserOrder_Update]
	@IdEntity INT,
    @DateOrder DATETIME,
    @Address NVARCHAR(500),
	@Status NVARCHAR(50),
    @UserId INT
AS

UPDATE  UserOrder SET 
UserOrder.DateOrder = @DateOrder,
UserOrder.Address = @Address,
UserOrder.Status = @Status,
UserOrder.User_UserId = @UserId


WHERE UserOrder.UserOrderId = @IdEntity

SELECT @@ROWCOUNT AS 'rows update'


GO
/****** Object:  StoredProcedure [dbo].[UserOrderProduct_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserOrderProduct_Delete]
	@UserOrderId INT,
	@ProductId INT
AS

DELETE FROM UserOrder_Product 
WHERE UserOrder_Product.UserOrder_UserOrderId = @UserOrderId AND
UserOrder_Product.Product_ProductId = @ProductId;

SELECT @@ROWCOUNT AS 'rows delete'

GO
/****** Object:  StoredProcedure [dbo].[UserOrderProduct_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserOrderProduct_Insert]
	@UserOrderId INT,
	@ProductId INT
AS

INSERT INTO UserOrder_Product(UserOrder_Product.UserOrder_UserOrderId, UserOrder_Product.Product_ProductId) 
VALUES (@UserOrderId, @ProductId)

GO
/****** Object:  StoredProcedure [dbo].[UserSystem_Delete]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_Delete]
    @IdEntity INT
AS
DELETE FROM UserSystem WHERE UserSystem.UserId = @IdEntity;

SELECT @@ROWCOUNT AS 'rows delete'
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_Insert]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_Insert]
    @FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
    @Email NVARCHAR(50),
	@Phone NVARCHAR(50),
    @UserAccessId INT
AS
INSERT INTO UserSystem(FirstName, LastName, Email, Phone, UserAccessId) 
VALUES(@FirstName, @LastName, @Email, @Phone,@UserAccessId)

SELECT @@IDENTITY
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_SelectAll]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_SelectAll]
   
AS
SELECT  
	UserSystem.UserId,
	 UserSystem.FirstName, UserSystem.LastName, 
	 UserSystem.Email, UserSystem.Phone, 
	 UserSystem.UserAccessId
	FROM UserSystem
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_SelectById]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_SelectById]
    @IdEntity INT
AS

SELECT  
	UserSystem.UserId, 
	UserSystem.FirstName, UserSystem.LastName, 
	UserSystem.Email, UserSystem.Phone, 
	UserSystem.UserAccessId
	FROM UserSystem
	WHERE UserSystem.UserId = @IdEntity
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_SelectByLoginPassword]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_SelectByLoginPassword]
    @Login NVARCHAR(50),
    @PasswordHash NVARCHAR(50)
AS

SELECT  
	UserSystem.UserId, UserSystem.FirstName, UserSystem.LastName, UserSystem.Email, UserSystem.Phone, UserSystem.UserAccessId
	FROM UserSystem
	JOIN UserAccess ON  UserSystem.UserAccessId = UserAccess.UserAccessId 
	WHERE UserAccess.Login = @Login AND UserAccess.PasswordHash = @PasswordHash
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_Update]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_Update]
	@IdEntity INT,
    @FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
    @Email NVARCHAR(50),
	@Phone NVARCHAR(50),
    @UserAccessId INT
AS
UPDATE UserSystem SET 
UserSystem.FirstName = @FirstName,
UserSystem.LastName = @LastName,
UserSystem.Email = @Email,
UserSystem.Phone = @Phone,
UserSystem.UserAccessId = @UserAccessId
WHERE UserSystem.UserId = @IdEntity

SELECT @@ROWCOUNT AS 'rows update'
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[ProductInformation_ProductInformationId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductInformation]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInformation](
	[ProductInformationId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ImageLocalSource] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ProductList] PRIMARY KEY CLUSTERED 
(
	[ProductInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductInformation_ProductCategory]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductInformation_ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategory_ProductCategoryId] [int] NOT NULL,
	[ProductInformation_ProductInformationId] [int] NOT NULL,
 CONSTRAINT [PK_Product_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAccess]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccess](
	[UserAccessId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserAccess] PRIMARY KEY CLUSTERED 
(
	[UserAccessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAuthorizationToken]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthorizationToken](
	[UserAuthorizationId] [int] IDENTITY(1,1) NOT NULL,
	[StartSession] [datetime] NOT NULL,
	[FinishSession] [datetime] NOT NULL,
	[UserToken] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[User_UserId] [int] NOT NULL,
 CONSTRAINT [PK_AuthorizationUser] PRIMARY KEY CLUSTERED 
(
	[UserAuthorizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserOrder]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOrder](
	[UserOrderId] [int] IDENTITY(1,1) NOT NULL,
	[DateOrder] [datetime] NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[User_UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserOrder] PRIMARY KEY CLUSTERED 
(
	[UserOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserOrder_Product]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOrder_Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserOrder_UserOrderId] [int] NOT NULL,
	[Product_ProductId] [int] NOT NULL,
 CONSTRAINT [PK_UserOrder_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSystem]    Script Date: 12.06.2019 11:59:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSystem](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[UserAccessId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductList] FOREIGN KEY([ProductInformation_ProductInformationId])
REFERENCES [dbo].[ProductInformation] ([ProductInformationId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductList]
GO
ALTER TABLE [dbo].[ProductInformation_ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductList_ProductCategory_ProductCategory] FOREIGN KEY([ProductCategory_ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
GO
ALTER TABLE [dbo].[ProductInformation_ProductCategory] CHECK CONSTRAINT [FK_ProductList_ProductCategory_ProductCategory]
GO
ALTER TABLE [dbo].[ProductInformation_ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductList_ProductCategory_ProductList] FOREIGN KEY([ProductInformation_ProductInformationId])
REFERENCES [dbo].[ProductInformation] ([ProductInformationId])
GO
ALTER TABLE [dbo].[ProductInformation_ProductCategory] CHECK CONSTRAINT [FK_ProductList_ProductCategory_ProductList]
GO
ALTER TABLE [dbo].[UserAuthorizationToken]  WITH CHECK ADD  CONSTRAINT [FK_AuthorizationUser_User] FOREIGN KEY([User_UserId])
REFERENCES [dbo].[UserSystem] ([UserId])
GO
ALTER TABLE [dbo].[UserAuthorizationToken] CHECK CONSTRAINT [FK_AuthorizationUser_User]
GO
ALTER TABLE [dbo].[UserOrder]  WITH CHECK ADD  CONSTRAINT [FK_UserOrder_User] FOREIGN KEY([User_UserId])
REFERENCES [dbo].[UserSystem] ([UserId])
GO
ALTER TABLE [dbo].[UserOrder] CHECK CONSTRAINT [FK_UserOrder_User]
GO
ALTER TABLE [dbo].[UserOrder_Product]  WITH CHECK ADD  CONSTRAINT [FK_UserOrder_Product_Product] FOREIGN KEY([Product_ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[UserOrder_Product] CHECK CONSTRAINT [FK_UserOrder_Product_Product]
GO
ALTER TABLE [dbo].[UserOrder_Product]  WITH CHECK ADD  CONSTRAINT [FK_UserOrder_Product_UserOrder] FOREIGN KEY([UserOrder_UserOrderId])
REFERENCES [dbo].[UserOrder] ([UserOrderId])
GO
ALTER TABLE [dbo].[UserOrder_Product] CHECK CONSTRAINT [FK_UserOrder_Product_UserOrder]
GO
ALTER TABLE [dbo].[UserSystem]  WITH CHECK ADD  CONSTRAINT [FK_User_UserAccess] FOREIGN KEY([UserAccessId])
REFERENCES [dbo].[UserAccess] ([UserAccessId])
GO
ALTER TABLE [dbo].[UserSystem] CHECK CONSTRAINT [FK_User_UserAccess]
GO
