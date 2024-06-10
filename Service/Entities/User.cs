using Core.Interfaces;

namespace Core.Entities;

public class User : IEntity
{
    public User(int Id, string Name, string Email, string Password)
    {
        if (!ValidateProperties(Id, Name, Email, Password))
            throw new ArgumentException("Invalid property values !");

        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.Password = Password;
    }

    public User() { }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    private bool ValidateProperties(int id, string name, string email, string password)
    {
        return id >= 0
            && name is not null
            && email is not null
            && password is not null
            && name.Length > 0
            && email.Length > 0
            && password.Length > 0;
    }
}
