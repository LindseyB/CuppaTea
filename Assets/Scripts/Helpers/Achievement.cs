using UnityEngine;
using System.Collections;

namespace Helpers {
	public class Achievement {
		public string name;
		public string description;
		public bool achieved;

		public Achievement(string n, string d) {
			name = n;
			description = d;
			achieved = false;
		}

		public Achievement(string n, string d, bool a) {
			name = n;
			description = d;
			achieved = a;
		}
	}

	public class AchievementRecorder : MonoBehaviour {
		public static Achievement curdledMess = new Achievement("Curdled Mess", "Ew I'm not drinking that");

		// TODO: add reading and writing the achievements to and from user storage
	}
}
