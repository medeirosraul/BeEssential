using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.Entities
{
    /// <summary>
    /// Base Entity Class
    /// </summary>
    public abstract class Entity
    {
        public virtual string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
