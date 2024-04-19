using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootZone : MonoBehaviour
{
    [SerializeField] private int lootAmount;
    public bool canLoot = false;
    [SerializeField] private Loot loot;
    [SerializeField] private Slider looting_slider;
    [SerializeField] private TextMeshProUGUI lootAmount_txt;
    [SerializeField] private Transform lookAtObject;
    [SerializeField] private bool fixedAmount;

    private int[] variantsLootAmount = { 10, 15, 20, 25, 30, 35, 40 };
    private int lootSecondQuest;
    private int lootAdditionalQuest1;

    private void Start()
    {
        if (!fixedAmount)
        {
            lootAmount = variantsLootAmount[Random.Range(0, variantsLootAmount.Length)];
        }
        
    }

    private void Update()
    {
        lookAtObject.LookAt(GameManager.instance.cam_transform.position);
        if (canLoot == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.player.input.IsActive("Dig"))
            {
                StartCorLooting();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.uIManager.digTip_txt.gameObject.SetActive(true);
            GameManager.instance.player.input.EnableInput("Dig", true);
            canLoot = true;
            GameManager.instance.uIManager.looting_button.gameObject.SetActive(true);
            GameManager.instance.uIManager.looting_button.onClick.AddListener(StartCorLooting);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.uIManager.digTip_txt.gameObject.SetActive(false);
            GameManager.instance.player.input.EnableInput("Dig", false);
            canLoot = false;
            GameManager.instance.uIManager.looting_button.gameObject.SetActive(false);
            GameManager.instance.uIManager.looting_button.onClick.RemoveAllListeners();
        }
    }

    IEnumerator Looting()
    {
        looting_slider.gameObject.SetActive(true);
        lootAmount_txt.text = "";
        int count = 0;
        

        for (int i = 0; i < 100; i++)
        {
            looting_slider.value += 1;
            if (i % 20 == 0)
            {
                count += 1;
                lootAmount_txt.text = "+ " + count.ToString();
            }
            yield return new WaitForSeconds(0.01f);
        }

        if (loot.lootID == 0)
        {
            Init.Instance.playerData.woodAmount += count;
        }
        else if (loot.lootID == 1)
        {
            Init.Instance.playerData.bonesAmount += count;

            if (GameManager.instance.questManager.activeQuests_bool[10])
            {
                GameManager.instance.questManager.kikosBonesAmount += 5;
                
                
            }
        }
        else if (loot.lootID == 2)
        {
            Init.Instance.playerData.meatAmount += count;

            if (GameManager.instance.questManager.activeQuests_bool[10])
            {
                GameManager.instance.questManager.kikosMeatAmount += 5;
                
            }
        }
        else if (loot.lootID == 3)
        {
            Init.Instance.playerData.leavesAmount += count;

            if (GameManager.instance.questManager.activeQuests_bool[1])
            {
                lootSecondQuest += count;
                if (lootSecondQuest >= 10)
                {
                    GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[3];
                    GameManager.instance.questManager.dialogWindow.SetActive(true);
                    GameManager.instance.questManager.FinishQuest(1, true);
                }
            }

            if (GameManager.instance.questManager.activeQuests_bool[3])
            {
                lootAdditionalQuest1 += count;
                GameManager.instance.questManager.progressForAdditional1_txt.text = lootAdditionalQuest1.ToString() + "/25";

                if (lootAdditionalQuest1 >= 25)
                {
                    GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[10];
                    GameManager.instance.questManager.dialogWindow.SetActive(true);
                    GameManager.instance.questManager.FinishQuest(3, false);
                    Init.Instance.playerData.exp += 10;
                    Init.Instance.playerData.meatAmount += 10;
                }
            }

            if (GameManager.instance.questManager.activeQuests_bool[10])
            {
                GameManager.instance.questManager.kikosLeavesAmount += 5;

            }
        }

        if (GameManager.instance.questManager.activeQuests_bool[10] && GameManager.instance.questManager.kikosLeavesAmount == 15 && GameManager.instance.questManager.kikosBonesAmount == 10 && GameManager.instance.questManager.kikosMeatAmount == 20)
        {
            GameManager.instance.questManager.FinishQuest(10, false);
            GameManager.instance.questManager.countHelpersQuests++;
            GameManager.instance.questManager.progressMasterFoxQuest_txt.text = GameManager.instance.questManager.countHelpersQuests.ToString() + "/3";
        }

        lootAmount -= 5;
        if (lootAmount <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.player.input.EnableInput("Dig", false);
            GameManager.instance.uIManager.digTip_txt.gameObject.SetActive(false);
        }
        looting_slider.gameObject.SetActive(false);
        looting_slider.value = 0;
        lootAmount_txt.text = "";

    }

    public void StartCorLooting()
    {
        StartCoroutine(Looting());
    }
}
