using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Items.ItemsForCraft
{
    public class ItemBlood : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Blue;
            Item.buyPrice(0,0,10,0);
            Item.sellPrice(0,0,0,50);
        }
    }
}