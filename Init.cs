using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour
{
    public PlayerData playerData;
    public static Init Instance;

    public int wolfColorIndex;
    public int magicColorIndex;
    public float headSize = 0.85f;
    public float forwardLegsSize = 1f;
    public float rearLegsSize = 1f;
    public int hatVariantIndex;
    public GameObject[] questsOnScene;
    public bool[] activeQuests_bool;

    protected void Awake()
    {
        //��������
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}