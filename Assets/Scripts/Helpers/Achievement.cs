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
		public static List<Achievement> achievements = new List<Achievement>(){
			new Achievement(0, "Curdled Mess", "Ew! I'm not drinking that!"),
			new Achievement(1, "First Cuppa", "Well it seems like you've figured this out"),
			new Achievement(2, "(╯°□°）╯︵ ┻━┻", "FFFFFFFFFFFuuuuuuuuu"),
			new Achievement(3, "Where we are going", "we don't need roads"),
			new Achievement(4, "Hot Like Venus", "Well, 467°C"),
			new Achievement(5, "Tea Party Foul!", "You gotta clean that up"),
			new Achievement(6, "┬─┬﻿ ノ( ゜-゜ノ)", "I'll just put this back"),
			new Achievement(7, "Mad Tea Party", "I'm fine I don't know about you"),
			new Achievement(8, "Oolong Long Time", "It's been awhile"),
			new Achievement(9, "Cup of What", "That wasn't tea"),
			new Achievement(10, "Tea With Your Sugar?", "Sucrose overdose"),
			new Achievement(11, "Smartly Tartly", "I've made a huge mistake"),
			new Achievement(12, "uniTEA", "All the teas in harmony"),
			new Achievement(13, "ETeaOverflow", "at 0x00746561"),
			new Achievement(14, "Bearable", "Glitch Bear's fav"),
			new Achievement(15, "Brew Tea Full", "It's so beautiful")
		};

		public static Achievement curdledMess      = achievements[0];
		public static Achievement firstCuppa       = achievements[1];
		public static Achievement rageFlip         = achievements[2];
		public static Achievement rewind           = achievements[3];
		public static Achievement hotLikeVenus     = achievements[4];
		public static Achievement teaPartyFoul     = achievements[5];
		public static Achievement unRage           = achievements[6];
		public static Achievement madTeaParty      = achievements[7];
		public static Achievement oolongLongTime   = achievements[8];
		public static Achievement cupOfWhat        = achievements[9];
		public static Achievement teaWithYourSugar = achievements[10];
		public static Achievement smartlyTartly    = achievements[11];
		public static Achievement uniTea           = achievements[12];
		public static Achievement eTeaOverflow     = achievements[13];
		public static Achievement bearable         = achievements[14];
		public static Achievement brewTeaFull      = achievements[15];

		public static void readAchievements() {
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
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create (Application.persistentDataPath + "/CuppaTea.ct");	
			bf.Serialize(file, achievements);
			file.Close();
		}
	}
}
