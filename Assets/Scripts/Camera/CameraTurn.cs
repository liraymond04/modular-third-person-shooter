using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModules {
	[RequireComponent (typeof(CameraModules.CameraMain))]
	public class CameraTurn : MonoBehaviour {
		private CameraModules.CameraMain main;
		public float responsiveness;

		void Start() {
			main = GetComponent<CameraModules.CameraMain>();
		}

		void Update() {
			if (transform.rotation != main.player.transform.rotation) {
				Quaternion final = Quaternion.FromToRotation(Vector3.forward, transform.forward);
				main.player.transform.rotation = Quaternion.Lerp(main.player.transform.rotation, final, responsiveness * Time.deltaTime);;
			}
		}
	}
}
