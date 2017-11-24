namespace AGRUsersAPI.DataObjects.Reposnse
{
    public class LoginUserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool Success { get; set; }

        public string ResponseMessage { get; set; }

        public LoginUserDto LoginSuccess(int userId, string userName, string email)
        {
            this.Success = true;
            this.ResponseMessage = "Succes on login.";

            this.UserId = userId;
            this.UserName = userName;
            this.Email = email;

            return this;
        }

        public LoginUserDto LoginFail()
        {
            this.Success = false;
            this.ResponseMessage = "Login or password invalid.";

            return this;
        }
    }
}