USE [OnlineStore]
GO
/****** Object:  StoredProcedure [dbo].[UserAccess_Create]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_Create]
    @Login NVARCHAR(50),
    @PasswordHash NVARCHAR(50),
    @Status NVARCHAR(50),
    @Role NVARCHAR(50)
AS
INSERT INTO UserAccess(UserAccess.Login, UserAccess.PasswordHash, UserAccess.Status, UserAccess.Role)
VALUES(@Login, @PasswordHash, @Status, @Role)

SELECT @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[UserAccess_Delete]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_Delete]
    @IdAccess INT

AS
DELETE FROM UserAccess WHERE UserAccess.UserAccessId = @IdAccess;

GO
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectAll]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAccess_SelectAll]
	
AS
SELECT UserAccess.UserAccessId, UserAccess.Login, UserAccess.PasswordHash, UserAccess.Status, UserAccess.Role 
FROM UserAccess 


GO
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectById]    Script Date: 20.05.2019 10:07:06 ******/
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
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectByLogin]    Script Date: 20.05.2019 10:07:06 ******/
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
/****** Object:  StoredProcedure [dbo].[UserAccess_SelectByLoginPassword]    Script Date: 20.05.2019 10:07:06 ******/
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
/****** Object:  StoredProcedure [dbo].[UserAccess_Update]    Script Date: 20.05.2019 10:07:06 ******/
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

GO
/****** Object:  StoredProcedure [dbo].[UserAuthorization_CancelSessionKey]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorization_CancelSessionKey]
	@UserId INT,
	@OldStatus NVARCHAR(50),
	@FinishSession DATETIME,
	@NewStatus NVARCHAR(50)

AS
UPDATE UserAuthorization SET 
UserAuthorization.FinishSession = @FinishSession,
UserAuthorization.Status = @NewStatus

WHERE UserAuthorization.User_UserId = @UserId AND UserAuthorization.Status = @OldStatus
GO
/****** Object:  StoredProcedure [dbo].[UserAuthorization_Delete]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorization_Delete]
  @IdAccess INT
AS
DELETE FROM UserAuthorization WHERE UserAuthorization.UserAuthorizationId = @IdAccess;
GO
/****** Object:  StoredProcedure [dbo].[UserAuthorization_Insert]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorization_Insert]
    @StartSession DATETIME,
    @FinishSession DATETIME,
    @SessionKey NVARCHAR(50),
	@Status NVARCHAR(50),
    @UserId INT
AS

INSERT INTO UserAuthorization(
	UserAuthorization.StartSession, UserAuthorization.FinishSession,
	UserAuthorization.SessionKey, UserAuthorization.Status, 
	UserAuthorization.User_UserId) 

VALUES(@StartSession, @FinishSession, @SessionKey, @Status, @UserId)
GO
/****** Object:  StoredProcedure [dbo].[UserAuthorization_SelectAll]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorization_SelectAll]

AS

SELECT 
UserAuthorization.UserAuthorizationId,  
UserAuthorization.StartSession, UserAuthorization.FinishSession, 
UserAuthorization.SessionKey, UserAuthorization.Status, UserAuthorization.User_UserId
FROM UserAuthorization




GO
/****** Object:  StoredProcedure [dbo].[UserAuthorization_SelectById]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorization_SelectById]
@IdEntity INT
   
AS

SELECT 
UserAuthorization.UserAuthorizationId,  
UserAuthorization.StartSession, UserAuthorization.FinishSession, 
UserAuthorization.SessionKey, UserAuthorization.Status, UserAuthorization.User_UserId
FROM UserAuthorization
WHERE UserAuthorization.UserAuthorizationId = @IdEntity



GO
/****** Object:  StoredProcedure [dbo].[UserAuthorization_Update]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserAuthorization_Update]
    @IdEntity INT,
	@StartSession DATETIME,
	@FinishSession DATETIME,
	@SessionKey NVARCHAR(50),
	@Status NVARCHAR(50),
	@UserId INT

AS
UPDATE UserAuthorization SET 
UserAuthorization.StartSession = @StartSession,
UserAuthorization.FinishSession = @FinishSession,
UserAuthorization.SessionKey = @SessionKey,
UserAuthorization.Status = @Status,
UserAuthorization.User_UserId = @UserId

WHERE UserAuthorization.UserAuthorizationId = @IdEntity
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_Create]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_Create]
    @FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
    @Email NVARCHAR(50),
	@Phone NVARCHAR(50),
    @UserAccess_Id INT
AS
INSERT INTO UserSystem(FirstName, LastName, Email, Phone, UserAccess_Id) 
VALUES(@FirstName, @LastName, @Email, @Phone,@UserAccess_Id)
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_Delete]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_Delete]
    @IdUser INT
AS
DELETE FROM UserSystem WHERE UserSystem.UserId = @IdUser;
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_SelectAll]    Script Date: 20.05.2019 10:07:06 ******/
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
	 UserSystem.UserAccess_Id
	FROM UserSystem
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_SelectById]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_SelectById]
    @IdUser INT
