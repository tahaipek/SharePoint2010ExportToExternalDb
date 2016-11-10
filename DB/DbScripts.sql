USE [SpAktarim]
GO
/****** Object:  Table [dbo].[DocumentLibrary]    Script Date: 10.11.2016 23:48:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentLibrary](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SiteID] [bigint] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[URL] [nvarchar](max) NOT NULL,
	[ItemCount] [bigint] NULL CONSTRAINT [DF_DocumentLibrary_ItemCount]  DEFAULT ((0)),
 CONSTRAINT [PK_DocumentLibrary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[File]    Script Date: 10.11.2016 23:48:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SiteID] [bigint] NULL,
	[DocumentLibraryID] [bigint] NULL,
	[FolderID] [bigint] NULL,
	[RemoteID] [bigint] NOT NULL,
	[FileLeafRef] [nvarchar](max) NULL,
	[FileRef] [nvarchar](max) NULL,
	[FileDirRef] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[Author] [nvarchar](max) NULL,
	[Modified] [datetime] NULL,
	[Editor] [nvarchar](max) NULL,
	[CopySource] [nvarchar](max) NULL,
	[FileType] [nvarchar](10) NULL,
	[FileSize] [int] NULL,
	[Aktarildimi] [bit] NOT NULL CONSTRAINT [DF_Files_Aktarildimi]  DEFAULT ((0)),
 CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileVersions]    Script Date: 10.11.2016 23:48:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileVersions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [int] NOT NULL,
	[DocumentLibraryID] [int] NOT NULL,
	[FileID] [int] NOT NULL,
	[VersionID] [int] NOT NULL,
	[URL] [nvarchar](max) NULL,
	[VersionLabel] [nvarchar](50) NULL,
	[Created] [datetime] NULL,
 CONSTRAINT [PK_FileVersions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Folder]    Script Date: 10.11.2016 23:48:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folder](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SiteID] [bigint] NOT NULL,
	[DocumentLibraryID] [bigint] NOT NULL,
	[ParentFolderID] [bigint] NOT NULL,
	[RemoteID] [bigint] NOT NULL,
	[FileLeafRef] [nvarchar](max) NULL,
	[FileRef] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[Author] [nvarchar](max) NULL,
	[Modified] [datetime] NULL,
	[Editor] [nvarchar](max) NULL,
	[CopySource] [nvarchar](max) NULL,
	[ItemChildCount] [bigint] NOT NULL CONSTRAINT [DF_Folders_ItemChildCount]  DEFAULT ((0)),
	[FolderChildCount] [bigint] NOT NULL CONSTRAINT [DF_Folders_FolderChildCount]  DEFAULT ((0)),
 CONSTRAINT [PK_Folders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Site]    Script Date: 10.11.2016 23:48:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Site](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
