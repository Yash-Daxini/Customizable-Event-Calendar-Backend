using Core.Interfaces;

namespace Core.Entities;

public class User : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is User user)
        {
            return this.Id == user.Id
                && this.Name.Equals(user.Name)
                && this.Email.Equals(user.Email)
                && this.Password.Equals(user.Password);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Id,
                                this.Name,
                                this.Email,
                                this.Password);
    }
}
