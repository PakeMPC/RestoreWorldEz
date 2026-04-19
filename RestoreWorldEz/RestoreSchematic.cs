using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Terraria;
using TShockAPI;
using Microsoft.Xna.Framework;

namespace RestoreWorldEz
{
    public static class RestoreSchematic
    {
        private static string RootPath => Path.Combine(TShock.SavePath, "RestoreSchematics");

        public static void SaveCurrentArea(int startX, int startY, int width, int height, string name)
        {
            if (!Directory.Exists(RootPath)) Directory.CreateDirectory(RootPath);
            string fullPath = Path.Combine(RootPath, $"{name}_X{startX}_Y{startY}.ezsch");

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(width);
                writer.Write(height);

                for (int x = startX; x < startX + width; x++)
                {
                    for (int y = startY; y < startY + height; y++)
                    {
                        ITile tile = Main.tile[x, y];

                        writer.Write(tile.active());
                        if (tile.active())
                        {
                            writer.Write(tile.type);
                            writer.Write(tile.frameX);
                            writer.Write(tile.frameY);
                            writer.Write((byte)tile.slope());
                            writer.Write(tile.halfBrick());
                            writer.Write(tile.color());
                            writer.Write(tile.inActive());
                            writer.Write(tile.fullbrightBlock());
                            writer.Write(tile.invisibleBlock());
                        }

                        writer.Write(tile.wall);
                        writer.Write(tile.wallColor());
                        writer.Write(tile.fullbrightWall());
                        writer.Write(tile.invisibleWall());

                        writer.Write(tile.liquid);
                        writer.Write((byte)tile.liquidType());

                        writer.Write(tile.wire());
                        writer.Write(tile.wire2());
                        writer.Write(tile.wire3());
                        writer.Write(tile.wire4());
                        writer.Write(tile.actuator());
                    }
                }

                var chestsInArea = new List<int>();
                for (int i = 0; i < Main.chest.Length; i++)
                {
                    Chest c = Main.chest[i];
                    if (c != null && c.x >= startX && c.x < startX + width && c.y >= startY && c.y < startY + height)
                        chestsInArea.Add(i);
                }

                writer.Write(chestsInArea.Count);
                foreach (int index in chestsInArea)
                {
                    Chest c = Main.chest[index];
                    writer.Write(c.x - startX);
                    writer.Write(c.y - startY);
                    writer.Write(c.name ?? "");

                    for (int i = 0; i < 40; i++)
                    {
                        writer.Write((short)c.item[i].stack);
                        if (c.item[i].stack > 0)
                        {
                            writer.Write(c.item[i].type);
                            writer.Write(c.item[i].prefix);
                        }
                    }
                }

                var signsInArea = new List<int>();
                for (int i = 0; i < Main.sign.Length; i++)
                {
                    Sign s = Main.sign[i];
                    if (s != null && s.text != null && s.x >= startX && s.x < startX + width && s.y >= startY && s.y < startY + height)
                        signsInArea.Add(i);
                }

                writer.Write(signsInArea.Count);
                foreach (int index in signsInArea)
                {
                    Sign s = Main.sign[index];
                    writer.Write(s.x - startX);
                    writer.Write(s.y - startY);
                    writer.Write(s.text ?? "");
                }
            }
        }

        public static void LoadAndPaste(string filePath, RestoreConfig config)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            Match match = Regex.Match(fileName, @"_X(\d+)_Y(\d+)");
            if (!match.Success) return;

            int startX = int.Parse(match.Groups[1].Value);
            int startY = int.Parse(match.Groups[2].Value);

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            bool refActive = reader.ReadBoolean();
                            ushort refType = 0;
                            short refFrameX = 0, refFrameY = 0;
                            byte refSlope = 0, refColor = 0;
                            bool refHalfBrick = false, refInActive = false, refFbBlock = false, refInvBlock = false;

                            if (refActive)
                            {
                                refType = reader.ReadUInt16();
                                refFrameX = reader.ReadInt16();
                                refFrameY = reader.ReadInt16();
                                refSlope = reader.ReadByte();
                                refHalfBrick = reader.ReadBoolean();
                                refColor = reader.ReadByte();
                                refInActive = reader.ReadBoolean();
                                refFbBlock = reader.ReadBoolean();
                                refInvBlock = reader.ReadBoolean();
                            }

