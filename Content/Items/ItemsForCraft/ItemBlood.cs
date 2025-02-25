using Terraria;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Items.ItemsForCraft 
{
    public class ItemBlood : ModItem 
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.buyPrice(0,0,10,0);
            Item.sellPrice(0,0,0,50);
        }
    }
}