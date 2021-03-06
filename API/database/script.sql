USE [master]
GO
/****** Object:  Database [manga]    Script Date: 5/1/2558 22:53:59 ******/
CREATE DATABASE [manga]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'manga', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\manga.mdf' , SIZE = 175104KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'manga_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\manga_log.ldf' , SIZE = 13632KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [manga] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [manga].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [manga] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [manga] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [manga] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [manga] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [manga] SET ARITHABORT OFF 
GO
ALTER DATABASE [manga] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [manga] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [manga] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [manga] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [manga] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [manga] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [manga] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [manga] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [manga] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [manga] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [manga] SET  DISABLE_BROKER 
GO
ALTER DATABASE [manga] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [manga] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [manga] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [manga] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [manga] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [manga] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [manga] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [manga] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [manga] SET  MULTI_USER 
GO
ALTER DATABASE [manga] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [manga] SET DB_CHAINING OFF 
GO
ALTER DATABASE [manga] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [manga] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [manga]
GO
/****** Object:  Table [dbo].[getcartoon-converted]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[getcartoon-converted](
	[result__id] [varchar](50) NULL,
	[result__release] [nvarchar](500) NULL,
	[result__time] [varchar](500) NULL,
	[result__view] [varchar](50) NULL,
	[result__name] [nvarchar](2000) NULL,
	[result__end] [bit] NULL,
	[result__new] [bit] NULL,
	[result__hot] [bit] NULL,
	[result__update] [bit] NULL,
	[result__vip] [bit] NULL,
	[result__img] [varchar](1000) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Manga]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manga](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[ImagePath] [nvarchar](1000) NULL,
	[IsEnable] [bit] NOT NULL,
	[NameDisplay] [ntext] NULL,
 CONSTRAINT [PK_Manga] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MangaChapter]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MangaChapter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MangaId] [int] NOT NULL,
	[ChapterId] [int] NOT NULL,
	[ChapterName] [ntext] NULL,
	[ChapterImagePath] [ntext] NULL,
	[IsEnable] [bit] NULL,
 CONSTRAINT [PK_MangaCharpter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MangaImage]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MangaImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MangaId] [int] NOT NULL,
	[Chapter] [int] NOT NULL,
	[Page] [int] NOT NULL,
	[ImagePath] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_MangaImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MangaSeed]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MangaSeed](
	[_id] [int] IDENTITY(1,1) NOT NULL,
	[mangaid] [int] NOT NULL,
	[release] [nvarchar](500) NULL,
	[time] [varchar](500) NULL,
	[view] [varchar](50) NULL,
	[name] [nvarchar](2000) NULL,
	[end] [bit] NULL,
	[new] [bit] NULL,
	[hot] [bit] NULL,
	[update] [bit] NULL,
	[vip] [bit] NULL,
	[img] [varchar](1000) NULL,
 CONSTRAINT [PK_MangaSeed] PRIMARY KEY CLUSTERED 
(
	[_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MangaSeedChapter]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MangaSeedChapter](
	[_id] [int] IDENTITY(1,1) NOT NULL,
	[mangaid] [int] NULL,
	[part] [varchar](50) NULL,
	[name] [nvarchar](2000) NULL,
	[isnew] [varchar](2) NULL,
 CONSTRAINT [PK_MangaSeedChapter] PRIMARY KEY CLUSTERED 
(
	[_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MangaSeedError]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MangaSeedError](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MangaId] [int] NOT NULL,
	[Chapter] [varchar](5) NOT NULL,
	[Page] [int] NOT NULL,
	[ErrorDetails] [text] NULL,
	[StackTrace] [text] NULL,
 CONSTRAINT [PK_MangaSeedError] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MangaSeedImage]    Script Date: 5/1/2558 22:53:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MangaSeedImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MangaId] [int] NOT NULL,
	[Chapter] [varchar](5) NOT NULL,
	[Page] [int] NOT NULL,
	[ImagePath] [nvarchar](2000) NOT NULL,
 CONSTRAINT [PK_MangaSeedImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Manga] ADD  CONSTRAINT [DF_Manga_IsEnable]  DEFAULT ((1)) FOR [IsEnable]
GO
ALTER TABLE [dbo].[MangaChapter] ADD  CONSTRAINT [DF_MangaCharpter_IsEnable]  DEFAULT ((1)) FOR [IsEnable]
GO
USE [master]
GO
ALTER DATABASE [manga] SET  READ_WRITE 
GO
