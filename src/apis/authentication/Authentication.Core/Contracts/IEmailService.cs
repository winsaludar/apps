namespace Authentication.Core.Contracts;

public interface IEmailService
{
    Task SendEmailConfirmation(string email, string token, string name);
    Task SendForgotPasswordUrl(string email, string token, string name);
}
