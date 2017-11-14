using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject food;
    public GameData data;
    private int x;
    private int y;

    float leftConstraint;
    float rightConstraint;
    float botConstraint;
    float topConstraint;
    // Use this for initialization
    void Start()
    {

        leftConstraint = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).x;
        rightConstraint = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0.0f)).x;
        botConstraint = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, 0.0f)).y;
        topConstraint = Camera.main.ScreenToWorldPoint(new Vector2(0.0f, Screen.height)).y;
        InvokeRepeating("spawn", 3, 3);
        
    }

    void spawn()
    {
        if (data.GameOver)
            return;
        x =(int) Random.Range(leftConstraint, rightConstraint);
        y = (int)Random.Range(botConstraint, topConstraint);

        Instantiate(food, new Vector2(x, y), Quaternion.identity, this.gameObject.transform);
    }
}
