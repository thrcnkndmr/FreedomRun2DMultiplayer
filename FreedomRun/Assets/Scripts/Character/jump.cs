using UnityEngine;
using System.Collections;

public class jump : MonoBehaviour
{

    public float playerSpeed;  //allows us to be able to change speed in Unity
    public Vector2 jumpHeight;
    [SerializeField]
    float speed;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(playerSpeed * Time.deltaTime, 0f, 0f);          //makes player run
        rb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode2D.Impulse);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))  //makes player jump
        {
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
        }
    }
}