CREATE TABLE [dbo].[Gemeente](
	[NIScode] [int] NOT NULL,
	[gemeentenaam] [varchar](250) NOT NULL,
	CONSTRAINT [PK_Gemeente] PRIMARY KEY CLUSTERED 
	(
		[NIScode] ASC
	)
)