using System;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
  [Serializable]
  public class KF_NyanpireMutation : BaseMutation
  {
    public KF_NyanpireMutation() => this.DisplayName = "Nyanpire";

    public override void Register(GameObject Object) => base.Register(Object);
	
	public override bool CanLevel() => false;
	
	public override string GetDescription() => "You are a spooky Nyanpire!\n\nBeware the light of day!";
	
	public override string GetLevelText(int Level) => "";

    public override bool ChangeLevel(int NewLevel) => base.ChangeLevel(NewLevel);
	
    public override bool WantTurnTick() => true;

    public override bool WantTenTurnTick() => true;

    public override bool WantHundredTurnTick() => true;

    public override void TurnTick(long TurnNumber)
    {
	  bool isDay = this.IsDay();
	  Zone currentZone = this.ParentObject.CurrentZone;
	  bool isOutside = (currentZone == null || currentZone.IsOutside());
      if (!this.ParentObject.OnWorldMap() && 2.in1000() && isDay && isOutside )
      {
        this.ParentObject.TemperatureChange(400 + Stat.Random(1, 300), this.ParentObject);
        if (this.ParentObject.IsAflame())
        {
          this.ParentObject.StopMoving();
          this.DidX("erupt", "into flames", "!", ColorAsBadFor: this.ParentObject);
        }
      }
      base.TurnTick(TurnNumber);
    }	

    public override bool Mutate(GameObject GO, int Level) => base.Mutate(GO, Level);

    public override bool Unmutate(GameObject GO) => base.Unmutate(GO);
  }
}