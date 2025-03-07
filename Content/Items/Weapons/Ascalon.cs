using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Stubble.Core.Settings;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Items.Weapons
{
    class Ascalon : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 120;
            Item.height = 120;
            Item.shoot = ProjectileID.StarWrath;
            Item.shootSpeed = 16f;
            Item.useStyle = ItemUseStyleID.Swing;
            //Item.UseSound = SoundID.DeerclopsScream;
            Item.UseSound = new SoundStyle("FinalFantasy/Content/Sounds/Sounds_SFX_RadGunSuccess")
            {
                MaxInstances = 30
            };
            Item.damage = 100000;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.autoReuse = true;
            Item.crit = 50;
            Item.knockBack = 10;
            Item.DamageType = DamageClass.Generic;

        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void HoldItem(Player player)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                if(Main.MouseWorld.X > player.Center.X)
                {
                    player.direction = 1;
                }else if(Main.MouseWorld.X < player.Center.X)
                {
                    player.direction = -1;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.altFunctionUse == 2)//If right mouse click
            {
               
                Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);//Top left corner of your screen(in world coordinates) + your mouse position
                float ceilingLimit = target.Y;
                if(ceilingLimit > player.Center.Y - 200f)//if your mouse cursor points to a position that is more than your character's Y - 200
                {
                    ceilingLimit = player.Center.Y - 200f;
                }
                for (int i = 0; i < 3; i++)
                {
                    position = player.Center - new Vector2(Main.rand.NextFloat(100) * player.direction, 600f);//sets the spawn position of projectile
                    position.Y -= 100 * i;//varies the spawn position of each of the 3 projectiles that is called in 1 frame
                    Vector2 heading = target - position;//difference between mouse cursor(in world coordinates) and spawn position of a projectile

                    if (heading.Y < 0f)
                    {
                        heading.Y *= -1f;
                    }

                    if (heading.Y < 20f)
                    {
                        heading.Y = 20f;
                    }

                    heading.Normalize();//normalizes heading turning it into direction
                    heading *= velocity.Length();
                    heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(source, position, heading, ProjectileID.StarWrath, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
                    
                }
            }
            else//If left mouse click
            {
                Item.shoot = ProjectileID.DarkLance;
                velocity *= 0.2f;
                Vector2 target = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
                position = new Vector2(player.Center.X, player.Center.Y - 200f);//spawn position of the projectile
                Vector2 heading = target - position;
                heading.Normalize();
                heading *= velocity.Length();
                Projectile.NewProjectile(source, position, heading, ProjectileID.IceBolt, damage * 2, knockback, player.whoAmI, 0f);
            }
                return false;
        }
        

       

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End();//ending drawing commands
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);//starts sprite batch using Immediate mode. Immediate mode, each draw call is processed as soon as it's made, which is necessary when you want to change rendering states (like applying a shader).
                ManagedShader shader = ShaderManager.GetShader("FinalFantasy.Text");//No need to load the shader through Load(). Luminance.dll does it for me. Just FinalFantasy.(shader file name)
                shader.TrySetParameter("mainColor", new Color(252, 0, 50));//setting main color
                shader.TrySetParameter("secondaryColor", new Color(0, 255, 200));//setting secondary color
                shader.Apply("ModifiedShader");//passing a pass. This time it is PulseUpwards
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);//draw tooltip
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            else
            {
                return true;
            }
        }

       //Effect AscalonEffect = ModContent.Load<Effect>("Effects/Ambient");   

        

        public override void AddRecipes()
        {
            CreateRecipe().AddIngredient(ItemID.DirtBlock, 1).AddTile(TileID.WorkBenches);
        }


    }
}
