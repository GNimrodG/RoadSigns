using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadSigns
{
    public class Sign
    {
        public string ObjectName;

        public string SpeedLimit;
        public Sign(string objectname, string speed)
        {
            ObjectName = objectname;
            SpeedLimit = speed;
        }

        public static implicit operator string(Sign sign) => sign.ObjectName;
    }
}
