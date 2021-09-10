using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData 
{
	public int coinCount;
	public int score;
	public bool[] keyfound;
	public LevelData[] levelDatas;
	public bool isFirst;
}
