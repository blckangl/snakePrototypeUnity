using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameData : ScriptableObject {

    public int score;
    public bool GameOver;

    private void OnEnable()
    {
        score = 0;
        GameOver = false;
    }

    
}
