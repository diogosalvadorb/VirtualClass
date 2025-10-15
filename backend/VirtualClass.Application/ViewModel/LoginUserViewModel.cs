namespace VirtualClass.Application.ViewModel
{
    public class LoginUserViewModel
    {
        public LoginUserViewModel(string name, string role, string email, string token)
        {
            Name = name;
            Role = role;
            Email = email;
            Token = token;
        }

        public string Name { get; private set; }
        public string Role { get; private set; }
        public string Email { get; private set; }
        public string Token { get; private set; }
    }
}
