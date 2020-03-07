using System;

namespace Beehouse.Essentials.Entities
{
    /// <summary>
    /// Base Entity Class
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Entity identificator
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// When entity created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Last entity modification
        /// </summary>
        public DateTime ModifiedAt { get; set; }

        /// <summary>
        /// If entity is deleted
        /// </summary>
        public bool Deleted { get; set; }
    }
}
