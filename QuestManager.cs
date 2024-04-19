using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questPanel;
    [SerializeField] private Button questPanel_button;
    private bool isOpenQuestPanel;

    [SerializeField] private GameObject[] quests;
    [SerializeField] private GameObject[] questsOnScene;
    public bool[] activeQuests_bool;

    public TextMeshProUGUI progressForAdditional1_txt;
    public TextMeshProUGUI progressMasterFoxQuest_txt;
    public int timer;

    public string[] dialogs;
    public GameObject dialogWindow;
    public TextMeshProUGUI dialog_txt;
    public Button skipDialog_button;
    
    [SerializeField] private GameObject shafransQuestObjects;
    [SerializeField] private GameObject questWolf;

    [SerializeField] private GameObject grassZone;
    [SerializeField] private GameObject lakeTrigger;
    public GameObject rabbitsZoneEffect;

    [HideInInspector] public int countHelpersQuests;
    [HideInInspector] public int kikosLeavesAmount;
    [HideInInspector] public int kikosMeatAmount;
    [HideInInspector] public int kikosBonesAmount;

    private void Start()
    {
        questPanel_button.onClick.AddListener(OpenQuestPanel);
        skipDialog_button.onClick.AddListener(() => dialogWindow.SetActive(false));
    }

    private void Update()
    {
        if (activeQuests_bool[5])
        {
            if (GameManager.instance.player.countEnemyDeath >= 5 && timer > 0)
            {
                
                FinishQuest(5, false);
                dialog_txt.text = dialogs[14];
                dialogWindow.SetActive(true);
            }

            if (timer <= 0 && GameManager.instance.player.countEnemyDeath < 5)
            {
                FinishQuest(5, false);
                dialog_txt.text = dialogs[15];
                dialogWindow.SetActive(true);
                AddQuest(5);
            }
            
        }
    }

    public void AddQuest(int id)
    {
        quests[id].SetActive(true);
        activeQuests_bool[id] = true;
        switch (id)
        {
            case 0:
                dialog_txt.text = dialogs[0];
                dialogWindow.SetActive(true);
                rabbitsZoneEffect.SetActive(true);
                GameManager.instance.tutorial = false;
                break;
            case 1:
                dialog_txt.text = dialogs[2];
                dialogWindow.SetActive(true);
                grassZone.SetActive(true);
                break;
            case 2:
                dialog_txt.text = dialogs[4];
                dialogWindow.SetActive(true);
                lakeTrigger.SetActive(true);
                break;
            case 3:
                dialog_txt.text = dialogs[9];
                dialogWindow.SetActive(true);
                break;
            case 4:
                dialog_txt.text = dialogs[11];
                questWolf.SetActive(true);
                dialogWindow.SetActive(true);
                break;
            case 5:
                dialog_txt.text = dialogs[13];
                dialogWindow.SetActive(true);
                GameManager.instance.player.countEnemyDeath = 0;
                StartCoroutine(Timer());
                break;
            case 6:
                GameManager.instance.uIManager.chat_button.onClick.AddListener(() => dialog_txt.text = dialogs[8]);
                GameManager.instance.uIManager.chat_button.onClick.AddListener(() => dialogWindow.SetActive(true));
                GameManager.instance.uIManager.chat_button.onClick.AddListener(() => FinishQuest(6, false));
                GameManager.instance.uIManager.chat_button.onClick.AddListener(() => AddQuest(7));
                GameManager.instance.uIManager.chat_button.onClick.AddListener(GameManager.instance.uIManager.chat_button.onClick.RemoveAllListeners);
                // открытие панели чата GameManager.instance
                break;

            case 9:
                dialog_txt.text = dialogs[18];
                dialogWindow.SetActive(true);
                break;

            case 10:
                dialog_txt.text = dialogs[19];
                dialogWindow.SetActive(true);
                break;

            case 11:
                dialog_txt.text = dialogs[21];
                shafransQuestObjects.SetActive(true);
                dialogWindow.SetActive(true);
                break;

            case 12:
                dialog_txt.text = dialogs[22];
                dialogWindow.SetActive(true);
                break;

            default:
                break;
        }
    }

    public void FinishQuest(int id, bool main)
    {
        quests[id].SetActive(false);
        activeQuests_bool[id] = false;
        if (main)
        {
            if (id < questsOnScene.Length - 1)
            {
                questsOnScene[id + 1].SetActive(true);
            }
        }
        
    }

    private void OpenQuestPanel()
    {
        if (isOpenQuestPanel)
        {
            isOpenQuestPanel = false;
        }
        else
        {            
            isOpenQuestPanel = true;
        }

        questPanel.SetActive(isOpenQuestPanel);
    }

    IEnumerator Timer()
    {
        
        GameManager.instance.uIManager.timer_txt.gameObject.SetActive(true);

        for (int i = 30; i > -1; i--)
        {
            timer = i;
            GameManager.instance.uIManager.timer_txt.text = timer.ToString();
            yield return new WaitForSeconds(1);
        }

        GameManager.instance.uIManager.timer_txt.gameObject.SetActive(false);
    }

}
