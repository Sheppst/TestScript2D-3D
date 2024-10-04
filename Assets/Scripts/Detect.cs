using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public float MaxVis = 10;

    [SerializeField] private Transform Target;

    private Transform Boundaries;
    private Transform TargTop;
    private Transform TargBot;
    private Transform TargLeft;
    private Transform TargRight;

    private Transform POV;
    private Transform TopLimit;
    private Transform BotLimit;
    private Transform LeftLimit;
    private Transform RightLimit;

    private bool IsDetect;
    private int TurnVis = 1;
    // Start is called before the first frame update
    void Start()
    {
        POV = transform.GetChild(0);
        TopLimit = POV?.GetChild(0);
        BotLimit = POV?.GetChild(1);
        if (POV.childCount == 4 )
        {
            LeftLimit = POV?.GetChild(2);
            RightLimit = POV?.GetChild(3);
        }

        if (Target !=  null) 
        {
            Boundaries = Target.GetChild(0);
            TargTop = Boundaries.GetChild(0);
            TargBot = Boundaries.GetChild(1);
            if (Boundaries.childCount == 4 ) 
            {
                TargLeft = Boundaries.GetChild(2);
                TargRight = Boundaries.GetChild(3);
            }
        }

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
        RaycastHit2D hit = Physics2D.Raycast(POV.position, Target.position);
        return false;
    }

    private float BoundRay()
    {
        return MaxVis;
    }

    public bool IsDetected()
    {
        return IsDetect;
    }
}
