using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour {
	private CoastController From;
	private CoastController To;
	public BoatController Boat;
    public int from_priest = 0;
    public int from_devil = 0;
    public int to_priest = 0;
    public int to_devil = 0;
    public Judge(CoastController from_coast,CoastController to_coast,BoatController boat)
	{
		this.From = from_coast;
		this.Boat = boat;
		this.To = to_coast;
	}
	public int check()
	{
        from_priest = 0;
        from_devil = 0;
        to_priest = 0;
        to_devil = 0;
        int[] fromCount = From.get_character_num ();
		from_priest += fromCount[0];
		from_devil += fromCount[1];

		int[] toCount = To.get_character_num ();
		to_priest += toCount[0];
		to_devil += toCount[1];

		if (to_priest + to_devil == 6)		
			return 2;

		int[] boatCount = Boat.getCharacterNum ();
		if (Boat.get_is_from () == -1) {	
			to_priest += boatCount[1];
			to_devil += boatCount[0];
		} else {	
			from_priest += boatCount[1];
			from_devil += boatCount[0];
		}
		if (from_priest < from_devil && from_priest > 0) {	
			Debug.Log("condition1");
			Debug.Log(from_priest+":p d:"+from_devil);
			return 1;
			
		}
		if (to_priest < to_devil && to_priest > 0) {
			Debug.Log("condition2");
			return 1;
			
		}
		return 0;	
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
