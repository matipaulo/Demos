﻿using System;
using System.Collections.Generic;
using Bogus;
using Bogus.Extensions;

namespace RecyclableMemoryStreamDemo
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public int? LotNumber { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string SomethingUnique { get; set; }
        public Guid SomeGuid { get; set; }

        public string Avatar { get; set; }
        public Guid CartId { get; set; }
        public string SSN { get; set; }
        public Gender Gender { get; set; }

        public List<Order> Orders { get; set; }
    }

    public static class PersonGenerator
    {
        public static List<User> Generate(int seed = 3897234, int count = 50000)
        {
            //Set the randomzier seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(seed);

            var fruit = new[] {"apple", "banana", "orange", "strawberry", "kiwi"};

            var orderIds = 0;
            var testOrders = new Faker<Order>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode if you prefer.
                .StrictMode(true)
                //OrderId is deterministic
                .RuleFor(o => o.OrderId, f => orderIds++)
                //Pick some fruit from a basket
                .RuleFor(o => o.Item, f => f.PickRandom(fruit))
                //A random quantity from 1 to 10
                .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
                //A nullable int? with 80% probability of being null.
                //The .OrNull extension is in the Bogus.Extensions namespace.
                .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .8f));

            var userIds = 0;
            var testUsers = new Faker<User>()
                //Optional: Call for objects that have complex initialization
                .CustomInstantiator(f => new User
                {
                    Id = userIds++,
                    SSN = f.Random.Replace("###-##-####")
                })

                //Basic rules using built-in generators
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")
                .RuleFor(u => u.SomeGuid, Guid.NewGuid)

                //Use an enum outside scope.
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                //Use a method outside scope.
                .RuleFor(u => u.CartId, f => Guid.NewGuid())
                //Compound property with context, use the first/last name properties
                .RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
                //And composability of a complex collection.
                .RuleFor(u => u.Orders, f => testOrders.Generate(3));

            return testUsers.Generate(count);
        }
    }
}