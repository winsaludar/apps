namespace Authentication.Infrastructure.Services;

public class EmailService(SmtpSettings smtpSettings, EmailSettings emailConfirmationSettings) : IEmailService
{
    public async Task SendEmailConfirmation(string email, string token, string name)
    {
        string template = GetEmailTemplateAsync(emailConfirmationSettings.EmailConfirmationTemplateName);
        string link = $"{emailConfirmationSettings.ClientBaseUrl}/{emailConfirmationSettings.ClientConfirmEmailPath}?email={email}&token={token}";
        StringBuilder message = new(template);
        message.Replace("{{username}}", name)
            .Replace("{{link}}", link)
            .Replace("{{fromEmail}}", smtpSettings.FromEmail)
            .Replace("{{fromName}}", smtpSettings.FromName);

        await SendAsync(email, emailConfirmationSettings.EmailConfirmationSubject, message.ToString());
    }

    public async Task SendForgotPasswordUrl(string email, string token, string name)
    {
        string template = GetEmailTemplateAsync(emailConfirmationSettings.ForgotPasswordTemplateName);
        string link = $"{emailConfirmationSettings.ClientBaseUrl}/{emailConfirmationSettings.ClientResetPasswordPath}?email={email}&token={token}";
        StringBuilder message = new(template);
        message.Replace("{{username}}", name)
            .Replace("{{link}}", link)
            .Replace("{{fromEmail}}", smtpSettings.FromEmail)
            .Replace("{{fromName}}", smtpSettings.FromName);

        await SendAsync(email, emailConfirmationSettings.ForgotPasswordSubject, message.ToString());
    }

    private static string GetEmailTemplateAsync(string templateName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string resourceName = $"{assembly.GetName().Name}.Templates.{templateName}";
        
        using Stream? stream = assembly.GetManifestResourceStream(resourceName) 
            ?? throw new EmailException("Email template does not exist");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private async Task SendAsync(string recipient, string subject, string message)
    {
        using SmtpClient client = new(smtpSettings.Host, smtpSettings.Port);
        client.Credentials = new NetworkCredential(smtpSettings.Login, smtpSettings.Password);
        client.EnableSsl = smtpSettings.EnableSsl;

        MailMessage mailMessage = new()
        {
            From = new MailAddress(smtpSettings.FromEmail, smtpSettings.FromName),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };

        mailMessage.To.Add(recipient);

        await client.SendMailAsync(mailMessage);
    }
}
