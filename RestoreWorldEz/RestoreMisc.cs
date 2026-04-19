using System;
using Terraria;
using TShockAPI;

namespace RestoreWorldEz
{
    public static class RestoreMisc
    {
        public static bool InMapBoundaries(int x, int y)
        {
            return (x >= 0 && y >= 0 && x < Main.maxTilesX && y < Main.maxTilesY);
        }

        public static TimeSpan ParseTimeSpan(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return TimeSpan.FromHours(1);

            char unit = input[input.Length - 1];
            if (double.TryParse(input.Substring(0, input.Length - 1), out double value))
            {
                switch (unit)
                {
                    case 'm': return TimeSpan.FromMinutes(value);
                    case 'h': return TimeSpan.FromHours(value);
                    case 'd': return TimeSpan.FromDays(value);
                }
            }
            return TimeSpan.FromHours(1);
        }

        public static void InformPlayers()
        {
            for (int i = 0; i < 255; i++)
            {
                if (Netplay.Clients[i].IsActive)
                {
                    for (int x = 0; x < Main.maxSectionsX; x++)
                    {
                        for (int y = 0; y < Main.maxSectionsY; y++)
                        {
                            Netplay.Clients[i].TileSections[x, y] = false;
                        }
                    }
                }
            }
 
            TSPlayer.All.SendData(PacketTypes.WorldInfo);
        }

        public static void Log(string message, bool error = false)
        {
            if (error)
            {
                TShock.Log.ConsoleError($"[RestoreWorldEz] {message}");
            }
            else
            {
                TShock.Log.ConsoleInfo($"[RestoreWorldEz] {message}");
            }
        }
    }
}