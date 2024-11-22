using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager _instance = null;

    public static ScoreManager Instance()
    {
        return _instance;
    }


    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        LoadBestScore();
    }

    int _bestScore = 0;
    int _myScore = 0;

    public int bestScore
    {
        get
        {
            return _bestScore;
        }
    }

    public int mySocre
    {
        get
        {
            return _myScore;
        }
        set
        {
            _myScore = value;
            if (_myScore > _bestScore)
            {
                _bestScore = _myScore;
                SaveBestScore();
                Debug.Log("최고 점수 갱신 ::: " + _bestScore);
            }
        }
    }

    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", _bestScore);
    }

    void LoadBestScore()
    {
        _bestScore = PlayerPrefs.GetInt("BestScore");
    }
}