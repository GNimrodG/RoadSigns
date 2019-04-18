using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadSigns
{
    public class Config
    {
        public List<SignGroup> SignGroups = new List<SignGroup>()
        {
            new SignGroup() {
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
            new SignGroup() {
                Name = "Trucks",
                UseText = true,
                Text = "TRUCKS",
                Signs = new Dictionary<string, string>(){
                    ["prop_sign_road_06m"] = "8",
                    ["prop_sign_road_06n"] = "32",
                    ["prop_sign_road_06o"] = "72",
                }
            },
            new SignGroup() {
                Name = "TrucksAndTrailers",
                UseText = true,
                Text = "TRUCKS & CARS WITH TRAILER",
                Signs = new Dictionary<string, string>(){
                    ["prop_sign_road_06p"] = "88"
                }
            }
        };
        public class SignGroup
        {
            public string Name { get; set; }
            public bool UseText { get; set; }
            public string Text { get; set; }
            public Dictionary<string, string> Signs { get; set; }
        }
    }
}
