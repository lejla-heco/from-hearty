
public record LoginRequest(string Username, string Password);
public enum RoleType { Doctor, Cardiolog, Patient };
public record LoginToken(Guid Id, DateTime ValidUntil, RoleType RoleType);
public class LoginWrapper {
    public LoginWrapper(LoginToken? loginToken, RoleType roleType)
    {
        this.LoginToken = loginToken;
        this.RoleType = roleType;
    }
    
    public LoginToken? LoginToken { get; set; }
    public RoleType RoleType { get; set; }
}

public class LoginService
{
    private readonly Dictionary<string, LoginWrapper> loginWrapperByLoginRequest = new()
    {
        { new LoginRequest("from.hearty.doctor@edu.fit.ba", "1234").ToString(), new LoginWrapper(null, RoleType.Doctor) },
        { new LoginRequest("from.hearty.cardiolog@edu.fit.ba", "1234").ToString(), new LoginWrapper(null, RoleType.Cardiolog) },
        { new LoginRequest("from.hearty.patient@edu.fit.ba", "1234").ToString(), new LoginWrapper(null, RoleType.Patient) }
    };

    private readonly Dictionary<Guid, LoginWrapper> loginWrapperByLoginId = [];

    public LoginToken? TryLogin(LoginRequest loginRequest)
    {
        if (!this.loginWrapperByLoginRequest.TryGetValue(loginRequest.ToString(), out var loginWrapper))
            return null;

        if (loginWrapper.LoginToken?.ValidUntil < DateTime.Now)
            this.loginWrapperByLoginId.Remove(loginWrapper.LoginToken.Id);

        if (loginWrapper.LoginToken == null || loginWrapper.LoginToken.ValidUntil < DateTime.Now)
        {
            loginWrapper.LoginToken = new LoginToken(
                Guid.NewGuid(),
                DateTime.Now.AddHours(12),
                loginWrapper.RoleType
            );

            this.loginWrapperByLoginRequest[loginRequest.ToString()] = loginWrapper;
            this.loginWrapperByLoginId.Add(loginWrapper.LoginToken.Id, loginWrapper);
        }

        return loginWrapper.LoginToken;
    }

    public bool TryLogout(Guid loginId)
    {
        if(this.loginWrapperByLoginId.TryGetValue(loginId, out var loginWrapper))
        {
            loginWrapper.LoginToken = null;
            return true;
        }
        return false;
    }

    public bool isTokenValid(Guid loginId)
    {
        if (!this.loginWrapperByLoginId.TryGetValue(loginId, out var loginWrapper))
            return false;
        if (loginWrapper.LoginToken?.ValidUntil < DateTime.Now)
            return false;
        return true;
    }
}

