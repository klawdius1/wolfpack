using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MalbersAnimations;
using UnityEngine.UI;
using TMPro;
using System;


public class Player : MonoBehaviour
{
    private float minCorners;
    private Transform closestAnimal;
    public List<GameObject> animals;
    [SerializeField] private GameObject fieldView;
    [SerializeField] private Transform camTarget;
    //[SerializeField] private CinemachineFreeLook cinemachine;
    public MalbersInput input;
    [SerializeField] private Stats hp;
    [SerializeField] private int maxExp;

    [SerializeField] private SkinnedMeshRenderer wolf;
    [SerializeField] private SkinnedMeshRenderer magic;
    [SerializeField] private Transform head;
    [SerializeField] private Transform forwardLegR;
    [SerializeField] private Transform forwardLegL;
    [SerializeField] private Transform rearLegR;
    [SerializeField] private Transform rearLegL;
    [SerializeField] private GameObject[] hats;

    [SerializeField] private Material[] wolfMaterials;
    [SerializeField] private Material[] magicMaterials;

    [SerializeField] private Slider hp_slider;
    [SerializeField] private TextMeshProUGUI hp_txt;
    [SerializeField] private GameObject reactionsPanel;
    [SerializeField] private Slider exp_slider;
    [SerializeField] private TextMeshProUGUI level_txt;

    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController mainAnimController;
    [SerializeField] private RuntimeAnimatorController reactionsAnimController;
    [SerializeField] private Avatar mainAvatar;
    [SerializeField] private Avatar reactionAvatar;
    [SerializeField] private Animation[] animations;
    [SerializeField] private Button reaction1_button;

    [SerializeField] private GameObject teleport;

    [HideInInspector]
    public int countEnemyDeath;
    private bool level5 = false;
    public int countShafransQuest;
    public int countBears;
    public int countCougars;
    public int countRaccoons;

    // Start is called before the first frame update
    void Start()
    {
        

        input.EnableInput("Dig", false);

        hp_slider.maxValue = hp.Stat_Get(1).maxValue;
        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        reaction1_button.onClick.AddListener(() => PlayAnim(0));

        exp_slider.maxValue = maxExp;
        exp_slider.value = Init.Instance.playerData.exp;
        level_txt.text = Init.Instance.playerData.level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.questManager.dialogWindow.activeSelf)
        {
            GameManager.instance.cm.m_XAxis.m_MaxSpeed = 0;
            GameManager.instance.cm.m_YAxis.m_MaxSpeed = 0;
            input.SetEnable(false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            input.SetEnable(true);
            GameManager.instance.cm.m_XAxis.m_MaxSpeed = 300;
            GameManager.instance.cm.m_YAxis.m_MaxSpeed = 5;
            Cursor.lockState = CursorLockMode.Locked;
        }

        hp_slider.value = Convert.ToInt32(hp.Stat_Get(1).value);
        hp_txt.text = hp_slider.value.ToString() + "/" + hp_slider.maxValue.ToString();

        exp_slider.value = Init.Instance.playerData.exp;

        /*if (Input.GetKeyDown(KeyCode.U))
        {
            OpenPanel(true);
            ChangeAvatar(false);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenPanel(false);
            ChangeAvatar(true);
        }*/
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayAnim(0);
        }*/

        if (Init.Instance.playerData.exp >= maxExp)
        {
            Init.Instance.playerData.level++;
            level_txt.text = Init.Instance.playerData.level.ToString();
            Init.Instance.playerData.exp = 0;
            maxExp = Convert.ToInt32(maxExp * 1.3f);
            exp_slider.maxValue = maxExp;
        }

        if (Init.Instance.playerData.level == 5 && level5 == false)
        {
            GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[16];
            GameManager.instance.questManager.dialogWindow.SetActive(true);
            level5 = true;
            GameManager.instance.questManager.AddQuest(8);
            teleport.SetActive(true);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Animal"))
        {
            animals.Add(other.gameObject);
        }
        
    }

    private IEnumerator Find()
    {
        animals.Clear();
        fieldView.SetActive(true);
        
        yield return new WaitForSeconds(0.2f);
        fieldView.SetActive(false);
        FindClosestObject();
        
    }

    public void StartCorFind()
    {
        StartCoroutine(Find());
    }

    public void FindClosestObject()
    {
        minCorners = 9999999999;
        foreach (var item in animals)
        {
            if (Vector3.Distance(transform.position, item.transform.position) < minCorners)
            {
                minCorners = Vector3.Distance(gameObject.transform.position, item.transform.position);
                closestAnimal = item.transform;
            }

        }
    }

    public void ChangeAvatar(bool main)
    {
        if (main)
        {
            animator.avatar = mainAvatar;
            animator.runtimeAnimatorController = mainAnimController;
            
        }
        else
        {
            animator.avatar = reactionAvatar;
            animator.runtimeAnimatorController = reactionsAnimController;
        }
    }

    public void OpenPanel(bool open)
    {
        if (open)
        {
            reactionsPanel.SetActive(true);
        }
        else
        {
            reactionsPanel.SetActive(false);
        }    
    }

    public void PlayAnim(int index)
    {
        
        animator.SetTrigger("Anim");
        //animations[index].Play();
    }

    public void SetCustom()
    {
        if (Init.Instance.magicColorIndex == 0)
        {
            magic.gameObject.SetActive(false);
        }
        else
        {
            magic.gameObject.SetActive(true);
        }

        magic.material = magicMaterials[Init.Instance.magicColorIndex];

        wolf.material = wolfMaterials[Init.Instance.wolfColorIndex];

        head.localScale = new Vector3(Init.Instance.headSize, Init.Instance.headSize, Init.Instance.headSize);

        forwardLegL.localScale = new Vector3(Init.Instance.forwardLegsSize, 1, 1);
        forwardLegR.localScale = new Vector3(Init.Instance.forwardLegsSize, 1, 1);

        rearLegL.localScale = new Vector3(1, 1, Init.Instance.rearLegsSize);
        rearLegR.localScale = new Vector3(1, 1, Init.Instance.rearLegsSize);

        foreach (var item in hats)
        {
            item.SetActive(false);
        }

        hats[Init.Instance.hatVariantIndex].SetActive(true);

        GameManager.instance.cm.LookAt = camTarget;
        GameManager.instance.cm.Follow = camTarget;
        GameManager.instance.cm.m_XAxis.m_MaxSpeed = 300;
        GameManager.instance.cm.m_YAxis.m_MaxSpeed = 5;

        hp.Stat_Get(1).maxValue = 100;
        hp.Stat_Get(1).value = hp.Stat_Get(1).maxValue;

        input.SetEnable(true);

    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(4);
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.uIManager.diePanel.SetActive(true);
        gameObject.SetActive(false);
        transform.position = GameManager.instance.startPoint.position;
        GameManager.instance.uIManager.StartCor_DieTimer();

    }

    public void StartCor_Die()
    {
        StartCoroutine(Die());
    }
}
