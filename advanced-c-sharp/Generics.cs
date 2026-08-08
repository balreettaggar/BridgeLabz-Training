using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanced_c_sharp
{
    public abstract class WarehouseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public WarehouseItem(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public abstract void DisplayInfo();
    }

    public class Electronics : WarehouseItem
    {
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }

        public Electronics(
            int id,
            string name,
            double price,
            string brand,
            int warrantyMonths)
            : base(id, name, price)
        {
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(
                $"Electronics | ID: {Id} | Name: {Name} | " +
                $"Price: {Price} | Brand: {Brand} | " +
                $"Warranty: {WarrantyMonths} months"
            );
        }
    }


    // GROCERY
    public class Grocery : WarehouseItem
    {
        public DateTime ExpiryDate { get; set; }

        public Grocery(
            int id,
            string name,
            double price,
            DateTime expiryDate)
            : base(id, name, price)
        {
            ExpiryDate = expiryDate;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(
                $"Grocery | ID: {Id} | Name: {Name} | " +
                $"Price: {Price} | Expiry: {ExpiryDate:dd-MM-yyyy}"
            );
        }
    }


    // FURNITURE
    public class Furniture : WarehouseItem
    {
        public string Material { get; set; }

        public Furniture(
            int id,
            string name,
            double price,
            string material)
            : base(id, name, price)
        {
            Material = material;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine(
                $"Furniture | ID: {Id} | Name: {Name} | " +
                $"Price: {Price} | Material: {Material}"
            );
        }
    }


    // GENERIC STORAGE
    public class Storage<T> where T : WarehouseItem
    {
        private List<T> items = new List<T>();

        public void AddItem(T item)
        {
            items.Add(item);
        }

        public void DisplayAllItems()
        {
            foreach (T item in items)
            {
                item.DisplayInfo();
            }
        }
    }

    internal class Generics
    {
        public static void Main(string[] args)
        {
            Storage<Electronics> electronicsStorage = new Storage<Electronics>();

            electronicsStorage.AddItem(
                new Electronics(
                    1,
                    "Laptop",
                    75000,
                    "Dell",
                    24
                )
            );

            electronicsStorage.AddItem(
                new Electronics(
                    2,
                    "Smartphone",
                    45000,
                    "Samsung",
                    12
                )
            );


            Storage<Grocery> groceryStorage = new Storage<Grocery>();

            groceryStorage.AddItem(
                new Grocery(
                    3,
                    "Rice",
                    2500,
                    new DateTime(2027, 5, 10)
                )
            );

            groceryStorage.AddItem(
                new Grocery(
                    4,
                    "Milk",
                    70,
                    new DateTime(2026, 8, 15)
                )
            );


            // Furniture Storage
            Storage<Furniture> furnitureStorage =
                new Storage<Furniture>();

            furnitureStorage.AddItem(
                new Furniture(
                    5,
                    "Chair",
                    3500,
                    "Wood"
                )
            );

            furnitureStorage.AddItem(
                new Furniture(
                    6,
                    "Table",
                    8500,
                    "Oak Wood"
                )
            );


            Console.WriteLine();
            electronicsStorage.DisplayAllItems();

            Console.WriteLine();
            groceryStorage.DisplayAllItems();

            Console.WriteLine();
            furnitureStorage.DisplayAllItems();

        }
    }
}
