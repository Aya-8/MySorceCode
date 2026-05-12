using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class PuddingController: MonoBehaviour
{
    [SerializeField] private SliderChanger sliderChanger;
    [SerializeField] private ColliderManager cm;

    [Header("Jump Setting")]
    [SerializeField]
    private float jumpPower = 35;
    [SerializeField]
    private float sideJumpPower = 15f;
    [SerializeField]
    private float velocityThreshold = 1f;

    [Header("Movement Setting")]
    [SerializeField]
    private float speed = 5;


    [Header("Gravity Setting")]
    [SerializeField]
    private float extraGravity = 12f;
    
    private Rigidbody rb;
    
    private bool isGrounded = false;

    [SerializeField]
    private SpoonManager spoonManager;
    [SerializeField]
    private ChickenManager chickenManager;
    
    [SerializeField] private PuddingSoundController puddingSoundController;

    private PuddingMoveInput move = new PuddingMoveInput();

    private bool jumpingStop = false;

    private bool isStopped = false;
    public bool IsStopped
    {
        get => isStopped;
        set => isStopped = value;
    }

    private float n = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sliderChanger = FindFirstObjectByType<SliderChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped) return;
        if (cm.GetIsScratched()) n = 0.3f;
        else n = 1f;
        
        move.shouldSignature = ControllerKey.GetKeyDownUp();//A = 0

        //ジャンプ操作
        //Debug.Log("shouldJump: " + ControllerKey.GetKeyDownUp());
        if (ControllerKey.GetKeyDownUp() && isGrounded)//Y
        {
            move.shouldJump = true;
            Debug.Log("ジャンプ");
        }

        //右に進む
        if (ControllerKey.GetKeyDownRight())
        {
            move.shouldRightJump = true;
        }
        move.shouldRightDush = ControllerKey.GetKeyRight();

        //左に進む
        if (ControllerKey.GetKeyDownLeft())
        {
            move.shouldLeftJump = true;
        }
        move.shouldLeftDush = ControllerKey.GetKeyLeft();
    }

    private void OnCollisionEnter(Collision collision)
    {
        puddingSoundController.PuddingLand.Play();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            isGrounded = false;
        }
    }


    void FixedUpdate()//物理演算に使う処理はFixed*を使うらしい
    {
        if (isStopped) return;
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }

        if (rb.linearVelocity.y < velocityThreshold)
        {
            rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }

        if (move.shouldJump)
        {
            Jump();
            move.shouldJump = false;
        }


        if (move.shouldRightJump)
        {
            Right();
            move.shouldRightJump = false;
        }

        if (move.shouldLeftJump)
        {
            Left();
            move.shouldLeftJump = false;
        }

        if (move.shouldRightDush)
        {
            RightDush();
            move.shouldRightDush = false;
        }

        if (move.shouldLeftDush)
        {
            LeftDush();
            move.shouldLeftDush = false;
        }

    }
    async UniTask Jump()
    {
        if (jumpingStop) return;
        jumpingStop = true;
        puddingSoundController.PuddingJump.Play();

        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        await UniTask.Delay(200);
        jumpingStop = false;
    }

    

    void Right()
    {
        if(!puddingSoundController.PuddingJump.IsPlaying() && isGrounded)
            puddingSoundController.PuddingJump.Play();
        
        //動き始めのジャンプと小ダッシュ
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * sideJumpPower* n, ForceMode.Impulse);
        }
        rb.AddForce(Vector3.right * sideJumpPower * n, ForceMode.Impulse);
       
    }

    void Left()
    {
        if(!puddingSoundController.PuddingJump.IsPlaying() && isGrounded)
            puddingSoundController.PuddingJump.Play();
        
        //動き始めのジャンプと小ダッシュ
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * sideJumpPower * n, ForceMode.Impulse);
        }
        rb.AddForce(Vector3.left * sideJumpPower * n, ForceMode.Impulse);
        

    }

    void RightDush()
    {
        if(!puddingSoundController.PuddingMove.IsPlaying() && isGrounded)
            puddingSoundController.PuddingMove.Play();
        
        transform.Translate(new Vector3(1, 0.0f, 0).normalized * speed * Time.deltaTime * n);
    }

    void LeftDush()
    {
        if(!puddingSoundController.PuddingMove.IsPlaying() && isGrounded)
            puddingSoundController.PuddingMove.Play();
        
        transform.Translate(new Vector3(-1, 0, 0).normalized * speed * Time.deltaTime * n);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if (collider.gameObject.CompareTag("Enemy"))
        //{

        if (collider.gameObject.CompareTag("Egg"))
        {
            Destroy(collider.gameObject);
            chickenManager.AttackChicken(sliderChanger);
        }

        if (collider.gameObject.CompareTag("Whip"))
        {
            sliderChanger.AddByPercent(0.05f);
            Destroy(collider.gameObject);
            Debug.Log("Whipに当たりました");
        }
    }

    // コモンメソッドに移動させたい。
    private GameObject GetFirstActiveTrueObject(List<GameObject> list) {
        foreach (GameObject obj in list)
        {
            if (obj.activeSelf)
            {
                return obj;
            }
        }
        return null;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
