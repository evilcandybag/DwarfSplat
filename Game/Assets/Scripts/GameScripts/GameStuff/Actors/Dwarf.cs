using UnityEngine;
using System.Collections;
using BehaviorTrees;

public class Dwarf : AbstractAIActor {
	
	//private IInteractable bed_, work_;
	private DwarfBehavior behavior;
	private DwarfEmotes emotes;
	public Status state;
	
	public Result moveResult,sleepResult,workResult;
	
	public static readonly double SLEEP_RATE = 1, WORK_RATE = 0.5, IDLE_RATE = 0.1, FLEE_RATE = 0.8,
		DISTANCE_FAR = 10, DISTANCE_CLOSE = 5;
	
	private DwarfManager manager_;
	public DwarfManager Manager {
		get { return manager_; }
		set { manager_ = value; }
	}
	
	public enum Status {
		SLEEP,
		WORK,
		FLEE,
		IDLE //including moving between the other tasks
	}
		
	// Use this for initialization
	void Start () {
		behavior = new DwarfBehavior(this);
		emotes = new DwarfEmotes(this);
		moveResult = Result.SUCCESS; sleepResult = Result.SUCCESS; 
		workResult = Result.SUCCESS;

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
		emotes.Run();
	}
	
	public void addInteractable() {
		//check if bed or workspace, then add that
	}
	
	//TODO: dunno if this is the right way to go about it or if we should use commands for that shit
	//public IInteractable Bed { get { return bed_;} }
	//public IInteractable Workplace { get { return work_;} }
	
	public void MovementCallback(Result res) {
		this.moveResult = res;
	}
	public void SleepCallback(Result res) {
		this.sleepResult = res;
	}
	public void WorkCallback(Result res) {
		this.workResult = res;
	}
	public void FleeCallback(Result res) {
		this.workResult = res;
	}
	
	public bool IsBallClose(){
		foreach (Ball b in ActorController.getActorController().getBallActors()) {
			float dist = Vector3.Distance(transform.position,b.transform.position);
			if (dist < ((state == Status.FLEE) ? DISTANCE_FAR : DISTANCE_CLOSE))
				return true;
		}
		return false;
	}
	
	public bool CanSeeBall() {
		LayerMask obstacles = LayerMask.NameToLayer("Obstacles");
		foreach (Ball b in ActorController.getActorController().getBallActors()) {
			if ( Physics.Linecast(transform.position, b.transform.position, 1 << obstacles))
				return false;
		}
		return true;
	}
}
