namespace GM.Store.Shared.Models
{
    public class PageModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string RelativePageUrl { get; set; }
        public string PageTitle { get; set; }
        public bool? ShowInMenu { get; set; }
        public bool? ShowInFooter { get; set; }
    }

    public class PageDetailModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Title { get; set; }
        public string RelativePageUrl { get; set; }
        public string PageContent { get; set; }
        public string PageTitle { get; set; }
        public string MetaKey { get; set; }
        public string MetaDescription { get; set; }
    }
}
