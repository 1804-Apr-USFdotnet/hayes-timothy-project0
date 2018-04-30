create database Anything;
go

use Anything;
go

create schema Products;
go

create table Products.Products
(
[ID] int primary key identity(1,1),
[Name] nvarchar(50) not null,
[Price] money not null
);

create table Products.Customers
(
[ID] int primary key identity(1,1),
[Firstname] nvarchar(50) not null,
[Lastname] nvarchar(50) not null,
[CardNumber] nchar(16) not null
);

create table Products.Orders
(
[ID] int unique identity(1,1),
[ProductID] int foreign key references Products.Products(ID),
[CustomerID] int foreign key references Products.Customers(ID)
);

insert into Products.Products ([Name], [Price])
values ('Laptop', $300.00), ('Chair', $50.00), 
       ('Chocolate Bar', $3.00), ('Car', $1500.00), ('iPhone', $200.00);

insert into Products.Customers ([Firstname], [Lastname], [CardNumber])
values ('Tina', 'Smith', '1234567890123456'), ('Tina', 'Johnson', '1111111111111111'),
       ('James', 'Maddison', '2222222222222222'), ('Alex', 'Lloyd', '3333333333333333');

insert into Products.Orders ([ProductID], [CustomerID])
values (4,1), (3,1), (1,1), (2,1), (1,2), (5,3), (5,1), (4,4), (3,4), (5,4), (2,4);

select * from Products.Customers;
select * from Products.Orders;
select * from Products.Products;

select Orders.[ID], Products.[Name], 
       Customers.[Firstname], Customers.[Lastname]
from (Products.Orders
join Products.Customers
on Orders.[CustomerID] = Customers.[ID])
join Products.Products
on Orders.[ProductID] = Products.[ID]
where Customers.[Firstname] = 'Tina' and 
      Customers.[Lastname] = 'Smith';

select sum(iPhones.[Price]) as [SUM_iPhones]
from 
(select Products.[ID], Products.[Name], Products.[Price]
from Products.Products
where Products.[Name] = 'iPhone') as [iPhones]
join Products.Orders
on [iPhones].[ID] = Orders.[ProductID];

update Products.Products
set [Price] = $250.00
where [Name] = 'iPhone';

select * from Products.Customers;
select * from Products.Orders;
select * from Products.Products;
