using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour {

    public Text text;
    public GameData data;

    public void Update()
    {
        text.text = data.score.ToString();
    }

}
