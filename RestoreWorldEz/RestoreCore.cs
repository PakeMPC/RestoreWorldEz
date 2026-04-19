using System;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Microsoft.Xna.Framework;

namespace RestoreWorldEz
{
    [ApiVersion(2, 1)]
    public class RestoreCore : TerrariaPlugin
    {
        public override string Name => "RestoreWorldEz";
        public override string Author => "PakeMPC";
        public override string Description => "System for restoring structures through block selection.";
        public override Version Version => new Version(1, 1, 0, 0);

        public static RestoreConfig Config;

        private static Timer _tickTimer;
        private static DateTime _nextRestore;
        private static bool _hasAnnounced;

        public static Dictionary<int, string> PlayerStates = new Dictionary<int, string>();

        public RestoreCore(Main game) : base(game) { }

        public override void Initialize()
        {
            Config = RestoreConfig.Read();
            string schematicsPath = Path.Combine(TShock.SavePath, "RestoreSchematics");
            if (!Directory.Exists(schematicsPath)) Directory.CreateDirectory(schematicsPath);

            RestoreCommands.Register();
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
            ServerApi.Hooks.NetGetData.Register(this, OnGetData);

            SetupTimer();
        }

        private void OnInitialize(EventArgs args)
        {
            RestoreMisc.Log(Restorei18n.Get(-1, "PluginLoaded"));
        }

        public static void SetupTimer()
        {
            if (_tickTimer != null)
            {
                _tickTimer.Stop();
                _tickTimer.Dispose();
            }

            if (Config.AutoRestore)
            {
                _nextRestore = DateTime.Now.Add(Config.GetIntervalTime());
                _hasAnnounced = false;

                _tickTimer = new Timer(1000);
                _tickTimer.Elapsed += OnTick;
                _tickTimer.AutoReset = true;
                _tickTimer.Start();
            }
        }

        private static void OnTick(object sender, ElapsedEventArgs e)
        {
            TimeSpan timeLeft = _nextRestore - DateTime.Now;

            if (Config.AnnounceBefore && !_hasAnnounced && timeLeft.TotalMinutes <= Config.AnnounceMinutes)
            {
                _hasAnnounced = true;
                string msg = Restorei18n.Get(-1, "AnnounceRestore", Config.AnnounceMinutes);
                TSPlayer.All.SendInfoMessage(msg); 
            }

            if (timeLeft.TotalSeconds <= 0)
            {
                _nextRestore = DateTime.Now.Add(Config.GetIntervalTime());
                _hasAnnounced = false;

                RestoreWorld.ExecuteRestore(Config);
            }
        }

        private void OnGetData(GetDataEventArgs args)
        {
            if (args.MsgID != (PacketTypes)17) return;

            using (var reader = new BinaryReader(new MemoryStream(args.Msg.readBuffer, args.Index, args.Length)))
            {
                byte action = reader.ReadByte();
                short x = reader.ReadInt16();
                short y = reader.ReadInt16();

                if (!PlayerStates.ContainsKey(args.Msg.whoAmI)) return;

                string state = PlayerStates[args.Msg.whoAmI];
                var player = TShock.Players[args.Msg.whoAmI];

                if (state == "rw_set1" || state == "rw_set2")
                {
                    args.Handled = true;

                    player.SendTileSquareCentered(x, y, 3);

                    if (state == "rw_set1")
                    {
                        RestoreCommands.Point1[player.Name] = new Point(x, y);
                        player.SendSuccessMessage(Restorei18n.Get(player.Index, "Set1", x, y));
                    }
                    else
                    {
                        RestoreCommands.Point2[player.Name] = new Point(x, y);
                        player.SendSuccessMessage(Restorei18n.Get(player.Index, "Set2", x, y));
                    }

                    PlayerStates.Remove(args.Msg.whoAmI);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
                ServerApi.Hooks.NetGetData.Deregister(this, OnGetData);
                if (_tickTimer != null)
                {
                    _tickTimer.Stop();
                    _tickTimer.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}