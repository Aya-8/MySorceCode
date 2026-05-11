using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 3000.0f;
    GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;　　　　　　　　//フレームレートを６０に固定する（お守り）
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //学食を取るために手を前に出す
        if (Input.GetMouseButtonDown(0))
        {
            this.rigid2D.AddForce(transform.up * this.jumpForce);
            Debug.Log("jump");
        }
    }

    //ScoreManagerにタグを渡す
    void OnTriggerEnter2D(Collider2D other)
    {
        string dish = other.gameObject.tag;
        Debug.Log("料理名:" + dish);
        if (ScoreManager.Instance == null) { Debug.Log("ScoreManagerがないよ"); }
        ScoreManager.Instance.DecideDish(dish);
        Destroy(other.gameObject, 0.2f);

    }

}
