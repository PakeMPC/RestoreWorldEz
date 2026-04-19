using System.Data;
using TShockAPI;
using TShockAPI.DB;

namespace RestoreWorldEz
{
    public static class RestoreDB
    {

        private static IDbConnection db => TShock.DB;

        public static void Connect()
        {
            string query = db.GetSqlType() == SqlType.Sqlite
                ? "CREATE TABLE IF NOT EXISTS RestoreChests (ID INTEGER PRIMARY KEY AUTOINCREMENT, X INTEGER, Y INTEGER, WorldID INTEGER, Items TEXT);"
                : "CREATE TABLE IF NOT EXISTS RestoreChests (ID INT AUTO_INCREMENT PRIMARY KEY, X INT, Y INT, WorldID INT, Items TEXT);";

            db.Query(query);
        }

        public static void AddOrUpdateChest(int x, int y, int worldId, string serializedItems)
        {
            using (var reader = db.QueryReader("SELECT ID FROM RestoreChests WHERE X = @0 AND Y = @1 AND WorldID = @2", x, y, worldId))
            {
                if (reader.Read())
                {
                    db.Query("UPDATE RestoreChests SET Items = @0 WHERE X = @1 AND Y = @2 AND WorldID = @3",
                        serializedItems, x, y, worldId);
                    return;
                }
            }

            db.Query("INSERT INTO RestoreChests (X, Y, WorldID, Items) VALUES (@0, @1, @2, @3)",
                x, y, worldId, serializedItems);
        }
    }
}