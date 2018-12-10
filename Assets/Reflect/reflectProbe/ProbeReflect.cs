using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeReflect : MonoBehaviour
{

    public ReflectionProbe probe;
    public Transform mirrorPlaneTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var diffY = mirrorPlaneTransform.position.y - Camera.main.transform.position.y;

        this.probe.transform.position = new Vector3(
            Camera.main.transform.position.x,
            mirrorPlaneTransform.position.y + diffY,
            Camera.main.transform.position.z
        );
    }
}
