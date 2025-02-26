using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Items.Weapons 
{
    public class BloodSword : ModItem 
    {
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 66;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 50;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.LifeDrain);
            //DustID.Water_BloodMoon
            //DustID.CrimtaneWeapons
            //DustID.VampireHeal
            //DustID.LifeDrain
        }

        public override void HoldItem(Player player)
        {
            Vector2 mouseWorld = Main.MouseWorld;
            player.direction = (mouseWorld.X > player.Center.X) ? 1 : -1;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ItemsForCraft.ItemBlood>(), 50)
                .AddIngredient(ItemID.SilverBroadsword)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}