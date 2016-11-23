using UnityEngine;
using System.Collections;

public class EnemyDrop : MonoBehaviour {

    private Vector3 shift = new Vector3(0.5f, 0, 0);

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
            Instantiate(expOrb, transform.position+shift, Quaternion.identity);
            Instantiate(oxygenOrb, transform.position-shift, Quaternion.identity);
        }
        else
        {
            Instantiate(expOrb, transform.position+shift, Quaternion.identity);
            Instantiate(healthOrb, transform.position-shift, Quaternion.identity);
        }
    }

}
