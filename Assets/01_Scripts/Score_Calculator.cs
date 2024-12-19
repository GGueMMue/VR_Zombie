using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score_Calculator : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void ScorePlus()
    {
        score += 300;
        scoreText.text = $"Score : {score}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
