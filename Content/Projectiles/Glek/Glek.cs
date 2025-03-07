using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Content.Projectiles.Glek
{
    class Glek : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 3600;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Generic;
            AIType = ProjectileID.NightBeam;
            Projectile.width = 60;
            Projectile.height = 60;
          
        }

        /*public override void AI()
        {
            float maxDistance = 1000f;

            if()
        }
        */

    }
}
