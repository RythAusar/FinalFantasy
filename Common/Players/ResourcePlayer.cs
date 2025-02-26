using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FinalFantasy.Common.Players
{
    public class ResourcePlayer : ModPlayer 
    {
        public int ResourceCurrent;
        public const int DefaultResourceMax = 100;
        public int ResourceMax;
        public int ResourceMax2;
        public float ResourceRegenRate;
        internal int ResourceRegenTimer = 0;
        public bool ResourceMagnet = false;
        public static readonly int ResourceMagnetGrabRange = 300;
        public static readonly Color HealResourceColor = new(187, 91, 291);

        public override void Initialize() 
        {
            ResourceMax = DefaultResourceMax;
        }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead() 
        {
			ResetVariables();
		}

        private void ResetVariables() 
        {
			ResourceRegenRate = 1f;
			ResourceMax2 = ResourceMax;
			ResourceMagnet = false;
		}
    }
}