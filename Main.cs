using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace RoadSigns
{
    public class Main : BaseScript
    {
        private Config config;

        private readonly Config DefaultConfig = new Config()
        {
            FadeEffects = true,
            FlashEffect = true,
            SignPillar = true,
            DisplayCorner = Config.Corner.TopLeft,
            SignGroups = new List<Config.SignGroup>()
            {
                new Config.SignGroup() {
                    Name = "Cars",
                    UseText = false,
                    Signs = new Dictionary<string, string>(){
                        ["prop_sign_road_06a"] = "24",
                        ["prop_sign_road_06b"] = "8",
                        ["prop_sign_road_06c"] = "24",
                        ["prop_sign_road_06d"] = "40",
                        ["prop_sign_road_06e"] = "56",
                        ["prop_sign_road_06f"] = "80",
                        ["prop_sign_road_06g"] = "96",
                        ["prop_sign_road_06i"] = "40",
                        ["prop_sign_road_06j"] = "88",
                        ["prop_sign_road_06k"] = "56",
                    }
                },
                new Config.SignGroup() {
                    Name = "Trucks",
                    UseText = true,
                    Text = "TRUCKS",
                    Signs = new Dictionary<string, string>(){
                        ["prop_sign_road_06m"] = "8",
                        ["prop_sign_road_06n"] = "32",
                        ["prop_sign_road_06o"] = "72",
                    }
                },
                new Config.SignGroup() {
                    Name = "TrucksAndTrailers",
                    UseText = true,
                    Text = "TRUCKS & CARS WITH TRAILER",
                    Signs = new Dictionary<string, string>(){
                        ["prop_sign_road_06p"] = "88"
                    }
                }
            }
        };
        List<SignWatcher> Watchers = new List<SignWatcher>();
        public Main()
        {
            Debug.WriteLine(JsonConvert.SerializeObject(new Config()));
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;

            string ConfigFile = LoadResourceFile(GetCurrentResourceName(), "config.json");
            if (string.IsNullOrWhiteSpace(ConfigFile))
            {
                config = DefaultConfig;
                Debug.WriteLine("Using default config!");
            }
            else
            {
                config = JsonConvert.DeserializeObject<Config>(ConfigFile);
                Debug.WriteLine("Using saved config!");
            }

            SendNuiMessage(JsonConvert.SerializeObject(new { Config = true, Corner = (int)config.DisplayCorner, config.FadeEffects, config.FlashEffect, config.SignPillar }));

            foreach (var signGroup in config.SignGroups)
            {
                SignWatcher watcher = new SignWatcher(this, signGroup.Name, signGroup.Signs.Select(kvp => new Sign(kvp.Key, kvp.Value)).ToList(), signGroup.UseText, signGroup.Text);
                Tick += watcher.TickHandler;
                Watchers.Add(watcher);
            }
        }

        public new Task Delay(int i) => BaseScript.Delay(i);
    }
}
