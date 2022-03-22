using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager pm;
    
    [HideInInspector] public int totalBricks = 0; 
    [HideInInspector] public bool stopShoot = false;
    [HideInInspector] public bool endingStart = false;
    [HideInInspector] public bool isKicked = false;
    [HideInInspector] public bool rotateCamera = false;
    public GameObject rodPiecePrefab,allRodParent,insideBag;
    public GameObject stackHolder, playerHolder;
    public GameObject stackBrick, shootBrick;
    Vector3 stackLocation = Vector3.zero;
    public Transform shootPoint;

    List<GameObject> stacks = new List<GameObject>();
    public int stackCount = -1, stackAdded = 0;
    public GameObject countingCanvas;

    public bool gameStarted = false;
    public float shootTime = 0.15f;

    GameObject[] c_canvas = new GameObject[100];
    GameObject old_canvas = null;
    TextMeshPro tempText;
    public List<GameObject> allRods = new List<GameObject>();
    int j = 0;
    float slowdownFactor = 0.07f, slowdownLenght = 2f;
    float fValue;

    void Awake()
    {
        if (pm == null)
        {
            pm = this;
        }
        
    }
    
    void Start()
    {
        stackLocation = stackHolder.transform.position;
    }

    void Update()
    {
        /*if (Input.GetMouseButtonUp(0))
        {
            InvokeRepeating("ShootTheBrick", 0f, shootTime);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            CancelInvoke("ShootTheBrick");
        }*/
    }

    void ShootTheBrick()
    {
       /* if (stackCount > 0 && gameStarted && (!stopShoot))
        {
            if (!GameManager.gm.feverModeActive)
            {
                Instantiate(shootBrick, shootPoint.transform.position, Quaternion.identity);
                if (!GameManager.MUSIC_OFF)
                {
                    GameObject tempAud = Instantiate(GameManager.gm.shootAudio, transform.position, Quaternion.identity);
                }
                DecreaseStack();
            }
            else
            {
                Instantiate(shootBrick, shootPoint.transform.position, Quaternion.identity);
                Instantiate(shootBrick, shootPoint.transform.position, Quaternion.Euler(0, -7, 0));
                Instantiate(shootBrick, shootPoint.transform.position, Quaternion.Euler(0, 7, 0));
                if (!GameManager.MUSIC_OFF)
                {
                    GameObject tempAud = Instantiate(GameManager.gm.shootAudio, transform.position, Quaternion.identity);
                    tempAud.GetComponent<AudioSource>().pitch = 1.2f;
                }
                DecreaseStack();
            }
        }*/
    }

    public void DecreaseStack()
    {
        Destroy(stacks[stacks.Count - 1]);
        totalBricks--;
        stackCount--;
        //stacks[stackCount - 1] = null;
        stacks.RemoveAt(stacks.Count - 1);
        //allRods.RemoveAt(allRods.Count - 1);
    }

    void CountingTagChange()
    {
        // GameObject.FindGameObjectWithTag("CountCanvas").tag = "Old";
        if (old_canvas != null)
            Destroy(old_canvas);
        
        c_canvas[1].tag = "Old";
        old_canvas = c_canvas[1];
        c_canvas[1] = null;
        stackAdded = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("GoodRod"))
        {
            Destroy(other.gameObject);
            totalBricks++;
            //GameObject rod=  Instantiate(rodPiecePrefab, transform,allRodParent.transform);
            //rod.transform.parent = allRodParent.transform;
            //rod.transform.localPosition = new Vector3(0.165f, 1.165f, totalBricks * 1.2f);
           // rod.transform.localPosition = new Vector3(0,-0.0002f, totalBricks * 1.2f);
            //rod.transform.localScale = new Vector3(7.4f, 7.4f, 74);
            //rod.transform.localScale = new Vector3(0.05f, 0.05f, 1);
            //rod.transform.localRotation = Quaternion.Euler(-180, 0, 0);
            //allRods.Add(rod);
            stackCount++;
            stackLocation.y  =1.1f;
            stackAdded++;


            if (stackCount <= 0)
            {
                stackLocation = new Vector3(0, stackLocation.y, -0.3f);
                stacks.Add(Instantiate(rodPiecePrefab, stackLocation, Quaternion.Euler(0, 90, 0), insideBag.transform));
                stacks[stackCount].transform.localPosition = new Vector3(0, stackLocation.y, -0.3f);
                stacks[stackCount].transform.localScale   = new Vector3(10, 10, 60);
                //stacks[stackCount].transform.localEulerAngles = new Vector3(0,180,0);
            }
            else
            {
                
                stackLocation = new Vector3(0, stacks[stackCount - 1].transform.localPosition.y, stacks[stackCount-1].transform.localPosition.z - 0.2f);
                stacks.Add(Instantiate(rodPiecePrefab, stackLocation, Quaternion.Euler(0, 90, 0), insideBag.transform));
                stacks[stackCount].transform.localPosition = new Vector3(0, stacks[stackCount - 1].transform.localPosition.y, stacks[stackCount - 1].transform.localPosition.z - 0.2f);
                if (stackCount % 3 == 0) { stacks[stackCount].transform.localPosition = new Vector3(0, stacks[stackCount - 3].transform.localPosition.y+0.2f, -0.3f); }
                /*stacks[stackCount].transform.localScale = new Vector3(
                    stacks[stackCount].transform.localScale.x / 30f,
                    stacks[stackCount].transform.localScale.y / 10f,
                    stacks[stackCount].transform.localScale.z / 30f);*/
                //stacks[stackCount].transform.localScale   = new Vector3(0.3f, 1, 1);
                stacks[stackCount].transform.localScale = new Vector3(10, 10, 60);
            }
        }
        else if(other.transform.CompareTag("BadRod"))
        {
            if (totalBricks > 0)
            {
                Destroy(other.gameObject);
                DecreaseStack();
                //allRods.RemoveAt(allRods.Count - 1);
            }
            /*GameObject rod = Instantiate(rodPiecePrefab, transform);
            rod.transform.localPosition = new Vector3(0.165f, 1.165f, totalBricks * 1.2f);
            rod.transform.localScale = new Vector3(7.4f, 7.4f, 74);
            rod.transform.localRotation = Quaternion.Euler(-180, 0, 0);*/
        }

       /* if (other.transform.CompareTag("Block"))
        {
            GameManager.gm.Invoke("ShowRetryButton", 2.5f);
            
            gameStarted = false;
            GetComponent<Animator>().SetBool("die", true);
            GetComponent<CapsuleCollider>().enabled = false;

            GameObject bucket = GameObject.FindGameObjectWithTag("Bucket");
            bucket.transform.SetParent(null);
            bucket.GetComponent<Rigidbody>().isKinematic = false;

            for (int i = 0; i < stacks.Length; i++)
            {
                if (stacks[i] != null)
                {
                    stacks[i].transform.SetParent(null);
                    stacks[i].GetComponentInChildren<Rigidbody>().isKinematic = false;
                }
            }

            if (!GameManager.VIBRATION_OFF)
            {
                Taptic.Failure();
            }
            if (!GameManager.MUSIC_OFF)
            {
                Instantiate(GameManager.gm.failureAudio, transform.position, Quaternion.identity);
            }

        }*/
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("CorrectPosition"))
        {
            if (playerHolder.transform.position.x < 1 && playerHolder.transform.position.x >= -1)
                playerHolder.transform.position = new Vector3(0f, playerHolder.transform.position.y, playerHolder.transform.position.z);
            
            else if (playerHolder.transform.position.x < 3 && playerHolder.transform.position.x >= 1)
                playerHolder.transform.position = new Vector3(2f, playerHolder.transform.position.y, playerHolder.transform.position.z);
            
            else if (playerHolder.transform.position.x < 5 && playerHolder.transform.position.x >= 3)
                playerHolder.transform.position = new Vector3(4f, playerHolder.transform.position.y, playerHolder.transform.position.z);
            
            else if (playerHolder.transform.position.x < -1 && playerHolder.transform.position.x >= -3)
                playerHolder.transform.position = new Vector3(-2f, playerHolder.transform.position.y, playerHolder.transform.position.z);
            
            else if (playerHolder.transform.position.x < -3 && playerHolder.transform.position.x >= -5)
                playerHolder.transform.position = new Vector3(-4f, playerHolder.transform.position.y, playerHolder.transform.position.z);
        }

        if (other.transform.CompareTag("End"))
        {
            gameStarted = false;
            // GameManager.gm.Invoke("ShowNextButton", 3f);
            // GameManager.gm.Invoke("GameComplete", 0.5f);
            // GetComponent<Animator>().SetTrigger("victory");
            GameManager.gm.CallEndPanel();
        }

        if (other.transform.CompareTag("EndingStart"))
        {
            PlayerController.pc.speed = 16f;
            stopShoot = true;
            endingStart = true;
            stackHolder.transform.parent = playerHolder.transform;
            stackHolder.transform.eulerAngles = new Vector3(0, 180f, 0);
            stackHolder.transform.localPosition = new Vector3(0, 0.55f, 2.5f);
            stackHolder.transform.localScale *= 2f;
            Invoke("KickTheStack", 3.0f);
        }

        if (other.transform.CompareTag("Cheque"))
        {
            Destroy(other.gameObject);
            if (!GameManager.MUSIC_OFF)
            {
                Instantiate(GameManager.gm.chequeAudio, other.transform.position, Quaternion.identity);
            }
            
            if (!GameManager.VIBRATION_OFF)
            {
                Taptic.Medium();
            }
            
            Instantiate(GameManager.gm.chequePop, other.transform.position, Quaternion.identity);
            GameManager.gm.score++;
            GameManager.gm.scoreText.text = GameManager.gm.score.ToString();
        }
    }*/

    void KickTheStack()
    {
        stackHolder.transform.localPosition = Vector3.Lerp(stackHolder.transform.localPosition, new Vector3(0, 0.55f, 6f), 0.1f);
        gameStarted = false;
        rotateCamera = true;
        Invoke("DoSlowmotion", 0.2f);
        GetComponent<Animator>().SetTrigger("kick");
        Invoke("ThrowStack", 0.4f);

        Invoke("TrueVariable", 0.75f);
    }

    void TrueVariable()
    {
        isKicked = true;
    }

    void ThrowStack()
    {
        GameManager.gm.Invoke("BlinkBonusGround", 3.0f);
        
        if (!GameManager.VIBRATION_OFF)
        {
            Taptic.Success();
        }
        for (int i = 0; i < stacks.Count; i++)
        {
            if (stacks[i] != null)
            {
                stacks[i].transform.SetParent(null);
                stacks[i].GetComponentInChildren<Rigidbody>().isKinematic = false;
                // stacks[i].GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0, 0, 10 * Time.deltaTime));
            }
        }

        for ( int i = stacks.Count - 1; i >= 0; i--)
        {
            if (stacks[i] != null)
            {
                stacks[i].GetComponentInChildren<Rigidbody>().AddForce(new Vector3(0, 0, (50000 + (i * 5000)) * Time.deltaTime));
            }
        }
    }

    void DoSlowmotion()
    {
        // print("slow");
        Time.timeScale = slowdownFactor; // 0.07f float variable
        fValue = Time.fixedDeltaTime;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        Invoke("normalMotion", 0.13f);
        
    } 
    void normalMotion()
    {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = fValue;
        
    }
    void DoFastmotion()
    {
        Time.timeScale = 7;
        fValue = Time.fixedDeltaTime;
        Time.fixedDeltaTime = Time.timeScale * 2.5f;
        Invoke("normalMotion", 2f);
    }
}
