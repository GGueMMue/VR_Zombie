using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public int hp = 15;
    public CameraShaker cameraShake;
    public AudioClip hurtSfx;
    public bool isDead = false;
    public GameObject gameOverText;
    public GameObject hurtEffect;

    public Slider hpBar;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        cameraShake = GetComponentInChildren<CameraShaker>();
    }

    void Update()
    {
        if (isDead == false)
        {
            hpBar.value = (float)hp / 5;
        }
        else
        {
            hpBar.value = 0;
            //gameOverText.SetActive(true);
        }

        int myCurScore = ScoreManager.Instance().mySocre;
        int myBestScore = ScoreManager.Instance().bestScore;

        scoreText.text = "MY SCORE : " + myCurScore + "\n" + "BEST SCORE : " + myBestScore;
    }

    IEnumerator HurtEffect()
    {
        hurtEffect.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        hurtEffect.SetActive(false);
    }
    public void DamageByEnemy()
    {
        --hp;
        cameraShake.PlayCameraShake();
        AudioManager.Instance().PlaySfx(hurtSfx);

        StartCoroutine(HurtEffect());
        if (hp <= 0)
        {
            hurtEffect.SetActive(true);
            isDead = true;
        }
        if (isDead == true) hurtEffect.SetActive(true);
        return;
    }
}