namespace AGRUsersAPI.DataObjects.Reposnse
{
    public class BaseUserDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool Success { get; set; }

        public string ResponseMessage { get; set; }

        protected BaseUserDto SetSuccess(int userId, string userName, string email, string message)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Email = email;
            this.Success = true;
            this.ResponseMessage= message;

            return this;
        }

        protected BaseUserDto SetFailure(string message)
        {
            this.Success = false;
            this.ResponseMessage = message;

            return this;
        }
    }
}