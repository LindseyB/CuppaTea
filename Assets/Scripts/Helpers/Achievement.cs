using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

namespace Helpers {
	[System.Serializable]
	public class Achievement {
		public string name;
		public string description;
		public bool achieved;
		public int index;
		private string objectName;

		[System.NonSerialized]
		private GameObject noteObject;

		public Achievement(int i, string n, string d) {
			index = i;
			name = n;
			description = d;
			achieved = false;
			objectName = "achievement" + ((index+1).ToString());
			noteObject = GameObject.Find(objectName);
		}

		public void SetReference() {
			noteObject = GameObject.Find(objectName);
		}

		public void Achieved() {
			achieved = true;
			noteObject.SetActive(true);
		}

		public void Hide() {
			noteObject.SetActive(false);
		}
	}

	public class AchievementRecorder : MonoBehaviour {
		public static Achievement curdledMess = new Achievement(0, "Curdled Mess", "Ew! I'm not drinking that!");

		// Used for writing out achievments
		public static List<Achievement> achievements = new List<Achievement>();

		public static void readAchievements() {
			// Read the achievements
			if(File.Exists(Application.persistentDataPath + "/CuppaTea.ct")) {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/CuppaTea.ct", FileMode.Open);
				achievements = (List<Achievement>)bf.Deserialize(file);
				file.Close();

				// I wish C# had metaprogramming
				// As it is this I hate it and it will get ugly
				curdledMess = achievements[curdledMess.index];
			}

			// Since this will need to happen with all achievements we probably want to keep it in the list
			// But we use them outside of the list in the code to easily name them
			// So maybe we need an enum for the outside things to reference the correct achievement
			curdledMess.SetReference();
			if(!curdledMess.achieved){
				curdledMess.Hide(); 
			}
		}

		public static void writeAchievements() {
			// Write the achivements
			achievements.Add(curdledMess);

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + "/CuppaTea.ct");	
			bf.Serialize(file, achievements);
			file.Close();
		}
	}
}
