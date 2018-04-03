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
--�û���
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
--���ű�
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
--�û����Ź�ϵ��
create TABLE ShakaUserDepartment
(
	UserId int not null,
	DepartmentId int not null
)

 if exists(select * from sysobjects where name='ShakaRole')
	drop Table ShakaRole
--��ɫ��
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
--�û���ɫ��
create TABLE ShakaUserRole
(
	UserId int,
	RoleId int
)
 
/*Ȩ��˼�룺
*     �û���Ӧ��ɫ����ɫ��Ӧ��ӦȨ�ޡ�
*     ����Ȩ�޵�����Ϊ MenuId + ButtonId
*/
 if exists(select * from sysobjects where name='ShakaMenu')
	drop Table ShakaMenu
--�˵���
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
--��ť��
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
--�û�Ȩ�ޱ�
create TABLE ShakaRoleMenuButton
(
	RoleId int,
	MenuId int,
	ButtonId int
)
go

--����
select * from ShakaUser;
insert into ShakaUser(UserId,UserName,UserPwd,UserDescription) values
('admin','��������Ա','21232F297A57A5A743894A0E4A801F','���ǳ�������Ա��!')