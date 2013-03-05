using UnityEngine;
using System.Collections.Generic;

public class GroundStuffController {
	
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
	
	public void addMine(MineActivated mine) {
		allMines.Add(mine);
	}
	
	public void addItemOnGround(ItemOnGround item) {
		allItems.Add(item);
	}
	
	public void destoyMine(MineActivated m) {
		allMines.Remove(m);
		if (m != null)
			Object.Destroy(m.transform.gameObject);
	}
	
	public void destroyItem(ItemOnGround i) {
		allItems.Remove(i);
		if (i != null)
			Object.Destroy(i.transform.gameObject);
	}
	
	public List<MineActivated> getAllMines() {
		return allMines;
	}
	public List<ItemOnGround> getAllItems() {
		return allItems;
	}
}
