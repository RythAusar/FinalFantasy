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

            if (KeybindSystem.GlekoStop.JustPressed)
            {
                player.AddBuff(ModContent.BuffType<TimeStopBuff>(), TimeStop.timeFreezeLength);
            }
        }
        

    }
    public class TimeStopGlobalNPC : GlobalNPC
    {

        public override bool PreAI(NPC npc)
        {
            if (TimeStop.timeFreeze)
            {
                npc.velocity = Vector2.Zero;
                return false;
            }
            else
            {
                return base.PreAI(npc);
            }
        }

        public override void FindFrame(NPC npc, int frameHeight)
        {
            if (TimeStop.timeFreeze == true)
            {
                npc.frameCounter = 0;
                return;
            }
            base.FindFrame(npc, frameHeight);
        }
    }

    public class TimeStopGlobalProjectile : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            if (TimeStop.timeFreeze && projectile.owner != Main.myPlayer)
            {
                projectile.velocity = Vector2.Zero;
                return false;
            }

            return base.PreAI(projectile);
        }
    }


    public class TimeStop
    {
        public static bool timeFreeze = false;
        public static int timeFreezeLength = 600;
        public static int timeFreezeLengthInSeconds = 10;
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

        public override void Update(Player player, ref int buffIndex)
        {
            TimeStop.timeFreeze = true;
            TimeStop.timeFreezeLengthInSeconds -= 1;
            if (TimeStop.timeFreezeLengthInSeconds == 0)
            {
                TimeStop.timeFreeze = false;
                TimeStop.timeFreezeLengthInSeconds = 10;
            }
        }
    }


}


