using Beehouse.Essentials.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuth.Entities.Identities
{
    public class BeAuthProfile:BaseEntity
    {
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
    }
}
