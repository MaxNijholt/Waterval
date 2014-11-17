Create Table Block
(
Block_ID int identity(1,1),
Title nvarchar(255),
isDeleted bit default 0 not null,
DeleteDate datetime,
primary key(Block_ID
))