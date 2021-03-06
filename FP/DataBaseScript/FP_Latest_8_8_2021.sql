USE [master]
GO
/****** Object:  Database [FPNew21]    Script Date: 08/08/2021 6:33:56 PM ******/
CREATE DATABASE [FPNew21]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FPNew21', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FPNew21.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FPNew21_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\FPNew21_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FPNew21] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FPNew21].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FPNew21] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FPNew21] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FPNew21] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FPNew21] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FPNew21] SET ARITHABORT OFF 
GO
ALTER DATABASE [FPNew21] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FPNew21] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FPNew21] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FPNew21] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FPNew21] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FPNew21] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FPNew21] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FPNew21] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FPNew21] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FPNew21] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FPNew21] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FPNew21] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FPNew21] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FPNew21] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FPNew21] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FPNew21] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FPNew21] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FPNew21] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FPNew21] SET  MULTI_USER 
GO
ALTER DATABASE [FPNew21] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FPNew21] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FPNew21] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FPNew21] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FPNew21] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FPNew21] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FPNew21] SET QUERY_STORE = OFF
GO
USE [FPNew21]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[IsApproved] [bit] NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[IsApproved] [bit] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AudienceAge]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AudienceAge](
	[AudienceAgeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](500) NULL,
	[MinValue] [int] NULL,
	[MaxValue] [int] NULL,
	[Duration] [varchar](10) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_AudienceAge] PRIMARY KEY CLUSTERED 
(
	[AudienceAgeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankDetail]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankDetail](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [nvarchar](255) NULL,
	[BankAddress] [nvarchar](255) NULL,
	[BankCode] [nvarchar](50) NULL,
	[UserId] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[AccountHolderName] [nvarchar](255) NULL,
 CONSTRAINT [PK_BankDetail] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandMail]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandMail](
	[BrandMailId] [int] IDENTITY(1,1) NOT NULL,
	[MailTypeId] [int] NULL,
	[MailFrom] [nvarchar](max) NULL,
	[Subject] [nvarchar](250) NULL,
	[Message] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeleteDate] [datetime] NULL,
	[PreviousBrandMailId] [int] NULL,
	[IsRead] [bit] NULL,
	[ProjectProposalId] [int] NULL,
	[ProjectProposalUpdateId] [int] NULL,
 CONSTRAINT [PK_BrandMail] PRIMARY KEY CLUSTERED 
