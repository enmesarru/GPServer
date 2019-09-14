using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class GameRoot : IBaseEntity<int>
    {
        public string Name { get; set; }
        public int Id { get; set; }
        List<Game> Games { get; set; }
    }
}