using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyDir;
using myGame;

public class FirstController : MonoBehaviour, SceneController, UserAction {

	private Vector3 river = new Vector3(0.5F,-1,0);
	UserGUI userGUI;
	public CoastController fromCoast;
	public CoastController toCoast;
	public BoatController boat;
	private CharacterController[] characters;
	public Move actionManager;
	Judge judge;
    public AIcontroller ai;
    void Awake() {
		Director director = Director.getInstace ();
		director.current = this;
		userGUI = gameObject.AddComponent <UserGUI>() as UserGUI;
		characters = new CharacterController[6];
		loadResources ();
		//#########################
		actionManager = gameObject.AddComponent<Move> ()as Move;
        ai = gameObject.AddComponent<AIcontroller>() as AIcontroller;
    }

	public void loadResources() {
		GameObject River = Instantiate (Resources.Load ("Prefab/River", typeof(GameObject)), river, Quaternion.identity, null) as GameObject;
		River.name = "River";
		fromCoast = new CoastController ("from");
		toCoast = new CoastController ("to");
		boat = new BoatController ();
		judge = new Judge (fromCoast, toCoast, boat);
		loadCharacter ();
	}

	private void loadCharacter() {
		for (int i = 3; i < 6; i++) {
			CharacterController cha = new CharacterController ("priest",i);
			cha.setName("priest" + i);
			cha.setPos (fromCoast.getEmptyPos ());
			cha.getOnCoast (fromCoast);
			fromCoast.getOnCoast (cha);
			characters [i] = cha;
		}

		for (int i = 0; i < 3; i++) {
			CharacterController cha = new CharacterController ("devil",i);
			cha.setName("devil" + i);
			cha.setPos (fromCoast.getEmptyPos());
			cha.getOnCoast (fromCoast);
			fromCoast.getOnCoast (cha);
			characters [i] = cha;
		}
	}


	public void moveBoat() {
		if (boat.is_empty ())
			return;
		actionManager.moveBoat (boat.getGameobj (), boat.Move_to (), boat.speed);
	}

	public void moveCharacter(CharacterController characterCtrl) {
		if (characterCtrl.Is_On_Boat ()) {
			CoastController whichCoast;
			if (boat.get_is_from () == -1) {
				whichCoast = toCoast;
			} else {
				whichCoast = fromCoast;
			}

			boat.GetOffBoat (characterCtrl.getName());

//######################################################
			Vector3 end_pos = whichCoast.getEmptyPos();
			Vector3 mid_pos = new Vector3 (characterCtrl.getGameobj ().transform.position.x,end_pos.y, end_pos.z);
			actionManager.moveCharacter(characterCtrl.getGameobj(),mid_pos,end_pos,characterCtrl.move_speed);
//######################################################
			characterCtrl.getOnCoast (whichCoast);
			whichCoast.getOnCoast (characterCtrl);

		} else {								
			CoastController whichCoast = characterCtrl.getCoastController ();

			if (boat.getEmptyIndex () == -1) {		
				return;
			}

			if (whichCoast.get_is_from () != boat.get_is_from ())	
				return;

			whichCoast.getOffCoast(characterCtrl.getName());
//######################################################
			Vector3 end_pos = boat.getEmptyPosition();
			Vector3 mid_pos = new Vector3 (end_pos.x,characterCtrl.getGameobj().transform.position.y,end_pos.z);
			actionManager.moveCharacter(characterCtrl.getGameobj(),mid_pos,end_pos,characterCtrl.move_speed);
//######################################################
			characterCtrl.getOnBoat (boat);
			boat.GetOnBoat (characterCtrl);
		}
	}

