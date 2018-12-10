using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReflection : MonoBehaviour
{
    public LineRenderer line = null;
    public GameObject plane = null;
    public GameObject targetGo = null;
    public GameObject reflectedGo = null;
    public GameObject reflectPointGo = null;

    /// <summary>
    /// 起始点
    /// </summary>
    public Vector3 startPos = Vector3.zero;
    /// <summary>
    /// 运动方向
    /// </summary>
    public Vector3 moveVec = new Vector3(1, -1, 0);

    public Vector3 reflectPos = new Vector3(0, 0, 0);
    private Vector3 I = Vector3.zero;
    private Vector3 N = Vector3.zero;
    private Vector3 R = Vector3.zero;
    private float Imag = 1f;

    bool triggered = false;
    public float speed = 5f;
    private int lineIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //line.enabled = false;
        InitI();

        //line.startWidth = 0.2f;
        line.widthMultiplier = .05f;
    }

    void CheckTrigger ()
    {
        if (!triggered)
        {
            Vector3 curPos = targetGo.transform.position;
            float dis = (reflectPos - curPos).magnitude;
            if (dis < 0.03f)
            {
                UpdateR();
                moveVec = R;
                triggered = true;
            }
        }

    }

    void InitI()
    {
        moveVec = moveVec.normalized;
        startPos = targetGo.transform.position;

        RaycastHit hit;
        if (Physics.Raycast(startPos,moveVec,out hit))
        {
            Transform go = hit.transform;
            
            Vector3 point = hit.point;

            reflectPos = point;
        }

        I = moveVec;
    }

    void UpdateR()
    {
        N = plane.transform.up;

        Debug.Log("N :" + N);

        R = I - 2 * Vector3.Dot(I, N) * N;  //2 * Vector3.Dot(N, I) * N - I;

        R = R.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTrigger();

        Vector3 targetPos = targetGo.transform.position;

        targetPos += moveVec * 0.02f;

        targetGo.transform.position = targetPos;

        line.positionCount = lineIndex + 1;

        line.SetPosition(lineIndex, targetPos);

        ++lineIndex;
    }

    void UpdateLineRender()
    {

    }
}
