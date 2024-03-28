using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BigScoreManager : MonoBehaviour
{
    [SerializeField] public bool isPlayer1;
    private int currentScore = 0;
    [SerializeField] private float currentCombo = 0;

    [HideInInspector] public int comboMultiplier = 1;

    [SerializeField] private int maximumMultiplier = 4;
    [SerializeField] private float maximumCombo = 100;
    private float comboDecayRate = 13;
    private float delayTime = 0;
    private float decayDelayTime = 3;

    private float oldScore;

    [SerializeField] private TextMeshProUGUI scoreCounter;
    [SerializeField] private Image barFill;
    [SerializeField] private TextMeshProUGUI multiplierText;

    [SerializeField] private Color lowComboColour;
    [SerializeField] private Color mediumComboColour;
    [SerializeField] private Color highComboColour;
    [SerializeField] private Color maxComboColour;
    private Color currentColor;

    private LevelData lvlData;
    private bool initialized = false;
 
    private void Start()
    {

    }

    public void Initialize(LevelData data, bool player1)
    {
        lvlData = data;
        isPlayer1 = player1;
        initialized = true;
    }

    private void Update()
    {
        if (initialized)
        {
            comboMultiplier = Mathf.RoundToInt(maximumMultiplier * currentCombo / maximumCombo);
            comboMultiplier = Mathf.Clamp(comboMultiplier, 1, maximumMultiplier);

            scoreCounter.text = Mathf.RoundToInt(oldScore).ToString("D5");
            barFill.fillAmount = ((float)currentCombo / (float)maximumCombo);
            multiplierText.text = ("x" + comboMultiplier.ToString());

            if (currentCombo > 0 && Time.time - delayTime > decayDelayTime)
            {
                currentCombo -= comboDecayRate * Time.deltaTime;
            }
            currentCombo = Mathf.Clamp(currentCombo, 10, maximumCombo);



            if (comboMultiplier == 1)
            {
                currentColor = Color.Lerp(currentColor, lowComboColour, 3);
            }
            else if (comboMultiplier == 2)
            {
                currentColor = Color.Lerp(currentColor, mediumComboColour, 3);
            }
            else if (comboMultiplier == 3)
            {
                currentColor = Color.Lerp(currentColor, highComboColour, 3);
            }
            else if (comboMultiplier == maximumMultiplier)
            {
                currentColor = Color.Lerp(currentColor, maxComboColour, 3);
            }

            barFill.color = currentColor;
            multiplierText.color = currentColor;

            if (oldScore < currentScore)
            {
                oldScore += 1 * ((currentScore - oldScore) * 0.05f);
            }
            else
            {
                oldScore = currentScore;
            }
            
        }
        
    }

    public void AddScore(int value)
    {
        currentScore += value * comboMultiplier;
    }

    public void AddCombo(float value)
    {
        delayTime = Time.time;
        if (currentCombo + value < maximumCombo)
        { 
            currentCombo += value;
        }
        else
        {
            currentCombo = maximumCombo;
        }
    }

    public void AddDeathScore(int value, int multiplier)
    {
        currentScore += value * multiplier;
    }
}
