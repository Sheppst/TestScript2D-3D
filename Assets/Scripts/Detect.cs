using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{

    [SerializeField] private Transform Target;
    [SerializeField] private Transform DebugObj;

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
        Vector2 target = new Vector2(Detectfollow(Target.position.x), BoundRay(Target.position.y));
        RaycastHit2D hit = Physics2D.Raycast(POV.position, target, Vector3.Distance(target, POV.position));
        Debug.DrawLine(POV.position, target, Color.red);
        if (hit && hit.transform.tag == "Player")
        {
            Debug.DrawLine(POV.position, hit.point, Color.yellow);
            return true;
        }
        return false;
    }


    //(float ? float : float)

    private float BoundRay(float y)
    {
        float MaxVis = 0;
        if (y > POV.position.y && !(CheckBound(Target.position.x, TopLimit.position) > Target.position.y))
        {
            MaxVis = CheckBound(Target.position.x, TopLimit.position);
        }
        else if (y < POV.position.y && !(CheckBound(Target.position.x, BotLimit.position) < Target.position.y))
        {
            MaxVis = CheckBound(Target.position.x, BotLimit.position);
        }
        MaxVis = Mathf.Clamp(y, BotLimit.position.y, TopLimit.position.y);
        // MaxVis /= Vector2.Distance(POV.position, Target.position); // Pour créer une véritable sensation de cône de détection
        return MaxVis;
    }

    private float CheckBound(float x, Vector3 Limit) // si erreur voire l'origine
    {
        //float y = (Limit.y - POV.position.y) / (Limit.x - POV.position.x) * (x - POV.position.x)  + POV.position.y;
        Vector2 LimtRel = (Limit - POV.position);
        float a = (LimtRel.y /LimtRel.x) * (x - POV.position.x) + POV.position.y;
        DebugObj.position = new Vector2(x, a);
        return a;
    }
    private float Detectfollow(float x)
    {
        float DistVis = Mathf.Clamp(x, POV.position.x, LimitDist.position.x);
        return DistVis;
    }

    public bool IsDetected()
    {
        return IsDetect;
    }
}
