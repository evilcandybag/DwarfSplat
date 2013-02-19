using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviorTrees
{
	/// <summary>
	/// Selector for weighted randomized selections. The random distribution is immutable.
	/// </summary>
	public class RandomSelector : Selector
	{
		
		private HashSet<PriorityNode> children_; 
		private static readonly Random rand = new Random();
		private double prioTotal;
		
		public RandomSelector ()
		{
			children_ = new HashSet<PriorityNode>();
		}
		
		
		public override Status Visit() {
			if (State == Status.RUNNING) {
				return VisitRunning();
			} else {
				double prios = prioTotal;
				
				List<PriorityNode> nodes = (from c in children_ select c).ToList();
				while (nodes.Count() > 0) {
					PriorityNode pn = GetRandom(nodes,prios);
					switch (pn.Child.Visit()) {
					case Status.READY:
						throw new InvalidOperationException("A node should never return READY when visited!");
					case Status.RUNNING:
						State = Status.RUNNING;
						runningNode = pn.Child;
						return Status.RUNNING;
					case Status.SUCCESS:
						State = Status.READY;
						return Status.SUCCESS;
					case Status.FAIL:
						nodes.Remove(pn);
						prios -= pn.Prio;
						continue;
					}
				}
				State = Status.READY;
				return Status.FAIL;
			}
		}
		
		/// <summary>
		/// Helper to fetch a random weighted node.
		/// </summary>
		/// <returns>
		/// The random.
		/// </returns>
		/// <param name='nodes'>
		/// Nodes.
		/// </param>
		/// <param name='totWeight'>
		/// Tot weight.
		/// </param>
		private static PriorityNode GetRandom(IEnumerable<PriorityNode> nodes, double totWeight) {

            double randomNumber = rand.NextDouble()*totWeight;

            PriorityNode selectedNode = null;
            foreach (PriorityNode node in nodes)
            {
                if (randomNumber < node.Prio)
                {
                    selectedNode = node;
                    break;
                }

                randomNumber = randomNumber - node.Prio;
            }

            return selectedNode;
		}
		
		/// <summary>
		/// Add a new child to the Selector, creating a PriorityNode.
		/// </summary>
		/// <param name='child'>Child.</param>
		/// <param name="prio">The assigned priority</param>
		/// <returns>A reference to the PriorityNode.</returns>
		public void AddChild(Node child, double prio = 1.0) {
			if (prio <= 0)
				throw new ArgumentException("prio must be positive: " + prio);
			PriorityNode pn = new PriorityNode(child,prio);
			children_.Add(pn);
			prioTotal += pn.Prio;
		}
		
	}
}

