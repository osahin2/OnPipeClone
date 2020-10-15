using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingController : MonoBehaviour
{
    Vector3 cameraFirstPos, cameraLastPos;
    Vector3 scaleCollisionExit = new Vector3(2.0f, 2.0f, 0f);

    Rigidbody rigidBody;
    GameObject cam;
    GameObject gameController;
    GameController gameControl;

    public GameObject finishRing;
    public float scaleSpeed = 200.0f;
    public float ringSpeed = 5.0f;
    public Vector3 scaleChange = new Vector3(1.0f, 1.0f, 0f);
    public Text scoreText;
    public Text bestScoreText;
    public Text tapToStart;
    public Button coins;
    public Button rings;


    private int score;
    public int Score
    {
        get
        {
            return score;
        }
    }
    int bestScore;

    private bool _endGameControl=false;
    public bool endGameControl
    {
        get
        {
            return _endGameControl;
        }
            
    }

    private bool _finishControl = false;
    public bool finishControl
    {
        get
        {
            return _finishControl;
        }
    }

    int finishCounter=0;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        bestScore = PlayerPrefs.GetInt("bestScoreSave");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameControl = gameController.GetComponent<GameController>();

        cameraFirstPos = cam.transform.position - transform.position;
    }
    
    void FixedUpdate()
    {
        //Change Ring Scale
        if (Input.GetKey(KeyCode.Mouse0))
        {
            scoreText.enabled = true;
            tapToStart.enabled = false;
            coins.gameObject.SetActive(false);
            rings.gameObject.SetActive(false);
            rigidBody.transform.localScale -= scaleChange * scaleSpeed * Time.fixedDeltaTime;
        }
        else if (rigidBody.transform.localScale.x < 100)
        {
            scaleChange = scaleCollisionExit;
            rigidBody.transform.localScale += scaleChange * scaleSpeed * Time.fixedDeltaTime;
        }

        //Ring Move
        Vector3 position = rigidBody.position;
        position.z = position.z + ringSpeed * Time.deltaTime;
        rigidBody.MovePosition(position);

        if (finishCounter == 6)
        {
            gameController.GetComponent<ObjectPooler>().StopAllCoroutines();
            Instantiate(finishRing, new Vector3(0, 0, gameObject.transform.position.z + 18), Quaternion.identity);
            finishCounter++;
        }

        if(_endGameControl)
        {
            gameObject.GetComponent<RingController>().enabled = false;
        }
        
    }
    private void LateUpdate()
    {
        if (!_finishControl)
        {
            //Camera Control
            cameraLastPos = cameraFirstPos + transform.position;
            cam.transform.position = cameraLastPos;
        }
    }

    //Stop script when ring hits obstacles
    public void HitRing()
    {
        _endGameControl = true;
        ScoreSave();
    }

    public void GetScore()
    {
        score++;
        scoreText.text = ""+ score;
    }
    //Finish Line
    public void Finish()
    {
        gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
        ScoreSave();
        _finishControl = true;
    }
    public void FinishCounter()
    {
        finishCounter++;
    }

    void ScoreSave()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScoreSave", bestScore);
        }
    }
    
}
