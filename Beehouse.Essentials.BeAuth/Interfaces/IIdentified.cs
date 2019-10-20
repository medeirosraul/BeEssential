using Beehouse.Essentials.BeAuth.Entities;

namespace Beehouse.Essentials.BeAuth.Interfaces
{
    public interface IIdentified
    {
        public EntityIdentity Identity { get; set; }
    }
}
