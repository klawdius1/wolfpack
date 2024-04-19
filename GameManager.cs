using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UIManager uIManager;
    public Player player;
    public LootZone lootZone;
    public Transform cam_transform;
    public QuestManager questManager;
    public SpawnLootZones spawnLootZones;
    public int animalIndex;
    public Vector3 lastPos;
    public EnemySpawnSystem spawn;
    public CinemachineFreeLook cm;
    public GameObject additionalQuests;
    public bool tutorial;
    public GameObject cursor;
    public Transform startPoint;

    public void Awake()
    {
        instance = this;
    }
}
