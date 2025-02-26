//using FinalFantasy.Content;
//using FinalFantasy.Content.Items.Consumables;
//using FinalFantasy.Content.NPCs;
using FinalFantasy.Content.Items;
using Microsoft.Xna.Framework;
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
			ResourceEffect,
            SyncDust
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                /* case MessageType.SyncDust: // Обробка SyncDust
                    int dustType = reader.ReadInt32();
                    Vector2 position = reader.ReadVector2();
                    int width = reader.ReadInt32();
                    int height = reader.ReadInt32();

                    // Створення dust на сервері
                    Dust.NewDust(position, width, height, dustType);

                    // Відправка dust всім клієнтам
                    if (Main.netMode == NetmodeID.Server)
                        {
                            ModPacket packet = GetPacket();
                            packet.Write((byte)MessageType.SyncDust);
                            packet.Write(dustType);
                            packet.WriteVector2(position);
                            packet.Write(width);
                            packet.Write(height);
                            packet.Send(-1, whoAmI); // Відправка всім, крім відправника
                        }
                    break; */

                default:
                   Logger.WarnFormat("FinalFantasy: Unknown Message type: {0}", msgType);
					break; 
            }
        }

        public static void SyncDust(int dustType, Vector2 position, int width, int height)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = ModContent.GetInstance<FinalFantasy>().GetPacket();
                packet.Write((byte)FinalFantasy.MessageType.SyncDust);
                packet.Write(dustType);
                packet.WriteVector2(position);
                packet.Write(width);
                packet.Write(height);
                packet.Send();
            }
        }
    }
}