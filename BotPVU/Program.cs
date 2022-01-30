using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BotPVU
{
    class Program
    {
        public static IConfiguration configuration;
        static int Main(string[] args)
        {
            try
            {
                MainAsync(args).Wait();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        static async Task MainAsync(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();


            try
            {
                List<string> plantsWaterNeed = new List<string>();
                List<string> plantscrow = new List<string>();
                List<string> plantWithSeed = new List<string>();
                List<string> plantNeedHarvest = new List<string>();
                List<string> plantNew = new List<string>();
                Console.Title = "PVU Farm Notification";
                Console.WriteLine("--- Checking PVU FARM ---");
                while (!Console.KeyAvailable)
                {

                    var res = PVUHelper.getFarmInfo();
                    var resWeather = PVUHelper.GetWeatherToday();
                    var resFreeSlots = PVUHelper.GetFreeSlots();
                    if (res != null)
                    {
                        var plantsPerWeather = getPlantsByWeather(resWeather.data.season);
                        while (res.data.Count != plantsPerWeather.Count)
                        {
                            foreach (var item in plantsPerWeather)
                            {
                                if (res.data.FirstOrDefault(x => x.plantId.ToString() == item) == null)
                                {
                                    if (Models.Configuration.AutoFarming)
                                    {
                                        //check if have free slots for get the farm _id
                                        if(resFreeSlots.data.farm != null && resFreeSlots.data.farm.Count() > 0)
                                        {
                                            System.Threading.Thread.Sleep(Models.Configuration.AutoFarmingDelay);
                                            PVUHelper.AddPlant(resFreeSlots.data.farm.FirstOrDefault()._id, "0", item);
                                            Console.WriteLine("Plant a new plant " + item + "");
                                            System.Threading.Thread.Sleep(Models.Configuration.AutoFarmingDelay);
                                            res = PVUHelper.getFarmInfo();
                                            resFreeSlots = PVUHelper.GetFreeSlots();
                                        }
                                        
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Farm Information --- " + DateTime.Now.ToString());
                        Console.WriteLine("-- Plants: " + res.data.Count.ToString());
                        Console.WriteLine("-- Need Water: " + res.data.Where(x => x.needWater).Count().ToString());
                        Console.WriteLine("-- Have Crow: " + res.data.Where(x => x.stage == "paused").Count().ToString());
                        Console.WriteLine("-- Is New: " + res.data.Where(x => x.stage == "new").Count().ToString());
                        Console.WriteLine("-- Have Seed: " + res.data.Where(x => x.hasSeed).Count().ToString());
                        Console.WriteLine("-- Need Harvers: " + res.data.Where(x => x.totalHarvest != 0).Count().ToString());
                        foreach (var plant in res.data)
                        {
                            if (plant.stage == "new")
                            {
                                if (plantNew.FirstOrDefault(x => x == plant._id) == null)
                                {
                                    MailHelper.sendEmail("The plant is new", "The plant " + plant._id + " is new");
                                    Console.WriteLine("The plant " + plant._id + " is new");
                                    plantNew.Add(plant._id);
                                    if (Models.Configuration.AutoFarming)
                                    {
                                        System.Threading.Thread.Sleep(Models.Configuration.AutoFarmingDelay);
                                        PVUHelper.UseTool(plant._id, 1);
                                    }
                                }
                            }
                            else
                            {
                                if (plantNew.FirstOrDefault(x => x == plant._id) != null)
                                    plantNew.Remove(plant._id);
                            }
                            if (plant.needWater)
                            {
                                if (plantsWaterNeed.FirstOrDefault(x => x == plant._id) == null)
                                {
                                    MailHelper.sendEmail("The plant need water", "The plant " + plant._id + " need water");
                                    Console.WriteLine("The plant " + plant._id + " need water");
                                    plantsWaterNeed.Add(plant._id);
                                    if (Models.Configuration.AutoFarming)
                                    {
                                        System.Threading.Thread.Sleep(Models.Configuration.AutoFarmingDelay);
                                        PVUHelper.UseTool(plant._id, 3);
                                    }
                                }
                            }
                            else
                            {
                                if (plantsWaterNeed.FirstOrDefault(x => x == plant._id) != null)
                                    plantsWaterNeed.Remove(plant._id);
                            }
                            if (plant.stage == "paused")
                            {
                                if (plantscrow.FirstOrDefault(x => x == plant._id) == null)
                                {
                                    MailHelper.sendEmail("The plant have a crow", "The plant " + plant._id + " have a crow");
                                    Console.WriteLine("The plant " + plant._id + " have a crow");
                                    plantscrow.Add(plant._id);
                                    if (Models.Configuration.AutoFarming)
                                    {
                                        System.Threading.Thread.Sleep(Models.Configuration.AutoFarmingDelay);
                                        PVUHelper.UseTool(plant._id, 4);
                                    }
                                }
                            }
                            else
                            {
                                if (plantscrow.FirstOrDefault(x => x == plant._id) != null)
                                    plantscrow.Remove(plant._id);
                            }
                            if (plant.hasSeed)
                            {
                                if (plantWithSeed.FirstOrDefault(x => x == plant._id) == null)
                                {
                                    MailHelper.sendEmail("The plant have a seed", "The plant " + plant._id + " have a seed");
                                    Console.WriteLine("The plant " + plant._id + " have a seed");
                                    plantWithSeed.Add(plant._id);
                                }
                            }
                            else
                            {
                                if (plantWithSeed.FirstOrDefault(x => x == plant._id) != null)
                                    plantWithSeed.Remove(plant._id);
                            }
                            if (plant.totalHarvest != 0)
                            {
                                if (plantNeedHarvest.FirstOrDefault(x => x == plant._id) == null)
                                {
                                    MailHelper.sendEmail("The plant is ready to harvest", "The plan " + plant._id + " is ready to harvers");
                                    Console.WriteLine("The plan " + plant._id + " is ready to harvers");
                                    plantNeedHarvest.Add(plant._id);
                                    if (Models.Configuration.AutoFarming)
                                    {
                                        System.Threading.Thread.Sleep(Models.Configuration.AutoFarmingDelay);
                                        PVUHelper.HarvestPlant(plant._id);
                                    }
                                }
                            }
                            else
                            {
                                if (plantNeedHarvest.FirstOrDefault(x => x == plant._id) != null)
                                    plantNeedHarvest.Remove(plant._id);

                            }
                        }
                        Console.WriteLine("END Farm Information --- " + DateTime.Now.ToString());
                    }
                    else
                    {
                        Console.WriteLine("ERROR GET Farm Information --- " + DateTime.Now.ToString());
                    }
                    int number = (new Random()).Next(55000, 95000);
                    System.Threading.Thread.Sleep(number);
                }


                Console.WriteLine("Closing program! Bye!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }
        }


        private static List<string> getPlantsByWeather(string weather)
        {
            switch (weather.ToLower())
            {
                case "spring":
                    return Models.Configuration.MyPlantsSpring;
                case "summer":
                    return Models.Configuration.MyPlantsSummer;
                case "autumn":
                    return Models.Configuration.MyPlantsAutumn;
                case "winter":
                    return Models.Configuration.MyPlantsWinter;
                default:
                    return new List<string>();
            }
        }
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {

            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(GetBasePath())
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            Models.Configuration.AuthPVUToken = configuration.GetSection("LoginTokenPVU").Value;
            Models.Configuration.NotificationEmails = configuration.GetSection("NotificationEmails").Get<List<string>>();
            Models.Configuration.SendEmailNotification = configuration.GetSection("SendEmailNotification").Get<bool>();
            Models.Configuration.SmtpServer = configuration.GetSection("SmtpServer").Get<string>();
            Models.Configuration.SmtpPort = configuration.GetSection("SmtpPort").Get<int>();
            Models.Configuration.SmtpServerSSL = configuration.GetSection("SmtpServerSSL").Get<bool>();
            Models.Configuration.SmtpUserName = configuration.GetSection("SmtpUserName").Get<string>();
            Models.Configuration.SmtpPassword = configuration.GetSection("SmtpPassword").Get<string>();
            Models.Configuration.AutoFarming = configuration.GetSection("AutoFarming").Get<bool>();
            Models.Configuration.AutoFarmingDelay = configuration.GetSection("AutoFarmingDelay").Get<int>();
            Models.Configuration.MyPlantsSpring = configuration.GetSection("MyPlantsSpring").Get<List<string>>();
            Models.Configuration.MyPlantsSummer = configuration.GetSection("MyPlantsSummer").Get<List<string>>();
            Models.Configuration.MyPlantsAutumn = configuration.GetSection("MyPlantsAutumn").Get<List<string>>();
            Models.Configuration.MyPlantsWinter = configuration.GetSection("MyPlantsWinter").Get<List<string>>();
            Models.Configuration.UserAgent = configuration.GetSection("UserAgent").Get<string>();
            Models.Configuration.BackendEndpoint = configuration.GetSection("BackendEndpoint").Get<string>();
            Models.Configuration.ResponseMessages = configuration.GetSection("ResponseMessages").Get<List<string>>();
            Models.Configuration.PrintLogResponses = configuration.GetSection("PrintLogResponses").Get<bool>();
        }
        private static string GetBasePath()
        {
            using var processModule = Process.GetCurrentProcess().MainModule;
            return Path.GetDirectoryName(processModule?.FileName);
        }
    }
}

