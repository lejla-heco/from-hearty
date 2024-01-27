
public record LoginRequest(string Username, string Password);
public enum RoleType { Doctor, Cardiolog, Patient };
public record LoginToken(Guid Id, DateTime ValidUntil, RoleType RoleType);

public class LoginService
{
    private readonly Dictionary<string, RoleType> roleTypeByUsername = new()
    {
        { "from.hearty.doctor@edu.fit.ba", RoleType.Doctor },
        { "from.hearty.cardiolog@edu.fit.ba", RoleType.Cardiolog },
        { "from.hearty.patient@edu.fit.ba", RoleType.Patient },
    };

    private readonly Dictionary<LoginRequest, LoginToken?> users = new()
    {
        { new LoginRequest("from.hearty.doctor@edu.fit.ba", "1234"), null },
        { new LoginRequest("from.hearty.cardiolog@edu.fit.ba", "1234"), null },
        { new LoginRequest("from.hearty.patient@edu.fit.ba", "1234"), null }
    };

    private readonly Dictionary<Guid, LoginToken> tokens = [];

    public LoginToken? TryLogin(LoginRequest loginRequest)
    {
        if (!this.users.TryGetValue(loginRequest, out var loginToken))
            return null;

        if (loginToken?.ValidUntil < DateTime.Now)
            this.tokens.Remove(loginToken.Id);

        if (loginToken == null || loginToken.ValidUntil < DateTime.Now)
        {
            loginToken = new LoginToken(
                Guid.NewGuid(),
                DateTime.Now.AddHours(12),
                roleTypeByUsername[loginRequest.Username]
            );
            this.users[loginRequest] = loginToken;
            this.tokens.Add(loginToken.Id, loginToken);
        }

        return loginToken;
    }

    public bool isTokenValid(Guid loginId)
    {
        if (!this.tokens.TryGetValue(loginId, out var loginToken))
            return false;
        if (loginToken.ValidUntil < DateTime.Now)
            return false;
        return true;
    }
}

