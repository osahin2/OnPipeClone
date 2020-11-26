using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Scene Objects")]
    [SerializeField] private GameObject finishRing;
    [SerializeField] private RingController ringController;
    [SerializeField] private BoxCollider exitControlBoundary;
    [SerializeField] private Collectible collectible;
    [SerializeField] private ObjectPooler pooler;
    [Header("UI Elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text tapToRestart;
    [SerializeField] private Text tapToStart;
    [SerializeField] private Button coins;
    [SerializeField] private Button rings;
    [SerializeField] private Text endGameScoreText;
    [SerializeField] private Text endGameText;
    [SerializeField] private GameObject scoreBackground;
    [SerializeField] private GameObject glassBackground;

    private bool restartControl = false;
    private int bestScore;
    private int score;
    private int finishCounter;
    private Vector3 finishRingSpawnPos;

    private bool controlFinish = false;
    public bool ControlFinish
    {
        get
        {
            return controlFinish;
        }
    }

    private void Initialized()
    {
        FinishControl.OnEnterFinishControl += FinishCounter;
        Damager.OnDamagerEnter += RingHit;
        FinishPoint.OnEnterFinish += Finish;
        InputEventHandler.PointerDowned += TapToStart;
        Collectible.OnEnterCollectible += GetScore;
    }

    private void Awake()
    {
        Instance = this;
        collectible.Initialized();
        ringController.Initialized();
        UIGlassBar.Instance.Initialized();
        pooler.Initialized();
        Initialized();
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScoreSave");
        bestScoreText.text = "BEST " + bestScore;
    }

    private void CallText(string textEndGame)
    {
        if (score > bestScore)
        {
            bestScoreText.text = "BEST " + score;
        }
        bestScoreText.enabled = true;
        tapToRestart.enabled = true;
        restartControl = true;
        scoreBackground.SetActive(false);
        glassBackground.SetActive(false);
        endGameScoreText.text = "" + score;
        endGameScoreText.enabled = true;
        endGameText.text = textEndGame;
        endGameText.enabled = true;
    }

    private void TapToStart(PointerEventData eventData)
    {
        tapToStart.enabled = false;
        coins.gameObject.SetActive(false);
        rings.gameObject.SetActive(false);
        if (restartControl)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void FinishCounter(FinishControl finishControl)
    {
        finishCounter++;
        if (finishCounter == 6)
        {
            if (gameObject.TryGetComponent(out ObjectPooler pooler))
            {
                pooler.StopAllCoroutines();
            }
            finishRingSpawnPos = new Vector3(0, 0, pooler.Distance.z + 6);
            Instantiate(finishRing, finishRingSpawnPos, Quaternion.identity);
            finishCounter++;
        }
    }

    private void RingHit(Damager damager)
    {
        ringController.StopInputs();
        ringController.StopAllCoroutines();
        ScoreSave();
        CallText("TRY AGAIN");
    }

    private void Finish(FinishPoint finishPoint)
    {
        exitControlBoundary.enabled = false;
        ScoreSave();
        ringController.StopInputs();
        CallText("CLEARED");
        controlFinish = true;
    }

    private void ScoreSave()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScoreSave", bestScore);
        }
    }

    private void GetScore(Collectible collectible)
    {
        score++;
        UIGlassBar.Instance.SetValue(score / 10);
        scoreText.text = " " + score;
    }

    private void OnDestroy()
    {
        FinishControl.OnEnterFinishControl -= FinishCounter;
        Damager.OnDamagerEnter -= RingHit;
        FinishPoint.OnEnterFinish -= Finish;
        InputEventHandler.PointerDowned -= TapToStart;
        Collectible.OnEnterCollectible -= GetScore;

    }
}
