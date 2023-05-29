using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    public Transform tpobj;



    private void Update()
    {
        oldmove.tpObject = tpobj;
    }


}
