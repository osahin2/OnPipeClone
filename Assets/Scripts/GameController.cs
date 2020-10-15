using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Text endGameScoreText;
    public Text endGameBestScore;
    public Text tapToRestart;

    RingController ringController;
    bool restartControl = false;
    int bestScore;

    void Start()
    {
        ringController = GameObject.FindGameObjectWithTag("Player").GetComponent<RingController>();

        bestScore = PlayerPrefs.GetInt("bestScoreSave");
        endGameBestScore.text = "BEST " + bestScore;
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && restartControl )
        {
            SceneManager.LoadScene("SampleScene");
        }
        if(ringController.endGameControl)
        {
            gameObject.TryGetComponent(out ObjectPooler pooler);
            pooler.StopAllCoroutines();
            CallText();
        }
        if(ringController.finishControl)
        {
            CallText();
        }
    }
    void CallText()
    {
        if (ringController.Score > bestScore)
        {
            endGameBestScore.text = "BEST " + ringController.Score;
        }
        endGameScoreText.enabled = true;
        endGameBestScore.enabled = true;
        tapToRestart.enabled = true;
        restartControl = true;
    }
}
