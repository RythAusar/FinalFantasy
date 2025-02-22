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

namespace FinalFantasy
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    internal class GlekoPort : ModItem
    {
        public ushort[] trees = { 171, 72, 323, 170, 589, 584, 634, 588, 586, 293, 587, 5, 585, 583, 596, 595, 615, 616};

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
              tile.TileType == 5 ||
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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if(KeybindSystem.GlekoPort.JustPressed)
            {
                Tile tile = Framing.GetTileSafely((int)Main.MouseWorld.X /16, (int)Main.MouseWorld.Y / 16);
                Player localPlayer = Main.LocalPlayer;
                Vector2 playerPos = player.Center;
                if (!tile.HasTile || IsTree(tile))
                {
                    player.Teleport(Main.MouseWorld, 1, 0);
                }
                
            }
        }

        

    }
}
