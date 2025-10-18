namespace VirtualClass.Application.ViewModel.CourseModuleViewModels
{
    public class CourseModuleViewModel
    {
        public CourseModuleViewModel(Guid id, string title, string description, int order)
        {
            Id = id;
            Title = title;
            Description = description;
            Order = order;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
    }
}
