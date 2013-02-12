using System;

namespace BehaviorTrees
{
	/// <summary>
	/// Abstract class for leaves. Not sure if this will be useful, but might  
	/// be nice for refactoring later. Also, type safety! 
	/// Leaves are the only nodes without children, so they can afford to party
	/// and have all the Action.
	/// </summary>
	public abstract class Leaf : Node
	{
		
	}
}

