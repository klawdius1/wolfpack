using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MalbersAnimations;
using TMPro;
using System;
using MalbersAnimations.Controller;
using MalbersAnimations.Controller.AI;
public enum AnimalType
{
    Rabbit,
    Raccoon,
    Fox,
    Boar,
    Wolf,
    Cougar,
    Bear,
    QuestWolf,
    QuestBoar,
    QuestFox,
    QuestRaccoon,
    Deer
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int hp_multiplier;
    [SerializeField] private int damage_multiplier;
    [SerializeField] private TextMeshProUGUI hp_txt;
    [SerializeField] private TextMeshProUGUI level_txt;
    [SerializeField] private Slider hp_slider;
    [SerializeField] private Stats hp;
    [SerializeField] private MAttackTrigger frontAttack; 
    [SerializeField] private MAttackTrigger mouthAttack; 
    [SerializeField] private MAttackTrigger frontRightAttack;
    [SerializeField] private MAttackTrigger frontLeftAttack;
    [SerializeField] private bool isAgressive;
    [SerializeField] private AnimalType type;
    [SerializeField] private int index;
    [SerializeField] private MAnimalAIControl ai;
    private bool isAngry;
    [SerializeField] private MAnimal enemy;

    private void Start()
    {
        if (isAgressive)
        {
            frontAttack.statModifier.MinValue += level * damage_multiplier;
            frontAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;

            if (mouthAttack != null)
            {
                mouthAttack.statModifier.MinValue = frontAttack.statModifier.MinValue;
                mouthAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;
            }

            if (frontRightAttack != null)
            {
                frontRightAttack.statModifier.MinValue = frontAttack.statModifier.MinValue;
                frontRightAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;
            }

            if (frontLeftAttack != null)
            {
                frontLeftAttack.statModifier.MinValue = frontAttack.statModifier.MinValue;
                frontLeftAttack.statModifier.MaxValue = frontAttack.statModifier.MinValue;
            }
            
        }
        

        hp.Stat_Get(1).maxValue = hp.Stat_Get(1).maxValue + (level * hp_multiplier);
        hp.Stat_Get(1).value = hp.Stat_Get(1).maxValue;
        hp_slider.maxValue = hp.Stat_Get(1).maxValue;
        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        level_txt.text = level.ToString();
    }

    private void Update()
    {
        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        hp_slider.gameObject.transform.LookAt(GameManager.instance.cam_transform.position);

        /*if (type == AnimalType.Rabbit && GameManager.instance.questManager.activeQuests_bool[0])
        {
            arrowSign.SetActive(true);
        }
        else if (type == AnimalType.Rabbit && !GameManager.instance.questManager.activeQuests_bool[0])
        {
            arrowSign.SetActive(false);
        }*/
    }

    public IEnumerator OnDeath()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    public void StartCor_OnDeath()
    {
        if (type != AnimalType.QuestWolf && type != AnimalType.QuestBoar)
        {
            GameManager.instance.lastPos = transform.position;
            GameManager.instance.animalIndex = index;
        }

        StartCoroutine(OnDeath());

        if (type != AnimalType.QuestWolf && type != AnimalType.QuestBoar)
        {
            GameManager.instance.spawn.StartCor_Spawn();
        }
            

        Init.Instance.playerData.exp += level * 13;

        if (GameManager.instance.questManager.activeQuests_bool[0] && type == AnimalType.Rabbit)
        {
            GameManager.instance.questManager.FinishQuest(0, true);
            GameManager.instance.questManager.rabbitsZoneEffect.SetActive(false);
            GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[1];
            GameManager.instance.questManager.dialogWindow.SetActive(true);
        }

        if (GameManager.instance.questManager.activeQuests_bool[6])
        {
            GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[7];
            GameManager.instance.questManager.dialogWindow.SetActive(true);
            
        }

        if (GameManager.instance.questManager.activeQuests_bool[4] && type == AnimalType.QuestWolf)
        {
            GameManager.instance.questManager.FinishQuest(4, false);
        }

        if (GameManager.instance.questManager.activeQuests_bool[5] && GameManager.instance.questManager.timer > 0)
        {
            GameManager.instance.player.countEnemyDeath++;
        }

        if (GameManager.instance.questManager.activeQuests_bool[11] && (type == AnimalType.QuestBoar || type == AnimalType.QuestFox || type == AnimalType.QuestRaccoon))
        {
            GameManager.instance.player.countShafransQuest++;

            if (GameManager.instance.player.countShafransQuest == 3)
            {
                GameManager.instance.questManager.FinishQuest(11, false);
            }
        }

        if (GameManager.instance.questManager.activeQuests_bool[12] && type == AnimalType.Bear)
        {
            GameManager.instance.player.countBears++;

            if (GameManager.instance.player.countBears == 2 && GameManager.instance.player.countCougars == 3 && GameManager.instance.player.countRaccoons == 4)
            {
                GameManager.instance.questManager.FinishQuest(12, false);
            }
        }
        else if (GameManager.instance.questManager.activeQuests_bool[12] && type == AnimalType.Cougar)
        {
            GameManager.instance.player.countCougars++;

            if (GameManager.instance.player.countBears == 2 && GameManager.instance.player.countCougars == 3 && GameManager.instance.player.countRaccoons == 4)
            {
                GameManager.instance.questManager.FinishQuest(12, false);
            }
        }
        else if (GameManager.instance.questManager.activeQuests_bool[12] && type == AnimalType.Raccoon)
        {
            GameManager.instance.player.countRaccoons++;

            if (GameManager.instance.player.countBears == 2 && GameManager.instance.player.countCougars == 3 && GameManager.instance.player.countRaccoons == 4)
            {
                GameManager.instance.questManager.FinishQuest(12, false);
            }
        }

    }

    //смерть кабана
    public void QuestDeath()
    {

        GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[6];
        GameManager.instance.questManager.dialogWindow.SetActive(true);
        GameManager.instance.questManager.AddQuest(6);
        GameManager.instance.uIManager.chat_button.gameObject.SetActive(true);
        GameManager.instance.additionalQuests.SetActive(true);
        StartCor_OnDeath();
        
        
    }

    public void QuestDeathWolf()
    {

        GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[12];
        GameManager.instance.questManager.dialogWindow.SetActive(true);
        Init.Instance.playerData.exp += 10;
        StartCor_OnDeath();
    }
    
    public void SetTargetPlayer()
    {
        ai.SetTarget(GameManager.instance.player.gameObject.transform);
        isAngry = true;
    }

    IEnumerator Attack()
    {
        while (isAngry)
        {
            enemy.Mode_Activate(1);
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 3));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isAngry)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StopCoroutine(Attack());
        }
    }
}
