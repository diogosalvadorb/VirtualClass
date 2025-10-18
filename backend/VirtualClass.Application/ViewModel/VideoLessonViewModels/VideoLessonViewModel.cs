namespace VirtualClass.Application.ViewModel.VideoLessonViewModels
{
    public class VideoLessonViewModel
    {
        public VideoLessonViewModel(Guid id, string title, string description, string videoUrl, int order)
        {
            Id = id;
            Title = title;
            Description = description;
            VideoUrl = videoUrl;
            Order = order;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public int Order { get; set; }
    }
}
