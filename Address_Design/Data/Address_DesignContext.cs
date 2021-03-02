using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Address_Design.Models;

namespace Address_Design.Data
{
    public class Address_DesignContext : DbContext
    {
        public Address_DesignContext (DbContextOptions<Address_DesignContext> options)
            : base(options)
        {
        }

        public DbSet<Address_Design.Models.AddressForm> AddressForm { get; set; }
    }
}
