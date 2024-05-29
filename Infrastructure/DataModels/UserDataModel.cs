using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;

namespace Infrastructure.DataModels;

[Table("User")]
public class UserDataModel : IModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