(
	[BrandMailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrandMailFile]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrandMailFile](
	[BrandMailFileId] [int] IDENTITY(1,1) NOT NULL,
	[BrandMailId] [int] NULL,
	[FileName] [nvarchar](250) NULL,
	[FileData] [varbinary](max) NULL,
	[FilePath] [nvarchar](max) NULL,
	[ProjectProposalId] [int] NULL,
 CONSTRAINT [PK_BrandMailFile] PRIMARY KEY CLUSTERED 
(
	[BrandMailFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Budget]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Budget](
	[BudgetId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Campaign]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campaign](
	[CampaignId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[CampaignTypeId] [nvarchar](50) NULL,
	[SupplementalChannels] [nvarchar](250) NULL,
	[ProductCategoryId] [int] NULL,
	[ProductURL] [nvarchar](max) NULL,
	[ProductPhoto] [nvarchar](max) NULL,
	[ShippingProduct] [bit] NULL,
	[AboutYourProduct] [nvarchar](250) NULL,
	[CampaignTitle] [nvarchar](50) NULL,
	[AboutYourBrand] [nvarchar](250) NULL,
	[CampaignGoal] [nvarchar](250) NULL,
	[CampaignDurationId] [int] NULL,
	[PrivateCampaign] [bit] NULL,
	[AudienceAgeId] [nvarchar](50) NULL,
	[BudgetId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[Status] [nvarchar](50) NULL,
	[AudienceGender] [nvarchar](50) NULL,
	[YouTube] [nvarchar](50) NULL,
	[YouTubeVideoType] [nvarchar](max) NULL,
	[Approved] [bit] NULL,
	[Rejected] [bit] NULL,
	[Country] [nvarchar](max) NULL,
	[Budget] [float] NULL,
	[CurrencyType] [nvarchar](50) NULL,
 CONSTRAINT [PK_CampaignDetails] PRIMARY KEY CLUSTERED 
(
	[CampaignId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignCounty]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignCounty](
	[CampaignCountyId] [int] NOT NULL,
	[CampaignId] [int] NULL,
	[CountryId] [int] NULL,
 CONSTRAINT [PK_CampaignCounty] PRIMARY KEY CLUSTERED 
(
	[CampaignCountyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignDuration]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignDuration](
	[CampaignDurationId] [int] IDENTITY(1,1) NOT NULL,
	[Duration] [varchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CampaignDuration] PRIMARY KEY CLUSTERED 
(
	[CampaignDurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CampaignType]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CampaignType](
	[CampaignTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_CampaignType] PRIMARY KEY CLUSTERED 
(
	[CampaignTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Channel]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel](
	[ChannelId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Channel] PRIMARY KEY CLUSTERED 
(
	[ChannelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientWalletAmount]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientWalletAmount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [float] NULL,
	[UploadDate] [datetime] NULL,
	[UploadedBy] [nvarchar](255) NULL,
	[TransactionID] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_[ClientWalletAmount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreatorFeedback]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreatorFeedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatorId] [nvarchar](max) NULL,
	[Feedback] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_CreatorFeedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreatorProfile]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreatorProfile](
	[CreatorId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[FullName] [nvarchar](255) NULL,
	[ContactNumber] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[CountryId] [int] NULL,
	[YouTube] [nvarchar](max) NULL,
	[Instagram] [nvarchar](max) NULL,
	[Facebook] [nvarchar](max) NULL,
	[CategoryId] [int] NULL,
	[MinimumBudgetedProject] [nvarchar](255) NULL,
	[PastWorkExperience] [nvarchar](max) NULL,
	[Summary] [nvarchar](max) NULL,
	[TargetAudience] [nvarchar](255) NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[DOB] [datetime] NULL,
	[Language] [nvarchar](max) NULL,
	[Categories] [nvarchar](250) NULL,
	[Gender] [nvarchar](50) NULL,
	[CurrencyType] [nvarchar](50) NULL,
 CONSTRAINT [PK_CreatorProfile] PRIMARY KEY CLUSTERED 
(
	[CreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CurrencyType]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrencyType](
	[CurrencyId] [nvarchar](50) NOT NULL,
	[CurrencyName] [nvarchar](150) NULL,
 CONSTRAINT [PK_CurrencyType] PRIMARY KEY CLUSTERED 
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Level]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Level](
	[LevelId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[StartRange] [nvarchar](255) NULL,
	[EndRange] [nvarchar](255) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[LevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageType]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageType](
	[MessageTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
 CONSTRAINT [PK_MessageType] PRIMARY KEY CLUSTERED 
(
	[MessageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectId] [int] NOT NULL,
	[ProjectTitle] [nvarchar](max) NULL,
	[ProjectDescription] [nvarchar](max) NOT NULL,
	[Budget] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreateBy] [nvarchar](50) NULL,
	[CurrencyType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectProposal]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProposal](
	[ProjectProposalId] [int] IDENTITY(1,1) NOT NULL,
	[CampaignId] [int] NULL,
	[ProjectDescription] [nvarchar](max) NULL,
	[PaymentType] [nvarchar](255) NULL,
	[NoOfMilestone] [int] NULL,
	[Milestone1] [nvarchar](max) NULL,
	[Milestone2] [nvarchar](max) NULL,
	[Milestone3] [nvarchar](max) NULL,
	[Status] [nvarchar](255) NULL,
	[UserId] [nvarchar](max) NULL,
	[Approved] [bit] NULL,
	[Milestone] [int] NULL,
	[ProposalAmount] [float] NULL,
	[ReceivedAmount] [float] NULL,
	[ProposalDate] [datetime] NULL,
	[Milestone1Amount] [float] NULL,
	[Milestone2Amount] [float] NULL,
	[Milestone3Amount] [float] NULL,
	[CurrencyType] [nvarchar](50) NULL,
 CONSTRAINT [PK_ProjectProposal] PRIMARY KEY CLUSTERED 
(
	[ProjectProposalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectProposalUpdate]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProposalUpdate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CampaignId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL,
	[ProjectProposalId] [int] NULL,
	[IsApproved] [bit] NULL,
 CONSTRAINT [PK_ProjectProposalUpdate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectProposalUpdatesFile]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProposalUpdatesFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectProposalUpdateId] [int] NULL,
	[FileName] [nvarchar](max) NULL,
	[FilePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProjectProposalUpdatesFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplementalChannel]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplementalChannel](
	[SupplementalId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Path] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_SupplementalChannel] PRIMARY KEY CLUSTERED 
(
	[SupplementalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TargetedCampaign]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetedCampaign](
	[TargetedCampaignId] [int] IDENTITY(1,1) NOT NULL,
	[CampaignId] [int] NULL,
	[Target] [nvarchar](50) NULL,
 CONSTRAINT [PK_TargetedCampaign] PRIMARY KEY CLUSTERED 
(
	[TargetedCampaignId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WalletAmount]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WalletAmount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [float] NULL,
	[UploadDate] [datetime] NULL,
	[UploadedBy] [nvarchar](255) NULL,
 CONSTRAINT [PK_WalletAmount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YouTubeVideoType]    Script Date: 08/08/2021 6:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YouTubeVideoType](
	[YouTubeVideoTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_YouTubeVideoType] PRIMARY KEY CLUSTERED 
(
	[YouTubeVideoTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'201908220649369_InitialCreate', N'FP.Models.ApplicationDbContext', 0x1F8B0800000000000400DD5CDB6EE336107D2FD07F10F4D416A9954B77B10DEC5DA44EDC06DD5CB0CE167D5BD012ED082B51AA44651314FDB23EF493FA0B1D4AD48D175D6CC5768A02C55A1C9E190E87E47074947FFFFE67FCEED1F78C071CC56E4026E6D1E8D03430B103C725AB8999D0E5F76FCC776FBFFE6A7CE1F88FC66FB9DC0993839E249E98F79486A79615DBF7D847F1C877ED288883251DD9816F2127B08E0F0F7FB48E8E2C0C10266019C6F84342A8EBE3F407FC9C06C4C6214D90771538D88BF9736899A7A8C635F2711C221B4FCCD9ED2813328D33CF4560C01C7B4BD3408404145130EFF4638CE7340AC86A1EC203E4DD3D8518E496C88B3137FBB414EF3A82C3633602ABEC9843D9494C03BF27E0D109778925765FCBB166E13270DA0538973EB151A78E9B98970E4E1F7D083C7080A8F074EA454C78625E152ACEE2F01AD351DE719441CE2280FB12449F4755C403A373BF8322848E4787ECBF03639A783489F084E08446C83B306E9385E7DABFE2A7BBE033269393A3C5F2E4CDABD7C83979FD033E79551D298C15E46A0FE0D16D14843802DBF0B218BF6958F57E96D8B1E856E993790562095683695CA1C7F798ACE83DAC93E337A631731FB1933FE1C1F591B8B078A0138D12F8799D781E5A78B868B71A75B2FF37683D7EF57A10ADD7E8C15DA5532FE8878513C1BAFA80BDB435BE77C36C79D5E6FB13179B4581CF7ED7E32B6BFD340F92C8668309B42277285A615AB76E6C95C1DB29A419D4F0619DA3EE7F68334BE5F0568AB201ADB3127215DB5E0DB9BDCFABB773C49D85214C5E1A5ACC234D01579C5123A1D38131BB2D03E5A86BA01018C0FF79DFBBF091EB0DB0F175D002A9C6D28D7C5C8CF2A700C20C91DE36DFA2388675EFFC82E2FB06D3E19F03983EC776124138CE29F2C367D7767B1F107C9DF80B16E5DBD335D8D4DC7D0966C8A641744158AF8DF1DE07F6E720A117C43947147FA4760EC87EDEB97E778041CC39B36D1CC7330866EC4C03C8A473C04B424F8E7BC3B17D69D789C7D443AEAFCE3C841DF4532E5A661F6A092903D188A9B2902653DF072B9774333517D59B9A49B49ACAC5FA9ACAC0BA59CA25F586A602AD76665283E575E90C0D9FD8A5B0FB9FD96D7678EBF6828A1BE7B043E29F31C1116C63CE2DA21447A49C812EFBC62E928574FA98D2673F9B524DBF212F195AD55AAB21DD04865F0D29ECFEAF86D44C78FCE03A2C2BE970DDC98501BE93BCFA26D5BEE604CBB6BD1C6AC3DCB6F2EDEC01BAE57216C781EDA6AB4051E8E2658ABAFD90C319ED358B6C3462DD03060681EEB2230F9EC0D84C31A86EC839F630C5C6999D1502A728B69123BB1106E4F4302C3F51158695F58FBA71DF493A21D271C43A2176098A61A5BA84CACBC225B61B22AFD54B42CF8E47181B7BA1436C39C721264C61AB27BA2857973B9801851E6152DA3C34B62A11D71C889AAC5537E76D296C39EF5215622B31D9923B6BE292E76FCF1298CD1EDB427036BBA48B01DAD2DD2E0294DF55BA06807871D9B700156E4C9A00E529D55602B4EEB11D0468DD252F2E40B32B6AD7F917EEABFB169EF58BF2F68FF54677ED20366BFED8B3D0CC724FE843A1078EE4F03C5FB046FC48159733B093DFCF629EEA8A21C2C0E798D64B3665BEABCC43AD661031889A00CB406B01E52FFD24206941F5302EAFE5355AC7B3881EB079DDAD1196EFFD026C250664ECEACBCF8AA0FE15A9189C9D6E1FC5C88A689082BCD365A182A3080871F3AA0FBC8353747559D9315D72E13ED97065607C321A1CD492B96A9C940F66702FE5A1D9EE255542D62725DBC84B42FAA4F1523E98C1BDC463B4DD498AA4A0475AB0918BEA47F8408B2DAF7414A74DD136B6322A147F30B6349CA9F1150A4397AC2A1C2AFEC4986704AAE9F7F3FE14233FC3B0EC58C1342AAC2D34D120422B2CB4826AB074E646313D47142D10ABF34C1D5F12539EAD9AED3F57593D3EE549CCCF815C9AFD3BEB51BEAAAF1DB1720EC2BBCE60603E4B64D2EAB962DAD5DD0D4665431E8A1405FB69E0253ED1E755FADED96BBB6AFFEC898C30B604FBA5BC49729294DDD63DDE693EE4B5B0D9DC14D9CAFAF3A387D07939CF35AB7ED6E59F7A94BC1C5545D195A876365FBAB4A5CB1C898960FF296A45789E55C4D9275500FEA8274685C0208155DABAA3D6392655CC7A4B774481485285149A7A5859A58BD48CAC36AC85A7F1A85AA2BB0699205245975BBB232BA822556845F31AD80A9BC5B6EEA80A36491558D1DC1DBBA496887BE71E9F53DAEB49DF832ABBB86E765269309E67231CE6A0ABBC9FAF02551EF7C4E26FE02530FE7C2F83487B7BEB1B44599962B320D260E8F799DA0BEDFA36D3F8165E8F597B4B5DDBCA9BDED2EBF1FA85EAB30684746713450AEDC5DD4DB8A38DF97DA9FDE317E90295899846EE4638C69F628AFD111318CDFFF0A69E8BD9A69D0B5C21E22E714C336686797C78742C7C48B33F1FB55871EC788AFBA6EECB96FA9C6D8164451E5064DFA348A63C6CF0E147092A55932F89831F27E69F69AFD3B430C1FE953E3E302EE38FC4FD238186BB28C1C65F32857318227CF34D6A4F3F5BE8EED5CBDF3F655D0F8C9B0856CCA97128F8729D19AE7FCCD0CB9AACEB06D6ACFD89C3CB5D50B52F0A94A8C28258FF0382854B07F97820B7F21B1F3D7EDBD734E507021B212A3E02180A6F1017EA48FEEB606909FE0EFCA429C1BFDF60D584FF754CD392FD5DD21F4CA4FA77DF86F29E3B3C6A1457A16D6C49A99F5BA9D21BF126777D36498CEA8D16BACC9AEE01B701337A8DC87861A4E2C14E4705677830EC5D86F6B31385F7851B5CB236764B09DE260BB8E1DDCFFF8AFCBB07743505FD66F714DF6DC79AAE7CBBE73CC97E44DE3D0B364ECADA3D5D77DBC1A62BF3EE79B0F522E5EE59ACEDEAFCDC71A4753E42774EB195D9429AD730AA5A701B85362B9CC30D7F114010641965F6E5A39AB3D5C4376D51588AE895EAC962A26269E1487A258966B5FDC6CA0FFCC6C1729966B51A8A65936EBEFF37EAE632CDBA35C4C55D907F95D4411521BB651F6B623ABD24B26F6D242DDCF2B69CB5F19DFA4BE2F60EE294DAEAD1BC237E3954DE415C32E4D2E941DD955FF7C2D959F98B88707EC7EEAA84607F1F9160BB766A1632976419E487B760512E225468AE30450E1CA967117597C8A6D0CC6ACCE9A7DB69DD8EBDE95860E792DC24344C280C19FB0BAF56F062494093FE949F5CB7797C13A67F8564882180992EABCDDF909F12D7730ABB678A9A9006826517BCA2CBE692B2CAEEEAA940BA0E484720EEBE2229BAC37EE801587C43E6E801AF631B84DF7BBC42F6535901D481B44F44DDEDE37317AD22E4C71CA3EC0F3F21861DFFF1ED7F03E1CB7F18540000, N'6.1.3-40302')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Client')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Creator')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [UserId]) VALUES (N'Facebook', N'3733475533425257', N'97ae84d9-6d91-463f-9513-b503adf074d7')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsApproved]) VALUES (N'1a6a73c0-4705-4ee3-8502-ff7811db8138', N'1', NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsApproved]) VALUES (N'22b08833-358b-4b19-b974-74b840b53332', N'3', NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsApproved]) VALUES (N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', N'3', NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsApproved]) VALUES (N'922893f7-12e0-465b-8925-58d2b437c659', N'2', NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsApproved]) VALUES (N'ba785409-c69f-495d-ae4f-347bdf4df834', N'2', NULL)
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId], [IsApproved]) VALUES (N'f2363ef0-c455-454c-9aa2-2cd923fb598d', N'2', NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsApproved]) VALUES (N'1a6a73c0-4705-4ee3-8502-ff7811db8138', N'admin@gmail.com', 0, N'AN+S9Z6C2HXCEpCtk/Hurr9OtXOUKM65hGsaEPmRvAXZzrvVlGJoA73tI0u8U84XRA==', N'3161e5ae-1deb-4424-b011-381cb875aa4d', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsApproved]) VALUES (N'22b08833-358b-4b19-b974-74b840b53332', N'creatortest2@gmail.com', 0, N'AN+S9Z6C2HXCEpCtk/Hurr9OtXOUKM65hGsaEPmRvAXZzrvVlGJoA73tI0u8U84XRA==', N'0fdbb644-310a-46b0-89ab-4cad41155a67', NULL, 0, 0, NULL, 1, 0, N'creatortest2@gmail.com', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsApproved]) VALUES (N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', N'creatortest@gmail.com', 0, N'AHljSn80HH27jqH1BOqXGAZIndhWTsuZwB02YTIajY2aIpOpgkGHFz/yqe4hHvDZXQ==', N'a467791c-bc23-46cb-9b18-b239ce4b51ad', NULL, 0, 0, NULL, 1, 0, N'creatortest@gmail.com', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsApproved]) VALUES (N'922893f7-12e0-465b-8925-58d2b437c659', N'clienttest2@gmail.com', 0, N'AERgrVDNS5d9lnSu62bDs7UVj9L8KFnT32SyKjRxt2SmfLoNx/RMjUtCK8WJMlQaug==', N'47124f1b-ee84-4ef0-81e2-f6c81850aa5b', NULL, 0, 0, NULL, 1, 0, N'clienttest2@gmail.com', 0)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsApproved]) VALUES (N'97ae84d9-6d91-463f-9513-b503adf074d7', N'gauravsinghrana99@gmail.com', 0, N'ADhMGgHbYkN0Ngu3mHCYcK6JtjDWZaI7EtbavCerDV5rBTXFpjaqdRnXo160I0YryA==', N'99b35701-1073-446a-8d30-84bdcb91e739', NULL, 0, 0, NULL, 1, 0, N'gauravsinghrana99@gmail.com', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsApproved]) VALUES (N'ba785409-c69f-495d-ae4f-347bdf4df834', N'clienttest3@gmail.com', 0, N'AERgrVDNS5d9lnSu62bDs7UVj9L8KFnT32SyKjRxt2SmfLoNx/RMjUtCK8WJMlQaug==', N'9fc4f1a9-368a-45bf-9899-0f385f0ad631', NULL, 0, 0, NULL, 1, 0, N'clienttest3@gmail.com', 1)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsApproved]) VALUES (N'f2363ef0-c455-454c-9aa2-2cd923fb598d', N'deepdoon@gmail.com', 0, N'AOUblD/Cj6nVjYZxBF4dW1rLQB31XlOVV8EvywPa5Ka5cFGnXYi5nyTtEMjlXEwlVw==', N'82cb4365-f22e-49f8-8d00-78b5761dbeaf', NULL, 0, 0, NULL, 1, 0, N'deepdoon@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[AudienceAge] ON 

INSERT [dbo].[AudienceAge] ([AudienceAgeId], [Title], [MinValue], [MaxValue], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'10-20', 10, 20, N'12', 1, 0, CAST(N'2020-05-20T12:56:09.453' AS DateTime), NULL, NULL, 1, NULL, NULL)
INSERT [dbo].[AudienceAge] ([AudienceAgeId], [Title], [MinValue], [MaxValue], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1006, N'20-30', 20, 30, N'12', 1, 0, CAST(N'2020-05-05T00:00:00.000' AS DateTime), NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[AudienceAge] ([AudienceAgeId], [Title], [MinValue], [MaxValue], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2008, N'30-40', 30, 40, N'12', 1, 0, CAST(N'2019-12-12T00:00:00.000' AS DateTime), NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[AudienceAge] ([AudienceAgeId], [Title], [MinValue], [MaxValue], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (3005, N'40-60', 40, 60, NULL, 1, 0, CAST(N'2020-09-16T17:45:14.947' AS DateTime), NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AudienceAge] OFF
GO
SET IDENTITY_INSERT [dbo].[BankDetail] ON 

INSERT [dbo].[BankDetail] ([BankId], [BankName], [BankAddress], [BankCode], [UserId], [AccountNumber], [AccountHolderName]) VALUES (2, N'TestBank', N'Dehradun', N'248081', N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', N'123', N'Gaurav')
INSERT [dbo].[BankDetail] ([BankId], [BankName], [BankAddress], [BankCode], [UserId], [AccountNumber], [AccountHolderName]) VALUES (1003, N'Test Bnak 3', N'Test Address', N'25825', N'ba785409-c69f-495d-ae4f-347bdf4df834', N'1582589', N'Client Test 3')
SET IDENTITY_INSERT [dbo].[BankDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[BrandMail] ON 

INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6030, 1, N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', N'Project Proposal', N'In marketing, brand management begins with an analysis on how a brand is currently perceived in the market, proceeds to planning how the brand should be perceived if it is to achieve its objectives and continues with ensuring that the brand is perceived as planned and secures its objectives. Developing a good relationship with target markets is essential for brand management.', N'922893f7-12e0-465b-8925-58d2b437c659', CAST(N'2020-06-27T15:06:47.627' AS DateTime), 0, NULL, NULL, 1, 10, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6031, 1, N'22b08833-358b-4b19-b974-74b840b53332', N'Project Proposal', N'In marketing, brand management begins with an analysis on how a brand is currently perceived in the market, proceeds to planning how the brand should be perceived if it is to achieve its objectives and continues with ensuring that the brand is perceived as planned and secures its objectives. Developing a good relationship with target markets is essential for brand management.', N'ba785409-c69f-495d-ae4f-347bdf4df834', CAST(N'2020-06-27T15:15:18.137' AS DateTime), 0, NULL, NULL, 0, 11, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6032, 2, N'22b08833-358b-4b19-b974-74b840b53332', N'Project Proposal', N'In marketing, brand management begins with an analysis on how a brand is currently perceived in the market, proceeds to planning how the brand should be perceived if it is to achieve its objectives and continues with ensuring that the brand is perceived as planned and secures its objectives. Developing a good relationship with target markets is essential for brand management.', N'ba785409-c69f-495d-ae4f-347bdf4df834', CAST(N'2020-06-27T15:15:54.280' AS DateTime), 0, NULL, NULL, 0, 12, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6033, 2, N'ba785409-c69f-495d-ae4f-347bdf4df834', N'Project Proposal', N'In marketing, brand management begins with an analysis on how a brand is currently perceived in the market, proceeds to planning how the brand should be perceived if it is to achieve its objectives and continues with ensuring that the brand is perceived as planned and secures its objectives. Developing a good relationship with target markets is essential for brand management.', N'22b08833-358b-4b19-b974-74b840b53332', CAST(N'2020-06-27T15:22:45.303' AS DateTime), 0, NULL, 6032, 1, NULL, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6034, 4, N'22b08833-358b-4b19-b974-74b840b53332', N'Project Proposal Update', N'Brand management is a branding component that involves maintaining and bettering products, services and brand perception', N'ba785409-c69f-495d-ae4f-347bdf4df834', CAST(N'2020-06-27T15:29:28.737' AS DateTime), 0, NULL, NULL, 0, NULL, 7)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6035, 2, N'22b08833-358b-4b19-b974-74b840b53332', N'Project Proposal', N'test prop', N'922893f7-12e0-465b-8925-58d2b437c659', CAST(N'2020-08-16T16:24:54.603' AS DateTime), 0, NULL, NULL, 1, 13, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6036, 1, N'922893f7-12e0-465b-8925-58d2b437c659', N'Project Proposal', N'test', N'22b08833-358b-4b19-b974-74b840b53332', CAST(N'2020-08-16T16:27:00.740' AS DateTime), 0, NULL, 6035, 1, NULL, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (6037, 4, N'22b08833-358b-4b19-b974-74b840b53332', N'Project Proposal Update', N'test', N'922893f7-12e0-465b-8925-58d2b437c659', CAST(N'2020-08-16T16:38:29.967' AS DateTime), 0, NULL, NULL, 1, NULL, 8)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (7035, 1, N'22b08833-358b-4b19-b974-74b840b53332', N'Project Proposal', N'Test', N'922893f7-12e0-465b-8925-58d2b437c659', CAST(N'2020-09-13T08:51:30.337' AS DateTime), 0, NULL, 6036, 1, NULL, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (7036, 4, N'922893f7-12e0-465b-8925-58d2b437c659', N'Project Proposal Update', N'Test Reply', N'22b08833-358b-4b19-b974-74b840b53332', CAST(N'2020-09-17T05:47:05.287' AS DateTime), 0, NULL, 6037, 1, NULL, NULL)
INSERT [dbo].[BrandMail] ([BrandMailId], [MailTypeId], [MailFrom], [Subject], [Message], [UserId], [CreatedDate], [IsDeleted], [DeleteDate], [PreviousBrandMailId], [IsRead], [ProjectProposalId], [ProjectProposalUpdateId]) VALUES (7037, 4, N'22b08833-358b-4b19-b974-74b840b53332', N'Project Proposal Update', N'test reply', N'922893f7-12e0-465b-8925-58d2b437c659', CAST(N'2020-09-17T17:13:32.743' AS DateTime), 0, NULL, 7036, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[BrandMail] OFF
GO
SET IDENTITY_INSERT [dbo].[BrandMailFile] ON 

INSERT [dbo].[BrandMailFile] ([BrandMailFileId], [BrandMailId], [FileName], [FileData], [FilePath], [ProjectProposalId]) VALUES (1, NULL, N'FP_Users.txt', NULL, N'../Upload/Files/4f148ed2-eea8-4a56-9fa1-3c0b0f207007/FP_Users~692dc841-5a28-444e-a7f0-9e74124a0f4f.txt', 10)
INSERT [dbo].[BrandMailFile] ([BrandMailFileId], [BrandMailId], [FileName], [FileData], [FilePath], [ProjectProposalId]) VALUES (2, NULL, N'FP_Users.txt', NULL, N'../Upload/Files/22b08833-358b-4b19-b974-74b840b53332/FP_Users~d327b81e-8526-463c-b899-bfaa4e583eb3.txt', 11)
INSERT [dbo].[BrandMailFile] ([BrandMailFileId], [BrandMailId], [FileName], [FileData], [FilePath], [ProjectProposalId]) VALUES (3, NULL, N'FP_Users.txt', NULL, N'../Upload/Files/22b08833-358b-4b19-b974-74b840b53332/FP_Users~efde0ec6-a424-46ab-82a9-036d7b7d0582.txt', 12)
INSERT [dbo].[BrandMailFile] ([BrandMailFileId], [BrandMailId], [FileName], [FileData], [FilePath], [ProjectProposalId]) VALUES (4, NULL, N'DeferredPaymentAgreement-2020-08-15T211406.624.pdf', NULL, N'../Upload/Files/22b08833-358b-4b19-b974-74b840b53332/DeferredPaymentAgreement-2020-08-15T211406.624~ac182797-01df-45b2-a7aa-1eeb13016353.pdf', 13)
INSERT [dbo].[BrandMailFile] ([BrandMailFileId], [BrandMailId], [FileName], [FileData], [FilePath], [ProjectProposalId]) VALUES (1004, 7035, N'ClientPageMockup08-11-2020.png', NULL, N'../Upload/Files/22b08833-358b-4b19-b974-74b840b53332/ClientPageMockup08-11-2020~88322e40-6787-4c7d-b01c-4f37a3e41fe1.png', NULL)
INSERT [dbo].[BrandMailFile] ([BrandMailFileId], [BrandMailId], [FileName], [FileData], [FilePath], [ProjectProposalId]) VALUES (1005, 7035, N'ContactsPageMockup08-06-2020.png', NULL, N'../Upload/Files/22b08833-358b-4b19-b974-74b840b53332/ContactsPageMockup08-06-2020~888f6d5c-fea7-40c4-a105-95bb76d5d259.png', NULL)
SET IDENTITY_INSERT [dbo].[BrandMailFile] OFF
GO
SET IDENTITY_INSERT [dbo].[Budget] ON 

INSERT [dbo].[Budget] ([BudgetId], [Title], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'$ 1000', 1, 0, CAST(N'2019-08-31T15:44:54.493' AS DateTime), NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[Budget] ([BudgetId], [Title], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'$ 1500', 1, 0, CAST(N'2019-08-31T15:45:03.313' AS DateTime), 0, CAST(N'2020-05-20T12:57:16.083' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Budget] ([BudgetId], [Title], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (8, N'$ 2500', 1, 0, CAST(N'2020-06-10T23:34:03.610' AS DateTime), 0, NULL, 0, NULL, NULL)
INSERT [dbo].[Budget] ([BudgetId], [Title], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (9, N'5000', 1, 0, CAST(N'2021-07-19T02:32:45.980' AS DateTime), NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Budget] OFF
GO
SET IDENTITY_INSERT [dbo].[Campaign] ON 

INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (3023, N'ba785409-c69f-495d-ae4f-347bdf4df834', NULL, N'1,4,', 3, N'http://localhost:51656/Client/CreateCampaign', N'../Upload/ProductPhoto/ba785409-c69f-495d-ae4f-347bdf4df834/image_584583f8-5208-44ad-bf4b-099c70cb6c9d.png', NULL, N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Test Client 3 Campaign2', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', 3, NULL, N'1006,2,', 8, CAST(N'2020-06-27T14:52:36.047' AS DateTime), NULL, N'Draft', N'All', NULL, N'1,7,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (3024, N'ba785409-c69f-495d-ae4f-347bdf4df834', NULL, N'1,2,', 2, N'http://localhost:51656/Client/CreateCampaign', N'../Upload/ProductPhoto/ba785409-c69f-495d-ae4f-347bdf4df834/image_560428a9-27bf-49c5-95c1-2f5a7976cc5e.png', NULL, N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Client Test3 Campaign', N'CreateCampaign', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', 3, NULL, N'2,', 2, CAST(N'2020-06-27T14:54:06.467' AS DateTime), NULL, N'Publish', N'All', NULL, N'1,', 1, NULL, N'India', 1000, N'USD')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (3025, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'2', 1, N'http://localhost:51656/Client/CreateCampaign', N'../Upload/ProductPhoto/922893f7-12e0-465b-8925-58d2b437c659/image_990786cc-e404-4cd4-933f-20b8aacca83c.png', 0, N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Client 2 Campaign', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', 2, NULL, N'1006,2,', 1, CAST(N'2020-06-27T14:57:38.707' AS DateTime), NULL, N'Publish', N'All', NULL, N'1,2,', 1, NULL, N'India', 100, N'USD')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (3026, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,2,', 3, N'http://localhost:51656/Client/CreateCampaign', N'../Upload/ProductPhoto/922893f7-12e0-465b-8925-58d2b437c659/image_d9c7e154-662d-4da0-9b1a-cc07a0184169.png', 0, N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Client 2 Campaign2', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', 2, NULL, N'1006,', 2, CAST(N'2020-06-27T14:58:51.593' AS DateTime), NULL, N'Publish', N'All', NULL, N'7,', 0, 1, N'India', 500, N'USD')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (3027, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,2,', 2, N'http://localhost:51656/Client/CreateCampaign', N'../Upload/ProductPhoto/ba785409-c69f-495d-ae4f-347bdf4df834/image_560428a9-27bf-49c5-95c1-2f5a7976cc5e.png', NULL, N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Client Test2 Campaign3', N'CreateCampaign', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', 3, NULL, N'2,', 2, CAST(N'2020-06-27T14:54:06.467' AS DateTime), NULL, N'Publish', N'All', NULL, N'1,', 1, NULL, N'India', 20000, N'INR')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (3028, N'ba785409-c69f-495d-ae4f-347bdf4df834', NULL, N'3', 1, N'http://localhost:51656/Client/CreateCampaign', N'../Upload/ProductPhoto/922893f7-12e0-465b-8925-58d2b437c659/image_990786cc-e404-4cd4-933f-20b8aacca83c.png', 0, N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Client 3 Campaign3', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', N'Brand management is a function of marketing that uses techniques to increase the perceived value of a product line or brand over time.', 2, NULL, N'1006,2,', 8, CAST(N'2020-06-27T14:57:38.707' AS DateTime), NULL, N'Publish', N'All', NULL, N'1,2,', 1, NULL, N'India', 10000, N'INR')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (3029, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, 3, N'http://localhost:51656/Client/CreateCampaign', NULL, 0, N'test', N'Test Campaign 22/2020', N'Test', N'TEst', 3, NULL, N'1006,2,', 2, CAST(N'2020-07-22T16:58:42.257' AS DateTime), NULL, N'Draft', N'All', NULL, NULL, NULL, NULL, N'India', 100, N'USD')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (4029, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, 4, N'http://localhost:51656/Client/CreateCampaign?CampaignId=4029', NULL, NULL, N'test', N'test 29/9/2020', N'tet', N'tesat', NULL, NULL, NULL, 8, CAST(N'2020-08-03T07:13:05.630' AS DateTime), NULL, N'Draft', N'All', NULL, NULL, NULL, NULL, N', Russia', 200, N'GBP')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (5029, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', 3, N'http://localhost:51656/Client/CreateCampaign?Length=6', N'../Upload/ProductPhoto/922893f7-12e0-465b-8925-58d2b437c659/d56c23b2-0bb0-44be-b0dd-f73bbfd39afa_fe6c398d-64e1-496d-a58d-997aa49cf3fd.jpg', 1, N'test about', N'test', N'test', N'test', 3, NULL, N'1006,', 1, CAST(N'2020-08-16T15:49:00.313' AS DateTime), NULL, N'Publish', N'All', NULL, N'1,3,', 1, 0, N'India, UK', 2000, N'USD')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6029, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, 4, N'asdtt/', NULL, NULL, N'ddfsdf', N'dfdsf', N'dsfdsfsd', N'sdfdsf', NULL, NULL, NULL, NULL, CAST(N'2020-09-07T08:34:05.917' AS DateTime), NULL, N'Draft', N'Female', NULL, NULL, NULL, NULL, N'India', 2000, N'INR')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6030, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'2,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1006,2,', NULL, CAST(N'2020-09-07T08:34:06.363' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,3,', NULL, NULL, N'India', 100, N'USD')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6031, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, 4, N'teesst', NULL, NULL, N'fghfghfghfgh', N'9/8/2020', N'ddghdgfdfgd', N'hgfjhfghf', NULL, NULL, NULL, NULL, CAST(N'2020-09-08T07:13:44.710' AS DateTime), NULL, N'Draft', N'All', NULL, NULL, NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6032, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2008,', NULL, CAST(N'2020-09-08T07:13:45.023' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6033, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2008,', NULL, CAST(N'2020-09-08T07:15:11.143' AS DateTime), NULL, N'Publish', NULL, NULL, N'1,', NULL, NULL, N'India', 12000, N'INR')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6034, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2008,', NULL, CAST(N'2020-09-08T07:15:50.487' AS DateTime), NULL, N'Publish', NULL, NULL, N'1,', NULL, NULL, N'India', 7400, N'INR')
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6035, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1006,2,', NULL, CAST(N'2020-09-08T07:20:32.980' AS DateTime), NULL, N'Draft', NULL, NULL, NULL, NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (6036, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-09-08T18:12:14.880' AS DateTime), NULL, N'Draft', NULL, NULL, NULL, NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (7029, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, 4, N'http://localhost:51656/Client/CreateCampaign?Length=6', NULL, 0, N'test', N'9/10/2020', N'test', N'tset', 3, NULL, NULL, NULL, CAST(N'2020-09-10T12:39:35.030' AS DateTime), NULL, N'Draft', N'All', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8029, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', 3, N'http://localhost:51656/Client/CreateCampaign?Length=6', N'../Upload/ProductPhoto/922893f7-12e0-465b-8925-58d2b437c659/image_4ed33169-ab61-4b52-9d8f-7cdfd34910cf.png', 0, N'test', N'test camp dd', N'test', N'test goals', 3, NULL, N'2008,', 8, CAST(N'2020-09-13T08:39:37.853' AS DateTime), NULL, N'Send To Platform', N'All', NULL, N'1,3,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8030, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,2,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-09-23T11:20:22.670' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,3,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8031, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,2,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2008,1006,', NULL, CAST(N'2020-09-28T12:49:45.240' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,3,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8032, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-09-29T15:02:06.797' AS DateTime), NULL, N'Draft', NULL, NULL, NULL, NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8033, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'3005,2008,', NULL, CAST(N'2020-09-29T15:03:29.710' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,3,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8034, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-09-29T15:10:31.267' AS DateTime), NULL, N'Draft', NULL, NULL, NULL, NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8035, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-09-29T15:11:17.030' AS DateTime), NULL, N'Draft', NULL, NULL, NULL, NULL, NULL, N', Russia', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8036, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'2,4,', 3, N'http://localhost:51657/Client/CreateCampaign?Length=6', NULL, 0, N'Test 7/15/2021', N'test campaign 7/15/2021', N'Test brand', N'Test goal test', NULL, NULL, NULL, 2, CAST(N'2021-07-15T07:28:48.837' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,6,7,', NULL, NULL, N'India, UK, Russia', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8037, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'2,4,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-07-15T07:28:49.287' AS DateTime), NULL, N'Draft', NULL, NULL, N'6,7,', NULL, NULL, N'India, Russia', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8038, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'2,4,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-07-15T07:30:17.940' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,6,7,', NULL, NULL, N'India, UK, Russia', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8039, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', 2, N'http://localhost:51657/Client/CreateCampaign?Length=6', NULL, 0, N'test teatet http://localhost:51657/Client/CreateCampaign?Length=6', N'7/15/2021', N'http://localhost:51657/Client/CreateCampaign?Length=6 tesfsdfsdf', N'teate http://localhost:51657/Client/CreateCampaign?Length=6', 3, NULL, N'1006,', 8, CAST(N'2021-07-15T07:34:16.710' AS DateTime), NULL, N'Draft', N'All', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8040, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,2,4,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-07-15T07:34:17.223' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,7,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8041, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1', 2, N'http://localhost:51657/Client/CreateCampaign?Length=6 dsfdsfsf1112', NULL, 1, N'http://localhost:51657/Client/CreateCampaign?Length=6', N'test july', N'http://localhost:51657/Client/CreateCampaign?Length=6', N'http://localhost:51657/Client/CreateCampaign?Length=6', 2, 1, N'3005,2008', 2, CAST(N'2021-07-15T07:44:10.153' AS DateTime), NULL, N'Draft', N'Female', NULL, N'3', NULL, NULL, N'Russia', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8042, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'1,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-07-15T07:45:46.983' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8043, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, N'2,', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-07-15T08:00:06.987' AS DateTime), NULL, N'Draft', NULL, NULL, N'1,3', NULL, NULL, N'India', NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8044, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-07-17T05:30:23.377' AS DateTime), NULL, N'Draft', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Campaign] ([CampaignId], [UserId], [CampaignTypeId], [SupplementalChannels], [ProductCategoryId], [ProductURL], [ProductPhoto], [ShippingProduct], [AboutYourProduct], [CampaignTitle], [AboutYourBrand], [CampaignGoal], [CampaignDurationId], [PrivateCampaign], [AudienceAgeId], [BudgetId], [CreatedDate], [ModifyDate], [Status], [AudienceGender], [YouTube], [YouTubeVideoType], [Approved], [Rejected], [Country], [Budget], [CurrencyType]) VALUES (8045, N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2021-07-17T05:54:09.967' AS DateTime), NULL, N'Draft', NULL, NULL, NULL, NULL, NULL, N'India, UK, Russia', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Campaign] OFF
GO
SET IDENTITY_INSERT [dbo].[CampaignDuration] ON 

INSERT [dbo].[CampaignDuration] ([CampaignDurationId], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'20 Days', 1, 0, CAST(N'2019-08-31T15:29:37.143' AS DateTime), NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[CampaignDuration] ([CampaignDurationId], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'30 Days', 1, 0, CAST(N'2019-08-31T15:30:24.497' AS DateTime), 0, CAST(N'2019-08-31T15:38:28.747' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[CampaignDuration] ([CampaignDurationId], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (3, N'45 Days', 1, 0, CAST(N'2019-08-31T15:30:36.777' AS DateTime), NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[CampaignDuration] ([CampaignDurationId], [Duration], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (4, N'60 Days', 1, 0, CAST(N'2021-07-19T02:33:04.707' AS DateTime), NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CampaignDuration] OFF
GO
SET IDENTITY_INSERT [dbo].[CampaignType] ON 

INSERT [dbo].[CampaignType] ([CampaignTypeId], [Title], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'Test', 1, 0, CAST(N'2019-08-31T15:33:28.980' AS DateTime), NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[CampaignType] ([CampaignTypeId], [Title], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'Tes12', 1, 0, CAST(N'2019-08-31T15:33:39.570' AS DateTime), 0, CAST(N'2020-05-20T12:56:31.890' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[CampaignType] OFF
GO
SET IDENTITY_INSERT [dbo].[Channel] ON 

INSERT [dbo].[Channel] ([ChannelId], [Name], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'Test111', 1, 0, CAST(N'2019-08-31T15:47:20.177' AS DateTime), 0, CAST(N'2019-08-31T15:48:54.197' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Channel] ([ChannelId], [Name], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'test1', 1, 0, CAST(N'2019-08-31T15:48:48.877' AS DateTime), NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Channel] OFF
GO
SET IDENTITY_INSERT [dbo].[ClientWalletAmount] ON 

INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2, 102.5, CAST(N'2020-05-30T14:41:59.647' AS DateTime), N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', NULL, NULL)
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (3, 100.25, CAST(N'2020-05-30T14:42:08.780' AS DateTime), N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', NULL, NULL)
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (1003, 5000, CAST(N'2020-06-15T06:50:37.637' AS DateTime), N'922893f7-12e0-465b-8925-58d2b437c659', NULL, NULL)
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2002, 1000, CAST(N'2020-06-20T09:29:28.853' AS DateTime), N'ba785409-c69f-495d-ae4f-347bdf4df834', NULL, NULL)
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2003, 50, CAST(N'2020-06-25T17:37:12.050' AS DateTime), N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', NULL, NULL)
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2004, 150000, CAST(N'2021-07-13T09:57:58.957' AS DateTime), N'922893f7-12e0-465b-8925-58d2b437c659', N'req_DQZBjkj2Fz02AP', N'succeeded')
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2005, 1100, CAST(N'2021-07-15T15:44:38.240' AS DateTime), N'922893f7-12e0-465b-8925-58d2b437c659', N'req_CYDzVGPw0IyVko', N'succeeded')
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2006, 1000, CAST(N'2021-07-16T02:22:50.550' AS DateTime), N'922893f7-12e0-465b-8925-58d2b437c659', N'req_FJCq1fAhmtjqAX', N'succeeded')
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2007, 100, CAST(N'2021-07-16T13:26:28.207' AS DateTime), N'922893f7-12e0-465b-8925-58d2b437c659', N'req_NoocnfEBAPrLji', N'succeeded')
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2008, 1000, CAST(N'2021-07-16T13:38:50.840' AS DateTime), N'922893f7-12e0-465b-8925-58d2b437c659', N'req_2GXXjcbnygKzi3', N'succeeded')
INSERT [dbo].[ClientWalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy], [TransactionID], [Status]) VALUES (2009, 2500, CAST(N'2021-07-16T13:58:57.380' AS DateTime), N'922893f7-12e0-465b-8925-58d2b437c659', N'req_5seHvcpRiQ7jd7', N'succeeded')
SET IDENTITY_INSERT [dbo].[ClientWalletAmount] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([CountryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (4, N'India', 1, 1, CAST(N'2020-02-01T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Country] ([CountryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (6, N'UK', 1, 1, CAST(N'2020-02-01T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Country] ([CountryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (9, N'Russia', 1, 1, CAST(N'2020-01-02T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Country] ([CountryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (12, N'Canada', 1, 1, CAST(N'2020-01-02T00:00:00.000' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Country] ([CountryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (13, N'Australia', 1, 1, CAST(N'2020-02-02T00:00:00.000' AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[CreatorFeedback] ON 

INSERT [dbo].[CreatorFeedback] ([Id], [CreatorId], [Feedback], [Date]) VALUES (1, N'22b08833-358b-4b19-b974-74b840b53332', N'Very Good', NULL)
INSERT [dbo].[CreatorFeedback] ([Id], [CreatorId], [Feedback], [Date]) VALUES (2, N'22b08833-358b-4b19-b974-74b840b53332', N'Something I really appreciate about you is your aptitude for problem solving in a proactive way.', NULL)
SET IDENTITY_INSERT [dbo].[CreatorFeedback] OFF
GO
SET IDENTITY_INSERT [dbo].[CreatorProfile] ON 

INSERT [dbo].[CreatorProfile] ([CreatorId], [UserId], [FullName], [ContactNumber], [State], [CountryId], [YouTube], [Instagram], [Facebook], [CategoryId], [MinimumBudgetedProject], [PastWorkExperience], [Summary], [TargetAudience], [ProfileImage], [DOB], [Language], [Categories], [Gender], [CurrencyType]) VALUES (3, N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', N'Gaurav Singh', N'9720359119', N'UK', 3, N'https://www.youtube.com/channel/UC9trsD1jCTXXtN3xIOIU8gg', N'GauravSingh', N'GauravSingh', NULL, N'1000', NULL, N'Test summary', N'1006,', N'../Images/profile.png', CAST(N'1990-12-25T00:00:00.000' AS DateTime), NULL, NULL, NULL, N'USD')
INSERT [dbo].[CreatorProfile] ([CreatorId], [UserId], [FullName], [ContactNumber], [State], [CountryId], [YouTube], [Instagram], [Facebook], [CategoryId], [MinimumBudgetedProject], [PastWorkExperience], [Summary], [TargetAudience], [ProfileImage], [DOB], [Language], [Categories], [Gender], [CurrencyType]) VALUES (9, N'f9928369-4cf6-45f0-8201-25003d2b567a', N'Creator Test', N'2589586254', N'Test', 6, N'https://www.youtube.com/channel/UCJg19noZp7-BYIGvypu_cow', N'http://localhost:51656/Creator', N'http://localhost:51656/Creator', NULL, N'10000', N'Test', N'Test', N'1006,2,', N'../Images/profile.png', CAST(N'1992-02-05T00:00:00.000' AS DateTime), NULL, NULL, NULL, N'INR')
INSERT [dbo].[CreatorProfile] ([CreatorId], [UserId], [FullName], [ContactNumber], [State], [CountryId], [YouTube], [Instagram], [Facebook], [CategoryId], [MinimumBudgetedProject], [PastWorkExperience], [Summary], [TargetAudience], [ProfileImage], [DOB], [Language], [Categories], [Gender], [CurrencyType]) VALUES (1039, N'22b08833-358b-4b19-b974-74b840b53332', N'Gaurav Rana', N'9720359100', N'uk', 12, N'https://www.youtube.com/channel/UCAXdw_jOLSOgJ4DYIL3eJaA', N'http://localhost:51656/Creator', N'http://localhost:51656/Creator', NULL, N'1200', N'test', N'test', N'2008,1006,', N'~/Upload/Profile/22b08833-358b-4b19-b974-74b840b53332/WIN_20201019_09_45_33_Pro_0b7d45df-b5b7-40a2-a25f-37187ce6f62f.jpg', CAST(N'2000-07-16T00:00:00.000' AS DateTime), N'English', N'Business and Finance, Healthcare', N'Male', N'GBP')
SET IDENTITY_INSERT [dbo].[CreatorProfile] OFF
GO
INSERT [dbo].[CurrencyType] ([CurrencyId], [CurrencyName]) VALUES (N'GBP', N'POUND')
INSERT [dbo].[CurrencyType] ([CurrencyId], [CurrencyName]) VALUES (N'INR', N'INR')
INSERT [dbo].[CurrencyType] ([CurrencyId], [CurrencyName]) VALUES (N'USD', N'USD')
GO
SET IDENTITY_INSERT [dbo].[Level] ON 

INSERT [dbo].[Level] ([LevelId], [Title], [StartRange], [EndRange], [IsActive], [IsDeleted]) VALUES (1, N'Test', N'10', N'50', 1, NULL)
SET IDENTITY_INSERT [dbo].[Level] OFF
GO
SET IDENTITY_INSERT [dbo].[MessageType] ON 

INSERT [dbo].[MessageType] ([MessageTypeId], [Title]) VALUES (1, N'Proposals')
INSERT [dbo].[MessageType] ([MessageTypeId], [Title]) VALUES (2, N'In Productions')
INSERT [dbo].[MessageType] ([MessageTypeId], [Title]) VALUES (3, N'Pending Approval')
INSERT [dbo].[MessageType] ([MessageTypeId], [Title]) VALUES (4, N'Content Edits Requested')
INSERT [dbo].[MessageType] ([MessageTypeId], [Title]) VALUES (5, N'Completed')
INSERT [dbo].[MessageType] ([MessageTypeId], [Title]) VALUES (6, N'Sent')
SET IDENTITY_INSERT [dbo].[MessageType] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([ProductCategoryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'Healthcare', 1, 0, CAST(N'2019-08-29T06:48:02.770' AS DateTime), 0, CAST(N'2019-08-30T13:45:43.007' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'Education', 1, 0, CAST(N'2019-08-30T07:03:16.597' AS DateTime), 0, CAST(N'2019-08-31T15:25:10.930' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (3, N'Business and Finance', 1, 0, CAST(N'2019-09-16T17:40:01.387' AS DateTime), 0, CAST(N'2020-05-20T12:54:40.047' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [Name], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (4, N'Food', 1, 0, CAST(N'2020-02-20T00:00:00.000' AS DateTime), NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
INSERT [dbo].[Project] ([ProjectId], [ProjectTitle], [ProjectDescription], [Budget], [IsActive], [CreateBy], [CurrencyType]) VALUES (1, N'Test Project', N'This is test project of FP', N'10K', 1, N'f2363ef0-c455-454c-9aa2-2cd923fb598d', NULL)
GO
SET IDENTITY_INSERT [dbo].[ProjectProposal] ON 

INSERT [dbo].[ProjectProposal] ([ProjectProposalId], [CampaignId], [ProjectDescription], [PaymentType], [NoOfMilestone], [Milestone1], [Milestone2], [Milestone3], [Status], [UserId], [Approved], [Milestone], [ProposalAmount], [ReceivedAmount], [ProposalDate], [Milestone1Amount], [Milestone2Amount], [Milestone3Amount], [CurrencyType]) VALUES (10, 3025, N'In marketing, brand management begins with an analysis on how a brand is currently perceived in the market, proceeds to planning how the brand should be perceived if it is to achieve its objectives and continues with ensuring that the brand is perceived as planned and secures its objectives. Developing a good relationship with target markets is essential for brand management.', N'1', NULL, NULL, NULL, NULL, N'Publish', N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007', NULL, 0, 1500, NULL, CAST(N'2020-06-27T15:06:47.620' AS DateTime), NULL, NULL, NULL, N'USD')
INSERT [dbo].[ProjectProposal] ([ProjectProposalId], [CampaignId], [ProjectDescription], [PaymentType], [NoOfMilestone], [Milestone1], [Milestone2], [Milestone3], [Status], [UserId], [Approved], [Milestone], [ProposalAmount], [ReceivedAmount], [ProposalDate], [Milestone1Amount], [Milestone2Amount], [Milestone3Amount], [CurrencyType]) VALUES (11, 3028, N'In marketing, brand management begins with an analysis on how a brand is currently perceived in the market, proceeds to planning how the brand should be perceived if it is to achieve its objectives and continues with ensuring that the brand is perceived as planned and secures its objectives. Developing a good relationship with target markets is essential for brand management.', N'2', 2, N'50%', N'100%', NULL, N'Draft', N'22b08833-358b-4b19-b974-74b840b53332', NULL, 0, 198.34, NULL, CAST(N'2021-08-05T16:54:06.000' AS DateTime), 10, 10, NULL, N'USD')
INSERT [dbo].[ProjectProposal] ([ProjectProposalId], [CampaignId], [ProjectDescription], [PaymentType], [NoOfMilestone], [Milestone1], [Milestone2], [Milestone3], [Status], [UserId], [Approved], [Milestone], [ProposalAmount], [ReceivedAmount], [ProposalDate], [Milestone1Amount], [Milestone2Amount], [Milestone3Amount], [CurrencyType]) VALUES (12, 3024, N'In marketing, brand management begins with an analysis on how a brand is currently perceived in the market, proceeds to planning how the brand should be perceived if it is to achieve its objectives and continues with ensuring that the brand is perceived as planned and secures its objectives. Developing a good relationship with target markets is essential for brand management.', N'1', NULL, NULL, NULL, NULL, N'Publish', N'22b08833-358b-4b19-b974-74b840b53332', 1, 0, 12000, NULL, CAST(N'2020-06-27T15:15:54.277' AS DateTime), NULL, NULL, NULL, N'INR')
INSERT [dbo].[ProjectProposal] ([ProjectProposalId], [CampaignId], [ProjectDescription], [PaymentType], [NoOfMilestone], [Milestone1], [Milestone2], [Milestone3], [Status], [UserId], [Approved], [Milestone], [ProposalAmount], [ReceivedAmount], [ProposalDate], [Milestone1Amount], [Milestone2Amount], [Milestone3Amount], [CurrencyType]) VALUES (13, 5029, NULL, NULL, NULL, NULL, NULL, NULL, N'Draft', N'22b08833-358b-4b19-b974-74b840b53332', 1, 0, 1000, NULL, CAST(N'2020-09-03T16:25:44.447' AS DateTime), NULL, NULL, NULL, N'USD')
SET IDENTITY_INSERT [dbo].[ProjectProposal] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectProposalUpdate] ON 

INSERT [dbo].[ProjectProposalUpdate] ([Id], [CampaignId], [Description], [Url], [UserId], [ProjectProposalId], [IsApproved]) VALUES (7, NULL, N'Brand management is a branding component that involves maintaining and bettering products, services and brand perception', N'http://localhost:51656/Creator/Projects', N'22b08833-358b-4b19-b974-74b840b53332', 12, 0)
INSERT [dbo].[ProjectProposalUpdate] ([Id], [CampaignId], [Description], [Url], [UserId], [ProjectProposalId], [IsApproved]) VALUES (8, NULL, N'test', N'http://localhost:51656/Creator/Projects?Length=7#v-pills-2', N'22b08833-358b-4b19-b974-74b840b53332', 13, NULL)
SET IDENTITY_INSERT [dbo].[ProjectProposalUpdate] OFF
GO
SET IDENTITY_INSERT [dbo].[ProjectProposalUpdatesFile] ON 

INSERT [dbo].[ProjectProposalUpdatesFile] ([Id], [ProjectProposalUpdateId], [FileName], [FilePath]) VALUES (1, 6, N'creatordashboard.png', N'../Upload/Files/4f148ed2-eea8-4a56-9fa1-3c0b0f207007/creatordashboard~24ec0911-4e88-4f90-a0bf-00f90f33685c.png')
INSERT [dbo].[ProjectProposalUpdatesFile] ([Id], [ProjectProposalUpdateId], [FileName], [FilePath]) VALUES (2, 6, N'sdsdffds.png', N'../Upload/Files/4f148ed2-eea8-4a56-9fa1-3c0b0f207007/sdsdffds~9778084c-ec15-4d43-8be8-4b87d538b3c0.png')
INSERT [dbo].[ProjectProposalUpdatesFile] ([Id], [ProjectProposalUpdateId], [FileName], [FilePath]) VALUES (3, 7, N'image.png', N'../Upload/Files/22b08833-358b-4b19-b974-74b840b53332/image~17535473-34f9-413a-8e2a-85fd5882a0bd.png')
INSERT [dbo].[ProjectProposalUpdatesFile] ([Id], [ProjectProposalUpdateId], [FileName], [FilePath]) VALUES (4, 8, N'DeferredPaymentAgreement-2020-08-15T210531.261.pdf', N'../Upload/Files/22b08833-358b-4b19-b974-74b840b53332/DeferredPaymentAgreement-2020-08-15T210531.261~c5694efc-82b6-4407-bf5b-f2828dfe43ae.pdf')
SET IDENTITY_INSERT [dbo].[ProjectProposalUpdatesFile] OFF
GO
SET IDENTITY_INSERT [dbo].[SupplementalChannel] ON 

INSERT [dbo].[SupplementalChannel] ([SupplementalId], [Title], [Path], [IsActive]) VALUES (1, N'Twitter', N'../images/twitter.png', 1)
INSERT [dbo].[SupplementalChannel] ([SupplementalId], [Title], [Path], [IsActive]) VALUES (2, N'Instragram', N'../images/instagram.png', 1)
INSERT [dbo].[SupplementalChannel] ([SupplementalId], [Title], [Path], [IsActive]) VALUES (3, N'Thumbrl', N'../images/tumblr.png', 1)
INSERT [dbo].[SupplementalChannel] ([SupplementalId], [Title], [Path], [IsActive]) VALUES (4, N'Facebook', N'../images/facebook.png', 1)
SET IDENTITY_INSERT [dbo].[SupplementalChannel] OFF
GO
SET IDENTITY_INSERT [dbo].[WalletAmount] ON 

INSERT [dbo].[WalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy]) VALUES (3, 200, CAST(N'2020-05-27T18:25:16.113' AS DateTime), N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007')
INSERT [dbo].[WalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy]) VALUES (4, 100, CAST(N'2020-05-27T18:29:10.297' AS DateTime), N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007')
INSERT [dbo].[WalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy]) VALUES (1002, 205, CAST(N'2020-05-28T18:19:08.670' AS DateTime), N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007')
INSERT [dbo].[WalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy]) VALUES (1003, 200, CAST(N'2020-05-28T18:27:16.543' AS DateTime), N'4f148ed2-eea8-4a56-9fa1-3c0b0f207007')
INSERT [dbo].[WalletAmount] ([ID], [Amount], [UploadDate], [UploadedBy]) VALUES (2002, 100, CAST(N'2020-06-25T06:28:14.267' AS DateTime), N'f9928369-4cf6-45f0-8201-25003d2b567a')
SET IDENTITY_INSERT [dbo].[WalletAmount] OFF
GO
SET IDENTITY_INSERT [dbo].[YouTubeVideoType] ON 

INSERT [dbo].[YouTubeVideoType] ([YouTubeVideoTypeId], [Title], [IsActive]) VALUES (1, N'Review', 1)
INSERT [dbo].[YouTubeVideoType] ([YouTubeVideoTypeId], [Title], [IsActive]) VALUES (2, N'Mention', 1)
INSERT [dbo].[YouTubeVideoType] ([YouTubeVideoTypeId], [Title], [IsActive]) VALUES (3, N'Haul', 1)
INSERT [dbo].[YouTubeVideoType] ([YouTubeVideoTypeId], [Title], [IsActive]) VALUES (4, N'LookBook', 1)
INSERT [dbo].[YouTubeVideoType] ([YouTubeVideoTypeId], [Title], [IsActive]) VALUES (5, N'Favorites', 1)
INSERT [dbo].[YouTubeVideoType] ([YouTubeVideoTypeId], [Title], [IsActive]) VALUES (6, N'Tutorial', 1)
INSERT [dbo].[YouTubeVideoType] ([YouTubeVideoTypeId], [Title], [IsActive]) VALUES (7, N'Unboxing', NULL)
SET IDENTITY_INSERT [dbo].[YouTubeVideoType] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 08/08/2021 6:33:57 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 08/08/2021 6:33:57 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 08/08/2021 6:33:57 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 08/08/2021 6:33:57 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 08/08/2021 6:33:57 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 08/08/2021 6:33:57 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AudienceAge] ADD  CONSTRAINT [DF_AudienceAge_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AudienceAge] ADD  CONSTRAINT [DF_AudienceAge_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Budget] ADD  CONSTRAINT [DF_Budget_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Budget] ADD  CONSTRAINT [DF_Budget_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CampaignDuration] ADD  CONSTRAINT [DF_CampaignDuration_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CampaignDuration] ADD  CONSTRAINT [DF_CampaignDuration_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[CampaignType] ADD  CONSTRAINT [DF_CampaignType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CampaignType] ADD  CONSTRAINT [DF_CampaignType_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Channel] ADD  CONSTRAINT [DF_Channel_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Channel] ADD  CONSTRAINT [DF_Channel_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BrandMailFile]  WITH CHECK ADD  CONSTRAINT [FK_BrandMailFile_BrandMail] FOREIGN KEY([BrandMailId])
REFERENCES [dbo].[BrandMail] ([BrandMailId])
GO
ALTER TABLE [dbo].[BrandMailFile] CHECK CONSTRAINT [FK_BrandMailFile_BrandMail]
GO
ALTER TABLE [dbo].[Campaign]  WITH CHECK ADD  CONSTRAINT [FK_CampaignDetails_CampaignDuration] FOREIGN KEY([CampaignDurationId])
REFERENCES [dbo].[CampaignDuration] ([CampaignDurationId])
GO
ALTER TABLE [dbo].[Campaign] CHECK CONSTRAINT [FK_CampaignDetails_CampaignDuration]
GO
ALTER TABLE [dbo].[Campaign]  WITH CHECK ADD  CONSTRAINT [FK_CampaignDetails_ProductCategory] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
GO
ALTER TABLE [dbo].[Campaign] CHECK CONSTRAINT [FK_CampaignDetails_ProductCategory]
GO
ALTER TABLE [dbo].[TargetedCampaign]  WITH CHECK ADD  CONSTRAINT [FK_TargetedCampaign_Campaign] FOREIGN KEY([TargetedCampaignId])
REFERENCES [dbo].[Campaign] ([CampaignId])
GO
ALTER TABLE [dbo].[TargetedCampaign] CHECK CONSTRAINT [FK_TargetedCampaign_Campaign]
GO
USE [master]
GO
ALTER DATABASE [FPNew21] SET  READ_WRITE 
GO
