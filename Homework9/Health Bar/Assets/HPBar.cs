using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class HPBar : MonoBehaviour
{
	public Slider mainSlider;
	public float HP;
	public float resulthealth;
	private void Start()
	{
		//mainSlider.value = mainSlider.maxValue;
		resulthealth = HP;
		mainSlider = GetComponent<Slider>();
		HP = mainSlider.maxValue;
	}

	void OnGUI()
	{
		if( GUI.Button(new Rect(300,250,80, 40), "加血")) {
			HP += 10;
		}
		if (GUI.Button(new Rect(300, 200, 80, 40), "减血")) {
			HP -= 10;
		}
		resulthealth = Mathf.Lerp(resulthealth, HP, 0.05f);
		if (resulthealth > 100) resulthealth = 100;
		else if (resulthealth < 0) resulthealth = 0;

		mainSlider.value = resulthealth;
	}
}
