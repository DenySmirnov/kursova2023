using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LegoPC.Models
{
    public class AccessoryDbInitializer : DropCreateDatabaseAlways<AccessoryContext>
    {
        protected override void Seed(AccessoryContext db)
        {
            db.Accessories.Add(new Accessory { Name = "Война и мир", Type = "Л.Толстой", Price = 220 });
            db.Accessories.Add(new Accessory { Name = "Отцы и дети", Type = "И.Тургенев", Price = 180 });
            db.Accessories.Add(new Accessory { Name = "Чайка", Type = "А. Чехов", Price = 150 });
            base.Seed(db);
        }
    }
}