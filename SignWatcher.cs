using CitizenFX.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace RoadSigns
{
    class SignWatcher
    {
        private readonly List<Sign> Signs;
        private readonly string Type;
        private readonly bool UseText;
        private readonly string Text;

        private readonly Main MainInstance;

        private string LastLimit = null;
        private string LastStreet = null;
        private int StreetDiff = 0;

        public SignWatcher(Main main, string type, List<Sign> signs, bool useText = false, string text = null)
        {
            Type = type;
            Signs = signs;
            UseText = useText;
            MainInstance = main;
            Text = text;
            SendNuiMessage(JsonConvert.SerializeObject(new { Create = true, Type, UseText }));
        }

        public async Task TickHandler()
        {
            if (IsPedInAnyVehicle(PlayerPedId(), false))
            {
                var Coords = GetEntityCoords(PlayerPedId(), true);
                Sign LimitSign = null;
                foreach (var sign in Signs)
                {
                    var Object = GetClosestObjectOfType(Coords.X, Coords.Y, Coords.Z, 10f, (uint)GetHashKey(sign), false, false, false);
                    if (Object != 0)
                    {
                        LimitSign = sign;
                        break;
                    }
                }

                if (LimitSign != null && LimitSign.SpeedLimit != LastLimit)
                {
                    if (UseText)
                        SendNuiMessage(JsonConvert.SerializeObject(new { Type, Show = true, Limit = LimitSign.SpeedLimit, Text }));
                    else
                        SendNuiMessage(JsonConvert.SerializeObject(new { Type, Show = true, Limit = LimitSign.SpeedLimit }));
                    LastLimit = LimitSign.SpeedLimit;
                    LastStreet = GetStreetName(Coords, out _, out _);
                    Debug.WriteLine($"New Limit: {LastLimit} Street: {LastStreet}");
                }
                else if (LastLimit != null && !CheckStreetNames(LastStreet, GetStreetName(Coords, out uint StreetHash, out _)))
                {
                    if (StreetDiff == 3)
                        ResetLimit();
                    else
                        StreetDiff++;
                }
            }
            else if (LastLimit != null)
            {
                ResetLimit();
            }
            await MainInstance.Delay(500);
        }

        private void ResetLimit()
        {
            SendNuiMessage(JsonConvert.SerializeObject(new { Type, Hide = true }));
            LastLimit = null;
            LastStreet = null;
            StreetDiff = 0;
        }

        private string GetStreetName(Vector3 Coords, out uint StreetHash, out uint CrossStreetHash)
        {
            StreetHash = 0;
            CrossStreetHash = 0;
            GetStreetNameAtCoord(Coords.X, Coords.Y, Coords.Z, ref StreetHash, ref CrossStreetHash);
            return GetStreetNameFromHashKey(StreetHash);
        }

        private bool CheckStreetNames(string OldStr, string NewStr)
        {
            if (OldStr == null || NewStr == null)
                return false;

            if (OldStr == NewStr)
                return true;

            var OldStrWords = OldStr.Split(' ');
            var NewStrWords = NewStr.Split(' ');
            foreach (var oldword in OldStrWords)
            {
                if (oldword.Length < 4)
                    continue;
                foreach (var newword in NewStrWords)
                {
                    if (newword.Length < 4)
                        continue;
                    if (oldword == newword)
                    {
                        LastStreet = NewStr;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
