# 昔の労働ゲーム(煮熟ゲーム)

インターンシップ応募用に、昔の労働ゲーム(煮熟ゲーム)で自分が書いたスクリプトをまとめたフォルダです。

## 使用技術

- Unity
- C#

## 担当範囲

- 煮熟ゲーム制作

## 収録内容

- `InputChecker.cs`: 現在のゲームフェーズとプレイヤー入力を照合し、正しい入力・間違った入力のイベントを発火します。
- `Controller/InputController.cs`: キーボードとコントローラー入力を上下左右の入力方向として取得します。
- `Controller/CommandController.cs`: 入力方向の履歴から、右回し・左回しのコマンドを判定します。
- `Controller/PGPlayerController.cs`: 判定されたコマンドに応じて、鍋を混ぜる棒の回転方向と速度を制御します。
- `Controller/TimeController.cs`: 右回し、左回し、両方向受付、終了の各フェーズを時間経過で切り替えます。
- `Controller/SoundController.cs`: 入力成功・失敗、説明、粉投入、終了演出などに合わせて効果音を再生します。
- `Controller/InputCommand.cs`: 右回し・左回し・入力なしを表すコマンド定義です。
- `Controller/InputDirection.cs`: 上下左右・入力なしを表す入力方向定義です。
- `Director/PGDirector.cs`: ゲーム全体のフェーズ遷移を管理し、開始演出後の進行や終了後のシーン遷移を行います。
- `Director/Phase.cs`: 煮熟ゲームの進行フェーズを定義します。
- `Enums/StartSequence.cs`: 説明、粉投入、カウントダウンなど、開始前演出の手順を定義します。
- `Pole/PoleMoveAnimation.cs`: 混ぜる棒を指定方向・角度で回転させるアニメーション処理です。
- `Controller/View/ArrowController.cs`: 現在のフェーズに合わせて、入力すべき回転方向の矢印を表示・回転させます。
- `Controller/View/EffectPresenter.cs`: 正しい入力時のパーティクル、失敗時のエラー表示を制御します。
- `Controller/View/EndEffectController.cs`: 終了時のパーティクル、カメラ揺れ、鍋の潰れ演出、終了パネル表示を制御します。
- `Controller/View/grugruAni.cs`: 失敗時に表示するエラー画像の表示・非表示を切り替えます。
- `Controller/View/PowderController.cs`: 開始前シーケンスに合わせて、粉を入れるアニメーションとパーティクルを再生します。
- `Controller/View/SliderController.cs`: 正しい入力回数に応じてスライダーを増加させ、終了時に最大値まで進める演出を行います。
- `Controller/View/UIController.cs`: 説明表示、粉投入、カウントダウン、開始通知などの開始前 UI シーケンスを管理します。
