# ブラウザ同士のオンライン対戦ができるChessゲーム

## 概要
- オンライン対戦ができるChessゲーム
- ルームの数は3つまで
- Hololens2とブラウザで対戦も可能(ブランチを「Main」から「Hololens」に変えると、サンプルがあります)

## 開発環境
- OS
  - Mac
- ゲームエンジン
  - Unity 2021.3.7f1
- 通信
  - Photon.Pun2
- 言語
  - C#


## 流れの説明
### 手順① ルームに入室したら先攻・後攻を決めます
- UintyEditorとローカルアプリでのサンプルになります

![スクリーンショット 2022-11-24 23 30 46](https://user-images.githubusercontent.com/81737817/203808557-fa91983b-9012-4165-b21d-a4c2dff7fdc2.png)

### 手順② 先行が銀駒・後攻が金駒

![スクリーンショット 2022-11-24 23 35 17](https://user-images.githubusercontent.com/81737817/203809450-5f73d321-90c4-4d09-8416-eaa8c9c6fefa.png)

### 手順③ その後は交互に手を打っていきます
#### ルール・説明
- 駒の移動可能箇所が黄色で表示される
- 取れる駒があれば赤く表示される
- 自滅はできない(自分のキングを相手に取られる場所には移動させられない)

https://user-images.githubusercontent.com/81737817/203801514-1a5d7ed7-028a-4dbf-8252-cb62f89896d3.mov

### 手順④ チェックメイトになったら「Restart」ボタンが表示されます

![スクリーンショット 2022-11-25 0 22 46](https://user-images.githubusercontent.com/81737817/203822431-c4feb4bd-628b-4290-a275-61507fa2f34f.png)

