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
using Terraria.Chat;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework.Graphics;
using Luminance.Core.Graphics;

namespace FinalFantasy.Content.Items.Accessories
{
    internal class GlekoPort : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public static bool isTree(Tile tile)
        {
            if (tile == null) return false;

            return tile.TileType == 171 ||
              tile.TileType == 72 ||
              tile.TileType == 323 ||
              tile.TileType == 170 ||
              tile.TileType == 589 ||
              tile.TileType == 584 ||
              tile.TileType == 634 ||
              tile.TileType == 588 ||
              tile.TileType == 586 ||
              tile.TileType == 293 ||
              tile.TileType == 587 ||
              tile.TileType == 5   ||
              tile.TileType == 585 ||
              tile.TileType == 583 ||
              tile.TileType == 596 ||
              tile.TileType == 595 ||
              tile.TileType == 615 ||
              tile.TileType == 616;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.noMelee = true;
            Item.accessory = true;
            Item.defense = 20;
            Item.rare = ItemRarityID.Master;
            Item.buyPrice(0,0,0,1);
            Item.sellPrice(100, 0, 0, 0);
            Item.DamageType = DamageClass.Generic;
        }

        public static void TeleportPlayer(Player player)
        {
            if (KeybindSystem.GlekoPort.JustPressed)
            {
                Tile tile = Framing.GetTileSafely((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
                if (!tile.HasTile || isTree(tile))
                {
                    Vector2 newPos = new Vector2((int)Main.MouseWorld.X, (int)(Main.MouseWorld.Y - 35));
                    if(player.whoAmI == Main.myPlayer)
                    {
                        player.Teleport(newPos, 1, 1);
                        if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server)
                        {
                            //remoteClient - Which client receives the message (-1 = all clients); ignoreClient - Which client to ignore (-1 = none); text - Not used here (null); entityWhoAmI - The entity being teleported (player.whoAmI or npc.whoAmI); X - New X position of entity; Y - New Y position of entity
                            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI, newPos.X, newPos.Y, 1);
                            //Server broadcasting message in ingame chat to clients
                            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("GLEKS"), Color.White, -1);
                        }
                    }
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.50f;
            TeleportPlayer(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
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
                shader.Apply("PulseUpwards");//passing a pass. This time it is PulseUpwards
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
}