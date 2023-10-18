using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyScript : BaseReporter
{
    protected override void ReporterFunction(GameObject playerObj)
    {
        Destroy(gameObject, 3f);
        //Destroy(gameObject);
    }
}
