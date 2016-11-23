using UnityEngine;
using System.Collections;

public class EnemyDrop : MonoBehaviour {

    //[SerializeField]private Transform spawnPlace;
    //[SerializeField]private int dropChance;

    [SerializeField]GameObject expOrb;
    [SerializeField]GameObject oxygenOrb;
    [SerializeField]GameObject healthOrb;

    void OnDestroy()
    {
        int randomChance = Random.Range(0, 101);

        if(randomChance<70)
        {
            Instantiate(expOrb, transform.position, Quaternion.identity);
        }
        else if(randomChance<85)
        {
            Instantiate(expOrb, transform.position, Quaternion.identity);
            Instantiate(oxygenOrb, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(expOrb, transform.position, Quaternion.identity);
            Instantiate(healthOrb, transform.position, Quaternion.identity);
        }
    }

}
