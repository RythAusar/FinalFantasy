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
using Terraria.WorldBuilding;
using Terraria.GameInput;
using ExampleMod.Common.Systems;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

namespace FinalFantasy.Content.Items
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    internal class GlekoPort : ModItem
    {
        private Player player = Main.LocalPlayer;

        public static bool IsTree(Tile tile)
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
            Item.defense = 100;
            Item.rare = ItemRarityID.Master;
            Item.buyPrice(0,0,0,1);
            Item.sellPrice(100, 0, 0, 0);
            Item.DamageType = DamageClass.Summon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 0.50f;
            if (KeybindSystem.GlekoPort.JustPressed)
            {
                Tile tile = Framing.GetTileSafely((int)Main.MouseWorld.X / 16, (int)Main.MouseWorld.Y / 16);
                if (!tile.HasTile || IsTree(tile))
                {
                    //Vector2 newPos = new Vector2((int)Main.MouseWorld.X, (int)(Main.MouseWorld.Y - 35));

                    player.Teleport(new Vector2((int)Main.MouseWorld.X, (int)(Main.MouseWorld.Y - 35)), 1, 0);
                }
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod,"Damage","Increases summon damage by 50%"));
        }


    }
}
