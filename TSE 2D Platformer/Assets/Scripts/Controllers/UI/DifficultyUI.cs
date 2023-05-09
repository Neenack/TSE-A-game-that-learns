using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class DifficultyUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI _text;
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
        _text.text = "Next Difficulty: " + _levelGenScript.difficulty;
    }
}
