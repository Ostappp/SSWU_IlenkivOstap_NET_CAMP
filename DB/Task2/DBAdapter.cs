using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Task2.DB;

namespace Task2
{
    internal class DBAdapter
    {
        private ComputerStoreModelContainer context;
        public DBAdapter(ComputerStoreModelContainer context)
        {
            this.context = context;
        }
        public DBAdapter()
        {
            context = new ComputerStoreModelContainer();
        }
        public void ClearDB()
        {

        }
        public void RefillComputerStoreDB()
        {
            Category PCs = new Category
            {
                Id= 0,
                Name = "Computers And Laptops",
                Description = "Category include many different laptops and builded PC",
            };
            Category DRAM = new Category
            {
                Id = 1,
                Name = "Flash",
                Description = "Category contaions various types of DRAM memory for your PC"
            };
            Manufacturer Apple = new Manufacturer
            {
                Id = 0,
                Name = "Apple",
                WEB_Site_Link = "apple.com",
                Country = "USA"
            }; 
            Manufacturer ARTLINE = new Manufacturer
            {
                Id = 1,
                Name = "ARTLINE",
                WEB_Site_Link = "artline.ua",
                Country = "Ukraine"
            };
            Item MacBookAir13 = new Item
            {
                Id = 0,
                Name = "MacBook Air 13",
                Description = "The new 15‑inch MacBook Air makes room for more of what you love with a spacious Liquid Retina display. And with the 13‑inch model, you have more reasons than ever to choose Air. Supercharged by the M2 chip — and with up to 18 hours of battery life1 — both laptops deliver blazing-fast performance in an ultraportable design.",
                Price = 57499,
                SerialNum = "M2 Midnight 2022 (MLY33)",
                DateOfManufaturer = new DateTime(2022, 7, 1),
                CategoryId = PCs.Id,
                ManufacturerId = Apple.Id,
            };
            Item ARTLINEOverlordX97 = new Item
            {
                Id = 1,
                Name = "ARTLINE Overlord X97",
                Price = 103599,
                SerialNum = "X97v85",
                DateOfManufaturer = new DateTime(2022, 5, 1),
                CategoryId = PCs.Id,
                ManufacturerId = ARTLINE.Id,
            };

            ItemParams MacBookAir13Params = new ItemParams
            {
                Height = 1.13,
                Width = 30.41,
                Depth = 21.5,
                Weight = 1.24,
                Item = MacBookAir13,
            }; 
            ItemParams ARTLINEOverlordX97Params = new ItemParams
            {
                Height = 63,
                Width = 30,
                Depth = 65.5,
                Weight = 26,
                Item = ARTLINEOverlordX97,
            };
            context.CategorySet.RemoveRange(context.CategorySet.ToArray());
            context.ManufacturerSet.RemoveRange(context.ManufacturerSet.ToArray());
            context.ItemSet.RemoveRange(context.ItemSet.ToArray());
            context.ItemParamsSet.RemoveRange(context.ItemParamsSet.ToArray());
            context.SaveChanges();

            context.CategorySet.Add(PCs);
            context.CategorySet.Add(DRAM);

            context.ManufacturerSet.Add(Apple);
            context.ManufacturerSet.Add(ARTLINE);

            context.ItemSet.Add(MacBookAir13);
            context.ItemSet.Add(ARTLINEOverlordX97);

            context.ItemParamsSet.RemoveRange(context.ItemParamsSet.ToArray());
            context.ItemParamsSet.Add(MacBookAir13Params);
            context.ItemParamsSet.Add(ARTLINEOverlordX97Params);

            context.SaveChanges();
            Console.WriteLine(ARTLINEOverlordX97.Id+"\t"+ ARTLINEOverlordX97.Name);

        }
    }
}
