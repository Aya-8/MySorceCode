using UnityEngine;
public class FrontPopUp : MonoBehaviour
{
    [SerializeField] private PopUpFunction _popUpFunction;

    /// <summary>
    /// Windowが閉じていたら最前面で開く
    /// 開いていたら最前面にする
    /// </summary>
    public void BringFront()
    { 
        //オブジェクトを最前面に持ってくる
        transform.SetAsLastSibling();
        
        if (!_popUpFunction.IsActive)
        {
            this._popUpFunction.popUpFunction();
        } 
    }
    
}
