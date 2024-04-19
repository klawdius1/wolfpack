using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Button startGame_button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.tutorial)
        {
            startGame_button.onClick.AddListener(ShowTutorial);
        }
        else
        {
            startGame_button.onClick.RemoveListener(ShowTutorial);
        }
    }

    public void ShowTutorial()
    {
        GameManager.instance.questManager.dialog_txt.text = GameManager.instance.questManager.dialogs[24];
        GameManager.instance.questManager.dialogWindow.SetActive(true);
    }
}
