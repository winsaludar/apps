namespace Authentication.Core.Models;

public class User
{
    private User() { }

    public static User Create(string username, string email, string password, Guid? id = null)
    {
        return new User 
        {
            Id = id ?? Guid.Empty,
            Username = username, 
            Email = email, 
            Password = password 
        };
    }

    public Guid Id { get; private set; }
    public string Username { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;

    public void SetId(Guid id) => Id = id;
}
