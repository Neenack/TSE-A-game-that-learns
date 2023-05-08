using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class DifficultyUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _text;
    GameObject _levelGen;
    LevelGeneration _levelGenScript;

    // Start is called before the first frame update
    void Start()
    {
        _levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
        _levelGenScript = _levelGen.GetComponent<LevelGeneration>();
        _text = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "Next Difficulty: " + _levelGenScript.difficulty;
    }
}
