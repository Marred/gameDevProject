using UnityEngine;
using System.Collections;
[System.Serializable]
public class EnemyHP : MonoBehaviour {
    float enemyHealth = 50f;
    [SerializeField]private GameObject enemy;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Damage(float dmg)
    {
        enemyHealth -= dmg;
        Debug.Log(enemyHealth);
        if (enemyHealth<=0)
        {
            Destroy(this.enemy);
        }
    }
}
