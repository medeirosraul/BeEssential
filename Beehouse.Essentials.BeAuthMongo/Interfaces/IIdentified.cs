using Beehouse.Essentials.BeAuthMongo.Entities;
using System.Collections.Generic;

namespace Beehouse.Essentials.BeAuthMongo.Interfaces
{
    public interface IIdentified
    {
        public EntityIdentity Identity { get; set; }
        ICollection<EntityStamp> Stamps { get; set; }
    }
}
