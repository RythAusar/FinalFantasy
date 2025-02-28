using Microsoft.Xna.Framework;
using Stubble.Core.Settings;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Items.Weapons
{
    class Ascalon : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.noMelee = false;
            Item.shoot = ProjectileID.Arkhalis;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Cockatiel;
            Item.shootSpeed = 16f;
            Item.damage = 1488;

        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

    }
}
