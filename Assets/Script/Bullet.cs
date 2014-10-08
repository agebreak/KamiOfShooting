using UnityEngine;
using System.Collections;


public enum BulletType
{
    BULLET_TYPE_START,
    BULLET_NORMAL = BULLET_TYPE_START,
    BULLET_DODGE,
    BULLET_TYPE_END,
}

public class Bullet : MonoBehaviour {
	public float velocity;
    public Vector2 targetOffset;
    public Sprite[] images;

    public BulletType type;
    
    private Transform player;
    private Vector3 vecDir;
    private Vector3 vecNextPos;

	// Use this for initialization
	void Start () {

        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        if (objPlayer == null)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        player = objPlayer.transform;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = images[(int)type];

        CalcMoveVec();
	}
	
	// Update is called once per frame
	void Update () {

        switch (type)
        {
            case BulletType.BULLET_NORMAL:
                transform.Translate(vecDir * velocity * Time.deltaTime);
                break;
            case BulletType.BULLET_DODGE:
                transform.Translate(vecDir * Time.deltaTime);
                break;            
        }        
    

        if(transform.position.y < -10.0f)
        {
            GameObject.Destroy(gameObject);
        }
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
        GameObject.Destroy(gameObject);
	}

    void CalcMoveVec()
    {
        switch (type)
        {
            case BulletType.BULLET_NORMAL:
                CalcMoveNormal();
                break;
            case BulletType.BULLET_DODGE:
                CalcMoveDodge();
                break;            
        }       
    }

    void CalcMoveNormal()
    {
        vecDir = player.position - transform.position;
        vecDir.Normalize();
    }

    void CalcMoveDodge()
    {
        // Y값 시간 계산
        float yTime = (transform.position.y - player.position.y) / velocity;

        float guessXPos = player.position.x + GameManager.Instance.playerMoveVec * yTime;

        Vector3 targetPosition = new Vector3();

        float targetOffsetX = Random.Range(targetOffset.x, targetOffset.x * 5.0f);
        if (GameManager.Instance.playerMoveVec > 0)
            targetPosition.x = guessXPos + targetOffsetX;
        else if (GameManager.Instance.playerMoveVec < 0)
            targetPosition.x = guessXPos - targetOffsetX;
        else
            targetPosition.x = guessXPos;

        targetPosition.y = player.position.y + targetOffset.y;

        vecDir.x = (targetPosition.x - transform.position.x) / yTime;
        vecDir.y = -velocity;
    }
}
