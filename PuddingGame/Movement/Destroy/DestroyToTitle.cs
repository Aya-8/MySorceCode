using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class DestroyToTitle : MonoBehaviour
{
    private float time = 2f;

    private bool canInput = false;

    void Start()
    {
        UniTask.Delay((int)time * 1000).ContinueWith(() => canInput = true);
    }

    void Update()
    {
        if (canInput && ControllerKey.GetKeyDownUp())
        {
            SceneManager.LoadScene("Title");
        }

        if (canInput && ControllerKey.GetKeyDownDown())
        {
            Debug.Log("↓が押されました。");
            SceneManager.LoadScene("MasterGameScene");
        }
    }
}
