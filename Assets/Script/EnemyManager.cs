using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
	public GameObject bullet;
    public GameObject player;
    public float dodgeBulletOffset;

	// Use this for initialization
	void Start () {

        StartCoroutine_Auto(CreateBullet());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator CreateBullet()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.1f);


            GameObject newBullet = GameObject.Instantiate(bullet) as GameObject;
            Bullet cmpBullet = newBullet.GetComponent<Bullet>();

            Vector3 pos = new Vector3();            
            //pos.x = Random.Range(-10.0f, 10.0f);

            int rnd = Random.Range(0, 100);
            if(rnd < 20)
            {
                cmpBullet.type = BulletType.BULLET_NORMAL;
                pos.x = Random.Range(-10, 10);
            }
            else 
            {
                cmpBullet.type = BulletType.BULLET_DODGE;
                pos.x = player.transform.position.x + Random.Range(-dodgeBulletOffset * 0.5f, dodgeBulletOffset + 0.5f);
            }           


            pos.y = Random.Range(5.0f, 8.0f);
            newBullet.transform.position = pos;
		}
	}
}
