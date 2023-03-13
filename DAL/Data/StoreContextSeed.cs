using DAL.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Order;

namespace DAL.Data
{
    public class StoreContextSeed
    {
        public static async  Task SeedAsync(StoreContext context,ILoggerFactory loggerFactory)
        {
                try
                {

                if (!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../DAL/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    foreach (var item in brands)
                    {
                        context.Set<ProductBrand>().Add(item);
                        await context.SaveChangesAsync();
                    }
                }
                    if (!context.ProductTypes.Any())
                    {
                        
                            var brandData = File.ReadAllText("../DAL/Data/SeedData/types.json");
                            var brands = JsonSerializer.Deserialize<List<ProductType>>(brandData);
                            foreach (var item in brands)
                            {
                                context.Set<ProductType>().Add(item);
                                await context.SaveChangesAsync();
                            }
                        
                       
                          
                    }
                if (!context.Products.Any())
                {
                  
                        var brandData = File.ReadAllText("../DAL/Data/SeedData/products.json");
                        var brands = JsonSerializer.Deserialize<List<Product>>(brandData);
                        foreach (var item in brands)
                        {
                            context.Set<Product>().Add(item);
                            await context.SaveChangesAsync();
                        }
                    
                  
                }
                if (!context.DeliveryMethods.Any())
                {
                    var deliverMethodsData =
                        File.ReadAllText("../DAL/Data/SeedData/delivery.json");
                    var deliverMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverMethodsData);
                    foreach (var deliverMethod in deliverMethods)
                        context.DeliveryMethods.Add(deliverMethod);

                    await context.SaveChangesAsync();
                }
            }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                    logger.LogError(ex, ex.Message);
                }
            }
           
         



        
    }
}
