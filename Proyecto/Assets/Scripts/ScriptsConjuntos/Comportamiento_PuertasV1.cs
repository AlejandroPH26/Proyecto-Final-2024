using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comportamiento_PuertasV1 : MonoBehaviour
{
    public Animation dAnimator;
    // Start is called before the first frame update
    void Start()
    {
        dAnimator = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AbriendoPuertas()
    {
        dAnimator.Play("OpeningDoors");
    }
    private void CerrandoPuertas()
    {
        dAnimator.Play("ClosingDoors");
    }
}
