# Access to your 5Girls♥♥♥♥♥
Access to your 5Girls で自分が書いたスクリプトをまとめたフォルダです。

## 使用技術
- Unity
- C#
- QFramework

## 概要

5人のヒロインと並列して連絡を取りながら、デートや交際を進めていく2D恋愛シミュレーションノベルゲームです。ゲーム内の連絡、好感度、メモ機能などを通して、複数の女の子との関係性を管理しながら物語を進める作品です。

## 担当範囲

- ヒロインからの連絡や好感度の変化を知らせるシステムメッセージの生成と出現
- 物語の進行によって、ヒロインの連絡先が見えるようになる処理
- ヒロインの好感度などを確認できるゲーム内アプリ `LoveChecker` の実装
- セーブ関連処理
- メモアプリ周辺の表示・操作補助

## 収録内容

- `SMManager/GetSystemMessageManager.cs`: システムメッセージの生成・管理関連
- `SaveScripts`: セーブ・ロード関連
- `ForConversation/Views`: トーク画面、ヒロイン名、連絡先表示関連
- `QFramework/Views/Window/LoveChecker`: LoveChecker アプリ関連
- `QFramework/Views/Window/MemoApplication`: メモアプリ周辺の表示・操作補助
- `QFramework/Views/Window/FrontPopUp.cs`: 前面ポップアップ表示関連
