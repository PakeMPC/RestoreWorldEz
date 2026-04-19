using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TShockAPI;

namespace RestoreWorldEz
{
    public static class RestoreCommands
    {
        public static Dictionary<string, Point> Point1 = new Dictionary<string, Point>();
        public static Dictionary<string, Point> Point2 = new Dictionary<string, Point>();

        public static void Register()
        {
            Commands.ChatCommands.Add(new Command(new List<string> { "worldrestore.admin" }, RestoreCmd, "rw")
            {
                HelpText = Restorei18n.Get(-1, "CmdHelpRW")
            });

            Commands.ChatCommands.Add(new Command(new List<string> { "worldrestore.admin" }, RestoreLangCmd, "restorelang")
            {
                HelpText = Restorei18n.Get(-1, "CmdHelpLang")
            });
        }

        private static void RestoreLangCmd(CommandArgs args)
        {
            if (args.Parameters.Count < 1)
            {
                args.Player.SendErrorMessage(Restorei18n.Get(args.Player.Index, "LangUsage"));
                return;
            }
            string lang = args.Parameters[0].ToLower();
            Restorei18n.PlayerLangs[args.Player.Index] = lang;
            args.Player.SendSuccessMessage(Restorei18n.Get(args.Player.Index, "LangChanged"));
        }

        private static void RestoreCmd(CommandArgs args)
        {
            int pIndex = args.Player.Index;

            if (args.Parameters.Count == 0)
            {
                args.Player.SendInfoMessage(Restorei18n.Get(pIndex, "Help1"));
                args.Player.SendInfoMessage(Restorei18n.Get(pIndex, "Help2"));
                args.Player.SendInfoMessage(Restorei18n.Get(pIndex, "Help3"));
                args.Player.SendInfoMessage(Restorei18n.Get(pIndex, "Help4"));
                args.Player.SendInfoMessage(Restorei18n.Get(pIndex, "Help5"));
                return;
            }

            string subCommand = args.Parameters[0].ToLower();

            switch (subCommand)
            {
                case "set":
                    if (args.Parameters.Count < 2)
                    {
                        args.Player.SendErrorMessage(Restorei18n.Get(pIndex, "SetUsage"));
                        return;
                    }

                    string pNum = args.Parameters[1];
                    if (pNum == "1" || pNum == "2")
                    {
                        RestoreCore.PlayerStates[pIndex] = (pNum == "1") ? "rw_set1" : "rw_set2";
                        args.Player.SendInfoMessage(Restorei18n.Get(pIndex, (pNum == "1") ? "Hit1" : "Hit2"));
                    }
                    else
                    {
                        args.Player.SendErrorMessage(Restorei18n.Get(pIndex, "SetOnly1or2"));
                    }
                    break;

                case "define":
                    if (args.Parameters.Count < 2)
                    {
                        args.Player.SendErrorMessage(Restorei18n.Get(pIndex, "DefineUsage"));
                        return;
                    }

                    if (!Point1.ContainsKey(args.Player.Name) || !Point2.ContainsKey(args.Player.Name))
                    {
                        args.Player.SendErrorMessage(Restorei18n.Get(pIndex, "NoPoints"));
                        return;
                    }

                    Point p1 = Point1[args.Player.Name];
                    Point p2 = Point2[args.Player.Name];
                    string schematicName = args.Parameters[1];

                    int minX = Math.Min(p1.X, p2.X);
                    int minY = Math.Min(p1.Y, p2.Y);
                    int width = Math.Abs(p1.X - p2.X) + 1;
                    int height = Math.Abs(p1.Y - p2.Y) + 1;

                    try
                    {
                        RestoreSchematic.SaveCurrentArea(minX, minY, width, height, schematicName);
                        args.Player.SendSuccessMessage(Restorei18n.Get(pIndex, "DefineSuccess", schematicName));
                        args.Player.SendInfoMessage(Restorei18n.Get(pIndex, "DefineSize", width, height, minX, minY));

                        Point1.Remove(args.Player.Name);
                        Point2.Remove(args.Player.Name);
                    }
                    catch (Exception ex)
                    {
                        args.Player.SendErrorMessage(Restorei18n.Get(pIndex, "SaveError"));
                        RestoreMisc.Log($"Error en /rw define: {ex.Message}", true);
                    }
                    break;

                case "restore":
                case "reload":
                    args.Player.SendInfoMessage(Restorei18n.Get(pIndex, "Restoring"));
                    RestoreWorld.ExecuteRestore(RestoreCore.Config);
                    args.Player.SendSuccessMessage(Restorei18n.Get(pIndex, "RestoreDone"));
                    break;

                case "update":
                    RestoreCore.Config = RestoreConfig.Read();
                    RestoreCore.SetupTimer();
                    args.Player.SendSuccessMessage(Restorei18n.Get(pIndex, "UpdateConfig"));
                    break;

                default:
                    args.Player.SendErrorMessage(Restorei18n.Get(pIndex, "UnknownCmd"));
                    break;
            }
        }
    }
}