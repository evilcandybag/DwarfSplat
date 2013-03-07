using UnityEngine;
using System.Collections;

public class CreationPeterTest : MonoBehaviour {
	
	public GameObject dwarf;
	public GameObject bed;
	public GameObject work;
	public GameObject ball;
	public int numberOfDwarfs = 3;
	public int numberOfBeds = 4;
	public int randomOffset = 18;
	public float spawnPositionOffset = -10f;
	private DwarfManager manager;
	
	// Use this for initialization
	void Start () {
		manager = this.gameObject.GetComponent<DwarfManager>();
		
		for (int i = 0; i< numberOfBeds; i++) {
			float x1 = -0.1f+Random.Range(1,randomOffset), x2 = 0.3f+Random.Range(1,randomOffset), y1 = 0f+Random.Range(1,randomOffset), y2 = 0.3f+Random.Range(1,randomOffset);
			Bed sleeper = InstantiationUtils.GetNewInstance<Bed>(bed,new Vector3(spawnPositionOffset+x1,0.26f,spawnPositionOffset+y1));
			Workspace arbeit = InstantiationUtils.GetNewInstance<Workspace>(work,new Vector3(spawnPositionOffset+x2,0.26f,spawnPositionOffset+y2));
			InteractableController.Instance.addBed(sleeper);
			InteractableController.Instance.addWorkspace(arbeit);
		}
		
		Ball roller = InstantiationUtils.GetNewInstance<Ball>(ball);
		ActorController.getActorController().addBall(roller);
		
		for (int i = 0; i < numberOfDwarfs; i++) {
			SpawnRandomDwarf();
		}
		
		var bds = InteractableController.Instance.getAllBeds();
		var wrk = InteractableController.Instance.getAllWorkspaces();
		manager.Run = true;
		InvokeRepeating("SpawnRandomDwarf",10f,10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SpawnRandomDwarf() {
		float x1 = Random.value*randomOffset, x2 = Random.value*randomOffset;
		manager.Spawn(new Vector3(spawnPositionOffset+x1,0.5f,spawnPositionOffset+x2));
	}
}
