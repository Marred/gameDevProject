using UnityEngine;
using System.Collections;

public class LvlUpAnim : MonoBehaviour {
    [SerializeField] private Animator animator;
   
    void Start () {
        //pobranie informacji o animacji (potrzebujemy dlugosc animacji)
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        // zniszczenie obiektu po zakończeniu animacji 
        Destroy(gameObject, clipInfo[0].clip.length);
    }
	
	
}
