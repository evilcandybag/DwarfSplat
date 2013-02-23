using System;


public class DwarfManager : BehaviorManager<int,Dwarf>
{
	private int counter;
	
	protected override void Start() {
		base.Start();
		counter = 0;
	}
	
	protected override void Update() {
		base.Update();
	}
	
	protected override int GetUniqueKey() {
		return counter++;
	}
}


