using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float moveSpeed=2;
    int score, hscore;
    public Text scoreText, hScoreText;
    bool turnedRight=true;
    public Transform rayOrigin;
    
    GameManager gameManager { get {return FindObjectOfType<GameManager>();}}

    
    delegate void TurnDelegate();
    TurnDelegate turn;
    
    
    void Start()
    {
        //hscore= PlayerPrefs.GetInt("myhscore");
        //hScoreText.text = hscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameStarted)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            
            TurnUsingKeyboard();
            CheckFalling();
        }
        

    }
    
    Vector3 effectPos;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("crystal"))
        {
            effectPos=other.transform.position;
            //MakeScore();
            other.gameObject.SetActive(false);
            //MakeEffect();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject, 6f);
    }
    
    private void TurnUsingKeyboard()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Turn();
        }
    }
    
    private void Turn()
    {
        moveSpeed += Time.deltaTime * 2;   // -> her dönüşte hızlanmak için..
        if (turnedRight)
        {
            transform.Rotate(new Vector3(0, -90, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
        turnedRight = !turnedRight;
    }
    
    
    // When the ball falls, the game restarts
    private void CheckFalling()
    {
        if(!Physics.Raycast(rayOrigin.position,Vector3.down))
        {
            gameManager.RestartGame();
        }
    }
}
