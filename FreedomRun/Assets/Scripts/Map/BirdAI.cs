using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    float healt = 3;
    GameObject player;

    bool facingRight;

    float startX;

    void Start()
    {
        player = GameObject.Find("Player");
        startX = transform.position.x;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        Vector3 characterScale = transform.localScale*15;
        if (distanceFromPlayer< lineOfSite)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            
        }
        if (startX < transform.position.x && facingRight)
        {
            Flip();
        }
        if (healt == 0)
        {
            Destroy(gameObject);
        }
        
    }
    void Flip()
    {
        facingRight = !facingRight; //facingright'ý deðiline eþitle

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); // Karakterin yüzünü saða ve sola çevirme
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            healt--;
        }
    }
}
