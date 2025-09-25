using System.Collections.Generic;
using IT13_FinalProject;

namespace IT13_FinalProject;

public static class InMemoryCustomerStore
{
    private static readonly List<Customer> _customers = new()
    {
        new Customer { FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Phone = "555-1234", Address = "123 Main St", LoyaltyPoints = 120 },
        new Customer { FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com", Phone = "555-5678", Address = "456 Oak St", LoyaltyPoints = 85 },
        new Customer { FirstName = "Carol", LastName = "Lee", Email = "carol@example.com", Phone = "555-8765", Address = "789 Pine Rd", LoyaltyPoints = 200 },
        new Customer { FirstName = "David", LastName = "Kim", Email = "david@example.com", Phone = "555-4321", Address = "321 Maple Ave", LoyaltyPoints = 50 },
        new Customer { FirstName = "Eva", LastName = "Martinez", Email = "eva@example.com", Phone = "555-2468", Address = "654 Cedar Blvd", LoyaltyPoints = 0 }
    };

    public static List<Customer> GetCustomers() => _customers;
}