create table Categories
(
	[CategoryId] int identity(1,1) not null primary key,
	[Name] nvarchar(50) not null,
)

create table Products
(
	InternalId int identity(1,1) not null primary key,
	ProductId nvarchar(50) not null unique,
	[Name] nvarchar(50) not null,
	[Price] money,
    CategoryId int foreign key references Categories(CategoryId)
)

go
create procedure InsertData_sp
    @Category nvarchar(50),   
    @Id nvarchar(50),
	@Name nvarchar(50),   
    @Price money
as
    if not exists(select 1 from Categories where [Name] = @Category)
	begin
		insert into Categories values (@Category)
	end
    if exists(select 1 from Products where [ProductID] = @Id)
	begin
		update Products set Price = @Price where ProductId = @id
		return 1
	end
	declare @CategoryInfo int
	set @CategoryInfo = (select CategoryId from Categories where [Name] = @Category)

	insert into Products (ProductId, [Name], [Price], [CategoryId])
	values (@Id, @Name, @Price, @CategoryInfo)
go  


select * from Products
select * from Categories
