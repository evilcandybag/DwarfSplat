using System;
using BehaviorTrees;
using BehaviorTrees.Unity;
/// <summary>
/// Behavior tree wrapper for handling emotes and sounds for dwarves. 
/// </summary>
public class DwarfEmotes
{
	private Node root;
	private static readonly double COOLDOWN = 10.0;
	
	public DwarfEmotes (Dwarf d)
	{
		root = new UnityCooldownDecorator(COOLDOWN);
	}
	
	public void Run() {
		root.Visit();
	}
}

