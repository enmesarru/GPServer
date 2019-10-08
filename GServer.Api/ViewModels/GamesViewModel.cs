using ApplicationCore.Entities;

namespace GServer.Api.ViewModels
{
    public class GamesViewModel
    {
        public System.Guid Id { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string UserId { get; set; }
        public GameRoot GameRoot { get; set; }
        public UserInformationViewModel User { get; set; }
    }
}