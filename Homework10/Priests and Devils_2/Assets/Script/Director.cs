using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyDir{
	public class Director:System.Object {
		private static Director instance;
		public SceneController current{ get ; set; }
		public static Director getInstace(){
			if (instance == null) {
				instance = new Director ();
			}
			return instance;
		}
		// Use this for initialization
		void Start () {
			
		}
			
		// Update is called once per frame
		void Update () {
				
		}
	}
	public interface UserAction {
		void moveBoat();
		void moveCharacter(CharacterController characterCtrl);
		void restart();
        void NextActionAI();

    }
	public interface SceneController{
		void loadResources();
	}
}