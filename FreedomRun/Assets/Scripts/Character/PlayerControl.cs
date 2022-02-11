using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    public Transform FirePoint; // Mermi ��k�� noktas�
    public GameObject bullet; //Mermi nesnesi
    int counter = 1; //Karakterin zemine temas� i�in kontrol sayac�
    int diamondcounter = 0;
    public float speed = 10f; //Karakter h�z�
    Rigidbody2D rb;
    public float jump; //Karakter z�plama y�ksekli�i
    bool StartBtnStatus = false; //Ba�lama esnas�ndaki buton durumu kontrol�
    bool JumpStatus = false; //Oyun ba�lang�� esnas�nda jump kontrol�
    float PlayerlaserLength = 0.1f;
    float PlayerlaserLength2 = 0.1f;
    [SerializeField] private Transform player;


    Vector2 CharacterStartPosition;
    Vector2 CharacterUpdatePosition;

    private const float Distance = 500f;

    public GameObject StartBtngameo; //Start Buton gameobjeti
    public Text DiamondTxtgameo; //Diamond text sayac� gameobjecti
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        CharacterStartPosition = player.position;
        //print(CharacterStartPosition); // Karakter ba�lang�� posisyonunu yazd�r

    }
    private void FixedUpdate()
    {
        if (JumpStatus)
        {
            if (counter == 1)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = new Vector2(0, jump);
                    counter = 0;
                }
            }
        }
    }
    void Shoot()
    {
        Instantiate(bullet, FirePoint.position, FirePoint.rotation);
    }
    void raycastControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1.6f, -0.5f, 0f), Vector2.right, PlayerlaserLength);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, -3.1f, 0f), Vector2.down, PlayerlaserLength2);
        //Debug.DrawRay(transform.position + new Vector3(0, -3.1f, 0f), Vector2.down, Color.cyan, PlayerlaserLength2);
        //Debug.DrawRay(transform.position + new Vector3(1.6f, -0.5f, 0f), Vector2.right, Color.black, PlayerlaserLength);
        if (hit.collider != null)
        {
            if (hit.transform.name == "carry_side" || hit.transform.name == "enemy_pink" || hit.transform.name == "bomb_carry(Clone)" || hit.collider.gameObject.tag == "ground")
            {
                SceneManager.LoadScene(0);
                StartBtngameo.SetActive(true);
            }

        }
        if (hit2.collider != null)
        {
            if (hit2.transform.name == "carry_side" || hit2.transform.name == "enemy_pink" || hit2.transform.name == "bomb_carry(Clone)")
            {
                SceneManager.LoadScene(0);
                StartBtngameo.SetActive(true);
            }
            if (hit2.collider.gameObject.tag == "ground")
            {
                counter = 1;
            }
        }
    }
    private void Update()
    {
        raycastControl();
        if (StartBtnStatus)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Input.GetButtonDown("Fire1")) //Sol t�k kontrol
            {
                Shoot();
            }
        }

        CharacterUpdatePosition = player.position;
        if (Vector2.Distance(CharacterStartPosition, CharacterUpdatePosition) > Distance)
        {
            //print("S�n�r A��ld�"); //2 vector aras�ndaki mesafe > Distance de�erinden b�y�k ise �al���r.
        }
       // DiamondTxtgameo.text = diamondcounter.ToString();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "gold")
        {
            Destroy(col.gameObject);
            diamondcounter = (diamondcounter + 1);
        }
        if (col.tag == "bullet-col" || col.tag == "respawn" || col.tag == "bomb")
        {
            SceneManager.LoadScene(0);
            StartBtngameo.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        /*if (col.tag == "ground")
         {
             counter = 1;
         }*/
    }
    void OnTriggerExit2D(Collider2D col)
    {
        //counter = 0;
    }
    public void StartBtn()
    {
        StartBtnStatus = true;
        StartBtngameo.SetActive(false);
        JumpStatus = true;
    }
}