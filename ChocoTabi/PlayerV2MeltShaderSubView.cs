// Solid,Liquid間の見た目の遷移を滑らかにつなぐためのクラス。

using System;
using UnityEngine;

namespace PlayerV2
{
    [Serializable]
    public sealed class PlayerV2MeltShaderSubView : IPlayerV2SubView
    {
        [SerializeField] SkinnedMeshRenderer _solidMeshRenderer;
        [SerializeField] private MeshRenderer[] _liquidMeshRenderers;
        [SerializeField] private SkinnedMeshRenderer _middleMeshRenderer;
        [SerializeField] private Material _meltMaterial;
        private Material _middleMaterial;

        [Header("Middleが収縮・膨張する速さ")][SerializeField] private float _shiftSpeed = 0.1f;

        private Form _lastForm = Form.Solid;

        private bool _isShift = false;
        private bool _shouldBeLiquid = false;
        private bool _shouldBeSolid = false;

        private float _timer = 0;
        private const float MAX_MELT_MOUNT = 0.7f;
        private const float MAX_ANGLE = 0.5f;
        private const float MIN_MELT_MOUNT = 0.1f;

        public void Initialize(PlayerV2ViewContext context)
        {
            _middleMeshRenderer.enabled = false;

            //_middle用のマテリアルの実体をつくる
            _middleMaterial = new Material(_meltMaterial);
            var mats = _middleMeshRenderer.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = _middleMaterial;
            }
            _middleMeshRenderer.materials = mats;
        }

        public void Apply(PlayerV2Model model, PlayerV2ViewIntent intent)
        {
            _timer += Time.deltaTime;

            // Solid->LiquidへのMiddleの変形
            if (_lastForm == Form.Solid && model.CurrentForm == Form.Liquid)
            {
                if (_isShift)
                {
                    // Middleの変形中にFormは変わってしまったらMiddleを消す
                    _middleMeshRenderer.enabled = false;
                    return;
                }

                _lastForm = Form.Liquid;
                _shouldBeSolid = false; // Solid->Liquid のときはSolidになる方の処理はできないように

                BeginningSolidToLiquid();
            }

            // Liquid->SolidへのMiddleの変形
            if (_lastForm == Form.Liquid && model.CurrentForm == Form.Solid)
            {
                if (_isShift)
                {
                    // Middleの変形中にFormは変わってしまったらMiddleを消す
                    _middleMeshRenderer.enabled = false;
                    return;
                }

                _lastForm = Form.Solid;
                _shouldBeLiquid = false;
                BeginningLiquidToSolid();
            }

            //マテリアルの変数(MeltTimeとRotation)に代入する変数(溶けると固まるで値を増やすのか減らすのか変わる)
            float meltTime = Mathf.Clamp(_timer * _shiftSpeed, 0, MAX_MELT_MOUNT);
            float angleLarger = Mathf.Clamp(_timer * _shiftSpeed, 0, MAX_ANGLE);
            float hardenTime = Mathf.Clamp(MAX_MELT_MOUNT - _timer * _shiftSpeed, 0, MAX_MELT_MOUNT);
            float angleSmaller = Mathf.Clamp(MAX_ANGLE - _timer * _shiftSpeed, 0, MAX_ANGLE);

            if (_shouldBeLiquid)
            {
                // 各種変数を時間経過で増やす
                _middleMaterial.SetFloat("_MeltTime", meltTime);
                _middleMaterial.SetFloat("_Rotation", angleLarger);
            }
            if (_shouldBeSolid)
            {
                // 各種変数を時間経過で減らす
                _middleMaterial.SetFloat("_MeltTime", hardenTime);
                _middleMaterial.SetFloat("_Rotation", angleSmaller);
            }

            if (_shouldBeLiquid && _middleMaterial.GetFloat("_MeltTime") >= MAX_MELT_MOUNT)
            {
                EndingSolidToLiquid();
            }
            else if (_shouldBeSolid && _middleMaterial.GetFloat("_MeltTime") <= MIN_MELT_MOUNT)
            {
                EndingLiquidToSolid();
            }
        }

        private void BeginningSolidToLiquid()
        {
            _isShift = true;
            _shouldBeLiquid = false;
            //Liquidの各MeshRendererをinactive
            foreach (var renderer in _liquidMeshRenderers)
            {
                renderer.enabled = false;
            }
            _middleMeshRenderer.enabled = true;  //MiddleのMeshRendererをactive

            // MiddleについてるMeltShaderを初期化する(MeltTime,Rotationを０にする)
            _middleMaterial.SetFloat("_MeltTime", 0f);
            _middleMaterial.SetFloat("_Rotation", 0f);

            _timer = 0;

            //溶け始めていいようにする(MeltMaterialの値を操作できるようにする)
            _shouldBeLiquid = true;
        }

        private void EndingSolidToLiquid()
        {
            _shouldBeLiquid = false;
            //LiquidのMeshRendererをactiveにする
            foreach (var renderer in _liquidMeshRenderers)
            {
                renderer.enabled = true;
            }
            _middleMeshRenderer.enabled = false; // MiddleのMeshRendererをinactive

            _isShift = false;
        }

        private void BeginningLiquidToSolid()
        {
            _isShift = true;
            _shouldBeSolid = false;
            _shouldBeLiquid = false;

            _middleMeshRenderer.enabled = true; // MiddleのMeshRendererをactive
            _solidMeshRenderer.enabled = false; // SolidのMeshRendererをinactive

            // MiddleについてるMeltShaderを初期化する(MeltTime,Rotationの値を固有の値にする)
            _middleMaterial.SetFloat("_MeltTime", MAX_MELT_MOUNT);
            _middleMaterial.SetFloat("_Rotation", MAX_ANGLE);
            _timer = 0;

            // 固まっていいようにする(マテリアルの値を操作できるようにする)
            _shouldBeSolid = true;
        }

        private void EndingLiquidToSolid()
        {
            _shouldBeSolid = false;

            _middleMeshRenderer.enabled = false; // MiddleのMeshRendererをinactive
            _solidMeshRenderer.enabled = true;  // SolidのMeshRendererをactive

            _isShift = false;
        }
    }
}
