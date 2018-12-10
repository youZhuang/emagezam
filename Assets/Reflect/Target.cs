// 模拟激光反射
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public LineRenderer line = null;
    public float speed = 5f;

    private Vector3 I = Vector3.zero;
    private Vector3 N = Vector3.zero;
    private Vector3 R = Vector3.zero;

    /// <summary>
    /// 起始点
    /// </summary>
    public Vector3 startPos = Vector3.zero;

    /// <summary>
    /// 跟目标接触点
    /// </summary>
    public Vector3 hitPos = new Vector3(0, 0, 0);

    /// <summary>
    /// 运动方向
    /// </summary>
    public Vector3 moveVec = new Vector3(1, -1, 0);

    private int lineIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        line.widthMultiplier = .05f;
        InitI();
        line.SetPosition(0, startPos);
        lineIndex++;
    }

    void InitI()
    {
        moveVec = moveVec.normalized;
        startPos = transform.position;

        RaycastHit hit;
        if (Physics.Raycast(startPos, moveVec, out hit))
        {
            Transform go = hit.transform;

            Vector3 point = hit.point;

            hitPos = point;
        }

        I = moveVec;
    }

    public void UpdatePos()
    {
        Vector3 targetPos = transform.position;

        targetPos += moveVec * 0.02f;

        transform.position = targetPos;

        line.positionCount = lineIndex + 1;

        line.SetPosition(lineIndex, targetPos);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos();
    }

    public void CheckTrigger(GameObject hitGo)
    {
        N = hitGo.transform.up;
        Debug.Log("N :" + N);

        R = I - 2 * Vector3.Dot(I, N) * N;  //2 * Vector3.Dot(N, I) * N - I;
        R = R.normalized;

        moveVec = R;

        I = R;


        ++lineIndex;
        //Debug.Log("check trigger :" + I + " " + R);

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter:" + other.gameObject.layer);
        if (other.gameObject.layer == LayerMask.NameToLayer("Plane") )
        {
            CheckTrigger(other.gameObject);
        }
        else
        {
            //失败
        }
    }
}
