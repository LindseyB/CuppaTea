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
			new Achievement(1, "First Cuppa", "Some clever text I can't think of"),
			new Achievement(2, "(╯°□°）╯︵ ┻━┻", "FFFFFFFFFFFuuuuuuuuu"),
			new Achievement(3, "Where we are going", "we don't need roads"),
			new Achievement(4, "Hot Like Venus", "Well, 467°C"),
			new Achievement(5, "Tea Party Foul!", "You gotta clean that up"),
			new Achievement(6, "┬─┬﻿ ノ( ゜-゜ノ)", "I'll just put this back"),
			new Achievement(7, "Mad Tea Party", "I'm fine I don't know about you")
		};

		public static Achievement curdledMess = achievements[0];
		public static Achievement firstCuppa = achievements[1];
		public static Achievement rageFlip = achievements[2];
		public static Achievement rewind = achievements[3];
		public static Achievement hotLikeVenus = achievements[4];
		public static Achievement teaPartyFoul = achievements[5];
		public static Achievement unRage = achievements[6];
		public static Achievement madTeaParty = achievements[7];

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
