namespace Ontourage.Web.Models
{
    public class HeaderViewModel
    {
        public string Title { get; }

        public string ActionName { get; }

        public HeaderViewModel(string title, string actionName)
        {
            Title = title;
            ActionName = actionName;
        }
    }
}
