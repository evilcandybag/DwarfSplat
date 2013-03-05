using UnityEngine;
using System.Collections;

public class CreationPeterTest : MonoBehaviour {
	
	public GameObject dwarf;
	public GameObject bed;
	public GameObject work;
	public GameObject ball;
	public int numberOfDwarfs = 3;
	public int numberOfBeds = 4;
	public float randomOffset = 20f;
	public float spawnPositionOffset = -10f;
	private DwarfManager manager;
	
	// Use this for initialization
	void Start () {
		manager = this.gameObject.GetComponent<DwarfManager>();
		
		for (int i = 0; i< numberOfBeds; i++) {
			float x1 = Random.value*randomOffset, x2 = Random.value*randomOffset, y1 = Random.value*randomOffset, y2 = Random.value*randomOffset;
			Bed sleeper = InstantiationUtils.GetNewInstance<Bed>(bed,new Vector3(spawnPositionOffset+x1,0.26f,spawnPositionOffset+y1));
			Workspace arbeit = InstantiationUtils.GetNewInstance<Workspace>(work,new Vector3(spawnPositionOffset+x2,0.26f,spawnPositionOffset+y2));
			InteractableController.Instance.addBed(sleeper);
			InteractableController.Instance.addWorkspace(arbeit);
		}
		
		Ball roller = InstantiationUtils.GetNewInstance<Ball>(ball);
		ActorController.getActorController().addBall(roller);
		
		for (int i = 0; i < numberOfDwarfs; i++) {
		float x1 = Random.value*randomOffset, x2 = Random.value*randomOffset;
			manager.Spawn(new Vector3(spawnPositionOffset+x1,0.5f,spawnPositionOffset+x2));
		}
		
		var bds = InteractableController.Instance.getAllBeds();
		var wrk = InteractableController.Instance.getAllWorkspaces();
		manager.Run = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
