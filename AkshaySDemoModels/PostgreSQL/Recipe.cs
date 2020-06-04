using System.ComponentModel.DataAnnotations.Schema;

namespace AkshaySDemoModels.PostgreSQL
{
    [Table("recipes")]
    public class Recipe
    {
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("creatorsname")]
        public string CreatorsName { get; set; }

        [Column("notes")]
        public string Notes { get; set; }
    }
}

/* Azure SQL Table DDL

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recipes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Recipes] ADD [Comment] VARCHAR(max) NULL
GO

*/
