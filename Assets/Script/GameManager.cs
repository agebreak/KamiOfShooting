using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Rect rtTime;

    public float playerMoveVec = 0.0f;

    private float playTime = 0.0f;
    private bool gameOver = false;    

    public static GameManager Instance;
    

	// Use this for initialization
	void Start () {
        Instance = this;
        playTime = 0.0f;        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver)
            return;

        playTime += Time.deltaTime;
	
	}

    void OnGUI()
    {
        string time = "TIME : ";
        time += playTime.ToString("F2");

        GUI.Label(rtTime, time);
    }

    public void OnGameOver()
    {
        gameOver = true;
    }
}
