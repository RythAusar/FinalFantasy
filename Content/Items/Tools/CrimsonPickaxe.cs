using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

using FinalFantasy.Content.Buffs.DamageOverTime;
using FinalFantasy.Content.Items.ItemsForCraft;

namespace FinalFantasy.Content.Items.Tools 
{
    public class CrimsonPickaxe : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 9;
            Item.useAnimation = 20;

            Item.damage = 19;
            Item.knockBack = 2.5f;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;

            Item.pick = 105;
            Item.tileBoost += 1;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Bleeding>(), 150);
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<Bleeding>(), 150);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(4)) 
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.LifeDrain);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<ItemBlood>(18).
                AddIngredient(ItemID.SilverPickaxe).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}