USE [master]
GO
/****** Object:  Database [CustomizableEventCalendar]    Script Date: 26-07-2024 02:48:01 PM ******/
CREATE DATABASE [CustomizableEventCalendar]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CustomizableEventCalendar', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CustomizableEventCalendar.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CustomizableEventCalendar_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CustomizableEventCalendar_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CustomizableEventCalendar] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CustomizableEventCalendar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CustomizableEventCalendar] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET ARITHABORT OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CustomizableEventCalendar] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CustomizableEventCalendar] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CustomizableEventCalendar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CustomizableEventCalendar] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CustomizableEventCalendar] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET RECOVERY FULL 
GO
ALTER DATABASE [CustomizableEventCalendar] SET  MULTI_USER 
GO
ALTER DATABASE [CustomizableEventCalendar] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CustomizableEventCalendar] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CustomizableEventCalendar] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CustomizableEventCalendar] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CustomizableEventCalendar] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CustomizableEventCalendar] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CustomizableEventCalendar', N'ON'
GO
ALTER DATABASE [CustomizableEventCalendar] SET QUERY_STORE = ON
GO
ALTER DATABASE [CustomizableEventCalendar] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CustomizableEventCalendar]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Location] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StartHour] [int] NOT NULL,
	[EndHour] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Frequency] [varchar](50) NULL,
	[Interval] [int] NOT NULL,
	[ByWeekDay] [nvarchar](max) NULL,
	[WeekOrder] [int] NULL,
	[ByMonthDay] [int] NULL,
	[ByMonth] [int] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventCollaborator]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventCollaborator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[EventCollaboratorRole] [nvarchar](max) NOT NULL,
	[ConfirmationStatus] [nvarchar](max) NOT NULL,
	[ProposedStartHour] [int] NULL,
	[ProposedEndHour] [int] NULL,
	[EventDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EventCollaborator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SharedCalendar]    Script Date: 26-07-2024 02:48:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SharedCalendar](
	[SenderId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromDate] [datetime2](7) NOT NULL,
	[ToDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SharedCalendar_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240617071608_AddedIdentity', N'8.0.6')
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] ON 

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (4, N'aaa', N'AAA', N'aa@gmail.com', N'AA@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEAf1fuQdvobztx/Fieqldp3Y3a1FN+NSA7ezQ0grzFZYI8s603uhdRvZp90M1Cq1HA==', N'OZQAF7T3SOO7LIQ5VHEVZYKMOLANRITG', N'ad611a49-9550-4684-8f4f-3db890ac7a55', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (5, N'a', N'A', N'a@gmail.com', N'A@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEANCxsQDlDFOlAH/vd4GlKcQF19JDcbxksn4JQGJH4Z4VGgC65UXxYwpLaRsuCTMaw==', N'OKWX5O6ZNPZ5E324MJ26QSP7GI7CVQIS', N'6e64fa86-020c-4891-924b-0af318d9189e', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (7, N'aa', N'AA', N'aa@gmail.com', N'AA@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEMndOx1j1ADk5tRN3USZJx6crgy76FD+Uap0GlRVaILMvpQxIUD52aUQ9e2qQhdlnA==', N'K3ZQKKEXQCOZUBQWVF5YXGAMQBOOACHS', N'31c826bc-7ffa-4e39-944b-6b0c13470f6e', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (3, N'First event', N'First event', N'First event', 1, 2, CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (4, N'Second Event', N'Second Event', N'Second Event', 2, 3, CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-12T00:00:00.0000000' AS DateTime2), N'Daily', 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (5, N'Event from frontend', N'Event from frontend', N'Event from frontend', 6, 7, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (6, N'Second event from frontend', N'Virtual', N'Event add from add form of frontend', 19, 20, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (7, N'Second event ', N'Virtual', N'Second event from frontend', 19, 20, CAST(N'2024-04-16T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-16T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (8, N'Third event ', N'Virtual', N'Third event from frontend', 18, 19, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (9, N'Fourth', N'F', N'F', 0, 1, CAST(N'2024-07-17T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-17T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (10, N'KD''s Event', N'Virtual', N'KD''s Event', 0, 1, CAST(N'2024-07-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-19T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (11, N'Event of 18th', N'Virtual', N'This is event on 18th july', 1, 2, CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (12, N'Event of 20th', N'Virtual', N'20th event', 0, 1, CAST(N'2024-07-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-20T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (13, N'Fourth event on 16th', N'Virtual', N'none', 0, 1, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (14, N'Third event on 10th', N'Virtual', N'None', 0, 1, CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (15, N'Past Event', N'Rajkot', N'Time Travelling 😎', 0, 1, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (16, N'First daily event', N'Virtual', N'none', 0, 1, CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (17, N'Second Daily event', N'Virtual', N'None', 1, 2, CAST(N'2024-07-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-19T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (18, N'Third daily event', N'Virtual', N'none', 0, 1, CAST(N'2024-07-21T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-28T00:00:00.0000000' AS DateTime2), N'Daily', 2, NULL, NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (19, N'Fourth daily event with weekday', N'Virtual', N'none', 1, 2, CAST(N'2024-07-21T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-27T00:00:00.0000000' AS DateTime2), N'Daily', 1, N'7,6,5,4,3,2,1', NULL, NULL, NULL, 4)
INSERT [dbo].[Event] ([Id], [Title], [Location], [Description], [StartHour], [EndHour], [StartDate], [EndDate], [Frequency], [Interval], [ByWeekDay], [WeekOrder], [ByMonthDay], [ByMonth], [UserId]) VALUES (25, N'Testing Event', N'Rajkot', N'This is testing event.', 22, 23, CAST(N'2024-07-23T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-23T00:00:00.0000000' AS DateTime2), NULL, 1, NULL, NULL, NULL, NULL, 4)
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[EventCollaborator] ON 

INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (37, 3, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (38, 4, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (39, 4, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-11T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (40, 4, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-12T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (41, 5, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (42, 6, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (43, 7, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-04-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (44, 8, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (45, 9, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-17T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (46, 10, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-19T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (47, 11, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (48, 12, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-20T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (49, 13, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (50, 14, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-10T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (51, 15, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (52, 16, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-18T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (53, 17, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-19T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (54, 18, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-21T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (55, 18, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-23T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (56, 18, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (57, 18, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (58, 19, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-21T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (59, 19, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-22T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (60, 19, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-23T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (61, 19, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-24T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (62, 19, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-25T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (63, 19, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-26T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (64, 19, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[EventCollaborator] ([Id], [EventId], [UserId], [EventCollaboratorRole], [ConfirmationStatus], [ProposedStartHour], [ProposedEndHour], [EventDate]) VALUES (80, 25, 4, N'Organizer', N'Accept', NULL, NULL, CAST(N'2024-07-23T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[EventCollaborator] OFF
GO
SET IDENTITY_INSERT [dbo].[SharedCalendar] ON 

INSERT [dbo].[SharedCalendar] ([SenderId], [ReceiverId], [Id], [FromDate], [ToDate]) VALUES (4, 5, 1, CAST(N'2024-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-18T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[SharedCalendar] ([SenderId], [ReceiverId], [Id], [FromDate], [ToDate]) VALUES (4, 5, 3, CAST(N'2024-06-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-06-18T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[SharedCalendar] ([SenderId], [ReceiverId], [Id], [FromDate], [ToDate]) VALUES (4, 5, 4, CAST(N'2024-07-04T00:00:00.0000000' AS DateTime2), CAST(N'2024-07-04T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[SharedCalendar] OFF
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventCollaborator_EventId]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_EventCollaborator_EventId] ON [dbo].[EventCollaborator]
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EventCollaborator_UserId]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_EventCollaborator_UserId] ON [dbo].[EventCollaborator]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SharedCalendar_ReceiverId]    Script Date: 26-07-2024 02:48:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_SharedCalendar_ReceiverId] ON [dbo].[SharedCalendar]
(
	[ReceiverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[EventCollaborator]  WITH CHECK ADD  CONSTRAINT [FK_EventCollaborator_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EventCollaborator] CHECK CONSTRAINT [FK_EventCollaborator_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[EventCollaborator]  WITH CHECK ADD  CONSTRAINT [FK_EventCollaborator_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EventCollaborator] CHECK CONSTRAINT [FK_EventCollaborator_Event]
GO
ALTER TABLE [dbo].[SharedCalendar]  WITH CHECK ADD  CONSTRAINT [FK_SharedCalendar_AspNetUsers_ReceiverId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SharedCalendar] CHECK CONSTRAINT [FK_SharedCalendar_AspNetUsers_ReceiverId]
GO
ALTER TABLE [dbo].[SharedCalendar]  WITH CHECK ADD  CONSTRAINT [FK_SharedCalendar_AspNetUsers_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SharedCalendar] CHECK CONSTRAINT [FK_SharedCalendar_AspNetUsers_SenderId]
GO
USE [master]
GO
ALTER DATABASE [CustomizableEventCalendar] SET  READ_WRITE 
GO
