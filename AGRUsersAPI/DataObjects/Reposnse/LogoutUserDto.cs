namespace AGRUsersAPI.DataObjects.Reposnse
{
    public class LogoutUserDto : BaseUserDto
    {
        public LogoutUserDto SuccessLogout(int userId, string userName, string email)
        {
            return this.SetSuccess(userId, userName, email, "Success on logout.") as LogoutUserDto;
        }

        public LogoutUserDto LogoutFail()
        {
            return this.SetFailure("Fail on logout.") as LogoutUserDto;
        }
    }
}