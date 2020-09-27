using System.ComponentModel.DataAnnotations.Schema;

namespace AkshaySDemoModels.Oracle
{
    public class Recipe
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column("RecipeComment")]
        public string Comment { get; set; }

        public string CreatorsName { get; set; }

        public string Notes { get; set; }
    }
}
