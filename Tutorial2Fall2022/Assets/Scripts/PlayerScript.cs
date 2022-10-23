using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpForce;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private int scoreValue;
    private int livesValue;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;
        SetCountText();
        winTextObject.SetActive(false);
        SetCountText();
        loseTextObject.SetActive(false);
    }

    void SetCountText()
    {
        ScoreText.text = "Score: " + scoreValue.ToString();
        LivesText.text = "Lives: " + livesValue.ToString();
    }

    void Update()
    {
        if (scoreValue == 5)
        {
            winTextObject.SetActive(true);
            Destroy(gameObject);
        }
        
        if (livesValue == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }

    }

  

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            ScoreText.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if(collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetCountText();
        }

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround)
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }

    }

}
