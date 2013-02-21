using System;


public class DwarfManager : BehaviorManager<int,Dorf>
{
	private int counter;
	
	protected override void Start() {
		base.Start();
		counter = 0;
	}
	
	protected override int GetUniqueKey() {
		return counter++;
	}
}


