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
		
		work_ = root.AddChild(CreateInteractionBehavior(d, Dwarf.Status.WORK,d.WorkCallback),50.0);
		
		sleep_ = root.AddChild(CreateInteractionBehavior(d, Dwarf.Status.SLEEP, d.SleepCallback));
		
	}
	
	public void Run() {
		root.Visit();
		Debug.Log("Work: " + Work.Prio + " Sleep: " + Sleep.Prio);
	}
	
	private static Node CreateFleeBehavior(Dwarf d) {	
		Condition ifballsee = new Condition(d.CanSeeBall);
		
		SequenceSelector seq = new SequenceSelector();
		
		ConditionDecorator ballclose = new ConditionDecorator(d.IsBallClose);
		
		BehaviorTrees.Action run = new BehaviorTrees.Action();
		run.Task = () => {
			//TODO: flight location?
			if(run.State == Node.Status.RUNNING)
				return Node.Status.RUNNING;
			
			var mc = new MoveCommand(d, new Vector3(), RUNSPEED,(res) => {
				run.Free();
				d.FleeCallback(res);
			});
			if (mc.isAllowed()) {
				mc.execute();
				d.State = Dwarf.Status.FLEE; //TODO set this shit somewhere else, plox
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		};
		
		ballclose.SetChild(seq);
		seq.AddChild(ifballsee, run);
		
		return ballclose;
	}
	
	private static Node CreateInteractionBehavior(Dwarf d, Dwarf.Status s, Action<Result> callback) {
		
		IInteractable i = null;
		InteractableController.InteractableType type;
		switch (s) {
		case Dwarf.Status.WORK:
			type = InteractableController.InteractableType.WORKSPACE;
			break;
		case Dwarf.Status.SLEEP:
			type = InteractableController.InteractableType.BED;
			break;
		default:
			throw new NotImplementedException("No case implemented for status: " + s);
		}
		
		FindInteractableCommand c = new FindInteractableCommand(
				type, (interactable) => { i = interactable; });
		c.execute();
		
		BehaviorTrees.Action goToWork = new BehaviorTrees.Action();
		goToWork.Task = () => {
			Debug.Log("GOTOWORK: " + s);
			var mc = new MoveCommand(d,i.getPosition(),WALKSPEED,(res) => {
				goToWork.Free();
				d.MovementCallback(res);
			});
			if (mc.isAllowed()) {
				mc.execute();
				d.moveResult = Result.RUNNING;
				d.State = Dwarf.Status.IDLE; //TODO set this shit somewhere else, plox
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		};
		
		BehaviorTrees.Action work = new BehaviorTrees.Action();
		work.Task = () => {
			
			Debug.Log("WORK: " + s);
			var ic = new InteractCommand(d,i,(res) => {
				work.Free();
				callback(res);
			});
			if (ic.isAllowed()) {
				d.State = s;
				ic.execute();
				return Node.Status.RUNNING;
			} else {
				return Node.Status.FAIL;
			}
		};
		
		
		SequenceSelector root = new SequenceSelector(goToWork,work);
		return root;
	}
	
}

