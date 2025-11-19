using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Menu_Restaurante_API.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;
        public string? Description { get; set; } = string.Empty;


        public ICollection<Product> Products { get; set; } = new List<Product>();

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