AS

SELECT  
	UserSystem.UserId, 
	UserSystem.FirstName, UserSystem.LastName, 
	UserSystem.Email, UserSystem.Phone, 
	UserSystem.UserAccess_Id
	FROM UserSystem
	WHERE UserSystem.UserId = @IdUser
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_SelectByLoginPassword]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserSystem_SelectByLoginPassword]
    @Login NVARCHAR(50),
    @PasswordHash NVARCHAR(50)
AS

SELECT  
	UserSystem.UserId, UserSystem.FirstName, UserSystem.LastName, UserSystem.Email, UserSystem.Phone, UserSystem.UserAccess_Id
	FROM UserSystem
	JOIN UserAccess ON  UserSystem.UserAccess_Id = UserAccess.UserAccessId 
	WHERE UserAccess.Login = @Login AND UserAccess.PasswordHash = @PasswordHash
GO
/****** Object:  StoredProcedure [dbo].[UserSystem_Update]    Script Date: 20.05.2019 10:07:06 ******/
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
    @UserAccess_Id INT
AS
UPDATE UserSystem SET 
UserSystem.FirstName = @FirstName,
UserSystem.LastName = @LastName,
UserSystem.Email = @Email,
UserSystem.Phone = @Phone,
UserSystem.UserAccess_Id = @UserAccess_Id

WHERE UserSystem.UserId = @IdEntity
GO
/****** Object:  Table [dbo].[Product]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[ProductList_ProductListId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 20.05.2019 10:07:06 ******/
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
/****** Object:  Table [dbo].[ProductList]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductList](
	[ProductListId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ImageLocalSource] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ProductList] PRIMARY KEY CLUSTERED 
(
	[ProductListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductList_ProductCategory]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductList_ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategory_ProductCategoryId] [int] NOT NULL,
	[ProductList_ProductListId] [int] NOT NULL,
 CONSTRAINT [PK_Product_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserAccess]    Script Date: 20.05.2019 10:07:06 ******/
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
/****** Object:  Table [dbo].[UserAuthorization]    Script Date: 20.05.2019 10:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAuthorization](
	[UserAuthorizationId] [int] IDENTITY(1,1) NOT NULL,
	[StartSession] [datetime] NOT NULL,
	[FinishSession] [datetime] NOT NULL,
	[SessionKey] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[User_UserId] [int] NOT NULL,
 CONSTRAINT [PK_AuthorizationUser] PRIMARY KEY CLUSTERED 
(
	[UserAuthorizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserOrder]    Script Date: 20.05.2019 10:07:06 ******/
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
/****** Object:  Table [dbo].[UserOrder_Product]    Script Date: 20.05.2019 10:07:06 ******/
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
/****** Object:  Table [dbo].[UserSystem]    Script Date: 20.05.2019 10:07:06 ******/
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
	[UserAccess_Id] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductList] FOREIGN KEY([ProductList_ProductListId])
REFERENCES [dbo].[ProductList] ([ProductListId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductList]
GO
ALTER TABLE [dbo].[ProductList_ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductList_ProductCategory_ProductCategory] FOREIGN KEY([ProductCategory_ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
GO
ALTER TABLE [dbo].[ProductList_ProductCategory] CHECK CONSTRAINT [FK_ProductList_ProductCategory_ProductCategory]
GO
ALTER TABLE [dbo].[ProductList_ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductList_ProductCategory_ProductList] FOREIGN KEY([ProductList_ProductListId])
REFERENCES [dbo].[ProductList] ([ProductListId])
GO
ALTER TABLE [dbo].[ProductList_ProductCategory] CHECK CONSTRAINT [FK_ProductList_ProductCategory_ProductList]
GO
ALTER TABLE [dbo].[UserAuthorization]  WITH CHECK ADD  CONSTRAINT [FK_AuthorizationUser_User] FOREIGN KEY([User_UserId])
REFERENCES [dbo].[UserSystem] ([UserId])
GO
ALTER TABLE [dbo].[UserAuthorization] CHECK CONSTRAINT [FK_AuthorizationUser_User]
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
ALTER TABLE [dbo].[UserSystem]  WITH CHECK ADD  CONSTRAINT [FK_User_UserAccess] FOREIGN KEY([UserAccess_Id])
REFERENCES [dbo].[UserAccess] ([UserAccessId])
GO
ALTER TABLE [dbo].[UserSystem] CHECK CONSTRAINT [FK_User_UserAccess]
GO
