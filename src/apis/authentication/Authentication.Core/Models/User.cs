namespace Authentication.Core.Models;

public class User
{
    private User() { }

    public static User Create(
        string username, 
        string email, 
        Guid? id = null,
        bool isEmailConfirmed = false)
    {
        return new User 
        {
            Id = id ?? Guid.Empty,
            Username = username, 
            Email = email, 
            IsEmailConfirmed = isEmailConfirmed
        };
    }

    public Guid Id { get; private set; }
    public string Username { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public bool IsEmailConfirmed { get; set; } = false;

    public void SetId(Guid id) => Id = id;
}
