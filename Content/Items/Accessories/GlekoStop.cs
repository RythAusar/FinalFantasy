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
                //Filters.Scene.Activate("TimeStop", player.Center).GetShader().UseColor(3, 5, 15).UseTargetPosition(player.Center);
                player.AddBuff(ModContent.BuffType<TimeStopBuff>(), TimeStop.timeFreezeLength);
            }
            
            /*if (Main.netMode != NetmodeID.Server && Filters.Scene["TimeStop"].IsActive())
            {
                float progress = (180f) / 60f;
                Filters.Scene["TimeStop"].GetShader().UseProgress(progress).UseOpacity(100f * (1 - progress / 3f));
            }

            if (Main.netMode != NetmodeID.Server && Filters.Scene["TimeStop"].IsActive() && TimeStop.timeFreeze == false)
            {
                Filters.Scene["TimeStop"].Deactivate();
            }
            */
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
    
    /*public class TimeStopFX : Mod
    {
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Asset<Effect> timeStopFX = this.Assets.Request<Effect>("Effects/TimeStop");
                // "PassName" should correspond to the name of your pass within the *technique*,
                // so if you get an error here, make sure you've spelled it right across your effect file.
                Filters.Scene["TimeStop"] = new Filter(new ScreenShaderData(timeStopFX, "TimeStop"), EffectPriority.VeryHigh);
                Filters.Scene["TimeStop"].Load();
            }
        }

    }
    */
}




