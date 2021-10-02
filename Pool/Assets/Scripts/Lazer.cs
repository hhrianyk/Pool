using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{

    static public Lazer instance;
    public GameObject LinePrefab;
    float maxStepDistance = 20;


    List<WhiteBall> lasers = new List<WhiteBall>();
    List<GameObject> lines = new List<GameObject>();

    public void AddLazer(WhiteBall ball) { lasers.Add(ball); }


    void RemoveOldLine()
    {
        if(lines.Count>0)
        {
            Destroy(lines[lines.Count - 1]);
            lines.RemoveAt(lines.Count - 1);
            RemoveOldLine();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        RemoveOldLine();
        int lineCount = 0;

       
        foreach (WhiteBall laser in lasers)
        {
            if (lineCount == 0)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {

                    var ballPos = new Vector3(laser.transform.position.x, laser.transform.localScale.y/2, laser.transform.position.z);
                    var mousePos = new Vector3(hit.point.x, laser.transform.localScale.y / 2, hit.point.z);
                    CalcLaserLine(ballPos, (mousePos - ballPos).normalized, lineCount);
                }
            }
             else
             CalcLaserLine(laser.transform.position + laser.transform.forward * 0.6f, laser.transform.forward, lineCount);
        }
    }

    int CalcLaserLine(Vector3 startPos, Vector3 direction,int index)
    {
       
        int result=1;
        RaycastHit hit;
        
        Ray ray = new Ray(startPos, direction);
        bool intersect=Physics.Raycast(ray, out hit, maxStepDistance);

        Vector3 hitPosition = hit.point;
        if(!intersect)
        {
            hitPosition = startPos + direction * maxStepDistance;
        }
        DrawLine(startPos, hitPosition,index);
       
        if (intersect)
        {
            result+=CalcLaserLine(hitPosition, Vector3.Reflect(direction, hit.normal),index+result);
        }
        return result;
    }

    void DrawLine(Vector3 startPos, Vector3 finshPos,int index)
    {
        if (index > 2) return ;
        LineRenderer line = null;
        if(index <lines.Count)
        {
            line = lines[index].GetComponent<LineRenderer>();
        }
        else
        {
            GameObject go = Instantiate(LinePrefab, Vector3.zero, Quaternion.identity);
            line = go.GetComponent<LineRenderer>();
            lines.Add(go);
        }
         
        line.SetPosition(0, startPos);
        line.SetPosition(1, finshPos);
    }
}
