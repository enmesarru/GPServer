using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Category : IBaseEntity<int>
    {
        public string Title { get; set; }
        public int Id { get; set; }
        List<Game> Games { get; set; }
    }
}