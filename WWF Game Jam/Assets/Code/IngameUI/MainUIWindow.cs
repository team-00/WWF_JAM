using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIWindow : WindowBase
{
    [SerializeField]
    private GameManager gm;
    [SerializeField]
    private GameObject failureWindow;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private TextMeshProUGUI healthText, coinText;
    [SerializeField]
    private TextMeshProUGUI[] turretCostTexts;
    [SerializeField]
    private Image[] turretImages;
    [SerializeField]
    private Sprite emptyTurretSprite;
    [SerializeField]
    private string emptyTurretMessage;
    [SerializeField]
    private GameObject turretHoverPanel;
    [SerializeField]
    private TextMeshProUGUI turretName, turretSellValue;
    [SerializeField]
    private Image turretImage;
    [SerializeField]
    private TickBox[] attackModeSelection;

    private Turret currentDisplayedTurretInfo = null;
    private int maxhealth;

    private int currentAttackMode = 0;

    public override void Disable()
    {
        foreach (var button in buttons)
        {
            button.interactable = false;
        }
        turretHoverPanel.SetActive(false);
        for (int boxID = 0; boxID < attackModeSelection.Length; boxID++)
        {
            attackModeSelection[boxID].Denitialize();
        }
    }

    public override void Enable()
    {
        turretHoverPanel.SetActive(false);
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
        for (int boxID = 0; boxID < attackModeSelection.Length; boxID++)
        {
            attackModeSelection[boxID].Initialize(boxID, this, boxID == currentAttackMode);
        }
    }

    public void OpenPlacedTurretInfo(Turret turret)
    {
        currentDisplayedTurretInfo = turret;
        turretImage.sprite = turret.Stats.ShopSprite;
        turretName.text = turret.Stats.name;
        turretSellValue.text = turret.Stats.TurretSellPrice + " $";
        currentAttackMode = turret.TargetMode;

        for (int boxID = 0; boxID < attackModeSelection.Length; boxID++)
        {
            attackModeSelection[boxID].Initialize(boxID, this, boxID == currentAttackMode);
        }
        turretHoverPanel.SetActive(true);
    }

    public void ClosePlacedTurretInfo()
    {
        currentDisplayedTurretInfo = null;
        turretHoverPanel.SetActive(false);
        for (int boxID = 0; boxID < attackModeSelection.Length; boxID++)
        {
            attackModeSelection[boxID].Denitialize();
        }
    }

    public override void TickBox(bool on, int boxID)
    {
        if (on)
        {
            int oldBoxID = currentAttackMode;
            currentAttackMode = boxID;
            if (oldBoxID != -1)
                attackModeSelection[oldBoxID].Tick();
        }
        else if (boxID == currentAttackMode)
        {
            currentAttackMode = -1;
            attackModeSelection[boxID].Tick();
        }
    }

    public void SetUI(int maxhealth, int health, int coins, TurretStats[] turrets)
    {
        this.maxhealth = maxhealth;
        UpdateUI(health, coins);

        for (int i = 0; i < turretCostTexts.Length; i++)
        {
            if (i < turrets.Length)
                turretCostTexts[i].text = turrets[i].TurretPrice + " $";
            else
                turretCostTexts[i].text = emptyTurretMessage;
        }

        for (int i = 0; i < turretImages.Length; i++)
        {
            if (i < turrets.Length)
                turretImages[i].sprite = turrets[i].ShopSprite;
            else
                turretImages[i].sprite = emptyTurretSprite;
        }
    }

    //public void SetUI(int[] turretCosts, int maxhealth, int health, int coins, Sprite[] turretSprites)
    //{
    //    this.maxhealth = maxhealth;
    //    UpdateUI(health, coins);

    //    for (int i = 0; i < turretCostTexts.Length; i++)
    //    {
    //        if (i < turretCosts.Length)
    //            turretCostTexts[i].text = turretCosts[i] + " $";
    //        else
    //            turretCostTexts[i].text = emptyTurretMessage;
    //    }

    //    for (int i = 0; i < turretImages.Length; i++)
    //    {
    //        if (i < turretSprites.Length)
    //            turretImages[i].sprite = turretSprites[i];
    //        else
    //            turretImages[i].sprite = emptyTurretSprite;
    //    }
    //}

    public void UpdateUI(int health, int coins)
    {
        UpdateHeath(health);
        UpdateCoin(coins);
    }

    public void UpdateHeath(int health)
    {
        healthText.text = "" + health;
        healthImage.fillAmount = ((1f / (float)maxhealth) * (float)health);
    }
    public void UpdateCoin(int coins)
    {
        coinText.text = coins + "$";
    }

    public void ActivateFailureWindow()
    {
        AudioManager.PlayFailureSound();
        failureWindow.SetActive(true);
    }

    public void SetTargetMode(int mode)
    {
        if(currentDisplayedTurretInfo != null)
        {
            currentDisplayedTurretInfo.TargetMode = mode;
            AudioManager.PlayButtonSound();
        }
    }

    public void SellTurret()
    {
        if (currentDisplayedTurretInfo != null)
        {
            gm.Gold += currentDisplayedTurretInfo.Stats.TurretSellPrice;
            Destroy(currentDisplayedTurretInfo.gameObject);
            ClosePlacedTurretInfo();
            AudioManager.PlayMoneySound();
        }
    }
}
