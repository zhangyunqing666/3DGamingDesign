using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour {

	private int[,] chessBoard = new int[3, 3];
	//判断是O走或者是X走
	int my_turn = 1;
	//初始化函数
	void Init()
	{
		my_turn = 1;
		//把棋面上的每个位置都置0
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				chessBoard [i,j] = 0;
			}
		}

	}
	//
	int is_win(){

		for (int i = 0; i < 3; i++) {
			if (chessBoard[i,0] == chessBoard[i,1] && chessBoard[i,0] == chessBoard[i,2] && chessBoard[i,0] != 0) {
				return chessBoard[i,0]; 
			}
		}
		for (int j = 0; j < 3; j++) {
			if (chessBoard[0,j] == chessBoard[1,j] && chessBoard[0,j] == chessBoard[2,j] && chessBoard[0,j] != 0) {
				return chessBoard[0,j]; 
			}
		}
		if(chessBoard[0,0] == chessBoard[1,1] && chessBoard[0,0] == chessBoard[2,2] && chessBoard[0,0] != 0) 
			return chessBoard[0,0];
		if(chessBoard[0,2] == chessBoard[1,1] && chessBoard[0,2] == chessBoard[2,0] && chessBoard[0,2] != 0) 
			return chessBoard[0,2];
		
		int total_step = 0;
		for(int i = 0 ; i < 3 ; i++) {
			for (int j = 0; j < 3; j++) {
				if (chessBoard[i,j] != 0) {
					total_step++;
				}
			}
		}
		if (total_step == 9) {
			return 3;
		}
		return -1;
	}
	// Use this for initialization
	void Start () {
		Init ();
	}
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		GUI.contentColor = Color.yellow;
		if(GUI.Button(new Rect(250,30,200,40), "Reset"))
		{
			Init ();
		}
		int win = is_win ();
		if(win==1)
			GUI.Label (new Rect (330, 75, 60, 50), "O Win!");
		else if(win==2)
			GUI.Label (new Rect (330, 75, 60, 50), "X Win!");
		else if(win==3)
			GUI.Label (new Rect (330, 75, 60, 50), "Draw!");

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (GUI.Button (new Rect (275 + 50 * i, 100 + 50 * j, 50, 50), "")) {
					if(chessBoard[i,j]==0)
					{
						if (win == -1) {
							if (my_turn == 1) {
								chessBoard [i, j] = 1;
								my_turn = 0;
							} else if (my_turn == 0) {
								chessBoard [i, j] = 2;
								my_turn = 1;
							}
						}
					}
				}
			}
		}
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (chessBoard [i, j] == 1) {
					GUI.Button (new Rect (275 + 50 * i, 100 + 50 * j, 50, 50), "O");
				} else if (chessBoard [i, j] == 2) {
					GUI.Button (new Rect (275 + 50 * i, 100 + 50 * j, 50, 50), "X");
				}

			}
		}

	}
}
