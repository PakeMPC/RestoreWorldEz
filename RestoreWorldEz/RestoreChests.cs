using Terraria;

namespace RestoreWorldEz
{
    public static class RestoreChests
    {
        public static void ProcessChest(int x, int y, Item[] items)
        {
            int chestIndex = Chest.FindChest(x, y);

            if (chestIndex == -1)
            {
                chestIndex = Chest.CreateChest(x, y);
            }

            if (chestIndex != -1)
            {
                for (int i = 0; i < 40; i++)
                {
                    Main.chest[chestIndex].item[i] = items[i] ?? new Item();
                }
            }
        }
    }
}