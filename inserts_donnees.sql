
insert into [aspnet-Vidly-20180613032318].[dbo].[Movies]
  values('Hangover', Convert(datetime, '01/12/2015', 103), getdate(), 10, 1)

   insert into [aspnet-Vidly-20180613032318].[dbo].[Movies]
  values('Die Hard', Convert(datetime, '01/12/2016', 103), getdate(), 5, 2)

   insert into [aspnet-Vidly-20180613032318].[dbo].[Movies]
  values('The Terminator', Convert(datetime, '01/12/2018', 103), getdate(), 15, 2)

   insert into [aspnet-Vidly-20180613032318].[dbo].[Movies]
  values('Toy Story', Convert(datetime, '01/12/2017', 103), getdate(), 1, 3)

  insert into [aspnet-Vidly-20180613032318].[dbo].[Movies]
  values('Titanic', Convert(datetime, '01/12/1999', 103), getdate(), 2, 4)

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [Id]
      ,[Title]
      ,[ReleaseDate]
      ,[DateAdded]
      ,[NumberInStock]
      ,[Genre_Id]
  FROM [aspnet-Vidly-20180613032318].[dbo].[Movies]

  
