USE [FOODMATE]
GO
/****** Object:  Table [dbo].[Measurements]    Script Date: 04.04.2020 11:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measurements](
	[measurement_id] [int] IDENTITY(1,1) NOT NULL,
	[m_date] [date] NULL,
	[user_id] [int] NOT NULL,
	[l_calve] [int] NULL,
	[r_calve] [int] NULL,
	[l_thigh] [int] NULL,
	[r_thigh] [int] NULL,
	[butt] [int] NULL,
	[waist] [int] NULL,
	[chest] [int] NULL,
	[l_arm] [int] NULL,
	[r_arm] [int] NULL,
	[l_forearm] [int] NULL,
	[r_forearm] [int] NULL,
	[u_weight] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[measurement_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Measurements]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([user_id])
GO
