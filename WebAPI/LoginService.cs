
public record LoginRequest(string Username, string Password);
public record LoginToken(Guid Id, DateTime ValidUntil);

public class LoginService
{
    private readonly Dictionary<LoginRequest, LoginToken?> users = new()
    {
        { new LoginRequest("from.hearty@edu.fit.ba", "1234"), null }
    };

    private readonly Dictionary<Guid, LoginToken> tokens = [];

    public LoginToken? TryLogin(LoginRequest loginRequest)
    {
        if (!this.users.TryGetValue(loginRequest, out var loginToken))
            return null;

        if (loginToken == null)
        {
            loginToken = new LoginToken(Guid.NewGuid(), DateTime.Now.AddHours(12));
            this.users[loginRequest] = loginToken;
            this.tokens.Add(loginToken.Id, loginToken);
            return loginToken;
        }

        return loginToken;
    }

    public bool isTokenValid(Guid loginId) {
        if(!this.tokens.TryGetValue(loginId, out var loginToken))
            return false;
        if(loginToken.ValidUntil < DateTime.Now)
            return false;
        return true;
    }
}

