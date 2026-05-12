using UnityEngine;
using UnityEngine.SceneManagement;

public class PuddingDestroy : MonoBehaviour
{
    [SerializeField]
    private Transform dish;

    private bool isTutorial = false;

    void Start()
    {
        if (FindFirstObjectByType<TutorialProcess>() != null)
        {
            isTutorial = true;
        }
    }

    void Update()
    {
        if (this.transform.position.y + 10f < dish.position.y && !isTutorial)
        {
           // SceneManager.LoadScene("DestroyScene");←こっちは落ちたら終わり
            Debug.Log("Tutorial: Pudding Destroyed");
            this.transform.position = new Vector3(0.1f, 2.1f, 3.5f);
        }
        else if (this.transform.position.y + 10f < dish.position.y && isTutorial)
        {
            Debug.Log("Tutorial: Pudding Destroyed");
            this.transform.position = new Vector3(0.1f, 2.1f, 3.5f);
        }
    }
}
