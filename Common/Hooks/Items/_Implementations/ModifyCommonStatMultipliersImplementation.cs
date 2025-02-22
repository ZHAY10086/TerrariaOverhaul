﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Hook = TerrariaOverhaul.Common.Hooks.Items.IModifyCommonStatMultipliers;

namespace TerrariaOverhaul.Common.Hooks.Items
{
	internal sealed class ModifyCommonStatMultipliersImplementation : GlobalItem, IModifyItemMeleeRange
	{
		public override void ModifyHitNPC(Item item, Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
			var multipliers = Hook.GetMultipliers(item, player);

			damage = (int)(damage * multipliers.MeleeDamageMultiplier);
			knockback *= multipliers.MeleeKnockbackMultiplier;
		}

		public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			var multipliers = Hook.GetMultipliers(item, player);

			damage = (int)(damage * multipliers.ProjectileDamageMultiplier);
			knockback *= multipliers.ProjectileKnockbackMultiplier;
			velocity *= multipliers.ProjectileSpeedMultiplier;
		}

		void IModifyItemMeleeRange.ModifyMeleeRange(Item item, Player player, ref float range)
		{
			var multipliers = Hook.GetMultipliers(item, player);

			range *= multipliers.MeleeRangeMultiplier;
		}
	}
}
