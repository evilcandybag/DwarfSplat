using System;
using UnityEngine;

public class DwarfManager : BehaviorManager<int,Dwarf>
{
	private int counter;
	
	public override void Start() {
		base.Start();
		counter = 0;
	}
	
	protected override void Update() {
		base.Update();
	}
	
	protected override int GetUniqueKey() {
		return counter++;
	}
	
	public override Dwarf Spawn(GameObject proto, int key, Vector3 pos, Quaternion rot) {
		Dwarf d = base.Spawn(proto,key,pos,rot);
		d.Manager = this;
		
		ActorController.getActorController().addDwarf(d);
		return d;
	}

}


