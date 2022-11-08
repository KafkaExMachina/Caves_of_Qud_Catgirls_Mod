
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using XRL;
using XRL.Liquids;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace CatgirlMod.HarmonyPatches
{
	[HarmonyPatch(typeof(LiquidBlood))]
	public class KF_LiquidBloodHarmonyMod
	{
		[HarmonyPrefix]
        [HarmonyPatch("Drank")]
		static bool Prefix(
			ref bool __result,
			LiquidVolume Liquid,
			int Volume,
			GameObject Target,
			StringBuilder Message,
			ref bool ExitInterface)
		{
			if(Target.IsPlayer() && Target.HasPart("Stomach") 
				&& Target.HasPart("KF_NyanpireMutation"))
			{
				Target.FireEvent(Event.New("AddWater", "Amount", Volume * 10000, "Forced", 1));
				Target.FireEvent(Event.New("AddFood", "Satiation", "Meal"));
				Message.Compound("The sanguine essence of life flows down your throat. Delicious!");
				__result = true;
				return false;
			}
			return true;
		}	
	}
	
	[HarmonyPatch(typeof(LiquidWater))]
	public class KF_LiquidWaterHarmonyMod
	{
		[HarmonyPrefix]
        [HarmonyPatch("Drank")]
		static bool Prefix(
			ref bool __result,
			LiquidVolume Liquid,
			int Volume,
			GameObject Target,
			StringBuilder Message,
			ref bool ExitInterface)
		{
			if(Target.IsPlayer() && Target.HasPart("Stomach") 
				&& Target.HasPart("KF_NyanpireMutation"))
			{
				Message.Compound("{{G|IT BURNS!}}");
				string Dice = "1d10";
				Target.TakeDamage(Dice.Roll(), "from {{G|drinking water}}!", "Acid", Owner: Target, Attacker: Liquid.ParentObject);
				ExitInterface = true;
				__result = true;
				return false;
			}
			return true;
		}	
	}	
}