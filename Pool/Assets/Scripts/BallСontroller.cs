using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallСontroller : MonoBehaviour {



    protected LineRenderer line;
    protected WhiteBall WhiteBall;
    public GameObject ball;
 
    protected NormalBall[] balls;

    // Use this for initialization
    void Start () {
        line = FindObjectOfType<LineRenderer>();
        WhiteBall = FindObjectOfType<WhiteBall>();
 
        balls = FindObjectsOfType<NormalBall>();
    }
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var direction = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
             
            var ballPos = new Vector3(ball.transform.position.x, 0.1f, ball.transform.position.z);
            var mousePos = new Vector3(hit.point.x, 0.1f, hit.point.z);
            line.SetPosition(0, mousePos);
            line.SetPosition(1, ballPos);
            direction = (mousePos - ballPos).normalized;

        }
      //  if(!Input.GetMouseButtonDown(0)) line.gameObject.SetActive(false);

        if (Input.GetMouseButtonUp(0) && line.gameObject.activeSelf)
        {

            line.gameObject.SetActive(false);
            transform.Rotate(Vector3.zero);
            WhiteBall.GetComponent<Rigidbody>().velocity = direction * 20f;
        }

        if (!line.gameObject.activeSelf && WhiteBall.GetComponent<Rigidbody>().velocity.magnitude < 0.5f)
        {
            line.gameObject.SetActive(true);
            WhiteBall.GetComponent<Rigidbody>().velocity=Vector3.zero;

        }
         
    }

    public void Reset()
    {
        WhiteBall.ResetBall();
        foreach (var ball in balls)
        {
            ball.gameObject.SetActive(true);
            ball.ResetBall();
        }
 
    }

}
