namespace GServer.Api.ViewModels
{
    public class GameViewModel
    {
        public string Link { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public int GameRootId { get; set; }
    }
}