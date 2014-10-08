using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float moveSpeed = 0.1f;

    private Vector3 moveVec = new Vector3();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        moveVec = Vector3.right * moveSpeed * Time.deltaTime;

		float axis = Input.GetAxis("Horizontal");
		if(axis > 0)
		{
            
			transform.Translate(moveVec);
            GameManager.Instance.playerMoveVec = moveSpeed;
		}
		else if(axis < 0)
		{
			transform.Translate(-moveVec);
            GameManager.Instance.playerMoveVec = -moveSpeed;
		}
        else
        {
            GameManager.Instance.playerMoveVec = 0.0f;
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject.Destroy(gameObject);
        GameManager.Instance.OnGameOver();
    }

}
