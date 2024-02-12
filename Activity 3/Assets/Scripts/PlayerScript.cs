using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    public bool isGameOver;

    void Start()
    {
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            StartCoroutine(GameOver());
            
        }
    }

    IEnumerator GameOver()
    {
        
        yield return new WaitForSeconds(0.2f);
        gameOver.SetActive(true);

        Time.timeScale = 0;

    }
}
