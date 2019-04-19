using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadSigns
{
    public class Config
    {
        public bool FadeEffects { get; set; }
        public bool FlashEffect { get; set; }
        public bool SignPillar { get; set; }

        public Corner DisplayCorner { get; set; }

        public List<SignGroup> SignGroups { get; set; }
        public class SignGroup
        {
            public string Name { get; set; }
            public bool UseText { get; set; }
            public string Text { get; set; }
            public Dictionary<string, string> Signs { get; set; }
        }

        public enum Corner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
    }
}
