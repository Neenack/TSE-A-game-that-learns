using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class DifficultyUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _currentDifficultyText;
    public GameObject _levelGen;
    LevelGeneration _levelGenScript;

    // Start is called before the first frame update
    void Start()
    {
        _levelGenScript = _levelGen.GetComponent<LevelGeneration>();
        
    }

    // Update is called once per frame
    void Update()
    {
        int difficulty = _levelGenScript.difficulty;

        float currentGreen;
        float currentRed;

        if(difficulty >= 5)
        {
            currentGreen = 255 - (((float)difficulty - 5) * 255 / 5);
            currentRed = 255;
        }
        else
        {
            currentGreen = 255;
            currentRed = 0 + (((float)difficulty - 1) * 255 / 4);
        }
        _currentDifficultyText.color = new Color(currentRed / 255, currentGreen / 255, 0, 255);
        _currentDifficultyText.text = "CurrentDifficulty: " + difficulty;
    }
}
