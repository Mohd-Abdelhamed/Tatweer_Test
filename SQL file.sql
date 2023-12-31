﻿USE [master]
GO
/****** Object:  Database [Tatweer_assignment]    Script Date: 9/16/2023 7:15:57 AM ******/
CREATE DATABASE [Tatweer_assignment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tatweer_assignment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Tatweer_assignment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Tatweer_assignment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Tatweer_assignment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Tatweer_assignment] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tatweer_assignment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tatweer_assignment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tatweer_assignment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tatweer_assignment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Tatweer_assignment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tatweer_assignment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET RECOVERY FULL 
GO
ALTER DATABASE [Tatweer_assignment] SET  MULTI_USER 
GO
ALTER DATABASE [Tatweer_assignment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tatweer_assignment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tatweer_assignment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tatweer_assignment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Tatweer_assignment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Tatweer_assignment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Tatweer_assignment', N'ON'
GO
ALTER DATABASE [Tatweer_assignment] SET QUERY_STORE = ON
GO
ALTER DATABASE [Tatweer_assignment] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Tatweer_assignment]
GO
/****** Object:  Table [dbo].[chartData]    Script Date: 9/16/2023 7:15:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[chartData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [varchar](200) NULL,
	[Value1] [decimal](18, 0) NULL,
	[value2] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 9/16/2023 7:15:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](200) NULL,
	[Password] [varchar](200) NULL,
	[Email] [varchar](200) NULL,
	[Mobile] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_ChartData]    Script Date: 9/16/2023 7:15:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ChartData]
    @Date VARCHAR(200),
    @Value1 DECIMAL,
    @Value2 DECIMAL
AS
BEGIN
    INSERT INTO chartData (Date, Value1, Value2)
    VALUES (@Date, @Value1, @Value2)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EditChartData]    Script Date: 9/16/2023 7:15:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EditChartData]
	@ID int,
    @ChartDate VARCHAR(200),
    @NewValue1 DECIMAL,
    @NewValue2 DECIMAL
AS
BEGIN
    UPDATE chartData
    SET 
	Date = @ChartDate,
	Value1 = @NewValue1,
	Value2 = @NewValue2	
    WHERE [ID] = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_login]    Script Date: 9/16/2023 7:15:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_login](@Email VARCHAR(100),@Password VARCHAR(100))
AS
BEGIN
	SELECT * FROM users where Email = @Email and Password = @Password;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_register]    Script Date: 9/16/2023 7:15:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_register](@Username VARCHAR(100),@Password VARCHAR(100),@Email VARCHAR(100),@Mobile VARCHAR(100),@ErrorMessage VARCHAR(200) OUTPUT)
AS
Begin
	IF NOT EXISTS(SELECT 1 from users WHERE Email=@Email)
	BEGIN
		INSERT INTO users(Username,Password,Email,Mobile) VALUES (@Username,@Password,@Email,@Mobile)
		SET @ErrorMessage = 'User Created Sucessfully';
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'User Already Exist With Same Email';
	END
END
GO
USE [master]
GO
ALTER DATABASE [Tatweer_assignment] SET  READ_WRITE 
GO
