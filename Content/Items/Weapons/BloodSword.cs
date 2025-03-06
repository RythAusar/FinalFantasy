using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using FinalFantasy.Content.Buffs;
using FinalFantasy.Content.Items.ItemsForCraft;
using FinalFantasy.Content.Buffs.DamageOverTime;

namespace FinalFantasy.Content.Items.Weapons 
{
    public class BloodSword : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 66;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;
            Item.useTurn = true;

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
            /* int dustType = DustID.LifeDrain;
            Vector2 position = new Vector2(hitbox.X, hitbox.Y);
            int width = hitbox.Width;
            int height = hitbox.Height;
            
            Dust.NewDust(position, width, height, dustType); */
           
            //FinalFantasy.SyncDust(dustType, position, width, height); 
            //DustID.Water_BloodMoon
            //DustID.CrimtaneWeapons
            //DustID.VampireHeal
            //DustID.LifeDrain

            if (Main.rand.NextBool(3)) 
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.LifeDrain);
            }
        }

        public override void HoldItem(Player player)
        {
            
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(BuffID.OnFire, 60);
            target.AddBuff(ModContent.BuffType<Bleeding>(), 300);
        }

        public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
        {
            //target.AddBuff(BuffID.OnFire, 60);
            target.AddBuff(ModContent.BuffType<Bleeding>(), 300);
        }

        public override bool MeleePrefix() {
			return true;
		}

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient<ItemBlood>(18).
                AddIngredient(ItemID.SilverBroadsword).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}