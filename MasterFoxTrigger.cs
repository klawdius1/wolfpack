using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterFoxTrigger : MonoBehaviour
{
    [SerializeField] private GameObject helpersQuests;
    private bool firstMeet = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.questManager.activeQuests_bool[8])
            {
                GameManager.instance.spawnLootZones.StartCor_Spawn2();
                GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[17];
                GameManager.instance.questManager.dialogWindow.SetActive(true);
                GameManager.instance.questManager.FinishQuest(8, false);
                firstMeet = false;
            }

            else if (!firstMeet)
            {
                GameManager.instance.questManager.AddQuest(9);
                helpersQuests.SetActive(true);
                firstMeet = true;
            }
            
        }
    }
}
