using VotingApp.Data.Repositories;
using VotingApp.Models;
using VotingApp.Utilities;

namespace VotingApp.Services;

internal sealed class AuthService
{
    private readonly UserRepository _users = new();

    public User Register(string fullName, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new ArgumentException("Укажите имя.", nameof(fullName));
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Укажите e-mail.", nameof(email));
        }

        if (_users.EmailExists(email))
        {
            throw new InvalidOperationException("Такой e-mail уже зарегистрирован.");
        }

        var hash = PasswordHasher.Hash(password);
        return _users.Create(fullName.Trim(), email.Trim().ToLowerInvariant(), hash);
    }

    public User Login(string email, string password)
    {
        var user = _users.FindByEmail(email.Trim().ToLowerInvariant());
        if (user == null || !PasswordHasher.Verify(password, user.PasswordHash))
        {
            throw new InvalidOperationException("Неверный e-mail или пароль.");
        }

        return user;
    }

    public User LoginAdmin(string email, string password)
    {
        var normalizedEmail = email.Trim();
        var adminPasswordHash = PasswordHasher.Hash(DemoAccounts.AdminPassword);

        if (!string.Equals(normalizedEmail, DemoAccounts.AdminEmail, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Нет прав администратора.");
        }

        if (!string.Equals(PasswordHasher.Hash(password), adminPasswordHash, StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Неверный пароль администратора.");
        }

        return new User
        {
            Id = 0,
            FullName = "Системный администратор",
            Email = DemoAccounts.AdminEmail,
            PasswordHash = adminPasswordHash,
            CreatedAt = DateTime.Now,
            IsAdmin = true
        };
    }
}

