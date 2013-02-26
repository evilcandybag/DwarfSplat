using System;
using System.Collections.Generic;

namespace BehaviorTrees
{
	public class CaseSelector<TKey> : Selector
	{
		
		private Dictionary<TKey,Node> children;
		private Func<TKey> getKey;
		
		public CaseSelector (Func<TKey> getKey)
		{
			children = new Dictionary<TKey, Node>();
			this.getKey = getKey;
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
			exists = children.TryGetValue(getKey(),out n);
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
	}
}

