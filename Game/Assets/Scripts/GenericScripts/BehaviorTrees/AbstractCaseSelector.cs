using System;
using System.Collections.Generic;

namespace BehaviorTrees
{
	public abstract class AbstractCaseSelector<TKey> : Selector
	{
		
		private Dictionary<TKey,Node> children;
		
		public AbstractCaseSelector ()
		{
			children = new Dictionary<TKey, Node>();
		}
		
		public void AddChild(TKey key, Node child){
			children.Add(key,child);
		}
		
		public override Node.Status Visit() {
			if (State == Status.RUNNING) {
				return runningNode.Visit();
			}
			Node n;
			bool exists;
			exists = children.TryGetValue(FetchKey(),out n);
			if (exists) {
				Status s = n.Visit();
				switch (s) {
				case Status.RUNNING:
					runningNode = n;
					State = Status.RUNNING;
					break;
				case Status.READY: 
					throw new InvalidOperationException("A node should never return READY");
				default:
					State = Status.READY;
					break;
				}
				return s;
			} else {
				State = Status.READY;
				return Status.FAIL;
			}
		}
		
		/// <summary>
		/// Fetches the key for choosing a node from some kind of state.
		/// </summary>
		/// <returns>
		/// The key.
		/// </returns>
		protected abstract TKey FetchKey();
	}
}

