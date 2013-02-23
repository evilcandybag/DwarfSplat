using UnityEngine;
using System.Collections;
using BehaviorTrees;

public class Dwarf : AbstractAIActor {
	
	private IInteractable bed_, work_;
	private DwarfBehavior behavior;
	public Status state;
	
	public static readonly double SLEEP_RATE = 1, WORK_RATE = 0.5, IDLE_RATE = 0.1, FLEE_RATE = 0.8;
	
	public enum Status {
		SLEEP,
		WORK,
		FLEE,
		IDLE //including moving between the other tasks
	}
		
	// Use this for initialization
	void Start () {
		behavior = new DwarfBehavior(this);
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case Status.SLEEP:
			behavior.Sleep.Prio -= Time.deltaTime * SLEEP_RATE;
			break;
		case Status.WORK:
			behavior.Sleep.Prio += Time.deltaTime * WORK_RATE;
			break;
		case Status.FLEE:
			behavior.Sleep.Prio += Time.deltaTime * FLEE_RATE;
			break;
		default:
			behavior.Sleep.Prio += Time.deltaTime * IDLE_RATE;
			break;
		}
	}
	
	public override void RunAI(){
		behavior.Run();
	}
	
	public void RunEmotes() {
	}
	
	//TODO: dunno if this is the right way to go about it or if we should use commands for that shit
	public IInteractable Bed { get { return bed_;} }
	public IInteractable Workplace { get { return work_;} }
	
}
