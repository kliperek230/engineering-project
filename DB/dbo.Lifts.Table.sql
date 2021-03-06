USE [FOODMATE]
GO
/****** Object:  Table [dbo].[Lifts]    Script Date: 04.04.2020 11:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lifts](
	[lift_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[m_date] [date] NOT NULL,
	[lift_name] [nvarchar](8) NOT NULL,
	[value] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[lift_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lifts]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
