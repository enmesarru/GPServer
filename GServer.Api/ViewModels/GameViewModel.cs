namespace GServer.Api.ViewModels
{
    public class GameViewModel
    {
        public System.Guid Id { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public int GameRootId { get; set; }
        public UserInformationViewModel User { get; set; }
    }
}