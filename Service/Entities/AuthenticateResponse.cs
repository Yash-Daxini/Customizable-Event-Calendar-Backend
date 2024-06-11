namespace Core.Entities;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email{ get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(User user,string token)
    {
        Id = user.Id;
        Name = user.Name;
        Email = user.Email;
        Token = token;
    }
}
