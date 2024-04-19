using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLocation : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent.transform.position = spawnPoint.position;
            Debug.Log("работает");
        }
        
        
    }
}
