using UnityEngine;

public class IMGUI : MonoBehaviour
{
	public float HP = 0.0f;
	private float resultHP;

	private Rect HPBar;
	private Rect HPUp;
	private Rect HPDown;

	void Start()
	{
		HPBar = new Rect(50, 50, 200, 20);
		HPUp = new Rect(105, 80, 40, 20);
		HPDown = new Rect(155, 80, 40, 20);
		resultHP = HP;
	}

	void OnGUI()
	{
		if (GUI.Button(HPUp, "加血"))
		{
			resultHP = resultHP + 0.1f > 1.0f ? 1.0f : resultHP + 0.1f;
		}
		if (GUI.Button(HPDown, "减血"))
		{
			resultHP = resultHP - 0.1f < 0.0f ? 0.0f : resultHP - 0.1f;
		}

		//插值计算HP值，以实现血条值平滑变化
		HP = Mathf.Lerp(HP, resultHP, 0.05f);

		// 用水平滚动条的宽度作为血条的显示值
		GUI.HorizontalScrollbar(HPBar, 0.0f, HP, 0.0f, 1.0f);
	}
}