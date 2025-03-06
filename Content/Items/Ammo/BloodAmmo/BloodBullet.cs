﻿using System;
using FinalFantasy.Content.Items.ItemsForCraft;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Items.Ammo.BloodAmmo
{
    public class BloodBullet : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Ammo"; // Doesn`t working, Why ?
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<Content.Projectiles.BloodAmmo.ProjectileBloodBullet>();
            Item.shootSpeed = 4.5f;
            Item.ammo = AmmoID.Bullet; 
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ItemBlood>(), 1)
                .AddIngredient(ItemID.SilverBullet, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}

