using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Light lighting;

    [SerializeField] GameObject openingScene;
    Animator openingSceneAnimator;
    [SerializeField] GameObject gameOverScreen;
    Animator gameOverScreenAnimator;
    [SerializeField] GameObject loadingScreen;

    public int totalLifes = 5;
    public int currentLifes;
    // Start is called before the first frame update
    void Start()
    {
        currentLifes = totalLifes;
        openingSceneAnimator = openingScene.GetComponent<Animator>();
        gameOverScreenAnimator = gameOverScreen.GetComponent<Animator>();

        gameOverScreen.SetActive(false);
        loadingScreen.SetActive(false);

        StartGame();
    }

    void StartGame()
    {
        openingSceneAnimator.SetBool("isOpen", true);

    }

    IEnumerator WaitForPlay()
    {
        yield return new WaitForSeconds(3);
        openingSceneAnimator.SetBool("isOpen", false);
    }

    void LoseLife()
    {
        if (currentLifes > 0)
        {
            currentLifes -= 1;
            lighting.intensity = (float)(currentLifes/totalLifes);
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