                            ushort refWall = reader.ReadUInt16();
                            byte refWallColor = reader.ReadByte();
                            bool refFbWall = reader.ReadBoolean();
                            bool refInvWall = reader.ReadBoolean();

                            byte refLiquid = reader.ReadByte();
                            byte refLiquidType = reader.ReadByte();

                            bool refWire = reader.ReadBoolean();
                            bool refWire2 = reader.ReadBoolean();
                            bool refWire3 = reader.ReadBoolean();
                            bool refWire4 = reader.ReadBoolean();
                            bool refActuator = reader.ReadBoolean();

                            int tx = startX + x;
                            int ty = startY + y;

                            if (!RestoreMisc.InMapBoundaries(tx, ty)) continue;

                            ITile targetTile = Main.tile[tx, ty];

                            bool pasteBlock = true;
                            if (!refActive && !config.ReplaceWithReferenceAir) pasteBlock = false;
                            if (!targetTile.active() && !config.ReplaceServerAir) pasteBlock = false;

                            if (pasteBlock)
                            {
                                targetTile.active(refActive);
                                if (refActive)
                                {
                                    targetTile.type = refType;
                                    targetTile.frameX = refFrameX;
                                    targetTile.frameY = refFrameY;
                                    targetTile.slope(refSlope);
                                    targetTile.halfBrick(refHalfBrick);
                                    targetTile.color(refColor);
                                    targetTile.inActive(refInActive);
                                    targetTile.fullbrightBlock(refFbBlock);
                                    targetTile.invisibleBlock(refInvBlock);
                                }
                            }

                            bool pasteWall = true;
                            if (refWall == 0 && !config.ReplaceWithReferenceAir) pasteWall = false;

                            if (pasteWall)
                            {
                                if (config.BackgroundOverwriteTiles && refWall > 0 && !refActive)
                                {
                                    targetTile.active(false);
                                }

                                targetTile.wall = refWall;
                                targetTile.wallColor(refWallColor);
                                targetTile.fullbrightWall(refFbWall);
                                targetTile.invisibleWall(refInvWall);
                            }

                            bool pasteLiquid = true;
                            if (refLiquid == 0 && !config.ReplaceWithReferenceAir) pasteLiquid = false;

                            if (pasteLiquid)
                            {
                                targetTile.liquid = refLiquid;
                                targetTile.liquidType(refLiquidType);
                            }

                            targetTile.wire(refWire);
                            targetTile.wire2(refWire2);
                            targetTile.wire3(refWire3);
                            targetTile.wire4(refWire4);
                            targetTile.actuator(refActuator);
                        }
                    }

                    if (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        int chestCount = reader.ReadInt32();
                        for (int i = 0; i < chestCount; i++)
                        {
                            int relX = reader.ReadInt32();
                            int relY = reader.ReadInt32();
                            string cName = reader.ReadString();

                            Item[] items = new Item[40];
                            for (int j = 0; j < 40; j++)
                            {
                                items[j] = new Item();
                                short stack = reader.ReadInt16();
                                if (stack > 0)
                                {
                                    items[j].netDefaults(reader.ReadInt32());
                                    items[j].stack = stack;
                                    items[j].prefix = reader.ReadByte();
                                }
                            }

                            int targetX = startX + relX;
                            int targetY = startY + relY;

                            if (config.RestoreChests && RestoreMisc.InMapBoundaries(targetX, targetY))
                            {
                                RestoreChests.ProcessChest(targetX, targetY, items);
                            }
                        }
                    }

                    if (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        int signCount = reader.ReadInt32();
                        for (int i = 0; i < signCount; i++)
                        {
                            int relX = reader.ReadInt32();
                            int relY = reader.ReadInt32();
                            string text = reader.ReadString();

                            int targetX = startX + relX;
                            int targetY = startY + relY;

                            if (RestoreMisc.InMapBoundaries(targetX, targetY))
                            {
                                int signIndex = Sign.ReadSign(targetX, targetY);
                                if (signIndex != -1)
                                {
                                    Main.sign[signIndex].text = text;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { RestoreMisc.Log(Restorei18n.Get(-1, "LoadError", ex.Message), true); }
        }
    }
}