    public void NextActionAI()
    {
        //保证游戏状态是游戏中
        int flag = judge.check();
        if (flag == 1 || flag ==2)
            return;

        //得到当前状态
        //计算所有在左边的牧师/恶魔
        int numDevilLeft = judge.from_devil, numPriestLeft = judge.from_priest;
        Debug.Log("numDevilLeft " + numDevilLeft);
        Debug.Log("numPriestLeft " + numPriestLeft);
        int boatPos = judge.Boat.get_is_from();


        //获取下一个状态
        Debug.Log("State:"+numDevilLeft+numPriestLeft+judge.Boat.get_is_from());
        int[] next = ai.getNextAction(numDevilLeft, numPriestLeft, judge.Boat.get_is_from());
        Debug.Log("next[0] " + next[0]);
        Debug.Log("next[1] " + next[1]);
        //其实next就表示要放在船上的人

        //转换到下一个状态
        /*
         numDveilToBoat = 0, numPriestToBoat = 0 要变得和next一样
         先boat -> 岸
         第一个是P
            next[0] > 0 留着 numPriestToBoat++
            next[0] = 0 放回去
         第一个是D 
            next[1] 同上 numDveilToBoat++
         第二个是P
            next[0] = 2 且 numPriestToBoat = 1 留着
            next[0] = 1 放回去
         第二个是D
            同上
         再 岸 -> boat
            看一下 num和next没满足的，放岸上的去船上即可
         */
        int numPriestToBoat = 0, numDevilToBoat = 0;
        int[] personOnBoat = judge.Boat.getPersonOnBoat();
        Debug.Log("******************Boat0 "+personOnBoat[0]+"*******************boat1"+personOnBoat[1]);
        if (personOnBoat[0] >= 3 && personOnBoat[0] <= 6)   //第一个是P
        {
            if (next[0] > 0)           //P保持不动
                numPriestToBoat++;
            else if (next[0] == 0)      //把这个P放去岸上
            {
                Debug.Log("**********************************getoff P"+personOnBoat[0]);
               moveCharacter(characters[personOnBoat[0]]);
            }
        }
        else if (personOnBoat[0] >= 0 && personOnBoat[0] <= 2)  //第一个是D
        {
            if (next[1] > 0)
                numDevilToBoat++;
            else if (next[1] == 0)
            {
                Debug.Log("**********************************getoff D"+personOnBoat[0]);
                moveCharacter(characters[personOnBoat[0]]);
            }
        }

        if (personOnBoat[1] >= 3 && personOnBoat[1] <= 6)        //第二个是P
        {
            if (next[0] == 2 || (next[0] == 1 && numPriestToBoat == 0))    //P不用动
                numPriestToBoat++;
            else if (next[0] == 0 || (next[0] == 1 && numPriestToBoat == 1))   //把这个P放去岸上
            {
                Debug.Log("**********************************getoff P"+personOnBoat[1]);
                moveCharacter(characters[personOnBoat[1]]);
            }
        }
        else if (personOnBoat[1] >= 0 && personOnBoat[1] <= 2)     //第二个是D
        {
            if (next[1] == 2 || (next[1] == 1 && numDevilToBoat == 0))
                numDevilToBoat++;
            else if (next[1] == 0 || (next[1] == 1 && numDevilToBoat == 1))
            {
                Debug.Log("**********************************getoff D"+personOnBoat[1]);
                moveCharacter(characters[personOnBoat[1]]);
            }
        }

        /*
         岸 -> 船
         */
        for (int i = 0; i < next[0] - numPriestToBoat; i++)            // P
        {
            int index = -1;
            if (boatPos == 1)
                index = fromCoast.getAPriestIndex();
            else if (boatPos == -1)
                index = toCoast.getAPriestIndex();
            Debug.Log("**********************************geton P"+index);
            moveCharacter(characters[index]);
        }
        for (int i = 0; i < next[1] - numDevilToBoat; i++)            // D
        {

            int index = -1;
            if (boatPos == 1)
                index = fromCoast.getADevilIndex();
            else if (boatPos == -1)
                index = toCoast.getADevilIndex();
            Debug.Log("**********************************geton D"+index);
            moveCharacter(characters[index]);
        }


        //移动船
        Invoke("moveBoat", (float)0.6);
        Debug.Log("------------------------------------------------------");
    }

    public void restart() {
		boat.reset ();
		fromCoast.reset ();
		toCoast.reset ();
		for (int i = 0; i < characters.Length; i++) {
			characters [i].reset ();
		}
	}
	void Update () {
		userGUI.status = judge.check ();
	}
}