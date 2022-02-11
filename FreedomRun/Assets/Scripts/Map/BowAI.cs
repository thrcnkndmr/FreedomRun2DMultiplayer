using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAI : MonoBehaviour
{
    public Animator BowAnim;
    public GameObject Arrow;
    public Transform ArrowPosition;
    public float laserLength = 30;
    float healt = 5;
    float timeLeft = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BowAIControl();
        if (healt==0)
        {
            Destroy(gameObject);
        }
    }
    void BowAIControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-4.5f, 3f, 0f), -Vector2.right, laserLength);
        BowAnim.SetBool("bow", false);
        if (hit.collider != null)
        {
            if (hit.transform.name == "Player")
            {
                timeLeft -= Time.deltaTime;
                BowAnim.SetBool("bow", true);
                if (timeLeft <= 0)
                {
                    Instantiate(Arrow, ArrowPosition.transform.position, Quaternion.identity);
                    timeLeft = 2.58f;
                }

            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="bullet")
        {
            healt--;
        }
    }
}
