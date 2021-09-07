using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimer HarvestTimer;
    public ImageTimer EatingTimer;
    public Image RaidTimerImg;
    public Image PeasantTimerImg;
    public Image WarriorTimerImg;
    public GameObject GameOverScreen;
    public GameObject WinGameScreen;
    public GameObject GameOverScreenFood;
    

    public Button peasantButton;
    public Button warriorButton;
    public Button bonusbutton;
    public Button pauseButton;

    public Text costWheat;
    public Text resourcesText;
    public Text statsGame;
    public Text statsGameFoodOver;

    public AudioSource audioClick;
    public AudioSource harvestAudio;
    public AudioSource battleAudio;
    public AudioSource foodAudio;
    public AudioSource spawnsound;

    public int roundNum;
    public int roundBonus;

    public int peasantCount;
    public int warriorsCount;
    public int wheatCount;

    public int wheatPerPeasant;
    public int wheatToWarriors;

    public int peasantCost;
    public int warriorCost;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int nextraid;

    public float peasantTimer = -2;
    public float warriorTimer = -2;
    public float raidTimer;

    private bool checkbonusUse = true;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
        raidTimer = raidMaxTime;
        audioClick.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        raidTimer -= Time.deltaTime;
        RaidTimerImg.fillAmount = raidTimer / raidMaxTime;
        if (raidTimer <=0)
        {
            battleAudio.Play();
            raidTimer = raidMaxTime;
            warriorsCount -= nextraid;
            nextraid += raidIncrease;
            roundNum++;
        }
        if (roundNum == 5 && checkbonusUse == true)
        {
            bonusbutton.gameObject.SetActive(true);
        }
        if (HarvestTimer.Tick)
        {
            wheatCount += peasantCount * wheatPerPeasant;
            harvestAudio.Play();
        }

        if (EatingTimer.Tick )
        {
            wheatCount -= warriorsCount * wheatToWarriors;
            foodAudio.Play();
            if (wheatCount < 0)
            {
                Time.timeScale = 0;
                GameOverScreenFood.SetActive(true);
                statsGameFoodOver.text = $" Кол-во раундов: {roundNum} \n Кол-во воинов: {warriorsCount} \n Кол-во крестьян: {peasantCount}";
            }
        }

        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            PeasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if (peasantTimer > -1)
        {
            spawnsound.Play();
            PeasantTimerImg.fillAmount = 1;
            peasantButton.interactable = true;
            peasantCount += 1;
            peasantTimer = -2;
        }
        if (warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            WarriorTimerImg.fillAmount = warriorTimer / warriorCreateTime;
        }
        else if (warriorTimer > -1)
        {
            spawnsound.Play();
            WarriorTimerImg.fillAmount = 1;
            warriorButton.interactable = true;
            warriorsCount += 1;
            warriorTimer = -2;
        }
        WheatCountCheck();
        UpdateText();
        if (warriorsCount < 0)
        {
            Time.timeScale = 0;
            GameOverScreen.SetActive(true);
            statsGame.text = $" Кол-во раундов: {roundNum} \n Кол-во зерна: {wheatCount} \n Кол-во крестьян: {peasantCount}";
        }
        if (roundNum == 11)
        {
            WinGameScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CreatePeasant()
    {
        wheatCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        peasantButton.interactable = false;
        audioClick.Play();
    }   
    
    public void CreateWarrior()
    {
        wheatCount -= warriorCost;
        warriorTimer = warriorCreateTime;
        warriorButton.interactable = false;
        audioClick.Play();
    }
    public void WheatCountCheck()
    {
        if (wheatCount < warriorCost || warriorTimer > 0)
        {
            warriorButton.interactable = false;
        }
        else
        {
            warriorButton.interactable = true;
        }
        if (wheatCount < peasantCost || peasantTimer > 0)
        {
            peasantButton.interactable = false;
        }
        else
        {
            peasantButton.interactable = true;
        }
    }
    public void BonusButton()
    {
        warriorsCount += 30;
        wheatCount += 100;
        bonusbutton.gameObject.SetActive(false);
        checkbonusUse = false;
    }
    private void UpdateText()
    {
        costWheat.text = $"Стоимость крестьянина: {peasantCost} \n Стоимость воина: {warriorCost}";
        resourcesText.text = peasantCount + "\n" + warriorsCount + "\n\n" + wheatCount + "\n\n" + nextraid + "\n" + roundNum;
    }
}
