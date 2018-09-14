using UnityEngine;
using UnityEditor;

public class PlayerState {

	public int Credits;
	public int Research;
	public int Materials;

	private static PlayerState instance = null;
	public static PlayerState Instance {
		get {
			if(instance == null) {
				instance = new PlayerState();
			}
			return instance;
		}
	}
	private PlayerState() {}
}