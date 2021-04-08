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
