using System;
using BehaviorTrees;
using UnityEngine;

/// <summary>
/// Behavior utilities for the DwarfSplat game.
/// </summary>
public class DwarfBehavior
{
	public static readonly int WALKSPEED = 2;
	
	private PrioritySelector root;
	private PriorityNode flee_,work_,sleep_;
	
	public PriorityNode Flee { get { return flee_;} }
	public PriorityNode Work { get { return work_;} }
	public PriorityNode Sleep { get { return sleep_;} }
	
	public DwarfBehavior(Dwarf d) {
		root = new PrioritySelector();
		
		flee_ = root.AddChild(CreateFleeBehavior(d),Double.MaxValue);
		
		work_ = root.AddChild(CreateInteractionBehavior(d, d.Workplace, Dwarf.Status.WORK),50.0);
		
		sleep_ = root.AddChild(CreateInteractionBehavior(d, d.Bed, Dwarf.Status.SLEEP));
		
	}
	
	public void Run() {
		root.Visit();
	}
	
	private static Node CreateFleeBehavior(Dwarf d) {	
		ConditionDecorator ifballsee = new ConditionDecorator(() => false);
		
		
		//TODO: this shit aint done yet!
		
		
		return ifballsee;
	}
	
	private static Node CreateInteractionBehavior(Dwarf d, IInteractable i, Dwarf.Status s) {
		
		Condition findWork = new Condition(() => {
			//TODO: what check here? Maybe an action to get a work-place?
			return false;
		});
		
		BehaviorTrees.Action goToWork = new BehaviorTrees.Action(() => {
			//TODO: replace vector param with location of workplace!
			var mc = new MoveCommand(d,new Vector3(),WALKSPEED,d.MovementCallback);
			if (mc.isAllowed()) {
				mc.execute();
				d.state = Dwarf.Status.IDLE;
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		});
		
		BehaviorTrees.Action work = new BehaviorTrees.Action(() => {
			//TODO: replace null value with some kind of interactable variable from d.
			var ic = new InteractCommand(d,i,d.WorkCallback);
			d.state = s;
			if (ic.isAllowed()) {
				ic.execute();
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		});
		
		
		SequenceSelector root = new SequenceSelector(findWork,goToWork,work);
		return root;
	}
	
}

