USE [master];
CREATE LOGIN [sf.application] WITH PASSWORD = '#4F(?[o1[8Tls)}52JkvCzev5Pt@Im';
GO

USE [safit];
CREATE USER [sf.application] FOR LOGIN [sf.application];
GRANT EXECUTE TO [sf.application];
GO