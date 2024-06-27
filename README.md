# RoL4YChecker 0.1.1

このツールは、Ragnarok Online のジューンブライドイベントで登場した MD『Letter For You』(2024年版) のいわゆる街クエ部分の進行状況を可視化するものです。

![image](https://github.com/majo-sama/RoL4YChecker/assets/157029319/8876eac9-f829-425f-9c2d-08c0c5a1cb0b)


## ダウンロード

- [RoL4YChecker.zip](https://github.com/majo-sama/RoL4YChecker/releases/download/0.1.1/RoL4YChecker.zip)
  - 通常版
- [RoL4UChecker_Debug.zip](https://github.com/majo-sama/RoL4YChecker/releases/download/0.1.1/RoL4UChecker_Debug.zip)
  - Windows Defender等によりダウンロード/実行がブロックされる場合は、こちらをお試しください

## 使い方

### 初期設定

1. 起動すると表示される半透明のダイアログを右クリックし、 `設定` を選択してください
2. ROのチャットログの保存先フォルダを指定し、 `OK` を押してください
  - 通常は `C:\Gravity\Ragnarok\Chat\` 等ですが、インストール先により異なります

### 進行状況の反映

1. 『Letter For You』の MD の進行中に、ゲーム内で `/savechat` コマンドを実行してください
  - ショートカットに `/savechat` を登録し、これを実行することを推奨します
2. 画面にクエストの進行状況が反映されます

※ このツールの動作のためには、ゲーム内で `クエスト情報表示` が有効なチャットウィンドウが1つ以上存在する必要があります。

## クエスト情報の修正について

クエスト情報は `quests.csv` ファイルに記載されたものを使用しています。
攻略サイト等によってクエストの番号が異なるので、必要に応じて修正してください。

## 注意

フルスクリーンモードで使うとロクなことにならないと思うので、ROはウィンドウモード推奨です。

## 連絡先

不具合・意見等があれば 丼 `@majo-sama` まで連絡または Issue を立てるなり PR 作るなりしてください。


## 更新履歴

- 0.1.1
  - クエストのフェーズが進行しない場合がある問題を修正
