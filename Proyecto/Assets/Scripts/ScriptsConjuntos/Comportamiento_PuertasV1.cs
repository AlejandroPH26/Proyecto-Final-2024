using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comportamiento_PuertasV1 : MonoBehaviour
{
    public Animator dAnimator;
    // Start is called before the first frame update
    void Start()
    {
        dAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AbriendoPuertas()
    {
        dAnimator.SetBool("HayEnemigos", true);
        //dAnimator.Play("OpeningDoors");
    }
    public void CerrandoPuertas()
    {
        dAnimator.SetBool("HayEnemigos", false);
        //dAnimator.Play("ClosingDoors");
    }
}
