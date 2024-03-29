create database DBServicoDeEnviarEmail;

USE [DBServicoDeEnviarEmail]
GO
/****** Object:  Table [dbo].[tblEnviarEmail]    Script Date: 1/17/2023 9:36:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblEnviarEmail]') AND type in (N'U'))
DROP TABLE [dbo].[tblEnviarEmail]
GO
/****** Object:  Table [dbo].[tblEnviarEmail]    Script Date: 1/17/2023 9:36:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEnviarEmail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailOrigem] [varchar](30) NOT NULL,
	[EmailDestino] [varchar](30) NOT NULL,
	[NomeOrigem] [varchar](30) NOT NULL,
	[NomeDestino] [varchar](30) NOT NULL,
	[Assunto] [varchar](30) NOT NULL,
	[Mensagem] [varchar](30) NOT NULL,
	[Status] [varchar](30) NOT NULL,
	[DataHora] [datetime2](7) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblEnviarEmail] ON 

INSERT [dbo].[tblEnviarEmail] ([Id], [EmailOrigem], [EmailDestino], [NomeOrigem], [NomeDestino], [Assunto], [Mensagem], [Status], [DataHora]) VALUES (1, N'wabtectester@gmail.com', N'wabtectester@gmail.com', N'JucelioOrigem', N'WabtecDestino', N'Compra de FOGÃO', N'Você receberá seu FOGÃO.', N'N', CAST(N'2023-01-17T21:30:07.4150000' AS DateTime2))
INSERT [dbo].[tblEnviarEmail] ([Id], [EmailOrigem], [EmailDestino], [NomeOrigem], [NomeDestino], [Assunto], [Mensagem], [Status], [DataHora]) VALUES (2, N'wabtectester@gmail.com', N'wabtectester@gmail.com', N'JucelioOrigem', N'WabtecDestino', N'Compra de GELADEIRA', N'Você receberá seu GELADEIRA.', N'N', CAST(N'2023-01-17T21:30:07.4150000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[tblEnviarEmail] OFF
GO
