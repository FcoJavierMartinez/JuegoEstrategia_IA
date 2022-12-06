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

    public CanvasGroup lostImageCanvasGroup;
    public CanvasGroup winImageCanvasGroup;
    public Timer gameTime;

    public void timeEnds()
    {
        if (gameTime.time < 0f)
        {
            gameTimeEnd = true;
        }
    }

    void Update()
    {
        timeEnds();

        if (gameTimeEnd)
        {
            EndLevel(lostImageCanvasGroup, false);
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
