using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Terraria;
using TShockAPI;

namespace RestoreWorldEz
{
    public static class RestoreWorld
    {
        public static string RootPath => Path.Combine(TShock.SavePath, "RestoreSchematics");

        public static void Setup()
        {
            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
                RestoreMisc.Log(Restorei18n.Get(-1, "FolderCreated"));
            }
        }

        public static void ExecuteRestore(RestoreConfig config)
        {
            try
            {
                Setup();

                string[] files = Directory.GetFiles(RootPath, "*.ezsch");
                if (files.Length == 0)
                {
                    RestoreMisc.Log(Restorei18n.Get(-1, "NoSchematics"), true);
                    return;
                }

                var mapGroups = GroupFilesByCoordinates(files);
                List<string> selectedMaps = new List<string>();

                RestoreMisc.Log(Restorei18n.Get(-1, "ZonesDetected", mapGroups.Count));

                Random rnd = new Random();
                foreach (var group in mapGroups)
                {
                    if (config.RandomizeMaps)
                    {
                        selectedMaps.Add(group.Value[rnd.Next(group.Value.Count)]);
                    }
                    else if (config.MultiMapOverlay)
                    {
                        selectedMaps.AddRange(group.Value);
                    }
                    else
                    {
                        selectedMaps.Add(group.Value.First());
                    }
                }

                foreach (string mapPath in selectedMaps)
                {
                    RestoreMisc.Log(Restorei18n.Get(-1, "PastingSchematic", Path.GetFileName(mapPath)));
                    RestoreSchematic.LoadAndPaste(mapPath, config);
                }

                RestoreMisc.InformPlayers();
                RestoreMisc.Log(Restorei18n.Get(-1, "RestoreSuccess"));
            }
            catch (Exception ex)
            {
                RestoreMisc.Log(Restorei18n.Get(-1, "CriticalError", ex.Message), true);
            }
        }

        private static Dictionary<string, List<string>> GroupFilesByCoordinates(string[] files)
        {
            var groups = new Dictionary<string, List<string>>();
            Regex regex = new Regex(@"_X(\d+)_Y(\d+)");

            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                Match match = regex.Match(fileName);

                if (match.Success)
                {
                    string key = $"{match.Groups[1].Value},{match.Groups[2].Value}";

                    if (!groups.ContainsKey(key))
                        groups[key] = new List<string>();

                    groups[key].Add(file);
                }
                else
                {
                    RestoreMisc.Log(Restorei18n.Get(-1, "InvalidFileName", Path.GetFileName(file)), true);
                }
            }

            return groups;
        }
    }
}