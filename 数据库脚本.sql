USE [vue3_netcore_database]
GO
/****** Object:  Table [dbo].[ts_Permiss]    Script Date: 2024/12/27 14:36:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ts_Permiss](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[roleId] [int] NULL,
	[permiss] [nvarchar](500) NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_ts_Permiss] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ts_Role]    Script Date: 2024/12/27 14:36:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ts_Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[rolename] [nvarchar](200) NULL,
	[account] [nvarchar](200) NULL,
	[state] [bit] NULL,
	[CreateUserId] [int] NULL,
	[CreateUserName] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyUserId] [int] NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_ts_role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ts_User]    Script Date: 2024/12/27 14:36:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ts_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[account] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
	[CreateUserId] [int] NULL,
	[CreateUserName] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyUserId] [int] NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_ts_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ts_UserRole]    Script Date: 2024/12/27 14:36:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ts_UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NULL,
	[IsDel] [bit] NULL,
 CONSTRAINT [PK_ts_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ts_Permiss] ON 

INSERT [dbo].[ts_Permiss] ([Id], [roleId], [permiss], [IsDel]) VALUES (1, 1, N'0,2,21,22,23', 0)
INSERT [dbo].[ts_Permiss] ([Id], [roleId], [permiss], [IsDel]) VALUES (2, 2, N'0,11,1', 0)
SET IDENTITY_INSERT [dbo].[ts_Permiss] OFF
GO
SET IDENTITY_INSERT [dbo].[ts_Role] ON 

INSERT [dbo].[ts_Role] ([Id], [rolename], [account], [state], [CreateUserId], [CreateUserName], [CreateDate], [ModifyUserId], [ModifyUserName], [ModifyDate], [IsDel]) VALUES (1, N'管理员', N'admin', 1, 1, N'admin', CAST(N'2002-01-06T19:17:35.000' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[ts_Role] ([Id], [rolename], [account], [state], [CreateUserId], [CreateUserName], [CreateDate], [ModifyUserId], [ModifyUserName], [ModifyDate], [IsDel]) VALUES (2, N'普通用户', N'user', 1, 1, N'admin', CAST(N'2002-01-06T19:17:35.000' AS DateTime), 1, N'admin', CAST(N'2024-12-25T09:36:26.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[ts_Role] OFF
GO
SET IDENTITY_INSERT [dbo].[ts_User] ON 

INSERT [dbo].[ts_User] ([Id], [name], [account], [password], [email], [phone], [CreateUserId], [CreateUserName], [CreateDate], [ModifyUserId], [ModifyUserName], [ModifyDate], [IsDel]) VALUES (1, N'管理员', N'admin', N'admin', N'www.baidu.com', N'17622345523', 1, N'admin', CAST(N'2002-01-06T19:17:35.000' AS DateTime), 0, N'', CAST(N'2024-12-18T09:46:13.113' AS DateTime), 0)
INSERT [dbo].[ts_User] ([Id], [name], [account], [password], [email], [phone], [CreateUserId], [CreateUserName], [CreateDate], [ModifyUserId], [ModifyUserName], [ModifyDate], [IsDel]) VALUES (2, N'张三', N'zhangsan', N'zhangsan', N'n.ukrteu@wgvzx.tel', N'17622345523', 1, N'admin', CAST(N'1986-12-30T03:23:50.000' AS DateTime), 0, N'', CAST(N'2024-12-13T09:43:35.547' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[ts_User] OFF
GO
SET IDENTITY_INSERT [dbo].[ts_UserRole] ON 

INSERT [dbo].[ts_UserRole] ([Id], [UserId], [RoleId], [IsDel]) VALUES (1, 1, 1, 0)
INSERT [dbo].[ts_UserRole] ([Id], [UserId], [RoleId], [IsDel]) VALUES (2, 2, 2, 0)
SET IDENTITY_INSERT [dbo].[ts_UserRole] OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_Role', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_Role', @level2type=N'COLUMN',@level2name=N'CreateUserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_Role', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_Role', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_Role', @level2type=N'COLUMN',@level2name=N'ModifyUserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_Role', @level2type=N'COLUMN',@level2name=N'ModifyDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_Role', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_User', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_User', @level2type=N'COLUMN',@level2name=N'CreateUserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_User', @level2type=N'COLUMN',@level2name=N'CreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_User', @level2type=N'COLUMN',@level2name=N'ModifyUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_User', @level2type=N'COLUMN',@level2name=N'ModifyUserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_User', @level2type=N'COLUMN',@level2name=N'ModifyDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_User', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ts_UserRole', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
