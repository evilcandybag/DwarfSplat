using UnityEngine;
using System.Collections.Generic;

public class GroundStuffController : MonoBehaviour {
	
	private static GroundStuffController ac;
		
	private List<MineActivated> allMines;
	private List<ItemOnGround> allItems;
	
	private GroundStuffController() {
		allMines = new List<MineActivated>();
		allItems = new List<ItemOnGround>();
	}
	
	public static GroundStuffController getGroundStuffController() {
		if (ac==null)
			ac = new GroundStuffController();
		return ac;
	}
	
	public void createMine(Vector3 position) {
	
	}
	
	public void createItemOnGround(Vector3 position, IItem item) {
	
	}
	
	public void destoyMine(MineActivated m) {
		allMines.Remove(m);
		Destroy(m);
	}
	
	public void destroyItem(ItemOnGround i) {
		allItems.Remove(i);
		Destroy(i);
	}
	
	public List<MineActivated> getAllMines() {
		return allMines;
	}
	public List<ItemOnGround> getAllItems() {
		return allItems;
	}
}
