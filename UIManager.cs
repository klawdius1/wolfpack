using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button findAnimal_button;
    public Button looting_button;
    public Button chat_button;
    public Button customPanel_button;

    [SerializeField] private TextMeshProUGUI meatAmount_txt;
    [SerializeField] private TextMeshProUGUI leavesAmount_txt;
    [SerializeField] private TextMeshProUGUI bonesAmount_txt;
    [SerializeField] private TextMeshProUGUI woodAmount_txt;

    public TextMeshProUGUI timer_txt;
    private bool firstOpenChat = true;
    [SerializeField] private GameObject castomWolf;
    [SerializeField] private GameObject cam_target;

    public GameObject diePanel;
    [SerializeField] private Button revive_button;
    private int dieTimer = 6;
    [SerializeField] private TextMeshProUGUI dieTimer_txt;

    public TextMeshProUGUI digTip_txt;

    // Start is called before the first frame update
    void Start()
    {
        revive_button.onClick.AddListener(OpenCastomPanel);
        customPanel_button.onClick.AddListener(OpenCastomPanel);
        findAnimal_button.onClick.AddListener(GameManager.instance.player.StartCorFind);
        meatAmount_txt.text = Init.Instance.playerData.meatAmount.ToString();
        leavesAmount_txt.text = Init.Instance.playerData.leavesAmount.ToString();
        bonesAmount_txt.text = Init.Instance.playerData.bonesAmount.ToString();
        woodAmount_txt.text = Init.Instance.playerData.woodAmount.ToString();
        GameManager.instance.cm.m_XAxis.m_MaxSpeed = 0;
        GameManager.instance.cm.m_YAxis.m_MaxSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        meatAmount_txt.text = Init.Instance.playerData.meatAmount.ToString();
        leavesAmount_txt.text = Init.Instance.playerData.leavesAmount.ToString();
        bonesAmount_txt.text = Init.Instance.playerData.bonesAmount.ToString();
        woodAmount_txt.text = Init.Instance.playerData.woodAmount.ToString();


    }

    IEnumerator DieTimer()
    {
        revive_button.enabled = false;
        dieTimer = 5;
        for (int i = 0; i < 5; i++)
        {
            dieTimer -= 1;
            dieTimer_txt.text = dieTimer.ToString();
            yield return new WaitForSeconds(1);
        }

        revive_button.enabled = true;
    }

    public void StartCor_DieTimer()
    {
        StartCoroutine(DieTimer());
    }

    public void OpenCastomPanel()
    {
        GameManager.instance.player.gameObject.SetActive(false);
        castomWolf.SetActive(true);
        GameManager.instance.cm.LookAt = cam_target.transform;
        GameManager.instance.cm.Follow = cam_target.transform;
        GameManager.instance.cm.m_XAxis.m_MaxSpeed = 0;
        GameManager.instance.cm.m_YAxis.m_MaxSpeed = 0;
        GameManager.instance.cm.m_XAxis.Value = 0;
        GameManager.instance.cm.m_YAxis.Value = 0;
    }
   

}
