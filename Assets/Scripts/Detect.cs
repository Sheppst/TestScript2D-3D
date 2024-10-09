using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{

    [SerializeField] private Transform Target;

    private Transform Boundaries;
    private Transform TargTop;
    private Transform TargBot;

    private Transform POV;
    private Transform TopLimit;
    private Transform BotLimit;
    private Transform LimitDist;

    private bool IsDetect;
    private int TurnVis = 1;
    // Start is called before the first frame update
    void Start()
    {
        POV = transform.GetChild(0);
        TopLimit = POV.GetChild(0);
        BotLimit = POV.GetChild(1);
        LimitDist = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null) 
        {
            IsDetect = RayDetectTarget();
        }
    }

    private bool RayDetectTarget()
    {
        Vector2 target = new Vector2(LimitDist.position.x, BoundRay(Target.position.y));
        RaycastHit2D hit = Physics2D.Raycast(POV.position, target, Vector3.Distance(Target.position, POV.position));
        Debug.DrawLine(POV.position, target, Color.red);
        if (hit && hit.transform.tag == "Player")
        {
            Debug.DrawLine(POV.position, hit.point, Color.yellow);
            return true;
        }
        return false;
    }

    private float BoundRay(float y)
    {
        float MaxVis = Mathf.Clamp(y, BotLimit.position.y, TopLimit.position.y);
        return MaxVis;
    }

    public bool IsDetected()
    {
        return IsDetect;
    }
}
