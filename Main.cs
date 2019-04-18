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
    public class Main :BaseScript
    {
        private Config config;
        List<SignWatcher> Watchers = new List<SignWatcher>();
        public Main()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnClientResourceStart);
        }

        private void OnClientResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName) return;
            string ConfigFile = LoadResourceFile(GetCurrentResourceName(), "config.json");
            if (string.IsNullOrWhiteSpace(ConfigFile))
            {
                config = new Config();
                Debug.WriteLine("Using default config!");
            }
            else
            {
                config = JsonConvert.DeserializeObject<Config>(ConfigFile);
                Debug.WriteLine("Using saved config!");
            }
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
