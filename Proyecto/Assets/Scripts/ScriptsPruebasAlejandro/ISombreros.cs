using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISombreros
{
    public void Shoot();
    public void SetDirection(Direction dir);
    public void SombreroRecogido();

    public GameObject gameObject {  get; }

    public Transform anclajeSuperior { get; set; }
}
