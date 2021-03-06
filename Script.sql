create database DBEnviaEmail;

USE [DBEnviaEmail]
GO
/****** Object:  Table [dbo].[tblEnviarEmail]    Script Date: 04/13/2020 10:37:08 ******/
DROP TABLE [dbo].[tblEnviarEmail]
GO
/****** Object:  Table [dbo].[tblEnviarEmail]    Script Date: 04/13/2020 10:37:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
	[DataHora] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblEnviarEmail] ON
INSERT [dbo].[tblEnviarEmail] ([Id], [EmailOrigem], [EmailDestino], [NomeOrigem], [NomeDestino], [Assunto], [Mensagem], [Status], [DataHora]) VALUES (1, N'wabtectester@gmail.com', N'wabtectester@gmail.com', N'JucelioOrigem', N'WabtecDestino', N'Compra de FOGÃO', N'Você receberá seu FOGÃO.', N'S', CAST(0x0000AC69012A73E8 AS DateTime))
INSERT [dbo].[tblEnviarEmail] ([Id], [EmailOrigem], [EmailDestino], [NomeOrigem], [NomeDestino], [Assunto], [Mensagem], [Status], [DataHora]) VALUES (2, N'wabtectester@gmail.com', N'wabtectester@gmail.com', N'JucelioOrigem', N'WabtecDestino', N'Compra de GELADEIRA', N'Você receberá seu GELADEIRA.', N'S', CAST(0x0000AC69012A73E8 AS DateTime))
SET IDENTITY_INSERT [dbo].[tblEnviarEmail] OFF
