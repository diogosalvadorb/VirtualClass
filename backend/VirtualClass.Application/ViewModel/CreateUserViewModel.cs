namespace VirtualClass.Application.ViewModel
{
    public class CreateUserViewModel
    {
        public CreateUserViewModel(string email)
        {
            Email = email;
        }

        public string Email { get; set; } = string.Empty;
    }
}
