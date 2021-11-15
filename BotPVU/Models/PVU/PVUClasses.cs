using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPVU.Models.PVU
{
    public class FarmConfig
    {
        public int le { get; set; }
        public int hours { get; set; }
    }

    public class Stats
    {
        public string type { get; set; }
        public int hp { get; set; }
        public int defPhysics { get; set; }
        public int defMagic { get; set; }
        public int damagePhysics { get; set; }
        public int damageMagic { get; set; }
        public int damagePure { get; set; }
        public double damageHpLoss { get; set; }
        public int damageHpRemove { get; set; }
    }

    public class Synergy
    {
        public int requirement { get; set; }
        public string description { get; set; }
    }

    public class Plant
    {
        public FarmConfig farmConfig { get; set; }
        public Stats stats { get; set; }
        public int type { get; set; }
        public string iconUrl { get; set; }
        public int rarity { get; set; }
        public Synergy synergy { get; set; }
    }

    public class Elements
    {
        public int fire { get; set; }
        public int water { get; set; }
        public int ice { get; set; }
        public int wind { get; set; }
        public int electro { get; set; }
        public int parasite { get; set; }
        public int light { get; set; }
        public int dark { get; set; }
        public int metal { get; set; }
    }

    public class Capacity
    {
        public int plant { get; set; }
        public int motherTree { get; set; }
    }

    public class Land
    {
        public Elements elements { get; set; }
        public Capacity capacity { get; set; }
        public int landId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int totalOfElements { get; set; }
        public int rarity { get; set; }
    }

    public class ActiveTool
    {
        public int count { get; set; }
        public string _id { get; set; }
        public int id { get; set; }
        public string type { get; set; }
        public int duration { get; set; }
        public DateTime endTime { get; set; }
        public DateTime startTime { get; set; }
    }

    public class Rate
    {
        public int le { get; set; }
        public int hours { get; set; }
    }

    public class Datum
    {
        public string _id { get; set; }
        public Plant plant { get; set; }
        public Land land { get; set; }
        public bool isTempPlant { get; set; }
        public string stage { get; set; }
        public string ownerId { get; set; }
        public int landId { get; set; }
        public long plantId { get; set; }
        public int plantUnitId { get; set; }
        public int plantType { get; set; }
        public string plantElement { get; set; }
        public List<ActiveTool> activeTools { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
        public DateTime harvestTime { get; set; }
        public Rate rate { get; set; }
        public DateTime startTime { get; set; }
        public bool hasSynergy { get; set; }
        public bool needWater { get; set; }
        public bool hasSeed { get; set; }
        public DateTime? pausedTime { get; set; }
        public bool inGreenhouse { get; set; }
        public int count { get; set; }
        public int totalHarvest { get; set; }
        public int totalExtraHarvest { get; set; }
    }

    public class GetFarmResponse
    {
        public int status { get; set; }
        public List<Datum> data { get; set; }
        public int total { get; set; }
    }


    public class Token
    {
        public string challenge { get; set; }
        public string seccode { get; set; }
        public string validate { get; set; }
    }

    public class ApplyToolRequest
    {
        public string farmId { get; set; }
        public int toolId { get; set; }
        public Token token { get; set; }
    }
    
    public class ApplyToolResponse
    {
        public int status { get; set; }
        public object data { get; set; }
    }

    public class PlantPlantRequest
    {
        public long landId { get; set; }
        public long plantId { get; set; }
    }

    public class PlantPlantResponse
    {
        public int status { get; set; }
        public object data { get; set; }
    }

    public class Reward
    {
        public int type { get; set; }
        public string name { get; set; }
        public int target { get; set; }
        public string status { get; set; }
    }

    public class DataWordTree
    {
        public int totalWater { get; set; }
        public int level { get; set; }
        public int myWater { get; set; }
        public bool yesterdayReward { get; set; }
        public bool rewardAvailable { get; set; }
        public List<Reward> reward { get; set; }
    }

    public class WordTreeResponse
    {
        public int status { get; set; }
        public DataWordTree data { get; set; }
    }
    
    public class BuySunFlowerRequest
    {
        public int amount { get; set; }
        public int sunflowerId { get; set; }
    }

    public class DataBuySunFlower
    {
        public int quantity { get; set; }
        public int sunflowerId { get; set; }
        public string type { get; set; }
    }

    public class BuySunFlowerResponse
    {
        public int status { get; set; }
        public DataBuySunFlower data { get; set; }
    }
}
