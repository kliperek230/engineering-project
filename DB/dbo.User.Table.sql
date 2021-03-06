USE [FOODMATE]
GO
/****** Object:  Table [dbo].[User]    Script Date: 04.04.2020 11:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](20) NOT NULL,
	[last_name] [nvarchar](20) NOT NULL,
	[sex] [nvarchar](1) NOT NULL,
	[birth_date] [date] NOT NULL,
	[u_height] [int] NOT NULL,
	[u_weight] [int] NOT NULL,
	[email] [nvarchar](100) NULL,
	[password] [nvarchar](100) NULL,
	[username] [nvarchar](20) NOT NULL,
	[kcal] [float] NULL,
	[protein] [float] NULL,
	[carbs] [float] NULL,
	[fats] [float] NULL,
	[bench] [int] NULL,
	[ohp] [int] NULL,
	[squat] [int] NULL,
	[deadlift] [int] NULL,
	[measurements] [int] NULL,
	[Token] [nvarchar](20) NULL,
	[Role] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([bench])
REFERENCES [dbo].[Lifts] ([lift_id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([deadlift])
REFERENCES [dbo].[Lifts] ([lift_id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([measurements])
REFERENCES [dbo].[Measurements] ([measurement_id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([ohp])
REFERENCES [dbo].[Lifts] ([lift_id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([squat])
REFERENCES [dbo].[Lifts] ([lift_id])
GO
