if exists( select 1 from sys. views where name= 'VW_RevIcon' )
	drop view VW_RevIcon
go
create view VW_RevIcon
as
	select iconNumber [Number],iconName [Name],[Status] from sadDTICON
go