using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragAndShoot : MonoBehaviour
{
    [Header("Movement")]
    public float maxPower;
    float shootPower;
    public float gravity = 1;
    public int jumpcount = 5;

    public bool shootWhileMoving = false;
    public bool forwardDraging = true;
    public bool showLineOnScreen = false;

    Transform direction;
    Rigidbody2D rb;
    LineRenderer line;
    LineRenderer screenLine;

    // Vectors // 
    Vector2 startPosition;
    Vector2 targetPosition;
    Vector2 startMousePos;
    Vector2 currentMousePos;

    bool canShoot = true;

    Renderer capsuleColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        line = GetComponent<LineRenderer>();
        direction = transform.GetChild(0);
        screenLine = direction.GetComponent<LineRenderer>();

        capsuleColor = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
            MouseClick();
        }
        if (Input.GetMouseButton(0))
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
            MouseDrag();

            if (shootWhileMoving) rb.velocity /= (1);

        }

        if (Input.GetMouseButtonUp(0))
        {
            // if (EventSystem.current.currentSelectedGameObject) return;  //ENABLE THIS IF YOU DONT WANT TO IGNORE UI
            MouseRelease();
        }


        if (shootWhileMoving)
            return;

        if (rb.velocity.magnitude < 0.001f)
        {
            jumpcount = 5;
            capsuleColor.material.color = Color.white;
            //rb.velocity = new Vector2(0, 0); //ENABLE THIS IF YOU WANT THE BALL TO STOP IF ITS MOVING SO SLOW
            canShoot = true;
        }
    }

    // MOUSE INPUTS
    void MouseClick()
    {
        if (shootWhileMoving)
        {
            Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.right = dir * 1;

            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            if (canShoot)
            {
                Vector2 dir = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.right = dir * 1;

                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

    }
    void MouseDrag()
    {
        if (shootWhileMoving)
        {
            LookAtShootDirection();
            DrawLine();


            float distance = Vector2.Distance(currentMousePos, startMousePos);


        }
        else
        {
            if (canShoot)
            {
                LookAtShootDirection();
                DrawLine();

                float distance = Vector2.Distance(currentMousePos, startMousePos);

                if (distance > 1)
                {
                    line.enabled = true;
                }
            }
        }

    }
    void MouseRelease()
    {
        if (shootWhileMoving /*&& !EventSystem.current.IsPointerOverGameObject()*/)
        {
            Shoot();
            screenLine.enabled = false;
            line.enabled = false;
        }
        else
        {
            if (canShoot /*&& !EventSystem.current.IsPointerOverGameObject()*/)
            {
                if (jumpcount > 0)
                {

                    jumpcount--;
                    Shoot();
                    screenLine.enabled = false;
                    line.enabled = false;
                    if (jumpcount == 0) capsuleColor.material.color = new Color(40 / 255f, 40 / 255f, 40 / 255f);
                    else if (jumpcount == 1) capsuleColor.material.color = new Color(130 / 255f, 130 / 255f, 130 / 255f);
                    else if (jumpcount == 2) capsuleColor.material.color = new Color(170 / 255f, 170 / 255f, 170 / 255f);
                    else if(jumpcount == 3) capsuleColor.material.color = new Color(200 / 255f, 200 / 255f, 200 / 255f);
                    else if(jumpcount == 4) capsuleColor.material.color = new Color(230 / 255f, 230 / 255f, 230 / 255f);

                }
            }
        }

    }


    // ACTIONS  
    void LookAtShootDirection()
    {
        Vector3 dir = startMousePos - currentMousePos;

        if (forwardDraging)
        {
            transform.right = dir * -1;
        }
        else
        {
            transform.right = dir;
        }


        float dis = Vector2.Distance(startMousePos, currentMousePos);
        dis *= 4;


        if (dis < maxPower)
        {
            direction.localPosition = new Vector2(dis / 6, 0);
            shootPower = dis;
        }
        else
        {
            shootPower = maxPower + jumpcount*2;
            direction.localPosition = new Vector2(maxPower / 6, 0);
        }

    }
    public void Shoot()
    {
        if (jumpcount==0)
        {
            canShoot = false;
            
        }
        
        rb.velocity = transform.right * shootPower;
    }



    void DrawLine()
    {

        startPosition = transform.position;

        line.positionCount = 1;
        line.SetPosition(0, startPosition);


        targetPosition = direction.transform.position;
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        line.positionCount = 2;
        line.SetPosition(1, targetPosition);
    }


    Vector3[] positions;


}
