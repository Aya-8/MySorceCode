using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PuddingStatusChanger : MonoBehaviour
{
    [SerializeField] private GameObject pudding;

    private PuddingStatus status = new PuddingStatus();
    public PuddingStatus Status => status;

    private bool canSideButton = true;

    private bool canJump = true;


    private Vector3 previousPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousPosition = pudding.transform.position;//初期位置の記録
        status.time = 0;
    }

    // Update is called once per frame
    async void Update()
    {
        if (ControllerKey.GetKeyDownRight() || ControllerKey.GetKeyDownLeft())
        {
            if (!canSideButton) return;
            canSideButton = false;
            status.sideButton++;
            GrobalPlayerScore.sideButton++;
            await UniTask.Delay(200);
            canSideButton = true;
        }

        if(ControllerKey.GetKeyDownUp())
        {
            if (!canJump) return;
            canJump = false;
            status.jump++;
            GrobalPlayerScore.jump++;
            await UniTask.Delay(200);
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("合計移動距離" + status.totalDistance);
            Debug.Log("サイドボタン回数" + status.sideButton);
            Debug.Log("ジャンプ回数" + status.jump);
            Debug.Log("決めポーズ回数" + status.signaturePose);
            Debug.Log("タイム" + status.time);
        }

        status.time += Time.deltaTime;
        GrobalPlayerScore.second = (int)status.time;

        float distance = Vector3.Distance(pudding.transform.position, previousPosition);

        GrobalPlayerScore.totalDistance += distance;

        previousPosition = pudding.transform.position;

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToZero();
        }

    }

    void ResetToZero()
    {
        GrobalPlayerScore.jump = 0;
        GrobalPlayerScore.sideButton = 0;
        GrobalPlayerScore.signaturePose = 0;
        GrobalPlayerScore.totalDistance = 0;
        GrobalPlayerScore.second = 0;
    }

    public void SignaturePose()
    {
        status.signaturePose++;
        GrobalPlayerScore.signaturePose++;
    }
}
