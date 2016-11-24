using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FlyDmgText : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
   // [SerializeField] private Text dmgText;
    void Start()
    {
        //pobranie informacji o animacji (potrzebujemy dlugosc animacji)
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        // zniszczenie obiektu po zakończeniu animacji 
        Destroy(gameObject, clipInfo[0].clip.length);

       
        
       
    }
  
  
    public void SetTextDmg(string damage)
    {

        animator.GetComponent<Text>().text = damage;

    }

}
