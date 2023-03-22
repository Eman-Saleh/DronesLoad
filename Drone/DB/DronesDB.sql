USE [DronesDB]
GO
/****** Object:  Table [dbo].[Drone]    Script Date: 3/22/2023 2:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drone](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SerialNumber] [nvarchar](100) NOT NULL,
	[modelID] [int] NULL,
	[stateID] [int] NULL,
	[weightLimit] [float] NULL,
	[batteryCapacity] [float] NULL,
 CONSTRAINT [PK_Drone] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DroneModel]    Script Date: 3/22/2023 2:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DroneModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModelName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DroneState]    Script Date: 3/22/2023 2:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DroneState](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[stateName] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medication]    Script Date: 3/22/2023 2:31:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medication](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[Weight] [decimal](4, 2) NULL,
	[Code] [nvarchar](50) NULL,
	[Image] [nvarchar](50) NULL,
	[DroneID] [int] NULL,
 CONSTRAINT [PK_Medication] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[DroneModel] ON 
GO
INSERT [dbo].[DroneModel] ([ID], [ModelName]) VALUES (1, N'Light weight')
GO
INSERT [dbo].[DroneModel] ([ID], [ModelName]) VALUES (2, N'Middle weight')
GO
INSERT [dbo].[DroneModel] ([ID], [ModelName]) VALUES (3, N'Cruiser weight')
GO
INSERT [dbo].[DroneModel] ([ID], [ModelName]) VALUES (4, N'Heavy weight')
GO
SET IDENTITY_INSERT [dbo].[DroneModel] OFF
GO
SET IDENTITY_INSERT [dbo].[DroneState] ON 
GO
INSERT [dbo].[DroneState] ([ID], [stateName]) VALUES (1, N'IDLE')
GO
INSERT [dbo].[DroneState] ([ID], [stateName]) VALUES (2, N'LOADING')
GO
INSERT [dbo].[DroneState] ([ID], [stateName]) VALUES (3, N'LOADED')
GO
INSERT [dbo].[DroneState] ([ID], [stateName]) VALUES (4, N'DELIVERING')
GO
INSERT [dbo].[DroneState] ([ID], [stateName]) VALUES (6, N'DELIVERED')
GO
INSERT [dbo].[DroneState] ([ID], [stateName]) VALUES (7, N'RETURNING')
GO
SET IDENTITY_INSERT [dbo].[DroneState] OFF
GO

SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Drone] ADD UNIQUE NONCLUSTERED 
(
	[SerialNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Drone]  WITH CHECK ADD  CONSTRAINT [FK_Drone_Model] FOREIGN KEY([modelID])
REFERENCES [dbo].[DroneModel] ([ID])
GO
ALTER TABLE [dbo].[Drone] CHECK CONSTRAINT [FK_Drone_Model]
GO
ALTER TABLE [dbo].[Drone]  WITH CHECK ADD  CONSTRAINT [FK_Drone_State] FOREIGN KEY([stateID])
REFERENCES [dbo].[DroneState] ([ID])
GO
ALTER TABLE [dbo].[Drone] CHECK CONSTRAINT [FK_Drone_State]
GO
ALTER TABLE [dbo].[Medication]  WITH CHECK ADD  CONSTRAINT [FK_Medication_Drone] FOREIGN KEY([DroneID])
REFERENCES [dbo].[Drone] ([ID])
GO
ALTER TABLE [dbo].[Medication] CHECK CONSTRAINT [FK_Medication_Drone]
GO
