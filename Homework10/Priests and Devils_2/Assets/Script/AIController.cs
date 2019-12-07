using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myGame
{
    public class AIcontroller : MonoBehaviour
    {

        public int[] getNextAction(int curNumDevilLeft, int curNumPriestLeft, int curBoatPos)
        {
            //next[0]  从 岸 转移到 另一岸 的牧师数
            //next[1]  从 岸 转移到 另一岸 的恶魔数
            int[] next = new int[2];

            int P = curNumPriestLeft, D = curNumDevilLeft, B = curBoatPos;  //1:from 2：to
            if (P == 3 && D == 3 && B == 1)
            {
                next[0] = 1;
                next[1] = 1;
            }
            else if (P == 3 && D == 2 && B == -1)
            {
                next[0] = 0;
                next[1] = 1;
            }
            else if (P == 2 && D == 2 && B == -1)
            {
                next[0] = 1;
                next[1] = 0;
            }
            else if (P == 3 && D == 1 && B == -1)
            {
                next[0] = 0;
                next[1] = 1;
            }
            else if (P == 3 && D == 2 && B == 1)
            {
                next[0] = 0;
                next[1] = 2;
            }
            else if (P == 3 && D == 0 && B == -1)
            {
                next[0] = 0;
                next[1] = 1;
            }
            else if (P == 3 && D == 1 && B == 1)
            {
                next[0] = 2;
                next[1] = 0;
            }
            else if (P == 1 && D == 1 && B == -1)
            {
                next[0] = 1;
                next[1] = 1;
            }
            else if (P == 2 && D == 2 && B == 1)
            {
                next[0] = 2;
                next[1] = 0;
            }
            else if (P == 0 && D == 2 && B == -1)
            {
                next[0] = 0;
                next[1] = 1;
            }
            else if (P == 0 && D == 3 && B == 1)
            {
                next[0] = 0;
                next[1] = 2;
            }
            else if (P == 0 && D == 1 && B == -1)
            {
                next[0] = 0;
                next[1] = 1;
            }
            else if (P == 2 && D == 1 && B == 1)
            {
                next[0] = 2;
                next[1] = 0;
            }
            else if (P == 1 && D == 1 && B == 1)
            {
                next[0] = 1;
                next[1] = 1;
            }
            else if (P == 0 && D == 2 && B == 1)
            {
                next[0] = 0;
                next[1] = 2;
            }
            return next;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}