USE [FOODMATE]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 04.04.2020 11:29:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[category] [nvarchar](20) NULL,
	[product_name_eng] [varchar](50) NULL,
	[kcal] [float] NULL,
	[protein] [float] NULL,
	[carbs] [float] NULL,
	[fats] [float] NULL,
	[product_name_pl] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
