using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int woodAmount;
    public int bonesAmount;
    public int meatAmount;
    public int leavesAmount;

    //public int[] lootAmount; /* 0 - wood | 1 - bones | 2 - meat | 3 - leaves */

    public int exp;
    public int level;

    public List<int> unlockedWolfColors = new List<int>();
    public List<int> unlockedMagicColors = new List<int>();
    public List<int> unlockedHats = new List<int>();
}
