using UnityEngine;
using System.Collections;

public class CreationPeterTest : MonoBehaviour {
	
	public GameObject dwarf;
	public GameObject bed;
	public GameObject work;
	private DwarfManager manager;
	
	// Use this for initialization
	void Start () {
		manager = this.gameObject.GetComponent<DwarfManager>();
		
		manager.Spawn(new Vector3(0.75f,0.5f,0.75f));
		Bed sleeper = InstantiationUtils.GetNewInstance<Bed>(bed,new Vector3(-0.75f,0.26f,-1.75f));
		Workspace arbeit = InstantiationUtils.GetNewInstance<Workspace>(work,new Vector3(-0.75f,0.26f,0.75f));
		InteractableController.Instance.addBed(sleeper);
		InteractableController.Instance.addWorkspace(arbeit);
		
		var bds = InteractableController.Instance.getAllBeds();
		var wrk = InteractableController.Instance.getAllWorkspaces();
		manager.Run = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
