using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterLevel : MonoBehaviour
{
    public int level;
    public float currentExp;
    public float requiredExp;

    public float lerpTimer;
    public float delayTimer;

    [SerializeField] public CharacterInfoWindow charInfoWindow;

    [Header("UI")]
    public Image frontExpBar;
    public Image backExpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;

    [Header("Multipliers")]
    [Range(1f,300f)]
    public float additionMultiplier = 300;
    [Range(2f,4f)]
    public float powerMultiplier = 2;
    [Range(7f,14f)]
    public float divisionMultiplier = 7;

    // Start is called before the first frame update
    void Start()
    {
        frontExpBar.fillAmount = currentExp / requiredExp;    
        backExpBar.fillAmount = currentExp / requiredExp;
        requiredExp = CalculateRequiredExp();
        levelText.text = "Level " + level;
        charInfoWindow.UpdateCharInfoWindow(this);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExpUI();
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            GainExperience(20);
        }
        if (currentExp > requiredExp)
        {
            LevelUp();
        }
        charInfoWindow.UpdateCharInfoWindow(this);
    }

    public void UpdateExpUI()
    {
        float expFraction = currentExp / requiredExp;
        float fillExp = frontExpBar.fillAmount;
        if (fillExp < expFraction)
        {
            delayTimer += Time.deltaTime;
            backExpBar.fillAmount = expFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 2;
                percentComplete = percentComplete * percentComplete;
                frontExpBar.fillAmount = Mathf.Lerp(fillExp, backExpBar.fillAmount, percentComplete);
            }
        }
        expText.text = currentExp + "/" + requiredExp;
    }

    public void GainExperience(float expGained)
    {
        currentExp += expGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }

    public void LevelUp()
    {
        level++;
        frontExpBar.fillAmount = 0f;
        backExpBar.fillAmount = 0f;
        currentExp = Mathf.RoundToInt(currentExp - requiredExp);
        GetComponent<CharacterHealth>().IncreaseHealth(level);
        requiredExp = CalculateRequiredExp();
        levelText.text = "Level " + level;
    }

    public int CalculateRequiredExp()
    {
        int solveForRequiredExp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredExp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredExp / 4;
    }
}
