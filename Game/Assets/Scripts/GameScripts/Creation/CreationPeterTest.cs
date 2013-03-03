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
		
		for (int i = 0; i< 4; i++) {
			float x1 = Random.value*20f, x2 = Random.value*20f, y1 = Random.value*20f, y2 = Random.value*20f;
			Bed sleeper = InstantiationUtils.GetNewInstance<Bed>(bed,new Vector3(-10f+x1,0.26f,-10f+y1));
			Workspace arbeit = InstantiationUtils.GetNewInstance<Workspace>(work,new Vector3(-10f+x2,0.26f,-10f+y2));
			InteractableController.Instance.addBed(sleeper);
			InteractableController.Instance.addWorkspace(arbeit);
		}
		
		for (int i = 0; i < 3; i++) {
		float x1 = Random.value*20f, x2 = Random.value*20f;
			manager.Spawn(new Vector3(-10f+x1,0.5f,-10f+x2));
		}
		
		var bds = InteractableController.Instance.getAllBeds();
		var wrk = InteractableController.Instance.getAllWorkspaces();
		manager.Run = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
