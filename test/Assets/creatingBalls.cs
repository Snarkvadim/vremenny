using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class creatingBalls : MonoBehaviour
{
	public GameObject Canvas;
    public GameObject krug;
    public float speed;
	List<GameObject> balls;
    List<int> xmov_cur;
    List<int> xmov_beg;
    List<int> ymov_cur;
    List<int> ymov_beg;
    List<int> numMov;
    public int ballsNum = 0;
    public float moveSpeed = 0.10F;
    public Vector3 resolution_max;
    public Vector3 resolution_min;

    void Start()
    {

        balls = new List<GameObject>();
        xmov_cur = new List<int>();
        ymov_cur = new List<int>();
        xmov_beg = new List<int>();
        ymov_beg = new List<int>();
        numMov = new List<int>();
		Vector3 resolution_max = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width-25), (Screen.height-25), 0));
		Vector3 resolution_min = Camera.main.ScreenToWorldPoint(new Vector3(25, 25, 0));

        for (var i = 0; i < 5; i++)
            {
                var randX = Random.Range(resolution_min.x, resolution_max.x);
                var randY = Random.Range(resolution_min.y, resolution_max.y);
                GameObject currentBall = (GameObject)Instantiate(krug, new Vector3(randX, randY,0), Quaternion.identity);
				currentBall.transform.parent = Canvas.transform ;
                currentBall.name = "krug" + ballsNum;
                balls.Add(currentBall);

                var randNumX = Random.Range(-1000, 1000);
                xmov_beg.Add(randNumX);
                xmov_cur.Add(10 * randNumX);
                var randNumY = Random.Range(-1000, 1000);
                ymov_beg.Add(randNumY);
                ymov_cur.Add(10 * randNumY);
                numMov.Add(0);
                ballsNum++;
            Debug.Log(randX +randY);
            }

    }



    void Update()
    {
       
        Vector3 resolution_max = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width-25), (Screen.height-25), 0));
        Vector3 resolution_min = Camera.main.ScreenToWorldPoint(new Vector3(25, 25, 0));

        if (Input.GetMouseButtonDown(0))
        {
            var mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousPos.z = 0;


            GameObject currentBall = (GameObject)Instantiate(krug, mousPos, Quaternion.identity);
            currentBall.name = "krug" + ballsNum;
			currentBall.transform.parent = Canvas.transform ;
            balls.Add(currentBall);

            var randNumX = Random.Range(-1000, 1000);
            xmov_beg.Add(randNumX);
            xmov_cur.Add(10 * randNumX);
            var randNumY = Random.Range(-1000, 1000);
            ymov_beg.Add(randNumY);
            ymov_cur.Add(10 * randNumY);
            numMov.Add(0);
            ballsNum++;
        }
      

        if (ballsNum > 0)
        {
            for (int i = 0; i < ballsNum; i++)
            {
                balls[i].transform.position += ((new Vector3(xmov_cur[i], ymov_cur[i], 0)) * moveSpeed * Time.deltaTime);

                if (balls[i].transform.position.x > (resolution_max.x))
                {
                    var resX = resolution_max.x;
                    balls[i].transform.position = (new Vector3(resX, balls[i].transform.position.y, 0));
                    xmov_cur[i] *= (-1);
                    xmov_beg[i] *= (-1);
                    xmov_cur[i] -= xmov_beg[i];
                    ymov_cur[i] -= ymov_beg[i];
                    numMov[i]++;
                }
                if (balls[i].transform.position.x < (resolution_min.x))
                {
                    var resX = resolution_min.x;
                    balls[i].transform.position = (new Vector3(resX, balls[i].transform.position.y, 0));
                    xmov_cur[i] *= (-1);
                    xmov_beg[i] *= (-1);
                    xmov_cur[i] -= xmov_beg[i];
                    ymov_cur[i] -= ymov_beg[i];
                    numMov[i]++;
                }
                if (balls[i].transform.position.y > (resolution_max.y))
                {
                    var resY = resolution_max.y;
                    balls[i].transform.position = (new Vector3(balls[i].transform.position.x, resY, 0));
                    ymov_cur[i] *= (-1);
                    ymov_beg[i] *= (-1);
                    xmov_cur[i] -= xmov_beg[i];
                    ymov_cur[i] -= ymov_beg[i];
                    numMov[i]++;
                }
                if (balls[i].transform.position.y < (resolution_min.y))
                {
                    var resY = resolution_min.y;
                    balls[i].transform.position = (new Vector3(balls[i].transform.position.x, resY, 0));
                    ymov_cur[i] *= (-1);
                    ymov_beg[i] *= (-1);
                    xmov_cur[i] -= xmov_beg[i];
                    ymov_cur[i] -= ymov_beg[i];
                    numMov[i]++;
                }

                if (numMov[i] > 9)
                {

                    xmov_cur[i] = 0;
                    xmov_beg[i] = 0;
                    ymov_cur[i] = 0;
                    ymov_beg[i] = 0;
                }
            }
        }
    }


}
