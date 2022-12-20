using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;

    float m_Timer;
    bool gameTimeEnd;
    bool torresDestruidas;
    bool torresEnemigasDestruidas;

    public CanvasGroup lostImageCanvasGroup;
    public CanvasGroup winImageCanvasGroup;
    public Timer gameTime;
    public towerScript towerDestroyed;
    public towerScript enemytowerDestroyed;

    public void timeEnds()
    {
        if (gameTime.time <= 0f)
        {
            gameTimeEnd = true;
        }
    }

    public void TowerDestroyed()
    {
        if (towerDestroyed.torrePrincpDestruida == true)
        {
            torresDestruidas = true;
        }
        if (enemytowerDestroyed.torrePrincpEnemigaDestruida == true)
        {
            torresEnemigasDestruidas = true;
        }
    }

    void Update()
    {
        timeEnds();
        TowerDestroyed();

        if (gameTimeEnd || torresDestruidas)
        {
            EndLevel(lostImageCanvasGroup, true);
        }
        if (torresEnemigasDestruidas)
        {
            EndLevel(winImageCanvasGroup, true);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
