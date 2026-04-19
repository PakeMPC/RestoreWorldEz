using Newtonsoft.Json;
using System;
using System.IO;
using TShockAPI;

namespace RestoreWorldEz
{
    public class RestoreConfig
    {
        public string DefaultLang = "en";

        public bool AutoRestore = false;
        public string RestoreInterval = "12h";

        public bool RestoreChests = true;
        public bool ReplaceServerAir = true;
        public bool ReplaceWithReferenceAir = false;

        public bool BackgroundOverwriteTiles = true;

        public bool AnnounceBefore = true;
        public int AnnounceMinutes = 5;

        public bool RandomizeMaps = true;
        public bool MultiMapOverlay = false;

        public static RestoreConfig Read()
        {
            string path = Path.Combine(TShock.SavePath, "RestoreConfig.json");
            try
            {
                if (!File.Exists(path))
                {
                    var newConfig = new RestoreConfig();
                    File.WriteAllText(path, JsonConvert.SerializeObject(newConfig, Formatting.Indented));
                    return newConfig;
                }
                return JsonConvert.DeserializeObject<RestoreConfig>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                RestoreMisc.Log(Restorei18n.Get(-1, "ConfigReadError", ex.Message), true);
                return new RestoreConfig();
            }
        }

        public TimeSpan GetIntervalTime()
        {
            try
            {
                string timeStr = RestoreInterval.ToLower().Trim();
                if (string.IsNullOrEmpty(timeStr)) return TimeSpan.FromHours(1);

                char unit = timeStr[timeStr.Length - 1];
                string valueStr = timeStr.Substring(0, timeStr.Length - 1);

                if (!double.TryParse(valueStr, out double value)) return TimeSpan.FromHours(1);

                switch (unit)
                {
                    case 'm': return TimeSpan.FromMinutes(value);
                    case 'h': return TimeSpan.FromHours(value);
                    case 'd': return TimeSpan.FromDays(value);
                    default: return TimeSpan.FromHours(1);
                }
            }
            catch
            {
                RestoreMisc.Log(Restorei18n.Get(-1, "InvalidInterval"), true);
                return TimeSpan.FromHours(1);
            }
        }
    }
}