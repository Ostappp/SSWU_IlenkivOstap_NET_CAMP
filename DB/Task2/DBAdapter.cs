using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Task2.DB;

namespace Task2
{
    internal class DBAdapter
    {
        private ComputerStoreModelContainer context;
        public DBAdapter(ComputerStoreModelContainer context)
        {
            this.context = context;
            context.CategorySet.Load();
            context.ManufacturerSet.Load();
            context.ItemSet.Load();
            context.ItemParamsSet.Load();
        }
        public DBAdapter()
        {
            context = new ComputerStoreModelContainer();
            context.CategorySet.Load();
            context.ManufacturerSet.Load();
            context.ItemSet.Load();
            context.ItemParamsSet.Load();

        }
        public void ClearDB()
        {
            context.CategorySet.RemoveRange(context.CategorySet.ToArray());
            context.ManufacturerSet.RemoveRange(context.ManufacturerSet.ToArray());
            context.ItemSet.RemoveRange(context.ItemSet.ToArray());
            context.ItemParamsSet.RemoveRange(context.ItemParamsSet.ToArray());
            context.SaveChanges();
        }
        public void RefillComputerStoreDB()
        {
            Category PCs = new Category
            {
                //Id = 0,
                Name = "Computers And Laptops",
                Description = "Category include many different laptops and builded PC",
            };
            Category DRAM = new Category
            {
                //Id = 1,
                Name = "Flash",
                Description = "Category contaions various types of DRAM memory for your PC"
            };
            Manufacturer Apple = new Manufacturer
            {
                //Id = 0,
                Name = "Apple",
                WEB_Site_Link = "apple.com",
                Country = "USA"
            };
            Manufacturer ARTLINE = new Manufacturer
            {
                //Id = 1,
                Name = "ARTLINE",
                WEB_Site_Link = "artline.ua",
                Country = "Ukraine"
            };
            Item MacBookAir13 = new Item
            {
                //Id = 0,
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
                //Id = 1,
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
            ClearDB();

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

        }

        public IEnumerable<int> GetCategoryIds()
        {
            List<int> ids = new List<int>();
            context.CategorySet.ToList().ForEach(x => ids.Add(x.Id));

            return ids;
        }
        public Category GetCategory(int id) => context.CategorySet.Find(id);
        public void AddCategory(string Name, string Description)
        {
            Category tmpCategory = new Category
            {
                //Id = context.CategorySet.ToList().Max(x => x.Id) + 1,
                Name = Name,
                Description = Description
            };

            context.CategorySet.Add(tmpCategory);
            context.SaveChanges();
        }
        public void ChangeCategory(int id, string newName, string newDescription)
        {
            Category tmpCategory = context.CategorySet.Find(id);
            tmpCategory.Name = newName;
            tmpCategory.Description = newDescription;

            context.SaveChanges();
        }
        public void DeleteCategory(int id)
        {
            if (context.CategorySet.Find(id) != null)
            {
                context.CategorySet.Remove(context.CategorySet.Find(id));
                context.SaveChanges();
            }
        }





        public IEnumerable<int> GetManufacturerIds()
        {
            List<int> ids = new List<int>();
            context.ManufacturerSet.ToList().ForEach(x => ids.Add(x.Id));

            return ids;
        }
        public Manufacturer GetManufacturer(int id) => context.ManufacturerSet.Find(id);
        public void AddManufacturer(string Name, string WebLink, string Country)
        {
            Manufacturer tmpCategory = new Manufacturer
            {
                //Id = context.CategorySet.ToList().Max(x => x.Id) + 1,
                Name = Name,
                WEB_Site_Link = WebLink,
                Country = Country
            };

            context.ManufacturerSet.Add(tmpCategory);

            context.SaveChanges();
        }
        public void ChangeManufacturer(int id, string Name, string WebLink, string Country)
        {
            Manufacturer tmp = context.ManufacturerSet.Find(id);
            tmp.Name = Name;
            tmp.WEB_Site_Link = WebLink;
            tmp.Country = Country;

            context.SaveChanges();
        }
        public void DeleteManufacturer(int id)
        {
            if (context.ManufacturerSet.Find(id) != null)
            {
                context.ManufacturerSet.Remove(context.ManufacturerSet.Find(id));
                context.SaveChanges();
            }
        }




        public IEnumerable<int> GetItemIds()
        {
            List<int> ids = new List<int>();
            context.ItemSet.ToList().ForEach(x => ids.Add(x.Id));

            return ids;
        }
        public IEnumerable<int> GetItemParamsIds()
        {
            List<int> ids = new List<int>();
            context.ItemParamsSet.ToList().ForEach(x => ids.Add(x.Id));

            return ids;
        }
        public Item GetItem(int id) => context.ItemSet.Find(id);
        public ItemParams GetItemParams(int id) => context.ItemParamsSet.Find(id);
        public void AddItem(string Name, string Description, decimal Price, string SerialNum, DateTime DateOfManufacture,
            double Height, double Width, double Depth, double Weight, int CategoryId, int ManufacturerId)
        {
            Item tmpItem = new Item
            {
                Name = Name,
                Description = Description,
                Price = Price,
                SerialNum = SerialNum,
                DateOfManufaturer = DateOfManufacture,
                CategoryId = CategoryId,
                ManufacturerId = ManufacturerId,
            };

            ItemParams tmpItemParams = new ItemParams
            {
                Height = Height,
                Width = Width,
                Depth = Depth,
                Weight = Weight,
                Item = tmpItem,
            };

            context.ItemSet.Add(tmpItem);
            context.ItemParamsSet.Add(tmpItemParams);
            context.SaveChanges();
        }
        public void ChangeItem(int id, string Name, string Description, decimal Price, string SerialNum, DateTime DateOfManufacture, int CategoryId, int ManufacturerId)
        {
            Item tmp = context.ItemSet.Find(id);
            tmp.Name = Name;
            tmp.Description = Description;
            tmp.Price = Price;
            tmp.SerialNum = SerialNum;
            tmp.DateOfManufaturer = DateOfManufacture;
            tmp.CategoryId = CategoryId;
            tmp.ManufacturerId = ManufacturerId;


            context.SaveChanges();
        }
        public void ChangeItemParams(int id, double Height, double Width, double Depth, double Weight)
        {
            ItemParams tmp = context.ItemParamsSet.Find(id);
            tmp.Height = Height;
            tmp.Width = Width;
            tmp.Depth = Depth;
            tmp.Weight = Weight;

            context.SaveChanges();
        }
        public void DeleteByItem(int id)
        {
            if (context.ItemSet.Find(id) != null)
            {
                Item tmp = context.ItemSet.Find(id);
                context.ItemParamsSet.Remove(tmp.ItemParams);
                context.ItemSet.Remove(tmp);
                context.SaveChanges();
            }
        }
        public void DeleteByItemParams(int id)
        {
            if (context.ItemSet.Find(id) != null)
            {
                ItemParams tmp = context.ItemParamsSet.Find(id);
                context.ItemParamsSet.Remove(tmp);
                context.ItemSet.Remove(tmp.Item);
                context.SaveChanges();
            }
        }




        public enum DBTable
        {
            None,
            Category,
            Manufacturer,
            Item,
            ItemParams
        }
    }
}
