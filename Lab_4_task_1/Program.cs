using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4_task_1
{
interface ISearchable
{
    List<Tovar> SearchByPrice(double maxPrice);
    List<Tovar> SearchByCategory(string category);
}

class Tovar
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}

class User
{
    public string Login { get; set; }
    public string Password { get; set; }
    public List<Zamovlennya> PurchaseHistory { get; set; } = new List<Zamovlennya>();
}

class Zamovlennya
{
    public List<Tovar> Products { get; set; }
    public List<int> Quantities { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }
}

class Magazin : ISearchable
{
    public List<Tovar> Products { get; set; } = new List<Tovar>();
    public List<User> Users { get; set; } = new List<User>();
    public List<Zamovlennya> Orders { get; set; } = new List<Zamovlennya>();

    public List<Tovar> SearchByPrice(double maxPrice)
    {
        return Products.Where(p => p.Price <= maxPrice).ToList();
    }

    public List<Tovar> SearchByCategory(string category)
    {
        return Products.Where(p => p.Category.ToLower() == category.ToLower()).ToList();
    }


    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void AddProduct(Tovar product)
    {
        Products.Add(product);
    }

    public void PlaceOrder(User user, List<Tovar> products, List<int> quantities)
    {
        var order = new Zamovlennya
        {
            Products = products,
            Quantities = quantities,
            TotalPrice = CalculateTotalPrice(products, quantities),
            Status = "Pending"
        };

        user.PurchaseHistory.Add(order);
        Orders.Add(order);
    }

    private double CalculateTotalPrice(List<Tovar> products, List<int> quantities)
    {
        double totalPrice = 0;

        for (int i = 0; i < products.Count; i++)
        {
            totalPrice += products[i].Price * quantities[i];
        }

        return totalPrice;
    }
}

class Program
{
    static void Main()

    {
        Console.WriteLine("Минко Ярослав");
        
        var store = new Magazin();

        var user1 = new User { Login = "user1", Password = "password1" };
        var user2 = new User { Login = "user2", Password = "password2" };

        store.AddUser(user1);
        store.AddUser(user2);

        var product1 = new Tovar { Name = "Product 1", Price = 10, Category = "Electronics" };
        var product2 = new Tovar { Name = "Product 2", Price = 30, Category = "Clothing" };

        store.AddProduct(product1);
        store.AddProduct(product2);

        var orderProducts = new List<Tovar> { product1, product2 };
        var orderQuantities = new List<int> { 2, 3 };

        store.PlaceOrder(user1, orderProducts, orderQuantities);

        Console.WriteLine(user1.Login + " " + user1.Password);
        Console.WriteLine(user2.Login + " " + user2.Password);
        Console.WriteLine(product1.Name + " " + product1.Price + " " + product1.Category);
        Console.WriteLine(product2.Name + " " + product2.Price + " " + product2.Category);


    }
}

}