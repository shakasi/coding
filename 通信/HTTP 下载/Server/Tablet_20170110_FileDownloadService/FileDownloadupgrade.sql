if not exists(select 1 from sysobjects where name =N'REVHotfixLog' and type=N'U')
begin
CREATE TABLE [dbo].[REVHotfixLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Project] [nvarchar](50) NULL,
	[Version] [nvarchar](50) NULL,
	[Updt] [datetime] NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[REVTempHotfixLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Project] [nvarchar](50) NULL,
	[Version] [nvarchar](50) NULL,
	[Updt] [datetime] NULL
) ON [PRIMARY]
ALTER TABLE [dbo].[REVTempHotfixLog] ADD  CONSTRAINT [DF_REVTempHotfixLog_Updt]  DEFAULT (getdate()) FOR [Updt]

end
GO

if exists(select * from sysobjects where name =N'VW_REVHotfixLog' and type=N'V')
begin
Drop View Vw_REVHotfixLog
End
Go

CREATE VIEW [dbo].[VW_REVHotfixLog]
AS
select b.Project,b.[Version],b.Updt from 
(
SELECT      Project, MAX(ID) as ID
FROM            (select * from REVHotfixLog
union 
select 2000000000 as ID,Project,[Version],Updt from REVTempHotfixLog) t
 Group by Project )a
left join 
(select * from REVHotfixLog
union 
select 2000000000 as ID,Project,[Version],Updt from REVTempHotfixLog) b on a.Project=b.Project and a.ID=b.ID
GO

insert into REVHotfixLog (Project,[Version],Updt) select Project,[Version],Updt from REVTempHotfixLog

delete REVTempHotfixLog



declare @project nvarchar(50)
declare @version nvarchar(50)
set @project='FileDownloadService'
set @version='V2.00.001.000_20170214'
if not exists (select 1 from REVTempHotfixLog where Project=@project)
begin 
insert into REVTempHotfixLog ([Project],[Version]) values (@project,@version)
end
else
begin
update REVTempHotfixLog set Updt=GETDATE(),[Version]=@version where Project=@project
end
go


declare @project nvarchar(50)
declare @version nvarchar(50)
set @project='FileDownloadService'
set @version='V2.00.001.001_20170327'
if not exists (select 1 from REVTempHotfixLog where Project=@project)
begin 
insert into REVTempHotfixLog ([Project],[Version]) values (@project,@version)
end
else
begin
update REVTempHotfixLog set Updt=GETDATE(),[Version]=@version where Project=@project
end
go
