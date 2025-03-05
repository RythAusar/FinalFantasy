using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using ExampleMod.Common.Systems;
using System.Security.Cryptography.X509Certificates;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.Enums;
using Terraria.Graphics.Effects;
using System.Net.Http.Headers;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Luminance.Core.Graphics;


namespace FinalFantasy.Content.Items.Accessories
{
    public class GlekoStop : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 7));
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.DamageType = DamageClass.Generic;
            Item.noMelee = true;
            Item.defense = 20;
            Item.rare = -12;//Rainbow
            Item.buyPrice(platinum: 10, 0, 0, 0);
            Item.sellPrice(platinum: 10, 0, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.20f;

            if (KeybindSystem.GlekoStop.JustPressed && !TimeStop.timeFreeze)
            {
                TimeStop.timeFreeze = true;
                TimeStop.timeFreezeLength = 600;
                player.AddBuff(ModContent.BuffType<TimeStopBuff>(), TimeStop.timeFreezeLength);
            }
            
            if (TimeStop.timeFreeze)
            {
                TimeStop.timeFreezeLength--;

                if (TimeStop.timeFreezeLength <= 0)
                {
                    TimeStop.timeFreeze = false;
                    TimeStop.timeFreezeLength = 600;
                }
            }
        }

        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End();//ending drawing commands
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);//starts sprite batch using Immediate mode. Immediate mode, each draw call is processed as soon as it's made, which is necessary when you want to change rendering states (like applying a shader).
                ManagedShader shader = ShaderManager.GetShader("FinalFantasy.Text");//No need to load the shader through Load(). Luminance.dll does it for me. Just FinalFantasy.(shader file name)
                shader.TrySetParameter("mainColor", new Color(0,0,0));//setting main color
                shader.TrySetParameter("secondaryColor", new Color(255, 255, 255));//setting secondary color
                shader.Apply("PulseDiagonal");//passing a pass. This time it is PulseDiagonal
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

    }
    public class TimeStopGlobalNPC : GlobalNPC
    {

        public override bool PreAI(NPC npc)
        {
            bool retval = base.PreAI(npc);
            if (TimeStop.timeFreeze)
            {
                //npc.velocity = Vector2.Zero;
                npc.position = npc.oldPosition;
                npc.frameCounter = 0;
                retval = false;
            }
            return retval;
        }


        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            if (TimeStop.timeFreeze)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }

    public class TimeStopGlobalProjectile : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            bool retval = base.PreAI(projectile);
            if (TimeStop.timeFreeze && projectile.owner != Main.myPlayer)
            {
                projectile.position = projectile.oldPosition;
                projectile.velocity = Vector2.Zero;
                projectile.frameCounter = 0;
                retval = false;
            }

            return retval;
        }

        public override void PostAI(Projectile projectile)
        {
            if (TimeStop.timeFreeze && projectile.owner != Main.myPlayer)
            {
                return;
            }
            else
            {
                base.PostAI(projectile);
            }

        }

        public override bool CanHitPlayer(Projectile projectile, Player target)//Can't hit player when time is stopped
        {
            bool retval = base.CanHitPlayer(projectile, target);
            if (TimeStop.timeFreeze == true && projectile.owner != Main.myPlayer)
            {
                retval = false;
            }
            return retval;
        }

    }


    public class TimeStop
    {
        public static bool timeFreeze = false;
        public static int timeFreezeLength;
    }

    public class TimeStopBuff : ModBuff
    {
        public override string Texture => "FinalFantasy/Content/Items/Accessories/TimeStopBuff";

        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;

        }
    }
}




