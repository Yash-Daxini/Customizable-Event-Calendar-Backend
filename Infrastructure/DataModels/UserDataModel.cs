using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DataModels
{
    [Table("User")]
    public class UserDataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
