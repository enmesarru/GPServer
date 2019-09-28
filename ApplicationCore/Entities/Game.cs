using System;

namespace ApplicationCore.Entities
{
    public class Game : IBaseEntity<Guid>
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string Link { get; set; }

        public int Vote { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }

        public int GameRootId { get; set; }

        public string Name { get; set; }

        public virtual GameRoot GameRoot { get; set; }
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public Guid Id { get; set; }
    }
}