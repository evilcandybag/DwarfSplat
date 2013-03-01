using UnityEngine;
using System.Collections;

public class CreationAllmighty : MonoBehaviour {
	
	public GameObject dwarf;
	public GameObject bed;
	public GameObject workspace;
	
	ICommand command;
	// Use this for initialization
	void Start () {
		
		ActorController ac = ActorController.getActorController();
		InteractableController ic = InteractableController.getInteractableController();
		
		GameObject dwarfy = Instantiate(dwarf,new Vector3(1f,0.5f,1f), Quaternion.identity) as GameObject;
		GameObject bedy = Instantiate(bed,new Vector3(-0.75f,0.26f,-0.75f), Quaternion.identity) as GameObject;
		
		Dwarf dwarfy2 = dwarfy.GetComponent<Dwarf>();
		Bed bedy2 = bedy.GetComponent<Bed>();
		
		command = new MoveCommand(dwarfy2, new Vector3(-0.75f,0.5f,-0.75f),2,(Result) => {
														if (Result.Equals(Result.SUCCESS)) {
															ICommand command2 = new InteractCommand(dwarfy2, bedy2, (result) => {Debug.Log("Result bedy: ");});
															Debug.Log("Result: "+Result);
															command2.execute();
														}
													});
		command.execute();
		//ac.createDwarf(new Vector3(0.5f,0f,0.5f));
		//ac.createBall(new Vector3(-0.5f, 0f, -0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		//command.execute();
	}
}
