using UnityEngine;
using System.Collections;

public class CreationAllmighty : MonoBehaviour {
	
	public GameObject dwarf;
	public Transform bed;
	
	ICommand command;
	// Use this for initialization
	void Start () {
		
		ActorController ac = ActorController.getActorController();
		InteractableController ic = InteractableController.getInteractableController();
		
		GameObject dwarfy = Instantiate(dwarf,new Vector3(1f,0.5f,1f), Quaternion.identity) as GameObject;
		Instantiate(bed,new Vector3(-0.75f,0.26f,-0.75f), Quaternion.identity);
		
		Dwarf dwarfy2 = dwarfy.GetComponent<Dwarf>();
		
		command = new MoveCommand(dwarfy2, new Vector3(-0.75f,0.5f,-0.75f),2,(Result) => {Debug.Log("Result: "+Result);});
		command.execute();
		//ac.createDwarf(new Vector3(0.5f,0f,0.5f));
		//ac.createBall(new Vector3(-0.5f, 0f, -0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		//command.execute();
	}
}
