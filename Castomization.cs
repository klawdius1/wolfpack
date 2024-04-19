using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Castomization : MonoBehaviour
{
    public bool wolfColorActive;
    public bool magicColorActive;
    public bool headSizeActive;
    public bool forwardLegSizeActive;
    public bool rearLegSizeActive;
    public bool hatVariantActive;

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

    [SerializeField] private Button switchRight_button;
    [SerializeField] private Button switchLeft_button;
    [SerializeField] private Button wolfColor_button;
    [SerializeField] private Button magicColor_button;
    [SerializeField] private Button headSize_button;
    [SerializeField] private Button forwardLegsSize_button;
    [SerializeField] private Button rearLegsSize_button;
    [SerializeField] private Slider sizeChanger_slider;
    [SerializeField] private Button hatVariant_button;

    [SerializeField] private GameObject sizeChangerPanel;
    [SerializeField] private TextMeshProUGUI meatAmount_txt;
    [SerializeField] private TextMeshProUGUI leavesAmount_txt;
    [SerializeField] private TextMeshProUGUI bonesAmount_txt;
    [SerializeField] private TextMeshProUGUI woodAmount_txt;

    [SerializeField] private List<int> colorPrice = new List<int>();
    [SerializeField] private List<int> magicPrice = new List<int>();
    [SerializeField] private List<int> hatsPrice = new List<int>();
    [SerializeField] private Button buy_button;
    [SerializeField] private TextMeshProUGUI buy_txt;
    [SerializeField] private GameObject[] shopIcons;

    // Start is called before the first frame update
    void Start()
    {
        meatAmount_txt.text = Init.Instance.playerData.meatAmount.ToString();
        leavesAmount_txt.text = Init.Instance.playerData.leavesAmount.ToString();
        bonesAmount_txt.text = Init.Instance.playerData.bonesAmount.ToString();
        woodAmount_txt.text = Init.Instance.playerData.woodAmount.ToString();

        switchRight_button.onClick.AddListener(SwitchItemRight);
        switchLeft_button.onClick.AddListener(SwitchItemLeft);

        wolfColor_button.onClick.AddListener(() => ChooseItem(0));
        magicColor_button.onClick.AddListener(() => ChooseItem(1));
        headSize_button.onClick.AddListener(() => ChooseItem(2));
        forwardLegsSize_button.onClick.AddListener(() => ChooseItem(3));
        rearLegsSize_button.onClick.AddListener(() => ChooseItem(4));
        hatVariant_button.onClick.AddListener(() => ChooseItem(5));
        sizeChanger_slider.onValueChanged.AddListener(delegate { ChangeSize(); });

        wolfColorActive = true;
        shopIcons[0].SetActive(true);
        buy_button.onClick.AddListener(BuyColor);
    }


    void SwitchItemRight()
    {
        if (wolfColorActive)
        {
            if (Init.Instance.wolfColorIndex < wolfMaterials.Length - 1)
            {
                Init.Instance.wolfColorIndex++;
            }
            else
            {
                Init.Instance.wolfColorIndex = 0;
            }

            wolf.material = wolfMaterials[Init.Instance.wolfColorIndex];

            foreach (var item in Init.Instance.playerData.unlockedWolfColors)
            {
                if (item == Init.Instance.wolfColorIndex)
                {
                    buy_button.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    buy_button.gameObject.SetActive(true);
                    buy_txt.text = colorPrice[Init.Instance.wolfColorIndex].ToString();
                    
                }
            }

            
        }

        if (magicColorActive)
        {
            if (Init.Instance.magicColorIndex == 0)
            {
                magic.gameObject.SetActive(false);
            }
            else
            {
                magic.gameObject.SetActive(true);
            }

            if (Init.Instance.magicColorIndex < magicMaterials.Length - 1)
            {
                Init.Instance.magicColorIndex++;

                magic.gameObject.SetActive(true);
            }
            else
            {
                Init.Instance.magicColorIndex = 0;

                magic.gameObject.SetActive(false);
            }

            magic.material = magicMaterials[Init.Instance.magicColorIndex];

            foreach (var item in Init.Instance.playerData.unlockedMagicColors)
            {
                if (item == Init.Instance.magicColorIndex)
                {
                    buy_button.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    buy_button.gameObject.SetActive(true);
                    buy_txt.text = magicPrice[Init.Instance.magicColorIndex].ToString();

                }
            }
        }

        if (hatVariantActive)
        {
            foreach (var item in hats)
            {
                item.SetActive(false);
            }

            if (Init.Instance.hatVariantIndex < hats.Length - 1)
            {
                Init.Instance.hatVariantIndex++;
                
            }
            else
            {
                Init.Instance.hatVariantIndex = 0;
            }

            hats[Init.Instance.hatVariantIndex].SetActive(true);

            foreach (var item in Init.Instance.playerData.unlockedHats)
            {
                if (item == Init.Instance.hatVariantIndex)
                {
                    buy_button.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    buy_button.gameObject.SetActive(true);
                    buy_txt.text = hatsPrice[Init.Instance.hatVariantIndex].ToString();

                }
            }
        }
    }

    void SwitchItemLeft()
    {
        if (wolfColorActive)
        {
            if (Init.Instance.wolfColorIndex > 0)
            {
                Init.Instance.wolfColorIndex--;
            }
            else
            {
                Init.Instance.wolfColorIndex = wolfMaterials.Length - 1;
            }

            wolf.material = wolfMaterials[Init.Instance.wolfColorIndex];

            foreach (var item in Init.Instance.playerData.unlockedWolfColors)
            {
                if (item == Init.Instance.wolfColorIndex)
                {
                    buy_button.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    buy_button.gameObject.SetActive(true);
                    buy_txt.text = colorPrice[Init.Instance.wolfColorIndex].ToString();
                }
            }

            
        }

        if (magicColorActive)
        {
            if (Init.Instance.magicColorIndex == 0)
            {
                magic.gameObject.SetActive(false);
            }
            else
            {
                magic.gameObject.SetActive(true);
            }

            if (Init.Instance.magicColorIndex > 0)
            {
                Init.Instance.magicColorIndex--;

                magic.gameObject.SetActive(true);
            }
            else
            {
                Init.Instance.magicColorIndex = magicMaterials.Length - 1;

                magic.gameObject.SetActive(true);
            }

            if (Init.Instance.magicColorIndex == 0)
            {
                magic.gameObject.SetActive(false);
            }

            magic.material = magicMaterials[Init.Instance.magicColorIndex];

            foreach (var item in Init.Instance.playerData.unlockedMagicColors)
            {
                if (item == Init.Instance.magicColorIndex)
                {
                    buy_button.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    buy_button.gameObject.SetActive(true);
                    buy_txt.text = magicPrice[Init.Instance.magicColorIndex].ToString();

                }
            }
        }

        if (hatVariantActive)
        {
            foreach (var item in hats)
            {
                item.SetActive(false);
            }

            if (Init.Instance.hatVariantIndex > 0)
            {
                Init.Instance.hatVariantIndex--;

            }
            else
            {
                Init.Instance.hatVariantIndex = hats.Length - 1;
            }

            hats[Init.Instance.hatVariantIndex].SetActive(true);

            foreach (var item in Init.Instance.playerData.unlockedHats)
            {
                if (item == Init.Instance.hatVariantIndex)
                {
                    buy_button.gameObject.SetActive(false);
                    return;
                }
                else
                {
                    buy_button.gameObject.SetActive(true);
                    buy_txt.text = hatsPrice[Init.Instance.hatVariantIndex].ToString();

                }
            }
        }
    }

    void ChangeSize()
    {
        if (headSizeActive)
        {
            if (sizeChanger_slider.value <= 1)
            {
                Init.Instance.headSize = 0.85f;
            }
            else if (sizeChanger_slider.value > 1 && sizeChanger_slider.value <= 2)
            {
                Init.Instance.headSize = 1f;
            }
            else if (sizeChanger_slider.value > 2)
            {
                Init.Instance.headSize = 1.15f;
            }

            head.localScale = new Vector3(Init.Instance.headSize, Init.Instance.headSize, Init.Instance.headSize);
        }

        if (forwardLegSizeActive)
        {
            if (sizeChanger_slider.value <= 1)
            {
                Init.Instance.forwardLegsSize = 1;
            }
            else if (sizeChanger_slider.value > 1 && sizeChanger_slider.value <= 2)
            {
                Init.Instance.forwardLegsSize = 1.25f;
            }
            else if (sizeChanger_slider.value >2)
            {
                Init.Instance.forwardLegsSize = 1.5f;
            }

            forwardLegL.localScale = new Vector3(Init.Instance.forwardLegsSize, 1, 1);
            forwardLegR.localScale = new Vector3(Init.Instance.forwardLegsSize, 1, 1);
        }

        if (rearLegSizeActive)
        {
            if (sizeChanger_slider.value <= 1)
            {
                Init.Instance.rearLegsSize = 1;
            }
            else if (sizeChanger_slider.value > 1 && sizeChanger_slider.value <= 2)
            {
                Init.Instance.rearLegsSize = 1.5f;
            }
            else if (sizeChanger_slider.value > 2)
            {
                Init.Instance.rearLegsSize = 2f;
            }

            rearLegL.localScale = new Vector3(1, 1, Init.Instance.rearLegsSize);
            rearLegR.localScale = new Vector3(1, 1, Init.Instance.rearLegsSize);
        }
    }

    void ChooseItem(int item)
    {
        buy_button.onClick.RemoveAllListeners();

        foreach (var icon in shopIcons)
        {
            icon.SetActive(false);
        }

        if (item == 0)
        {

            shopIcons[0].SetActive(true);
            buy_button.onClick.AddListener(BuyColor);

            wolfColorActive = true;
            magicColorActive = false;
            headSizeActive = false;
            forwardLegSizeActive = false;
            rearLegSizeActive = false;
            hatVariantActive = false;

            sizeChangerPanel.SetActive(false);
            switchLeft_button.gameObject.SetActive(true);
            switchRight_button.gameObject.SetActive(true);
        }
        else if (item == 1)
        {
            shopIcons[1].SetActive(true);
            buy_button.onClick.AddListener(BuyMagic);

            wolfColorActive = false;
            magicColorActive = true;
            headSizeActive = false;
            forwardLegSizeActive = false;
            rearLegSizeActive = false;
            hatVariantActive = false;

            sizeChangerPanel.SetActive(false);
            switchLeft_button.gameObject.SetActive(true);
            switchRight_button.gameObject.SetActive(true);
        }
        else if (item == 2)
        {
            wolfColorActive = false;
            magicColorActive = false;
            headSizeActive = true;
            forwardLegSizeActive = false;
            rearLegSizeActive = false;
            hatVariantActive = false;

            sizeChangerPanel.SetActive(true);

            switchLeft_button.gameObject.SetActive(false);
            switchRight_button.gameObject.SetActive(false);
        }
        else if (item == 3)
        {
            wolfColorActive = false;
            magicColorActive = false;
            headSizeActive = false;
            forwardLegSizeActive = true;
            rearLegSizeActive = false;
            hatVariantActive = false;

            sizeChangerPanel.SetActive(true);

            switchLeft_button.gameObject.SetActive(false);
            switchRight_button.gameObject.SetActive(false);
        }
        else if (item == 4)
        {
            wolfColorActive = false;
            magicColorActive = false;
            headSizeActive = false;
            forwardLegSizeActive = false;
            rearLegSizeActive = true;
            hatVariantActive = false;

            sizeChangerPanel.SetActive(true);

            switchLeft_button.gameObject.SetActive(false);
            switchRight_button.gameObject.SetActive(false);
        }
        else if (item == 5)
        {
            shopIcons[2].SetActive(true);
            buy_button.onClick.AddListener(BuyHat);

            wolfColorActive = false;
            magicColorActive = false;
            headSizeActive = false;
            forwardLegSizeActive = false;
            rearLegSizeActive = false;
            hatVariantActive = true;

            sizeChangerPanel.SetActive(false);

            switchLeft_button.gameObject.SetActive(true);
            switchRight_button.gameObject.SetActive(true);
        }
    }

    void BuyColor()
    {
        if (Init.Instance.playerData.meatAmount >= colorPrice[Init.Instance.wolfColorIndex])
        {
            Init.Instance.playerData.meatAmount -= colorPrice[Init.Instance.wolfColorIndex];
            meatAmount_txt.text = Init.Instance.playerData.meatAmount.ToString();
            Init.Instance.playerData.unlockedWolfColors.Add(Init.Instance.wolfColorIndex);
        }
    }

    void BuyMagic()
    {
        if (Init.Instance.playerData.leavesAmount >= magicPrice[Init.Instance.magicColorIndex])
        {
            Init.Instance.playerData.leavesAmount -= magicPrice[Init.Instance.magicColorIndex];
            leavesAmount_txt.text = Init.Instance.playerData.leavesAmount.ToString();
            Init.Instance.playerData.unlockedMagicColors.Add(Init.Instance.magicColorIndex);
        }
    }

    void BuyHat()
    {
        if (Init.Instance.playerData.bonesAmount >= hatsPrice[Init.Instance.hatVariantIndex])
        {
            Init.Instance.playerData.bonesAmount -= hatsPrice[Init.Instance.hatVariantIndex];
            bonesAmount_txt.text = Init.Instance.playerData.bonesAmount.ToString();
            Init.Instance.playerData.unlockedHats.Add(Init.Instance.hatVariantIndex);
        }
    }
}
