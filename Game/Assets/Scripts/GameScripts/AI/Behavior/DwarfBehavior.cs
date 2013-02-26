using System;
using BehaviorTrees;
using UnityEngine;

/// <summary>
/// Behavior utilities for the DwarfSplat game.
/// </summary>
public class DwarfBehavior
{
	public static readonly int WALKSPEED = 2, RUNSPEED = 3;
	
	private PrioritySelector root;
	private PriorityNode flee_,work_,sleep_;
	
	public PriorityNode Flee { get { return flee_;} }
	public PriorityNode Work { get { return work_;} }
	public PriorityNode Sleep { get { return sleep_;} }
	
	public DwarfBehavior(Dwarf d) {
		root = new PrioritySelector();
		
		flee_ = root.AddChild(CreateFleeBehavior(d),Double.MaxValue);
		
		work_ = root.AddChild(CreateInteractionBehavior(d, d.Workplace, Dwarf.Status.WORK,d.WorkCallback),50.0);
		
		sleep_ = root.AddChild(CreateInteractionBehavior(d, d.Bed, Dwarf.Status.SLEEP, d.SleepCallback));
		
	}
	
	public void Run() {
		root.Visit();
	}
	
	private static Node CreateFleeBehavior(Dwarf d) {	
		Condition ifballsee = new Condition(() => false);
		
		SequenceSelector seq = new SequenceSelector();
		
		ConditionDecorator ballclose = new ConditionDecorator(d.IsBallClose);
		
		BehaviorTrees.Action run = new BehaviorTrees.Action(() => {
			//TODO: flight location?
			var mc = new MoveCommand(d, new Vector3(), RUNSPEED,d.FleeCallback);
			if (mc.isAllowed()) {
				mc.execute();
				d.state = Dwarf.Status.FLEE; //TODO set this shit somewhere else, plox
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		});
		
		
		return ifballsee;
	}
	
	private static Node CreateInteractionBehavior(Dwarf d, IInteractable i, Dwarf.Status s,Action<Result> callback) {
		
		BehaviorTrees.Action goToWork = new BehaviorTrees.Action(() => {
			var mc = new MoveCommand(d,i.getPosition(),WALKSPEED,d.MovementCallback);
			if (mc.isAllowed()) {
				mc.execute();
				d.state = Dwarf.Status.IDLE; //TODO set this shit somewhere else, plox
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		});
		
		BehaviorTrees.Action work = new BehaviorTrees.Action(() => {
			var ic = new InteractCommand(d,i,callback);
			if (ic.isAllowed()) {
				d.state = s;
				ic.execute();
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		});
		
		
		SequenceSelector root = new SequenceSelector(goToWork,work);
		return root;
	}
	
}

