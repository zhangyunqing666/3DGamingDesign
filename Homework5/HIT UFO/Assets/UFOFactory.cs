using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : MonoBehaviour {
	public List<GameObject> used_ufos = new List<GameObject>();
	public List<GameObject> free_ufos = new List<GameObject>();
	public void Genufo()
	{
		GameObject ufo;
		if(free_ufos.Count == 0)
		{
			ufo = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UFO"), Vector3.zero, Quaternion.identity);
		}
		else
		{
			ufo = free_ufos[0];
			free_ufos.RemoveAt(0);
		}
		float x = Random.Range(-10.0f, 10.0f);
		ufo.transform.position = new Vector3(x, 0, 0);
		ufo.transform.Rotate(new Vector3(x < 0? -x*9 : x*9, 0, 0));

		float random = Random.Range (0f, 9f);
		Color red = new Color (1f, 0f, 0f);
		Color green = new Color (0f, 1f, 0f);
		Color blue = new Color (0f, 0f, 1f);
		Color color = new Color(0f,0f,0f);
		if (random >= 0 && random <= 3)
			color = red;
		else if (random > 3 && random <= 6)
			color = green;
		else if (random > 6 && random <= 9)
			color = blue;

		ufo.transform.GetComponent<Renderer>().material.color = color;
		used_ufos.Add(ufo);
	}
	public void Recycleufo(GameObject usedUFO)
	{
		usedUFO.transform.position = new Vector3 (0, 0, -10);
		free_ufos.Add(usedUFO);
	}
}