using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LegoPC.Models
{
    public class AccessoryContext : System.Data.Entity.DbContext
    {
        public DbSet<Accessory>Accessories { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
    }
}