using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PuddingAnimation : MonoBehaviour
{
    [SerializeField] private PuddingCounter pc;
    private Vector3 oriScale;
    private bool isShaking = false;
    public float mag = 1.2f;

    PuddingStatusChanger puddingStatusChanger;

    private int time;
    // Start is called before the first frame update
    void Start()
    {
        puddingStatusChanger = GetComponent<PuddingStatusChanger>();
        oriScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (ControllerKey.GetKeyDownDown())
        {
            if (!isShaking && Time.time > time)
            {
                time = (int)Time.time + 3;
                StartCoroutine(PuruPuru(2.0f));

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("spoon") && !pc.isCounter)
        {
            if (!isShaking)
            {
                StartCoroutine(PuruPuru(mag));
            }
        }

        
    }

    IEnumerator PuruPuru(float mag)
    {
        isShaking = true;
        puddingStatusChanger.SignaturePose();
        transform.localScale = oriScale * mag;
        yield return new WaitForSeconds(0.3f);

        transform.localScale = oriScale;
        isShaking = false;
    }
}
