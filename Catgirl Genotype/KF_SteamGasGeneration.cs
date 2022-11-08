using System;

namespace XRL.World.Parts.Mutation
{
  [Serializable]
  public class KF_SteamGasGeneration : GasGeneration
  {
    public KF_SteamGasGeneration()
      : base("SteamGas")
    {
    }
	
	public override int GetGasDensityForLevel(int Level) => this.GasObjectDensity + (Level * 100);	

    public override int GetReleaseDuration(int Level) => Level * 2;

    public override int GetReleaseCooldown(int Level) => 42 - (Level * 2);

    public override string GetReleaseAbilityName() => "Release Steam";
  }
}
