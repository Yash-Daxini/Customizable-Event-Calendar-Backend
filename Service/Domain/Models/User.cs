using Core.Interfaces;

namespace Core.Domain.Models;

public class User : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
