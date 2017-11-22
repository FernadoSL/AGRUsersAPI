namespace AGRUsersAPI.DataObjects.Reposnse
{
    public class RegisterUserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool Success { get; set; }

        public string ResponseMessage { get; set; }

        public RegisterUserDto() { }

        public RegisterUserDto(int userId, string userName, string email, bool success, string responseMessage)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Email = email;
            this.Success = success;
            this.ResponseMessage = responseMessage;
        }

        public RegisterUserDto EmailInUse()
        {
            this.Success = false;
            this.ResponseMessage = "This email already in use.";

            return this;
        }

        public RegisterUserDto InvalidEmail()
        {
            this.Success = false;
            this.ResponseMessage = "This email is invalid.";

            return this;
        }

        public RegisterUserDto NameInUse()
        {
            this.Success = false;
            this.ResponseMessage = "This user name already in use.";

            return this;
        }

        public RegisterUserDto RegisterSuccess(int userId, string userName, string email)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Email = email;
            this.Success = true;
            this.ResponseMessage= "Success on register.";

            return this;
        }
    }
}