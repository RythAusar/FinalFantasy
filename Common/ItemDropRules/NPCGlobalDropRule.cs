using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy 
{
    public class NPCGlobalDropRules : GlobalNPC 
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            int[] zombieTypes =
            {
                NPCID.Zombie,
                NPCID.BaldZombie,
                NPCID.PincushionZombie,
                NPCID.SlimedZombie,
                NPCID.SwampZombie,
                NPCID.TwiggyZombie,
                NPCID.FemaleZombie,
                NPCID.ZombieRaincoat,
                NPCID.ZombieEskimo,
                NPCID.ZombieMushroom,
                NPCID.ZombieMushroomHat,
                NPCID.ZombieDoctor,
                NPCID.ZombiePixie
            };

            if (zombieTypes.Contains(npc.type)) {
                /* IItemDropRule copperArmorRule = ItemDropRule.Common(ItemID.CopperHelmet, 1);
                copperArmorRule.OnSuccess(ItemDropRule.Common(ItemID.CopperChainmail, 1));
                copperArmorRule.OnSuccess(ItemDropRule.Common(ItemID.CopperGreaves, 1));
                npcLoot.Add(copperArmorRule); */

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Content.Items.ItemsForCraft.ItemBlood>(), 5, 2, 5));
            }
        }
    }
}