using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour {
    Rigidbody2D rigid;
    public float speed = 5.0f;
    // Use this for initialization
    public float leftConstraint = 0.0f;
    public float rightConstraint = 0.0f;
    public float topConstraint = 0.0f;
    public float botConstraint = 0.0f;
    public float buffer = 1.0f; // set this so the spaceship disappears offscreen before re-appearing on other side
    public List<GameObject> tail;

    public GameObject bodypart;
    public Vector2 dir = Vector3.zero;
    public bool isSpawned = false;
    public bool canMove = true;
    public GameData data;
	void Start () {
        rigid = GetComponent<Rigidbody2D>();

        leftConstraint = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).x;
        rightConstraint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0.0f)).x;
        botConstraint = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).y;
        topConstraint = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, Screen.height)).y;
        Debug.Log(topConstraint);
        Debug.Log(botConstraint);
        InvokeRepeating("Move", 0.3f, 0.3f);
        tail = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canMove)
        {
            if(dir != Vector2.down)
            {      
            dir = Vector2.up;
            canMove = false;
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.DownArrow) && canMove)
        {
            if (dir != Vector2.up)
            {
                dir = Vector2.down;
                canMove = false;

            }
                
        }
        else
        if (Input.GetKeyDown(KeyCode.LeftArrow) && canMove)
        {
            if (dir != Vector2.right)
            {
   
                dir = Vector2.left;
                canMove = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) &&canMove)
        {
            if (dir != Vector2.left)
            {

                dir = Vector2.right;
                canMove = false;
            }
        }

        // rigid.velocity = dir * speed;

        if (transform.position.x > rightConstraint + 0.5)
        {
            Debug.Log("pass");

            transform.position = new Vector2(leftConstraint, transform.position.y);
        }
        if (transform.position.x < leftConstraint - 0.5)
        {
            transform.position = new Vector2(rightConstraint, transform.position.y);
        }
        if (transform.position.y > topConstraint)
        {


            transform.position = new Vector2(transform.position.x, botConstraint);
        }
        if (transform.position.y < botConstraint)
        {


            transform.position = new Vector2(transform.position.x, topConstraint);
        }

       

    }
    void Move()
    {
        if (data.GameOver)
            return;

        if (isSpawned)
        {
            var temp = Instantiate(bodypart, tail[0].transform.position, Quaternion.identity);
            Destroy(tail[0]);
            tail.RemoveAt(0);
            tail.Insert(0, temp);
            isSpawned = false;
        }
        if (tail.Count > 0)
        {
            var temp = tail[tail.Count - 1];
            temp.transform.position = transform.position;
            tail.RemoveAt(tail.Count - 1);
            tail.Insert(0, temp);

        }
        transform.Translate(dir);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            Destroy(collision.gameObject);
            GameObject t = new GameObject();
            t.transform.position = collision.transform.position;
            tail.Insert(0, t);
            isSpawned = true;
            data.score++;
        }
        if (collision.gameObject.tag == "body")
        {
            data.GameOver = true;
        }
    }
  
}
