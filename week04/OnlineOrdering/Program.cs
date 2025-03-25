using System;
using System.Collections.Generic;

// Represents a product that can be ordered
public class Product
{
    // These are the details of the product
    private string name;
    private string productId;
    private double price;
    private int quantity;

    // This is a special method called a constructor.
    // It's used to create a new Product object and set its initial values.
    public Product(string productName, string productID, double productPrice, int productQuantity)
    {
        name = productName;
        productId = productID;
        price = productPrice;
        quantity = productQuantity;
    }

    // A method to calculate the total cost of this product based on quantity
    public double CalculateTotalCost()
    {
        return price * quantity;
    }

    // Methods to get the product's name and ID
    public string GetName()
    {
        return name;
    }

    public string GetProductId()
    {
        return productId;
    }
}

// Represents an address
public class Address
{
    // Address details
    private string streetAddress;
    private string city;
    private string stateProvince;
    private string country;

    // Constructor to create a new Address object
    public Address(string street, string cityName, string state, string countryName)
    {
        streetAddress = street;
        city = cityName;
        stateProvince = state;
        country = countryName;
    }

    // Method to check if the address is in the USA
    public bool IsInUSA()
    {
        return country.ToUpper() == "USA"; // Convert to uppercase for case-insensitive comparison
    }

    // Method to get the full address as a string
    public string GetFullAddress()
    {
        return streetAddress + "\n" + city + ", " + stateProvince + "\n" + country;
    }
}

// Represents a customer
public class Customer
{
    // Customer details
    private string name;
    private Address address;

    // Constructor to create a new Customer object
    public Customer(string customerName, Address customerAddress)
    {
        name = customerName;
        address = customerAddress;
    }

    // Method to check if the customer is in the USA (using the address)
    public bool IsInUSA()
    {
        return address.IsInUSA();
    }

    // Method to get the customer's name
    public string GetName()
    {
        return name;
    }

    // Method to get the customer's address
    public Address GetAddress()
    {
        return address;
    }
}

// Represents an order
public class Order
{
    // Order details
    private List<Product> products;
    private Customer customer;

    // Constructor to create a new Order object
    public Order(Customer orderCustomer)
    {
        products = new List<Product>(); // Create an empty list of products
        customer = orderCustomer;
    }

    // Method to add a product to the order
    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    // Method to calculate the total cost of the order
    public double CalculateTotalCost()
    {
        double productTotal = 0;
        foreach (Product product in products)
        {
            productTotal += product.CalculateTotalCost();
        }

        double shippingCost = customer.IsInUSA() ? 5.0 : 35.0; // USA shipping is $5, other is $35
        return productTotal + shippingCost;
    }

    // Method to get the packing label as a string
    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in products)
        {
            label += product.GetName() + " (ID: " + product.GetProductId() + ")\n";
        }
        return label;
    }

    // Method to get the shipping label as a string
    public string GetShippingLabel()
    {
        return "Shipping Label:\n" + customer.GetName() + "\n" + customer.GetAddress().GetFullAddress();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create some addresses
        Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        Address address2 = new Address("456 Elm St", "London", "", "UK");

        // Create some customers
        Customer customer1 = new Customer("Alice Smith", address1);
        Customer customer2 = new Customer("Bob Johnson", address2);

        // Create some products
        Product product1 = new Product("Laptop", "LP001", 1200.0, 1);
        Product product2 = new Product("Mouse", "MS002", 25.0, 2);
        Product product3 = new Product("Keyboard", "KB003", 50.0, 1);
        Product product4 = new Product("Headphones", "HP004", 100, 1);
        Product product5 = new Product("Charger", "CH005", 30, 3);

        // Create some orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        // Display the order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine("Total Order 1 Cost: $" + order1.CalculateTotalCost());
        Console.WriteLine();

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine("Total Order 2 Cost: $" + order2.CalculateTotalCost());
    }
}