using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimerScript WheatTimer;
    public ImageTimerScript EnsureWarriorsTimer;
    public Image RaidTimerImg;
    public Image PeasantTimerImg;
    public Image WarriorTimerImg;

    public Button FarmerButton;
    public Button WarriorButton;
    public TMP_Text WheatText;
    public TMP_Text WarriorsText;
    public TMP_Text FarmersText;
    public TMP_Text AttackTimerText;
    public TMP_Text AmountOfNextAttackText;


    public int FarmerCount;
    public int WarriorsCount;
    public int WheatCount;
    public int WheatPerFarmer;
    public int WheatToWarriors;
    public int FarmerCost;
    public int WarriorCost;
    public int AttackIncrease;
    public int nextAttack;
    public GameObject GameOverScreen;
    public GameObject GameScreen;
    public GameObject WinnerScreen;
    public GameObject MenuScreen;
    public GameObject WelcomeScreen;

    [SerializeField] private SoundController soundController;
    [SerializeField] private TMP_Text _fullAmountOfFarmersText;
    [SerializeField] private TMP_Text _fullAmountOfWarriorsText;
    [SerializeField] private TMP_Text _fullAmountOfWheatText;
    [SerializeField] private TMP_Text _fullAmountOfAttackText;

    public float FarmerCreateTime;
    public float WarriorCreateTime;
    public float AttackMaxTime;

    private float _farmerTimer = -2;
    private float _warriorTimer = -2;
    private float _attackTimer;
    private int _attackPerGame;
    private int _wheatPerGame;
    private int _warriersPerGame;
    private int _farmersPerGame = 3;
    private bool _paused = false;

    

    void Start()
    {
        UpdateText();
        _attackTimer = AttackMaxTime;
    }

   
    void Update()
    {

        if (FarmerCount >= 6)
        {
            _attackTimer -= Time.deltaTime;
            RaidTimerImg.fillAmount = _attackTimer / AttackMaxTime;

            if (_attackTimer <= 0)
            {
                _attackTimer = AttackMaxTime;
                WarriorsCount -= nextAttack;
                nextAttack += AttackIncrease;
                _attackPerGame++;
                soundController.PlayAudioAttack();
            }
        }

        if (WheatTimer.IsTimerEnd)
        {
            WheatCount += FarmerCount * WheatPerFarmer;
            _wheatPerGame += FarmerCount * WheatPerFarmer;
            soundController.PlayAudioCollectWheat();
        }

        if (EnsureWarriorsTimer.IsTimerEnd)
        {
            WheatCount -= WarriorsCount * WheatToWarriors;
            soundController.PlayAudioEnsureWarriors();
        }

        CheckButtonInteractable(FarmerButton, FarmerCost, _farmerTimer);
        CheckButtonInteractable(WarriorButton, WarriorCost, _warriorTimer);

        if (_farmerTimer > 0)
        {
            _farmerTimer -= Time.deltaTime;
            PeasantTimerImg.fillAmount = _farmerTimer / FarmerCreateTime;
        }
        else if (_farmerTimer > -1)
        {
            PeasantTimerImg.fillAmount = 1;
            FarmerCount += 1;
            _farmerTimer =- 2;
            soundController.PlayAudioCreateFarmer();
        }

        if (_warriorTimer > 0)
        {
            _warriorTimer -= Time.deltaTime;
            WarriorTimerImg.fillAmount = _warriorTimer / WarriorCreateTime;
        }
        else if (_warriorTimer > -1)
        {
            WarriorTimerImg.fillAmount = 1;
            WarriorsCount += 1;
            _warriorTimer =- 2;
            soundController.PlayAudioCreateWarrior();
        }

        if (WarriorsCount < 0)
        {
            Time.timeScale = 0;
            GameScreen.SetActive(false);
            GameOverScreen.SetActive(true);
            GetInfoAboutGame();
        }

        if (WheatCount > 100)
        {
            Time.timeScale = 0;
            WinnerScreen.SetActive(true);
            GameScreen.SetActive(false);
        }

        UpdateText();
    }

    public void CreateFarmer()
    {
        WheatCount -= FarmerCost;
        _farmerTimer = FarmerCreateTime;
        _farmersPerGame += 1;
        soundController.PlayAudioClickButton();
    }

    public void CreateWarrior()
    {
        WheatCount -= WarriorCost;
        _warriorTimer = WarriorCreateTime;
        _warriersPerGame += 1;
        soundController.PlayAudioClickButton();
    }

    private void UpdateText()
    {
        WheatText.text = WheatCount.ToString();
        WarriorsText.text = WarriorsCount.ToString();
        FarmersText.text = FarmerCount.ToString();
        AmountOfNextAttackText.text = nextAttack.ToString();
    }

    public void CheckButtonInteractable(Button someButton, int cost, double timer)
    {
        if (WheatCount > cost && timer == -2)
        {
            someButton.interactable = true;
        }
        else
        {
            someButton.interactable = false;
        }
    }

    public void GetInfoAboutGame()
    {
        _fullAmountOfAttackText.text = _attackPerGame.ToString();
        _fullAmountOfFarmersText.text = _farmersPerGame.ToString();
        _fullAmountOfWarriorsText.text = _warriersPerGame.ToString();
        _fullAmountOfWheatText.text = _wheatPerGame.ToString();
    }

    public void GamePaused()
    {
        if (_paused)
        {
            Time.timeScale = 1;
            MenuScreen.SetActive(false);
            
        }
        else
        {
            Time.timeScale = 0;
            MenuScreen.SetActive(true);
        }

        soundController.PlayAudioClickButton();

        _paused = !_paused;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        WelcomeScreen.SetActive(false);
        GameScreen.SetActive(true);
    }

}
