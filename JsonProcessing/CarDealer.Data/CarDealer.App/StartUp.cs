namespace CarDealer.App
{
    using CarDealer.Data;
    using System;
    using System.IO;
    using Newtonsoft;
    using Newtonsoft.Json;
    using CarDealer.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new CarDealerContext();


            //ResetDatabase(context);
            //ImportSuppliers(context);
            //ImportParts(context);
            //ImportCars(context);
            //ImportCarParts(context);
            //ImprtCustomers(context);
            //ImportSales(context);
            //GetAllCustomers(context);
            //GetAllToyotaCars(context);
            //GetLocalSupplier(context);
            //GetCarsAndTheirParts(context);
            GetCustomerBoughtCar(context);
        }

        private static void GetCustomerBoughtCar(CarDealerContext context)
        {
            var customers = context.Customers.Where(x => x.Sales.Count > 0).Select(x => new
            {
                fullName = x.Name,
                boughtCar = x.Sales.Count,
                spentMoney = x.Sales.Sum(s => s.Car.PartCars.Sum(p => p.Part.Price))
            });
            var serializer = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText(@"../../../Json\customers-total-sales.json", serializer);
        }

        private static void GetCarsAndTheirParts(CarDealerContext context)
        {
            var cars = context.PartCars.Select(x => new
            {
                car = new { x.Car.Make, x.Car.Model, x.Car.TravelledDistance },
                parts = x.Car.PartCars.Select(y => new
                {
                    y.Part.Name,
                    y.Part.Price
                })
            });
            var serializer = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText(@"../../../Json\cars-and-parts.json", serializer);
        }

        private static void GetLocalSupplier(CarDealerContext context)
        {
            var suppliers = context.Suppliers.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                PartsCount = x.Parts.Count
            });
            var serializer = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            File.WriteAllText(@"../../../Json\local-suppliers.json", serializer);
        }

        private static void GetAllToyotaCars(CarDealerContext context)
        {
            var cars = context.Cars.Where(x => x.Make == "Toyota").OrderBy(x => x.Model);

            var serializer = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText(@"../../../Json\toyota-cars.json", serializer);
        }

        private static void GetAllCustomers(CarDealerContext context)
        {
            var customers = context.Customers.OrderBy(x => x.BirthDate).ThenBy(x => !x.IsYoungDriver);
               
                

            var serializer = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText(@"../../../Json\ordered-customers.json", serializer);
        }

        private static void ImportSales(CarDealerContext context)
        {
            var discounts = new double[] { 0.05, 0.1, 0.2, 0.3, 0.4, 0.5, };
            var sales = new List<Sale>();

            for (int customerId = 1; customerId < 31; customerId++)
            {
                var randomDisc = new Random().Next(0, 6);
                var randomCarId = new Random().Next(1, 359);

                var sale = new Sale
                {
                    CarId = randomCarId,
                    CustomerId = customerId,
                    Discount = discounts[randomDisc]
                };

                sales.Add(sale);
            }
            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void ImprtCustomers(CarDealerContext context)
        {
            var json = File.ReadAllText(@"../../../Json\customers.json");

            var deserializer = JsonConvert.DeserializeObject<Customer[]>(json);

            var customers = new List<Customer>();
            foreach (var customer in deserializer)
            {
                customers.Add(customer);
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void ImportCarParts(CarDealerContext context)
        {
            var partCars = new List<PartCar>();
            var partId = 1;
            for (int carId = 1; carId < 359; carId++)
            {
                var randomPartsImport = new Random().Next(10, 21);
                for (int j = 0; j < randomPartsImport; j++)
                {
                    
             
                    var partCar = new PartCar
                    {
                        CarId = carId,
                        PartId = partId
                    };

                    if (partId == 131)
                    {
                        partId = 0;
                    }
                    partId++;
                    partCars.Add(partCar);
                }
            }
            
            context.PartCars.AddRange(partCars);
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            var json = File.ReadAllText(@"../../../Json\cars.json");

            var deserializer = JsonConvert.DeserializeObject<Car[]>(json);

            var cars = new List<Car>();
            foreach (var car in deserializer)
            {
                cars.Add(car);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            var json = File.ReadAllText(@"../../../Json\parts.json");

            var deserializer = JsonConvert.DeserializeObject<Part[]>(json);

            var parts = new List<Part>();
            foreach (var part in deserializer)
            {
                var supplierId = new Random().Next(1, 32);

                part.SupplierId = supplierId;

                parts.Add(part);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            var json = File.ReadAllText(@"../../../Json\suppliers.json");

            var deserializer = JsonConvert.DeserializeObject<Supplier[]>(json);

            var suppliers = new List<Supplier>();
            foreach (var supplier in deserializer)
            {
                suppliers.Add(supplier);
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        private static void ResetDatabase(CarDealerContext context)
        {
            Console.WriteLine("Reset Database...");

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
