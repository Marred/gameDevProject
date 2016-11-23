using UnityEngine;
using System.Collections;
[System.Serializable]
public class EnemyHP : MonoBehaviour
{
    [SerializeField]private GameObject dmgtext;
    [SerializeField]private float enemyHealth = 15f;
    private Vector3 polozenie = new Vector3(0, 0, 0); // w celu korekty polozenia tekstu mozna zmieniac
    private Transform enemyTransform;
  
    void Start()
    {
        
        enemyTransform = GetComponent<Transform>();
     
    }



    void Damage(float dmg)
    {
        GameObject canvas = GameObject.Find("EnemyCanvas");
        GameObject ObjTextDmg = Instantiate(dmgtext, enemyTransform.position + polozenie, Quaternion.identity) as GameObject;
     
        //przyporządkowanie naszego tekstu dla pobranego canvasa
        ObjTextDmg.transform.SetParent(canvas.transform); 

       //wyslanie wiadomości do naszego obiektu(tekst) z nazwa funkcji do wykonania oraz wartoscia obrazen
        ObjTextDmg.SendMessage("SetTextDmg", dmg.ToString());
        
        //zadawanie obrazen
        enemyHealth -= dmg; 

      //  Debug.Log("EnemyHP.cs: " + enemyHealth);

        if (enemyHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
