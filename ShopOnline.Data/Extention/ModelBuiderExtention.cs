using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Data.Extention
{
    public static class ModelBuiderExtention
    {
        public static void Seed(this ModelBuilder modelBuider)
        {
            modelBuider.Entity<Category>().HasData(
                new Category()
                {
                    isShowOnHome = true,
                    ParentId = null,
                    Sort="1",
                    Status=Entities.Enum.Status.Active,
                }
                );
            ;
            
        }
    }
}
