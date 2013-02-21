using System;
using BehaviorTrees;
using UnityEngine;

/// <summary>
/// Behavior utilities for the DwarfSplat game.
/// </summary>
public abstract class BehaviorUtils
{
	public static readonly int WALKSPEED = 1;
	
	public static Node CreateBehaviorTree(Dorf d) {
		PrioritySelector root = new PrioritySelector();
		
		root.AddChild(CreateFleeBehavior(d),Double.MaxValue);
		
		root.AddChild(CreateInteractionBehavior(d, d.Workplace),50.0);
		
		root.AddChild(CreateInteractionBehavior(d, d.Bed));
		
		
		return root;
	}
	
	private static Node CreateFleeBehavior(Dorf d) {	
		ConditionDecorator ifballsee = new ConditionDecorator(d.CanSeeBall);
		
		
		//TODO: this shit aint done yet!
		
		
		return ifballsee;
	}
	
	private static Node CreateInteractionBehavior(Dorf d, IInteractable i) {
		
		Condition findWork = new Condition(() => {
			//TODO: what check here? Maybe an action to get a work-place?
			return false;
		});
		
		BehaviorTrees.Action goToWork = new BehaviorTrees.Action(() => {
			//TODO: replace vector param with location of workplace!
			var mc = new MoveCommand(d,new Vector3(),WALKSPEED);
			if (mc.isAllowed()) {
				mc.execute();
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		});
		
		BehaviorTrees.Action work = new BehaviorTrees.Action(() => {
			//TODO: replace null value with some kind of interactable variable from d.
			var ic = new InteractCommand(d,null);
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

