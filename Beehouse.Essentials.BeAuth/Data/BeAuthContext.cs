using Beehouse.Essentials.BeAuth.Entities.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beehouse.Essentials.BeAuth.Data
{
    public class BeAuthContext: IdentityDbContext<BeAuthIdentity>
    {
        public BeAuthContext(DbContextOptions<BeAuthContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
