namespace AGRUsersAPI.DataObjects.Reposnse
{
    public class LoginUserDto : BaseUserDto
    {
        public LoginUserDto LoginSuccess(int userId, string userName, string email)
        {
            return this.SetSuccess(userId, userName, email, "Succes on login.") as LoginUserDto;
        }

        public LoginUserDto LoginFail()
        {
            return this.SetFailure("Login or password invalid.") as LoginUserDto;
        }
    }
}