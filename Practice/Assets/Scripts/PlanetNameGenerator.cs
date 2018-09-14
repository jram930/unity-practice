using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class PlanetNameGenerator {

	private List<string> AvailableNames;

	private PlanetNameGenerator() {
		AvailableNames = new List<string>();
		InitializeNameList();
	}

	public string GetRandomPlanetName() {
		if(AvailableNames.Count == 0) {
			return "NoNamesAvailable";
		} else {
			int index = GameState.Instance.GetRandomInt(0, AvailableNames.Count);
			string name = AvailableNames[index];
			AvailableNames.RemoveAt(index);
			return name;
		}
	}

	private void InitializeNameList() {
		// Need to come up with a better way to come up with planet names
		for(int i=0; i<500000; i++) {
			AvailableNames.Add(GenerateRandomString());
		}
	}

	private string GenerateRandomString() {
		GameState gameState = GameState.Instance;
		string result = "";
		string[] chars = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
		int stringLength = gameState.GetRandomInt(5, 10);
		for(int i=0; i<stringLength; i++) {
			int index = gameState.GetRandomInt(0, 26);
			result += chars[index];
		}
		return result;
	}

	private static PlanetNameGenerator instance = null;
	public static PlanetNameGenerator Instance {
		get {
			if (instance == null) {
				instance = new PlanetNameGenerator();
			}
			return instance;
		}
	}
}