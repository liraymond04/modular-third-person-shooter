using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Two identical threads posted on the same date by different users, not sure what's with that
// https://unitylist.com/p/nb1/Modular-Third-Person-Camera
// https://unitylist.com/p/vwh/Modular-Third-Person-Camera

namespace CameraModules {
	public class CameraMain : MonoBehaviour {

		public GameObject player;
		public Transform container;
		public Camera _camera;

		[HideInInspector]
		public Player _player;

		void Start() {
			_player = player.GetComponent<Player>();
		}

		void Update() {
			
		}
	}
}
