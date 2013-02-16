using UnityEngine;
using System.Collections;
using BehaviorTrees;


public class ProximityMove : MonoBehaviour {
	
	public GameObject buddy;
	
	private Transform mate;
	private Vector3 direction;
	private float speed;
	private SequenceSelector root, redSeq;
	private PrioritySelector colors, movement;
	private Condition close;
	private Action red, green, toward, away;
	private PrioritySelector.PriorityNode towardPrio;
	
	
	private readonly float realClose = 0.5f, kindaClose = 2.0f, farAway = 5.0f;
	// Use this for initialization
	void Start () {
		mate = buddy.transform;
		speed = 1.1f;
		direction = mate.position - transform.position;
		
		root = new SequenceSelector(); redSeq = new SequenceSelector();
		colors = new PrioritySelector(); movement = new PrioritySelector();
		close = new Condition(() => Mathf.Abs(Vector3.Distance(transform.position, mate.position)) < kindaClose);
		red = new Action(Red); green = new Action(Green); 
		toward = new Action(Toward); away = new Action(Away);
		
		root.AddChild(colors, movement);
		
		colors.AddChild(redSeq,1.0); colors.AddChild(green);
		redSeq.AddChild(close,red); 
		
		movement.AddChild(away,farAway); 
		towardPrio = movement.AddChild(toward);
		toward.name = "toward";
	}
	
	void MyUpdate() {
		towardPrio.Prio = Mathf.Abs(Vector3.Distance(transform.position,mate.position));
		root.Visit();
	}
	
	// Update is called once per frame
	void Update () {
		towardPrio.Prio = Mathf.Abs(Vector3.Distance(transform.position,mate.position));
		root.Visit();
	}
	
	Node.Status Red() {
		renderer.material.color = Color.red;
		return Node.Status.SUCCESS;
	}
	Node.Status Green() {
		renderer.material.color = Color.green;
		return Node.Status.SUCCESS;
	}
	Node.Status Toward() {
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
		if (Mathf.Abs(Vector3.Distance(transform.position,mate.position)) > realClose) {
			return Node.Status.RUNNING;
		} else {
			return Node.Status.SUCCESS;
		}
	}
	Node.Status Away() {
		transform.Translate((-1*direction) * speed * Time.deltaTime, Space.World);
		return Node.Status.SUCCESS;
	}
}
