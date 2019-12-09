USE [G02_Test]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 05-Dec-19 3:09:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 05-Dec-19 3:09:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[Price] [money] NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
/****** Object:  StoredProcedure [dbo].[InsertData_sp]    Script Date: 05-Dec-19 3:09:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[InsertData_sp]
    @CategoryName nvarchar(50),   
	@ProductCode nvarchar(50),
	@ProductName nvarchar(50),
    @Price money,  
	@ID int output
as
begin
	if not exists(select * from Categories where CategoryName = @CategoryName)
	begin
		insert into Categories values(@CategoryName)
		set @ID= SCOPE_IDENTITY()
	end
	else
	if exists(select * from Products where Price=@Price)
	begin
		set @ID = (select CategoryID from Categories where CategoryName = @CategoryName)
		update Products 
		set Price = @Price where ProductID = @ID
		return 0
	end
	insert into Products (ProductCode, ProductName, [Price], [CategoryId])
	values (@ProductCode, @ProductName, @Price, @CategoryName)
	
	return 0
end
GO
