using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuthMongo.Entities
{
    public class EntityStamp
    {
        public DateTime ModifiedAt { get; set; }
        public string By { get; set; }
    }
}
