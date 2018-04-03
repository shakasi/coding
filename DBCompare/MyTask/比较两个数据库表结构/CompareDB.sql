use master
go

if exists(Select * from sysdatabases where name='DBTest1')
	drop DATABASE DBTest1
go

Create DATABASE DBTest1
ON PRIMARY
(
	NAME='DBTest1_Data',
	FILENAME='e:\Database\DBTest1.mdf',
	size=100MB,
	MAXSIZE=1000MB,
	FILEGROWTH=10MB
)
LOG ON
(
	NAME='DBTest1_LOG',
	FILENAME='e:\Database\DBTest1.ldf',
	size=100MB,
	MAXSIZE=500MB,
	FILEGROWTH=5%
)
GO

use DBTest1
go

if exists(select * from sysObjects where name='MESUSER')
	drop Table MESUSER
--用户表
CREATE TABLE MESUSER
(
	UserCode nvarchar(10) primary key,
	UserName nvarchar(50) not null,
	UserPwd nvarchar(50) not null,
	Remark nvarchar(200)
)
go
if exists(select * from sysobjects where name='MESDPT')
	drop Table MESDPT
--部门表
create TABLE MESDPT
(
	Id varchar(32) primary key,
	DPTCode nvarchar(6)  not null unique,
	DPTName nvarchar(50) not null unique
)
go

-------------------------------------------------------------------------------------------------
use master
go

if exists(Select * from sysdatabases where name='DBTest2')
	drop DATABASE DBTest2
go

Create DATABASE DBTest2
ON PRIMARY
(
	NAME='DBTest2_Data',
	FILENAME='e:\Database\DBTest2.mdf',
	size=100MB,
	MAXSIZE=1000MB,
	FILEGROWTH=10MB
)
LOG ON
(
	NAME='DBTest2_LOG',
	FILENAME='e:\Database\DBTest2.ldf',
	size=100MB,
	MAXSIZE=500MB,
	FILEGROWTH=5%
)
GO

use DBTest2
go

if exists(select * from sysObjects where name='MESUSER')
	drop Table MESUSER
--用户表
CREATE TABLE MESUSER
(
	UserCode nvarchar(11) primary key,--长度长1
	UserName nvarchar(50),--可空了
	UserPwd int null,--变成int了，还可空
	Remark nvarchar(200)
)
go
if exists(select * from sysobjects where name='MESRole')
	drop Table MESRole
--角色表
create TABLE MESRole
(
	Id varchar(32) primary key,
	DPTCode nvarchar(6)  not null unique,
	DPTName nvarchar(50) not null unique
)
go