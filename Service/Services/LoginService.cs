
using Microsoft.EntityFrameworkCore;

public record LoginRequest(string Username, string Password);
public enum RoleType { Doctor, Cardiolog, Patient };
public record LoginToken(Guid Id, DateTime ValidUntil, String RoleType, Guid UserId);
public class LoginWrapper {
    public LoginWrapper(LoginToken? loginToken, String roleType, Guid userId)
    {
        this.LoginToken = loginToken;
        this.RoleType = roleType;
        this.UserId = userId;
    }
    
    public LoginToken? LoginToken { get; set; }
    public String RoleType { get; set; }
    public Guid UserId { get; set; }
}

public class LoginService
{
    private readonly Dictionary<string, LoginWrapper> loginWrapperByLoginRequest = [];
    private readonly Dictionary<Guid, LoginWrapper> loginWrapperByLoginId = [];

    public LoginToken? TryLogin(LoginRequest loginRequest, MyContext context)
    {
        var loggedInUser = context.Users.Where(x => x.Email == loginRequest.Username && x.Password == loginRequest.Password).SingleOrDefault();
        
        if (loggedInUser != null) {
            var roleType = context.UserRoles.Where(x => x.Id == loggedInUser.UserRoleId).SingleOrDefault();
            if (roleType != null) {

                var userId = loggedInUser.Id;
                var userQuery = context.Users
                    .Where(user => user.Id == loggedInUser.Id)
                    .Include(user => user.HouseDoctor)
                    .Include(user => user.Cardiologist)
                    .Include(user => user.Patient);

                var user = userQuery.FirstOrDefault();

                if (user != null)
                {
                    if (roleType.Role == "Doctor")
                    {
                        userId = user.HouseDoctor[0].Id;
                    }
                    else if (roleType.Role == "Cardiolog")
                    {
                        userId = user.Cardiologist[0].Id;
                    }
                    else if (roleType.Role == "Patient")
                    {
                        userId = user.Patient[0].Id;
                    }
                }

                if (!this.loginWrapperByLoginRequest.TryGetValue(loginRequest.ToString(), out var loginWrapper))
                {
                    loginWrapperByLoginRequest.Add(loginRequest.ToString(), new LoginWrapper(null, roleType.Role, userId));
                    loginWrapper = new LoginWrapper(null, roleType.Role, userId);
                }

                if (loginWrapper.LoginToken?.ValidUntil < DateTime.Now)
                    this.loginWrapperByLoginId.Remove(loginWrapper.LoginToken.Id);

                if (loginWrapper.LoginToken == null || loginWrapper.LoginToken.ValidUntil < DateTime.Now)
                {
                    loginWrapper.LoginToken = new LoginToken(
                        Guid.NewGuid(),
                        DateTime.Now.AddHours(12),
                        roleType.Role,
                        userId
                    );

                    this.loginWrapperByLoginRequest[loginRequest.ToString()] = loginWrapper;
                    this.loginWrapperByLoginId.Add(loginWrapper.LoginToken.Id, loginWrapper);
                }

                return loginWrapper.LoginToken;

            }
        }

        return null;
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

