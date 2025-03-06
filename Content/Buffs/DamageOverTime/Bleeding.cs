using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Buffs.DamageOverTime 
{
    public class Bleeding : ModBuff, ILocalizedModType
    {
        public new string LocalizationCategory => "Buffs";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.lifeRegen > 0)
                npc.lifeRegen = 0;
            
            npc.lifeRegen -= 10;

            if (Main.rand.NextBool(3)) 
            {
                int dust = Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood,
                    npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default, 1.5f);
                Main.dust[dust].noGravity = true;
            }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.lifeRegen > 0)
                player.lifeRegen = 0;
            
            player.lifeRegen -= 8;

            int dust = Dust.NewDust(player.position, player.width, player.height, DustID.Blood,
                    player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 100, default, 1.5f);
                Main.dust[dust].noGravity = true;
        }
    }
}