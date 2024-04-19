using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLootZones : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] lootZones;
    [SerializeField] private Transform parent;
    private List<GameObject> existZones = new List<GameObject>();
    private bool[] emptySpawnPoints;

    [Header("Second Location")]
    [SerializeField] private Transform[] spawnPoints2;
    private List<GameObject> existZones2 = new List<GameObject>();
    private bool[] emptySpawnPoints2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (existZones.Count > 0)
            {
                foreach (var item in existZones)
                {
                    Destroy(item);
                }
                existZones.Clear();
            }



            emptySpawnPoints = new bool[spawnPoints.Length];
            for (int i = 0; i < 6; i++)
            {
                int r = Random.Range(0, spawnPoints.Length);
                while (emptySpawnPoints[r] != false)
                {
                    r = Random.Range(0, spawnPoints.Length);
                }

                GameObject q = Instantiate(lootZones[Random.Range(0, lootZones.Length)], spawnPoints[r].position, Quaternion.identity, parent);
                existZones.Add(q);
                emptySpawnPoints[r] = true;
            }

            yield return new WaitForSeconds(90);
        }
        
    }


    IEnumerator Spawn2()
    {
        while (true)
        {
            if (existZones2.Count > 0)
            {
                foreach (var item in existZones2)
                {
                    Destroy(item);
                }
                existZones2.Clear();
            }



            emptySpawnPoints2 = new bool[spawnPoints2.Length];
            for (int i = 0; i < 6; i++)
            {
                int r = Random.Range(0, spawnPoints2.Length);
                while (emptySpawnPoints2[r] != false)
                {
                    r = Random.Range(0, spawnPoints2.Length);
                }

                GameObject q = Instantiate(lootZones[Random.Range(0, lootZones.Length)], spawnPoints2[r].position, Quaternion.identity, parent);
                existZones2.Add(q);
                emptySpawnPoints2[r] = true;
            }

            yield return new WaitForSeconds(90);
        }

    }

    public void StartCor_Spawn2()
    {
        StartCoroutine(Spawn2());
    }
}
