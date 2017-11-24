namespace AGRUsersAPI.DataObjects.Reposnse
{
    public class RegisterUserDto : BaseUserDto
    {
        public RegisterUserDto() { }
        
        public RegisterUserDto EmailInUse()
        {
            return this.SetFailure("This email already in use.") as RegisterUserDto;
        }

        public RegisterUserDto InvalidEmail()
        {
            return this.SetFailure("This email is invalid.") as RegisterUserDto;
        }

        public RegisterUserDto NameInUse()
        {
            return this.SetFailure("This user name already in use.") as RegisterUserDto;
        }

        public RegisterUserDto RegisterSuccess(int userId, string userName, string email)
        {
            return this.SetSuccess(userId, userName, email, "Success on register.") as RegisterUserDto;
        }
    }
}