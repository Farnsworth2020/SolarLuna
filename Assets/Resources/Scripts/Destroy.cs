using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField]
    GameObject obgectToDestroy;

    public void DestroyGameObject()
    {
        Debug.Log("VO Destroyed");
        Destroy(obgectToDestroy);
    }
}
