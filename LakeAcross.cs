using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeAcross : MonoBehaviour
{
    [SerializeField] private GameObject questBoar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.questManager.activeQuests_bool[2])
        {
            questBoar.SetActive(true);
            StartCoroutine(LookAtBoar());
            GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[5];
            GameManager.instance.questManager.dialogWindow.SetActive(true);
            GameManager.instance.questManager.FinishQuest(2, true);
            
        }
    }

    IEnumerator LookAtBoar()
    {
        Transform prevLookAt = GameManager.instance.cm.LookAt;
        GameManager.instance.cm.LookAt = questBoar.transform;
        yield return new WaitForSeconds(2.5f);
        GameManager.instance.cm.LookAt = prevLookAt;
        Destroy(gameObject);
    }
}
