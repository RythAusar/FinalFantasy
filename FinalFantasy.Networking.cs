//using FinalFantasy.Content;
//using FinalFantasy.Content.Items.Consumables;
//using FinalFantasy.Content.NPCs;
using FinalFantasy.Content.Items;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy 
{
    partial class FinalFantasy : Mod
    {
        internal enum MessageType : byte
        {
            ExampleStatIncreasePlayerSync,
			ExampleTeleportToStatue,
			ExampleDodge,
			ExampleTownPetUnlockOrExchange,
			ResourceEffect
        }

       /*  public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.ResourceEffect:
                    ResourcePlayer.HandleResourceEffectMessage(reader, whoAmI);
                    break;
                default:
                   Logger.WarnFormat("ExampleMod: Unknown Message type: {0}", msgType);
					break; 
            }
        } */
    }
}