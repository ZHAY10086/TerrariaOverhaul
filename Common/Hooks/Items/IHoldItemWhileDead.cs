﻿using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace TerrariaOverhaul.Common.Hooks.Items
{
	public interface IHoldItemWhileDead
	{
		public static readonly HookList<GlobalItem, Action<Item, Player>> Hook = ItemLoader.AddModHook(new HookList<GlobalItem, Action<Item, Player>>(
			//Method reference
			typeof(IHoldItemWhileDead).GetMethod(nameof(IHoldItemWhileDead.HoldItemWhileDead)),
			//Invocation
			e => (Item item, Player player) => {
				foreach(IHoldItemWhileDead g in e.Enumerate(item)) {
					g.HoldItemWhileDead(item, player);
				}
			}
		));

		void HoldItemWhileDead(Item item, Player player);
	}

	public sealed class PlayerHoldItemWhileDeadHookImplementation : ModPlayer
	{
		public override void UpdateDead()
		{
			var heldItem = Player.HeldItem;

			if(heldItem?.IsAir == false) {
				IHoldItemWhileDead.Hook.Invoke(heldItem, Player);
			}
		}
	}
}
