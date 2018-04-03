use master
go

if exists(Select * from sysdatabases where name='YeZiLiu')
	drop DATABASE YeZiLiu
go

Create DATABASE YeZiLiu
ON PRIMARY
(
	NAME='YeZiDan_Data',
	FILENAME='E:\Database\YeZiLiu.mdf',
	size=100MB,
	MAXSIZE=1000MB,
	FILEGROWTH=10MB
)
LOG ON
(
	NAME='YeZiDan_LOG',
	FILENAME='E:\Database\YeZiLiu.ldf',
	size=10MB,
	MAXSIZE=200MB,
	FILEGROWTH=5%
)
GO

use YeZiLiu
go

if exists(select * from sysObjects where name='ShakaUser')
	drop Table ShakaUser
--用户表
CREATE TABLE ShakaUser
(
	UserId nvarchar(50) primary key,
	UserName nvarchar(50) not null,
	UserPwd nvarchar(50),
	IsAble bit default 0,
	IfChangePwd bit default 0,
	AddDate datetime default getdate(),
	UserDescription nvarchar(200)
)

if exists(select * from sysobjects where name='ShakaDepartment')
	drop Table ShakaDepartment
--部门表
create TABLE ShakaDepartment
(
	Id int IDENTITY(1,1) primary key,
	DepartmentName nvarchar(50) not null,
	ParentId int,
	Sort int,
	AddDate datetime default getdate()
)

 if exists(select * from sysobjects where name='ShakaUserDepartment')
	drop Table ShakaUserDepartment
--用户部门关系表
create TABLE ShakaUserDepartment
(
	UserId int not null,
	DepartmentId int not null
)

 if exists(select * from sysobjects where name='ShakaRole')
	drop Table ShakaRole
--角色表
create TABLE ShakaRole
(
	Id int IDENTITY(1,1) primary key,
	RoleName nvarchar(50) not null,
	RoleDescription nvarchar(100),
	AddDate datetime default getdate(),
	ModifyDate datetime default getdate()
)

 if exists(select * from sysobjects where name='ShakaUserRole')
	drop Table ShakaUserRole
--用户角色表
create TABLE ShakaUserRole
(
	UserId int,
	RoleId int
)
 
/*权限思想：
*     用户对应角色，角色对应对应权限。
*     其中权限的内容为 MenuId + ButtonId
*/
 if exists(select * from sysobjects where name='ShakaMenu')
	drop Table ShakaMenu
--菜单表
create TABLE ShakaMenu
(
	Id int IDENTITY(1,1) primary key,
	MenuName nvarchar(50) not null,
	ParentId int,
	LinkAddress nvarchar(100),
	Icon nvarchar(50),
	Sort int,
	AddDate datetime default getdate()
)

 if exists(select * from sysobjects where name='ShakaButton')
	drop Table ShakaButton
--按钮表
create TABLE ShakaButton
(
	Id int IDENTITY(1,1) primary key,
	ButtonName nvarchar(50) not null,
	Icon nvarchar(50),
	Sort int,
	AddDate datetime default getdate()
)

 if exists(select * from sysobjects where name='ShakaRoleMenuButton')
	drop Table ShakaRoleMenuButton
--用户权限表
create TABLE ShakaRoleMenuButton
(
	RoleId int,
	MenuId int,
	ButtonId int
)
go

--测试
select * from ShakaUser;
insert into ShakaUser(UserId,UserName,UserPwd,UserDescription) values
('admin','超级管理员','21232F297A57A5A743894A0E4A801F','我是超级管理员啊!')