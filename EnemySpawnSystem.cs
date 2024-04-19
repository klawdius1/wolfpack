using MalbersAnimations;
using System.Collections;

using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(4, 15));

        GameObject g = Instantiate(enemies[GameManager.instance.animalIndex], GameManager.instance.lastPos, Quaternion.identity);
        g.SetActive(true);
    }

    public void StartCor_Spawn()
    {
        StartCoroutine(Spawn());
    }
}
