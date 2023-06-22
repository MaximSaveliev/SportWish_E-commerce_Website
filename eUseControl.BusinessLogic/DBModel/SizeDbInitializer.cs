using eUseControl.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BusinessLogic.DBModel
{
    public class SizeDbInitializer
    {
        public static void Seed()
        {
            using (SizeContext db = new SizeContext())
            {
                if (db.Database.Exists())
                {
                    if (!db.Sizes.Any())
                    {
                        var intSizes = Enumerable.Range(35, 16).Select(size => new Size { SizeValueInt = size }).ToList();
                        var strSizes = new List<Size>
                    {
                        new Size { SizeValueStr = "XS" },
                        new Size { SizeValueStr = "S" },
                        new Size { SizeValueStr = "M" },
                        new Size { SizeValueStr = "L" },
                        new Size { SizeValueStr = "XL" },
                        new Size { SizeValueStr = "2XL" },
                        new Size { SizeValueStr = "3XL" },
                        new Size { SizeValueStr = "4XL" },
                        new Size { SizeValueStr = "5XL" }
                    };

                        db.Sizes.AddRange(intSizes);
                        db.Sizes.AddRange(strSizes);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
