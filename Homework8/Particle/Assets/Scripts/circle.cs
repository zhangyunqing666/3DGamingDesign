using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circle : MonoBehaviour {

	public ParticleSystem particleSystem;
	public Camera camera;

	private ParticleSystem.Particle[] particles;
	private float[] Angle;  // 粒子角度
	private float[] R;  // 粒子半径
	private int speed = 5; // 粒子旋转速度水平
	private float rotate_speed = 0.1f;  // 粒子旋转速度
	public int Num = 50000; // 粒子数目
	public float minR = 5.0f;  // 光环最小半径
	public float maxR = 10.0f; // 光环最大半径

	private Ray ray;
	private RaycastHit hit;

	// 收缩前粒子位置
	private float[] before;   
	// 收缩后粒子位置
	private float[] after;   
	private float shrinkSpeed = 2f;

	private bool changed = false;


	// Use this for initialization
	void Start () {
		Angle = new float[Num];
		R = new float[Num];
		before = new float[Num];
		after = new float[Num];
		particles = new ParticleSystem.Particle[Num];

		particleSystem.maxParticles = Num;
		particleSystem.Emit(Num);
		particleSystem.GetParticles(particles);

		Ndistribution nd = new Ndistribution();

		// 每个粒子在初始化的时候都设定好收缩前和收缩后的粒子半径
		for (int i = 0; i < Num; i++)
		{
			float r = (float)nd.getNormalDistribution((minR+maxR)*0.5f, 1);
			float angle = UnityEngine.Random.Range(0.0f, 360.0f);
			Angle[i] = angle;
			R[i] = r;

			before[i] = r;
			after[i] = 0.7f * r;

			if (after[i] < minR * 1.1f)
			{
				float midRadius = minR * 1.05f;
				after[i] = UnityEngine.Random.Range(UnityEngine.Random.Range(minR, midRadius), (minR * 1.1f));
			}
		}
	}

	// Update is called once per frame
	void Update () {
		for(int i = 0; i < Num; i++)
		{
			if (changed)
			{
				// 开始收缩
				if(R[i] > after[i])
				{
					R[i] -= shrinkSpeed * (R[i] / after[i]) * Time.deltaTime;
				}
			}
			else
			{
				if (R[i] < before[i])
				{
					R[i] += shrinkSpeed * (before[i] / R[i]) * Time.deltaTime;
				}
				else if (R[i] > before[i])
				{
					R[i] = before[i];
				}
			}
				
			if (i % 2 == 0)  
			{  
				// 逆时针
				Angle[i] += (i % speed + 1) * rotate_speed;  
			}  
			else  
			{  
				// 顺时针
				Angle[i] -= (i % speed + 1) * rotate_speed;  
			}  

			Angle[i] = Angle[i] % 360;
			float rad = Angle[i] / 180 * Mathf.PI;  
			particles[i].position = new Vector3(R[i] * Mathf.Cos(rad), R[i] * Mathf.Sin(rad), 0f);  
		}  

		particleSystem.SetParticles(particles, Num);  

		ray = camera.ScreenPointToRay(Input.mousePosition);  
		if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "button") changed = true;  
		else changed = false;  
	}
}

class Ndistribution{
	System.Random rand = new System.Random();

	public double getNormalDistribution(double mean, double stdDev)
	{
		double u1 = 1.0 - rand.NextDouble();
		double u2 = 1.0 - rand.NextDouble();
		double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
			Math.Sin(2.0 * Math.PI * u2); 
		double randNormal = mean + stdDev * randStdNormal; 
		return randNormal;
	}
}