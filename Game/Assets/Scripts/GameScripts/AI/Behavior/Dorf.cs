using System;

/// <summary>
/// Silly Dwarf stub (lol).
/// </summary>
public class Dorf : AbstractAIActor
{
	
	public IInteractable Bed, Workplace;
	
	public Dorf ()
	{
	}
	
	public override void RunAI(){
	}
	
	public bool CanSeeBall() {
		return false;
	}
}


