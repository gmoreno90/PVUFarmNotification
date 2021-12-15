using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotPVU
{
    public static class PVUHelper
    {

        /// <summary>
        /// Get Farm Info
        /// </summary>
        /// <returns></returns>
        public static Models.PVU.GetFarmResponse getFarmInfo()
        {
            try
            {
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/v2/farms?limit=10&offset=0", "GET");
                var objRes = JsonConvert.DeserializeObject<Models.PVU.GetFarmResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Get Farm Info: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// Use Tool Method
        /// </summary>
        /// <param name="farmId">_id</param>
        /// <param name="toolId">1 Small Port, 2 Big POT, 3 water, 4 scarecrow, 5 greenhouse</param>
        /// <returns></returns>
        public static Models.PVU.ApplyToolResponse UseTool(string farmId, int toolId)
        {
            try
            {
                var rq = new Models.PVU.ApplyToolRequest()
                {
                    farmId = farmId,
                    toolId = toolId,
                    token = new Models.PVU.Token()
                    {
                        challenge = "default",
                        seccode = "default",
                        validate = "default"
                    }
                };
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/farms/apply-tool", "POST", JsonConvert.SerializeObject(rq).ToString());
                var objRes = JsonConvert.DeserializeObject<Models.PVU.ApplyToolResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Apply Tool: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="farmId">FreeSlot in Farms _id</param>
        /// <param name="landId">0</param>
        /// <param name="plantId">plantid or 1 supling 2 mama</param>
        /// <param name="sunflowerId"></param>
        /// <returns></returns>
        public static Models.PVU.PlantPlantResponse AddPlant(string farmId, string landId, string plantId)
        {
            try
            {
                var rq = new Models.PVU.PlantPlantRequest()
                {
                    farmId= farmId,
                    landId = landId,
                    plantId = plantId != "1" && plantId != "2" ? plantId : null ,
                    sunflowerId = plantId == "1" && plantId == "2" ? plantId : null
                };
                var rqJson = JsonConvert.SerializeObject(rq);
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/farm/add-plant", "POST", rqJson);
                var objRes = JsonConvert.DeserializeObject<Models.PVU.PlantPlantResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Add Plant: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public static Models.PVU.FreeSlotResponse GetFreeSlots()
        {
            try
            {
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/farms/free-slots", "GET");
                var objRes = JsonConvert.DeserializeObject<Models.PVU.FreeSlotResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Get Free Slots: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        

        /// <summary>
        /// WordTree Response
        /// </summary>
        /// <returns></returns>
        public static Models.PVU.WordTreeResponse GetWordTreeInfo()
        {
            try
            {

                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/world-tree/datas", "GET");
                var objRes = JsonConvert.DeserializeObject<Models.PVU.WordTreeResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Get Word Tree Info: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Buy Sun Flowers
        /// </summary>
        /// <param name="farmId">amount</param>
        /// <param name="toolId">1 Sunflower Sapling, 2 Sunflower mama, 3 Sun Box</param>
        /// <returns></returns>
        public static Models.PVU.BuySunFlowerResponse BuySunFlowers(int amount, int sunFlowerType)
        {
            try
            {
                var rq = new Models.PVU.BuySunFlowerRequest()
                {
                    amount = amount,
                    sunflowerId = sunFlowerType
                };
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/buy-sunflowers", "POST", JsonConvert.SerializeObject(rq).ToString());
                var objRes = JsonConvert.DeserializeObject<Models.PVU.BuySunFlowerResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Buy Sun Flowers: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// HarvestAll
        /// </summary>
        /// <returns></returns>
        public static Models.PVU.ApplyToolResponse HarvestAll()
        {
            try
            {
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/farms/harvest-all", "POST");
                var objRes = JsonConvert.DeserializeObject<Models.PVU.ApplyToolResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Harvest Plant: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// Harvest Plant
        /// </summary>
        /// <param name="plantId"></param>
        /// <returns></returns>
        public static Models.PVU.HarvestPlantResponse HarvestPlant(string plantId)
        {
            try
            {
                
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/farms/" + plantId.ToString() + "/harvest", "POST");
                var objRes = JsonConvert.DeserializeObject<Models.PVU.HarvestPlantResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Harvest Plant: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Get Farm Info
        /// </summary>
        /// <returns></returns>
        public static Models.PVU.WeatherTodayResponse GetWeatherToday()
        {
            try
            {
                var jsonString = getRequest(Models.Configuration.BackendEndpoint + "/weather-today", "GET");
                var objRes = JsonConvert.DeserializeObject<Models.PVU.WeatherTodayResponse>(jsonString);
                if (objRes != null && Models.Configuration.PrintLogResponses)
                {
                    Console.WriteLine("Get Weather Today: " + getMessageFromCode(objRes.status.ToString()));
                }
                return objRes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        private static string getRequest(string url, string method, string dataToPost = "")
        {
            WebClient client = new WebClient();

            // Add a user agent header in case the 
            // requested URI contains a query.

            client.Headers.Add("User-Agent", Models.Configuration.UserAgent);
            client.Headers.Add("Authorization", "Bearer Token: " + Models.Configuration.AuthPVUToken);
            if (method == "GET")
            {
                return client.DownloadString(url);
            }
            else if (method == "POST")
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                return client.UploadString(url, dataToPost);
            }
            else return "";

        }

        private static string getMessageFromCode(string v)
        {
            try
            {
                var messages = Models.Configuration.ResponseMessages.Select(x => new { Code = x.Split('|')[0], Message = x.Split('|')[1] });
                return messages.FirstOrDefault(x => x.Code == v)?.Message;
            }
            catch (Exception exLog)
            {
                return "Error " + exLog.Message + " " + exLog.StackTrace;
            }
        }
    }
}
