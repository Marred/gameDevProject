using UnityEngine;
using System.Collections;

public class LaserBolt : MonoBehaviour
{
    public float lifeTime = 3;
    void Start()
    {
        Destroy(gameObject, lifeTime);
        // niszczy obiekt po podanym czasie - nalezy przypisać skrypt do odpowienich prefabs
    }


}

