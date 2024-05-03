using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombas : MonoBehaviour
{
    public bool dañoExplosion = false;
    public bool areaExplosion = false;
    public Animator bAnimator;
    // Start is called before the first frame update
    void Start()
    {
        bAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    private void Explosion()
    {
        if(areaExplosion == true)
        {
            bAnimator.Play("BombaActivacion");
            Destroy(this.gameObject);
        }
    }    
}
