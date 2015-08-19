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
		// Used for writing out achievments
		public static List<Achievement> achievements = new List<Achievement>(){
			new Achievement(0, "Curdled Mess", "Ew! I'm not drinking that!"),
		};

		public static Achievement curdledMess = achievements[0];

		public static void readAchievements() {
			// Read the achievements
			if(File.Exists(Application.persistentDataPath + "/CuppaTea.ct")) {
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/CuppaTea.ct", FileMode.Open);
				achievements = (List<Achievement>)bf.Deserialize(file);
				file.Close();
			}

			foreach (Achievement a in achievements){
				a.SetReference();
				if(!a.achieved){ a.Hide(); }
			}
		}

		public static void writeAchievements() {
			// Write the achivements
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + "/CuppaTea.ct");	
			bf.Serialize(file, achievements);
			file.Close();
		}
	}
}
