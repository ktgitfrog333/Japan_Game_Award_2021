2021/04/10 19:41
ReadMeを少し修正

2021/04/10 19:20
★テープ長さに関する縮小・拡大処理の実装
Xボタン長押し・マウスホイール上回転でテープのサイズを拡大
最大のテープサイズを「等倍～4倍」設定（要調整）
Yボタン長押し・マウスホイール下回転でテープのサイズを縮小
最小のテープサイズを「等倍～4倍」設定（要調整）
★ゴール
プレイヤーがゴールの下に足場がある状態でたどり着くとゴール

・プレイヤー、壁、ゴールのゲームオブジェクトへタグ設定
main_scene\CalamariTape\Assets\Scenes\main.unity
・ゴール判定スクリプトクラス修正
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・プレイヤー操作スクリプトクラスへ拡大・縮小ロジック追加
main_scene\CalamariTape\Assets\Scripts\MoveController.cs
・InputManagerにてScaleUp（拡大）・ScaleDown（縮小）のキー設定
main_scene\CalamariTape\ProjectSettings\InputManager.asset
・ゴール判定用のタグを設定
main_scene\CalamariTape\ProjectSettings\TagManager.asset

2021/04/10 11:59
★ジャンプの実装
Ａ/スペースの入力の取得
高さ(プレイヤー最小サイズ)２.５マス分（２マス登れる分）移動
高さ(プレイヤー最大サイズ)４.５マス分（４マス登れる分）移動
ジャンプ中の最大横移動幅は４マス先までにする（※ここは要調整）
空中にいる場合はジャンプ不可
★他
プレイヤーの壁に衝突した際の判定部分の不具合を修正

・デモステージ少し拡張
main_scene\CalamariTape\Assets\Scenes\main.unity
・プレイヤー操作ロジックにジャンプを実装
・壁にぶつかっても止まっていなかった処理を止まるように修正
main_scene\CalamariTape\Assets\Scripts\MoveController.cs
・InputManagerにてコントーラーのAボタンを指定
main_scene\CalamariTape\ProjectSettings\InputManager.asset
・ゲームオブジェクトへ壁判定用のタグ追加
main_scene\CalamariTape\ProjectSettings\TagManager.asset
・壁のゲームオブジェクトのプレハブ作成
main_scene\CalamariTape\Assets\Prefabs\CubeWall.prefab

2021/04/08 20:56
★コリジョン
プレイヤーとブロック、テープ、ゴール判定の仮当て
main_scene\CalamariTape\Assets\Prefabs\GoalTrigger.prefab
main_scene\CalamariTape\Assets\Prefabs\Tape.prefab
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
★ブロック
壁にぶつかった際、プレイヤーが通れなくする
プレイヤーが上に乗れるようにする
main_scene\CalamariTape\Assets\Scenes\main.unity
・プレイヤーの各モード情報管理スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\PlayerManager.cs

2021/04/07 19:05
★ツルツルモードの実装 - 移動の実装
・スティック入力を取得
　自動でプレイヤーが移動する処理。（速さ100で設定 等速で3マス/1秒移動）
　※対応漏れ箇所の追加
main_scene\CalamariTape\Assets\Scripts\MoveController.cs

2021/04/06 22:10
★ツルツルモードの実装 - 移動の実装
・プレイヤーオブジェクトへCharacterControllerコンポーネント追加など
main_scene\CalamariTape\Assets\Scenes\main.unity
・スティック入力を取得
　自動でプレイヤーが移動する処理。（速さ100で設定 等速で3マス/1秒移動）
　スティックで入力される割合（-1~1）
　加速度「スティックで入力される割合×速さ×deltatime」を毎フレーム加算
　テープ拡大時の最高速の設定　4００で２マス/１秒で動く
main_scene\CalamariTape\Assets\Scripts\MoveController.cs

★ツルツルモードの実装 - 切り替えの実装
・切り替えの際に位置がリセットされる不具合を修正
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs

2021/04/05 22:43
★ツルツルモードの実装 - 切り替えの実装
・プレイヤー用のオブジェクト作成して、仮の床オブジェクトを配置
main_scene\CalamariTape\Assets\Scenes\main.unity
・InputManagerにツルツルモード切り替え名「RB」を追加
main_scene\CalamariTape\ProjectSettings\InputManager.asset
・「Rボタンでツルツルモードに切り替え」「再度Rボタン押下でツルツルモード解除の処理。（カラマリモードに切り替え）」
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs

2021/04/04 22:50
★ReadMeの記載が間違っていた為修正

2021/04/04 22:50
★新規プロジェクト作成
・main_sceneフォルダ作成
・main_sceneフォルダへ新規プロジェクの追加（Unity 2020.3.2f1（LTS））とCinemachineインポート
CalamariTape\*
・mainシーン作成
CalamariTape\Assets\Scenes\main.unity
