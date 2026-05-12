using UnityEngine;
using UnityEngine.UI;

public class grugruAni : MonoBehaviour
{
    private Image errorImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        errorImage = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGru()
    {
        errorImage.GetComponent<Image>().enabled = true;
        errorImage.enabled = true;
    }

    public void HideGru()
    {
        errorImage.enabled = false;
        errorImage.GetComponent<Image>().enabled = false;
    }
}
