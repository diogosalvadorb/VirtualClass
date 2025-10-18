namespace VirtualClass.Application.ViewModel.TeacherViewModels
{
    public class TeacherViewModel
    {
        public TeacherViewModel(Guid id, string bio, string specialty)
        {
            Id = id;
            Bio = bio;
            Specialty = specialty;
        }

        public Guid Id { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
    }
}
