using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//每一种房间具体的房间方向（门的方向）哪些方向有门存在
[System.Serializable]
public class WallType
{
    public GameObject singleUp, singleDown, singleLeft, singleRight,
                      doubleUD, doubleUL, doubleUR, doubleDL, doubleDR, doubleLR,
                      tripleUDL, tripleUDR, tripleULR, tripleDLR,
                      fourUDLR;

}
