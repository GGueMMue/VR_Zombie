using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public int hp = 5;
    public CameraShaker cameraShake;
    public AudioClip hurtSfx;
    public bool isDead = false;
    public GameObject gameOverText;
    public GameObject hurtEffect;

    public Slider hpBar;
    public TextMeshProUGUI scoreText;
    public GameObject score_HP_Panel;
    public GameObject gameoverPanel;
    public TextMeshProUGUI lastScoreText;

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
            //Time.timeScale = 0;
            
            score_HP_Panel.SetActive(false);
            lastScoreText.text = scoreText.text;
            gameoverPanel.SetActive(true);
            StartCoroutine(LoadMainScene());

            //gameOverText.SetActive(true);
        }
    }

    IEnumerator LoadMainScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
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