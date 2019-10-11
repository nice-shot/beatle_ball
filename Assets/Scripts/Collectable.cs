using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField] int size = 1;
    
    public int Size
    {
        get { return size; }
    }
}
