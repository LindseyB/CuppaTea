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
		public int points;
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
			points = 0;
		}

		public void SetReference() {
			noteObject = GameObject.Find(objectName);
		}

		public void Achieved() {
			achieved = true;
			noteObject.GetComponent<Renderer>().material.SetFloat("_isGrayscale", 0f);
			points = (int)(Random.Range(10000f, 100000f));
		}

		public void Unachieved() {
			achieved = false;
			noteObject.GetComponent<Renderer>().material.SetFloat("_isGrayscale", 1f);
			points = 0;
		}

		public void Hide() {
			if(noteObject){ noteObject.GetComponent<Renderer>().material.SetFloat("_isGrayscale", 1f); }
		}
	}

	public class AchievementRecorder : MonoBehaviour {
		public static List<Achievement> achievements = new List<Achievement>(){
			new Achievement(0, "Curdled Mess", "Ew! I'm not drinking that!"),
			new Achievement(1, "First Cuppa", "You got this!"),
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

		public static int curdledMess      = 0;
		public static int firstCuppa       = 1;
		public static int rageFlip         = 2;
		public static int rewind           = 3;
		public static int hotLikeVenus     = 4;
		public static int teaPartyFoul     = 5;
		public static int unRage           = 6;
		public static int madTeaParty      = 7;
		public static int oolongLongTime   = 8;
		public static int cupOfWhat        = 9;
		public static int teaWithYourSugar = 10;
		public static int smartlyTartly    = 11;
		public static int uniTea           = 12;
		public static int eTeaOverflow     = 13;
		public static int bearable         = 14;
		public static int brewTeaFull      = 15;

		public static Achievement metaAchievement = new Achievement(-1, "Meta Achievement", "Achieve all the things!");

		public static Achievement getAchievement(int achievement){
			return achievements[achievement];
		}

		public static int readAchievements() {
			int count = 0;
			if(File.Exists(Application.persistentDataPath + "/CuppaTea.ct")) {
				BinaryFormatter bf = new BinaryFormatter();

				FileStream file = File.Open(Application.persistentDataPath + "/CuppaTea.ct", FileMode.Open);
				achievements = (List<Achievement>)bf.Deserialize(file);
				file.Close();
			}

			foreach (Achievement a in achievements) {
				a.SetReference();
				if(!a.achieved) { 
					a.Hide(); 
				} else {
					count++;
				}
			}

			return count;
		}

		public static void initSpeedRunAchievements() {
			foreach (Achievement a in achievements) {
				a.Unachieved();
			}

		}

		public static int totalPoints() {
			int total = 0;
			foreach (Achievement a in achievements) {
				if(a.achieved) { total += a.points; }
			}

			return total;
		}
		
		public static void writeAchievements() {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/CuppaTea.ct");	
			bf.Serialize(file, achievements);
			file.Close();
		}

		public static void resetAchievements() {
			File.Delete(Application.persistentDataPath + "/CuppaTea.ct");
			foreach (Achievement a in achievements) {
				a.Unachieved();
			}
		}
	}
}
