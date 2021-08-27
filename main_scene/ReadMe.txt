2021/08/27 09:08
★縦向きの動く壁（カラマリ/ネンチャク）モード対応
・動く壁の配置変更
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・カラマリモードにて縦向きの動く壁に対する移動制御の追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
main_scene\CalamariTape\Assets\Scripts\CalamariWallMove.cs
・ネンチャクモードにて縦向きの動く壁に対する移動制御の追加
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
main_scene\CalamariTape\Assets\Scripts\NenchakWallMove.cs
・ギミック制御処理の対象に縦向きの動く壁を追加
main_scene\CalamariTape\Assets\Scripts\StopGimmick.cs
・動く壁を動かすスクリプトコンポーネントを定数化
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
main_scene\CalamariTape\Assets\Scripts\Common\ComponentManager.cs
・【削除】動く壁を動かすスクリプト名を変更
main_scene\CalamariTape\Assets\Scripts\MoveVerticalWall.cs
・【追加】動く壁を動かすスクリプト名を変更
main_scene\CalamariTape\Assets\Scripts\MoveWalls.cs
・縦向きの動く壁をプレハブ化
main_scene\CalamariTape\Assets\Prefabs\MoveHorizontalWall.prefab

2021/08/25 9:15
★ネンチャクモードにて横向きの動く壁対応
・横向きの動く壁に範囲外判定オブジェクト追加
main_scene\CalamariTape\Assets\Prefabs\MoveVerticalWall.prefab
・各ステージの情報を変更
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・共通処理の名称変更
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
main_scene\CalamariTape\Assets\Scripts\CalamariWallMove.cs
main_scene\CalamariTape\Assets\Scripts\NotAttached\CalamariStateConf.cs
・ネンチャクモードへ横向きの動く壁
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
main_scene\CalamariTape\Assets\Scripts\NenchakState.cs
main_scene\CalamariTape\Assets\Scripts\NenchakScaler.cs
main_scene\CalamariTape\Assets\Scripts\NenchakWallMove.cs

2021/08/22 14:52
★新ギミックの動く壁をカラマリモードで移動中の処理を追加、メニュー画面表示の際にギミック停止処理を追加
・各ステージにてギミック停止スクリプトを追加
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・カラマリモードにて動く壁を移動中の処理を適用
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・クリア画面表示の際にギミックを停止する処理を追加
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・横向きの動く壁へギミック停止処理を追加
main_scene\CalamariTape\Assets\Scripts\MoveVerticalWall.cs
・ポーズ画面表示の際にギミックを停止・開始する処理を追加
main_scene\CalamariTape\Assets\Scripts\PauseWindowManager.cs
・ギミック停止スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\StopGimmick.cs

2021/08/21 20:34
★（途中）新ギミックの動く壁の実装
・ステージ追加により、セーブデータを更新
main_scene\CalamariTape\Assets\data\data.json
・各ステージのカラマリモードのコンポーネント情報を更新
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・カラマリモードのプレイヤー情報を各コンポネントへ分割
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
main_scene\CalamariTape\Assets\Scripts\CalamariState.cs
main_scene\CalamariTape\Assets\Scripts\CalamariScaler.cs
main_scene\CalamariTape\Assets\Scripts\CalamariWallMove.cs
・セーブ処理へ新ステージ「Stage6_Scene」追加
main_scene\CalamariTape\Assets\Scripts\SaveControllerScene.cs
main_scene\CalamariTape\Assets\Scripts\Common\StageNameManager.cs
・動く壁の処理を追加
main_scene\CalamariTape\Assets\Scripts\MoveVerticalWall.cs
main_scene\CalamariTape\Assets\Scripts\Common\WallRunMoveVerticalMode.cs
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
・カラマリモードの大きさ調整処理を別ロジックにて追加
main_scene\CalamariTape\Assets\Scripts\NotAttached\CalamariStateConf.cs

2021/08/13 14:50
★メニュー画面、クリア画面の不具合修正
・各ステージのメニュー画面、クリア画面オブジェクト更新
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・メニュー画面、クリア画面を表示中にクリックにて項目非選択とならないよう修正
main_scene\CalamariTape\Assets\Scripts\ClearManager.cs
main_scene\CalamariTape\Assets\Scripts\Menu.cs
main_scene\CalamariTape\Assets\Scripts\UIController.cs
・チュートリアルメッセージ表示中にメニュー画面表示可能として、メニュー画面表示中はチュートリアルメッセージのテキスト送りを停止する
main_scene\CalamariTape\Assets\Scripts\MessageManager.cs
main_scene\CalamariTape\Assets\Scripts\MessageScrollText.cs
main_scene\CalamariTape\Assets\Scripts\PauseWindowManager.cs
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
・タグ管理スクリプトへチュートリアルメッセージタグを追加
main_scene\CalamariTape\ProjectSettings\TagManager.asset
・ゲームオブジェクトの名前を管理するスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\Common\NameManager.cs

2021/08/12 17:33
★各ステージにてオブジェクトが重なった時に点滅しないよう修正、ブロック系の見た目修正
・各ステージにてオブジェクトが重なった時に点滅しないよう修正、ブロック系の見た目修正
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity

・登れない壁ブロック、登れる壁ブロック、床マテリアルを作成
main_scene\CalamariTape\Assets\Prototype Textures\Materials\
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene\
main_scene\CalamariTape\Assets\Wood Patterns\Wood Patterns Demo\Materials\
main_scene\CalamariTape\Assets\Prototype Textures\Materials\Pink\
main_scene\CalamariTape\Assets\Prototype Textures\Materials\Yellow\
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene\
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene\
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene\
main_scene\CalamariTape\Assets\Wood Patterns\Wood Patterns Demo\Materials\

2021/08/11 19:00
★カラマリモードにて壁移動の振る舞い、耐久値による見た目の変更
・各ステージにてカラマリモードのコンポネント情報の更新、壁オブジェクトのタグ、レイヤー情報の変更
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・カラマリモードにて縦向きの壁を登るor降りる際の挙動を修正、耐久値によるマテリアルを点滅させる処理を追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・カラマリモードの状態管理にてテープのマテリアル情報と耐久値情報を追加
main_scene\CalamariTape\Assets\Scripts\CalamariState.cs
・モードチェンジ処理にてカラマリモードへ変更した際に実行させるメソッドを追加
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・レイヤー情報管理スクリプトへ壁レイヤーインデックスを追加
main_scene\CalamariTape\Assets\Scripts\Common\LayerManager.cs
・壁レイヤーを追加
main_scene\CalamariTape\ProjectSettings\TagManager.asset
・カラマリモードのテープマテリアル用スプライトを作成
main_scene\CalamariTape\Assets\Images\calamari_graphic.png
・カラマリモードのテープマテリアルを作成
main_scene\CalamariTape\Assets\Models\Materials\CalamariBody.mat
・カラマリモードの耐久値管理スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\CalamariHealth.cs

2021/08/10 12:39
★ネンチャクモードにて壁移動の振る舞い、耐久値による見た目の変更
・左スティックの入力状態によって転がらない不具合の修正
main_scene\CalamariTape\Assets\Animations\Scotch_tape_outside.controller
・各ステージにてネンチャクモードのコンポネント情報の更新
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・カラマリモードにてモルモットが動かない不具合の修正
main_scene\CalamariTape\Assets\Scripts\CalamariAnimation.cs
・ツルツルモードにてモルモットが動かない不具合の修正
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruAnimation.cs
・モードチェンジ処理にてネンチャクモードに切り替えた際にモードチェンジメソッド追加
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・ネンチャクモードにて回転モーションを追加、移動方向の調整、耐久ゲージに合わせて外観を変更
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・ネンチャクモードにて移動中にメニュー画面表示でアニメーション停止処理を追加
main_scene\CalamariTape\Assets\Scripts\PauseWindowManager.cs
・プレイヤー情報管理へネンチャクモードのアニメーション処理を追加
main_scene\CalamariTape\Assets\Scripts\PlayerManager.cs
・ネンチャクモードのマテリアルを作成
main_scene\CalamariTape\Assets\Models\Materials\NenchakBody.mat
・ネンチャクモードの回転モーションアニメーションスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\NenchakAnimation.cs
・ネンチャクモードの耐久ゲージ管理スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\NenchakHealth.cs
・ネンチャクモードの状態管理スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\NenchakState.cs

2021/08/08 15:37
★ツルツルモード見た目変更とネンチャクモード移動処理を変更
・モードチェンジ演出オブジェクトをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\ScissorsEffect.prefab
・各ステージにて透明ブロックの配置、チュートリアルステージへメッセージを追加
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・縦向きの壁のクラス名変更の影響
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・モードチェンジ演出効果を強調
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・ネンチャクモードにて透明ブロックの判定を追加
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・透明ブロックのタグ名を追加
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
main_scene\CalamariTape\ProjectSettings\TagManager.asset
・縦向きの透明ブロックの配置位置情報
main_scene\CalamariTape\Assets\Scripts\Common\WallRunHorizontalMode.cs
・横向きの透明ブロックの配置位置情報
main_scene\CalamariTape\Assets\Scripts\Common\WallRunVerticalMode.cs
・ツルツルモードの見た目マテリアル作成
main_scene\CalamariTape\Assets\Models\Materials\Body.mat
main_scene\CalamariTape\Assets\Models\Materials\TsuruTsuruBody.mat
・縦向き透明ブロックをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\ClearHorizontalWall.prefab
・横向き透明ブロックをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\ClearVerticalWall.prefab
・縦向き透明ブロックスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\ClearHorizontalWall.cs
・横向き透明ブロックスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\ClearVerticalWall.cs
・縦向きの壁を登る方向を管理スクリプトクラスのクラス名変更
main_scene\CalamariTape\Assets\Scripts\Common\WallRunHorizontalFrontMode.cs
・縦向き透明ブロック判定スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\NotAttached\NenchakWallHorizontalDecision.cs
・横向き透明ブロック判定スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\NotAttached\NenchakWallVerticalDecision.cs

2021/07/31 17:29
★モードチェンジ演出変更とツルツルモードのモーション修正
・各ステージのプレイヤー情報更新（ツルツルモードのコンポネント追加）
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・カラマリモードにてコメント修正
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ゴール時にアニメーションを止める
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・チュートリアルメッセージ表示時にアニメーションを止める
main_scene\CalamariTape\Assets\Scripts\MessageManager.cs
・モードチェンジ時にエフェクト発生（ハサミ追尾は廃止）
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・メニュー画面表示時にアニメーションを止める
main_scene\CalamariTape\Assets\Scripts\PauseWindowManager.cs
・ビルド画面用のデバッグ機能コメントアウト
main_scene\CalamariTape\Assets\Scripts\Player_Data_Main.cs
・プレイヤー管理機能へツルツルモードのアクション制御スクリプト追加
main_scene\CalamariTape\Assets\Scripts\PlayerManager.cs
・ツルツルモードの回転モーションをアニメーション化
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruAnimation.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruState.cs
・モードチェンジのパーティクル
main_scene\CalamariTape\Assets\Materials\ScissorsEffect.mat
main_scene\CalamariTape\Assets\Prefabs\ScissorsEffect.prefab

2021/07/28 9:16
★カラマリモードの回転モーション、チュートリアル修正
・各シーンへカラマリモードのスクリプトコンポネント追加
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・回転モーションのアニメーションのループチェック及び移動時の向き調整
main_scene\CalamariTape\Assets\Scripts\CalamariAnimation.cs
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・メニュー画面でゲームに戻る際のSEを変更
main_scene\CalamariTape\Assets\Scripts\GameBack.cs
・チュートリアルメッセージが全文表示されたらスキップSEを出さない
main_scene\CalamariTape\Assets\Scripts\MessageManager.cs
main_scene\CalamariTape\Assets\Scripts\MessageScrollText.cs

2021/07/25 11:41
★カラマリモードの転がりモーション修正
・カラマリモードへアニメーション部分を別ロジックへ切り分け
main_scene\CalamariTape\Assets\Scenes\main.unity
・カラマリモードの転がりモーションへAnimator適用
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
main_scene\CalamariTape\Assets\Scripts\MessageManager.cs
main_scene\CalamariTape\Assets\Scripts\PlayerManager.cs
main_scene\CalamariTape\Assets\Animations\CalamariTapeOutside.anim
main_scene\CalamariTape\Assets\Animations\CalamariTapeOutsideStanding.anim
main_scene\CalamariTape\Assets\Animations\Scotch_tape_outside.controller
main_scene\CalamariTape\Assets\Scripts\CalamariAnimation.cs
main_scene\CalamariTape\Assets\Scripts\CalamariState.cs
・セーブロジック検証
main_scene\CalamariTape\Assets\Scripts\Player_Data_Main.cs

2021/07/22 13:34
★デバッグ機能の追加とゴール演出の修正
・ゴールトリガーにデバッグの追加
main_scene\CalamariTape\Assets\Prefabs\GoalTrigger.prefab
・デバッグ画面用のUIを追加
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・壁に対する修正？
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・デバッグ処理の追加
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・デバッグUIのPrefab化
main_scene\CalamariTape\Assets\Prefabs\DebugUI.prefab
・ビルド画面のデバッグスクリプトの作成
main_scene\CalamariTape\Assets\Scripts\VisualizeDebugMode.cs
・デバッグ確認用スクリプトのインターフェース作成
main_scene\CalamariTape\Assets\Scripts\Interface\DebugDemo.cs

2021/05/30 15:21
★ゲージ減少SE、ゴール時の別メニュー表示不備の修正
・ゴールトリガーの変更
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・カラマリモードにてゲージ減少SE連続停止フラグのリセット
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ゴール処理スクリプトにてメニュー表示とモードチェンジ禁止処理追加
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・ネンチャクモードにてゲージ減少SE連続停止フラグのリセット
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs

2021/05/30 14:22
★アドバイス画像差し替え
・各ステージのオブジェクト情報の更新
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・各ステージへ画像ファイル追加
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene\game_tutorial_message.png
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene\game_tutorial_message.png
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene\game_tutorial_message.png
main_scene\CalamariTape\Assets\Images\game_tutorial_message_.png
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene\game_tutorial_message_.png
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene\game_tutorial_message_.png
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene\game_tutorial_message_.png

2021/05/30
★デバッグ不備対応
・ゲームオーバーゾーン修正
main_scene\CalamariTape\Assets\Scenes\main.unity
・ゲームオーバーゾーン修正と2～3用のBGM差し替え
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
・ゲームオーバーゾーン修正と4～5用のBGM差し替え
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・カラマリモードにてモードチェンジの際にサイズを元に戻す処理を追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ネンチャクモードにてモードチェンジの際にサイズを元に戻す処理を追加
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・フェード速度を1500へ修正
main_scene\CalamariTape\Assets\Scripts\ScreenDirectInOut.cs
・ツルツルモードにてモードチェンジの際にサイズと速度を元に戻す処理を追加
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・セクション2～3のBGMを追加
main_scene\CalamariTape\Assets\Audio\BGM\bgm_section_02_03.mp3
・セクション4～5のBGMを追加
main_scene\CalamariTape\Assets\Audio\BGM\bgm_section_04_05.mp3

2021/05/29 21:26
★落下時に自動復旧されない不具合を修正
・ステージ１へゲームオーバーゾーンを追加
main_scene\CalamariTape\Assets\Scenes\main.unity
・ステージ２へゲームオーバーゾーンを追加
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
・ステージ３へゲームオーバーゾーンを追加
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
・ステージ４へゲームオーバーゾーンを追加
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
・ステージ５へゲームオーバーゾーンを追加
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・ツルツルモードにて外部から速度を止める処理を追加
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・ゲームオーバーゾーンのプレハブ化
main_scene\CalamariTape\Assets\Prefabs\GameOverLine.prefab
・ゲームオーバーのスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\GameOverLine.cs

2021/05/29 17:18
★ステージ５実装
・ステージ１～４マウスポインタ非表示対応
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
・Stage5_Sceneの作成
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・コングラチュレーションロゴを追加
main_scene\CalamariTape\Assets\Images\game_allclear_logo.png
・カーソル非表示制御のオブジェクト追加
main_scene\CalamariTape\Assets\Prefabs\CursorController.prefab
・カーソル非表示制御のスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\CursorManager.cs

2021/05/29 13:00
★ステージ５仮実装
・Stage4_Sceneの作成
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity

2021/05/29 12:28
★ステージ４実装
・Stage4_Sceneの作成
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
・ビルドへステージ５を追加
main_scene\CalamariTape\ProjectSettings\EditorBuildSettings.asset
・Stage5_Sceneを仮作成
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity

2021/05/29 11:35
★ステージ３実装
・Stage3_Sceneの作成
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
・ビルドへステージ４を追加
main_scene\CalamariTape\ProjectSettings\EditorBuildSettings.asset
・Stage4_Sceneを仮作成
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity

2021/05/29 10:34
★ステージ２実装
・mainシーンを微調整
main_scene\CalamariTape\Assets\Scenes\main.unity
・Stage2_Sceneを作成
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
・５ステージ分のセーブ管理を追加
main_scene\CalamariTape\Assets\Scripts\SaveControllerScene.cs
main_scene\CalamariTape\Assets\Scripts\Common\StageNameManager.cs
・ビルドへステージ３を追加
main_scene\CalamariTape\ProjectSettings\EditorBuildSettings.asset
・ステージ演出のプレハブ化
main_scene\CalamariTape\Assets\Prefabs\StartPoint.prefab
・mainシーンのテンプレート追加
main_scene\CalamariTape\Assets\Scenes\main 1.scenetemplate
・Stage3_Sceneを仮作成
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity

2021/05/29 02:15
★メニュー選択とゴール時のメニュー選択を修正
・mainテンプレート更新
main_scene\CalamariTape\Assets\Scenes\main.scenetemplate
・メニューとゴールメニューの修正
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
・ゲーム開始時にメニュー表示操作を不可にする修正を追加
main_scene\CalamariTape\Assets\Scripts\ScreenDirectInOut.cs
・ステージ選択SEを追加
main_scene\CalamariTape\Assets\Scripts\SfxPlay.cs
・メニューUIの拡大演出を追加
main_scene\CalamariTape\Assets\Scripts\UIController.cs
・メニューセレクトSEファイルを追加
main_scene\CalamariTape\Assets\Audio\SFX\se_select.wav

2021/05/28 23:40
★ステージ１（チュートリアル）実装とステージテンプレート作成
・（※削除予定）テロップのアニメーション
main_scene\CalamariTape\Assets\Animations\Message001.anim
main_scene\CalamariTape\Assets\Animations\Message001.controller
main_scene\CalamariTape\Assets\Images\game_tutorial_serit001.png
・チュートリアルのテロップ作成、SkyBox、マテリアルなど
main_scene\CalamariTape\Assets\Scenes\main.unity
・カラマリモードにて耐久ゲージ減少SE、スケール拡大SEを追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ゴール時のクリアMEを再生
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・メッセージ表示の際にメニュー表示操作とモードチェンジ操作を禁止する処理を追加
main_scene\CalamariTape\Assets\Scripts\MessageManager.cs
・（※削除予定）テロップのアニメーション処理スクリプト
main_scene\CalamariTape\Assets\Scripts\MessageScroll.cs
・ツルツルモードにて移動中でもモード変更可能だった為、再度修正。（仕様認識ミス）
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・ネンチャクモードにて耐久ゲージ減少SE、スケール拡大SEを追加
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・セーブ管理スクリプトクラスにてロード機能を変更
main_scene\CalamariTape\Assets\Scripts\Player_Data_Main.cs
・SFX管理スクリプトクラスにてゲームクリア、耐久ゲージ減少、スケール変更SEを追加
main_scene\CalamariTape\Assets\Scripts\SfxPlay.cs
・ツルツルモードにて耐久ゲージ減少SE、スケール拡大SEを追加。
　移動中でもモード変更可能だった為、再度修正。（仕様認識ミス）
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・ビルド設定へステージ2を追加
main_scene\CalamariTape\ProjectSettings\EditorBuildSettings.asset
・仄暗い雰囲気のSkyboxを追加
main_scene\CalamariTape\Assets\8K Skybox Pack Free\
・レベルデザイン用のマテリアルを追加
main_scene\CalamariTape\Assets\Prototype Textures\Materials\Blue\Blue_1.mat
・mainシーンのシーンテンプレート作成
main_scene\CalamariTape\Assets\Scenes\main.scenetemplate
main_scene\CalamariTape\ProjectSettings\SceneTemplateSettings.json
・ステージ２「Stage2_Scene」シーンを作成
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
・UIのテキスト送りスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\MessageScrollText.cs
・床のマテリアルを追加
main_scene\CalamariTape\Assets\Wood Patterns\

2021/05/23 15:28
★SE・BGMの追加
・SEとBGMのセット
main_scene\CalamariTape\Assets\Scenes\main.unity
・移動SEのセット
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・移動SEのセット
main_scene\CalamariTape\Assets\Scripts\SfxPlay.cs

2021/05/23 12:11
★要調整項目
　地上移動の速さ
　ターン時の慣性
他、不具合修正

・プレイヤーの移動速度設定の変更
main_scene\CalamariTape\Assets\Scenes\main.unity
・カラマリモードにて移動に慣性をもたせる処理とスタート・ゴール時の停止処理を追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・特定の条件でプレイヤーの操作と移動を停止する処理を追加
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・ツルツルモードの移動中はモード切り替えを禁止する処理を追加
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・ネンチャクモードにて移動中の角度を調整
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・シーンのスタート時にプレイヤーの操作を禁止する処理を追加
main_scene\CalamariTape\Assets\Scripts\ScreenDirectInOut.cs
・ツルツルモードにて移動速度の調整と移動中はモード切り替えを禁止する処理を追加
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs

2021/05/23 2:31
★エフェクト
　スタート時のエフェクトを実装
　壁にぶつかったときのエフェクトを実装
　ゴール時エフェクト実装

・パーティクルエフェクトの配置
main_scene\CalamariTape\Assets\Scenes\main.unity
・ゴールスクリプトにて花火を発生される処理を追加
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・ゲームスタートスクリプトにてクラッカー演出処理を追加
main_scene\CalamariTape\Assets\Scripts\ScreenDirectInOut.cs
・ツルツルモードにて壁に衝突した際にエフェクトを出す処理を追加
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・ツルツルモード用のエフェクトスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruEffectController.cs
・花火（衝突）マテリアル
main_scene\CalamariTape\Assets\Materials\Fireworks.mat
・クラッカーの紙マテリアル
main_scene\CalamariTape\Assets\Materials\Paper.mat
・衝突演出のプレハブを作成
main_scene\CalamariTape\Assets\Prefabs\Collision.prefab
・花火のプレハブを作成
main_scene\CalamariTape\Assets\Prefabs\Fireworks.prefab
・クラッカーの紙のプレハブ
main_scene\CalamariTape\Assets\Prefabs\Paper.prefab

2021/05/15 21:20
★セーブロード
　ゲームクリア時セーブデータにクリア情報を書き込む

・セーブデータ管理オブジェクトの配置とゴールイベントトリガーへセーブデータ書き込み処理を呼び出す
main_scene\CalamariTape\Assets\Scenes\main.unity
・ゴールイベントスクリプトにて、セーブ実行とプレイヤーの各モード操作停止処理の不具合修正。
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・セーブデータのjsonファイル配置
main_scene\CalamariTape\Assets\data\data.json
・プレイヤーデータの管理スクリプトクラスを配置（Selectシーン、Titleシーン同様）
main_scene\CalamariTape\Assets\Scripts\Player_Data_Main.cs
・セーブデータ操作を行うスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\SaveControllerScene.cs
・クリア状態管理するenumを作成
main_scene\CalamariTape\Assets\Scripts\Common\StageClearNumber.cs
・ステージ名を管理する定義クラスを作成
main_scene\CalamariTape\Assets\Scripts\Common\StageNameManager.cs

2021/05/15 14:05
★リソースの差し替え
　はさみ
　ゴール

・はさみオブジェクトの配置、ゴールオブジェクトの配置
main_scene\CalamariTape\Assets\Scenes\main.unity
・モードチェンジ実施する際にはさみがテープを切る動きを追加
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・ツルツルモードのスクリプトクラスにて不要な記述を削除
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・ゴールの3Dモデル追加
main_scene\CalamariTape\Assets\Flag
・はさみの3Dモデル追加
main_scene\CalamariTape\Assets\Models\scissors.obj
・はさみのプレハブを作成
main_scene\CalamariTape\Assets\Prefabs\Scissors.prefab
・はさみ挙動制御スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\Scissors.cs
・はさみ衝突挙動制御スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\ScissorsCollision.cs

2021/05/09 22:17
★リソースの差し替え
プレイヤー（ネンチャクモード、ツルツルモード）
　モルモット
　セロハンテープ

・ネンチャクモード、ツルツルモードの設定変更
main_scene\CalamariTape\Assets\Scenes\main.unity
・ネンチャクモードにてスケール処理調整、接地判定の調整、登る壁の判定調整
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・ツルツルモードにてスケール処理調整、接地判定の調整、回転する動き実装
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs

2021/05/09 14:42
★リソースの差し替え
プレイヤー（カラマリモード、ネンチャクモード（途中））
　モルモット
　セロハンテープ

・ゴールトリガーのPrefabへ各モードオブジェクト情報を追加
main_scene\CalamariTape\Assets\Prefabs\GoalTrigger.prefab
・プレイヤーのカラマリモードとネンチャクモードへリソースの差し替え、でもオブジェクト配置、レベルデザインへレイヤー情報追加
main_scene\CalamariTape\Assets\Scenes\main.unity
・カラマリモードにてスケール処理調整、接地判定の調整、回転する動き実装、登る壁の判定調整
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ゴールイベントにてプレイヤー各モード管理方法の修正
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・ネンチャクモードにてリソースの差し替え後の調整（途中）
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・レイヤー情報へ地面判定レイヤー（レイヤー3：Field）を追加
main_scene\CalamariTape\ProjectSettings\TagManager.asset
・モルモットのアニメーターコントローラーを追加
main_scene\CalamariTape\Assets\Animations\Morumotto 1.controller
main_scene\CalamariTape\Assets\Animations\Morumotto.controller
main_scene\CalamariTape\Assets\Animations\MorumottoStanding.anim
・モルモットの3Dモデルを追加
main_scene\CalamariTape\Assets\Models\morumotto.fbx
・セロハンテープの3Dモデルを追加
main_scene\CalamariTape\Assets\Models\Scotch_tape.fbx
main_scene\CalamariTape\Assets\Models\Materials\BodyColor.mat
main_scene\CalamariTape\Assets\Models\Materials\Circle_Color.mat
・セロハンテープ（外側のみ）の3Dモデルを追加
main_scene\CalamariTape\Assets\Models\Scotch_tape_outside.fbx
・レイヤー情報管理スクリプトenumを作成
main_scene\CalamariTape\Assets\Scripts\Common\LayerManager.cs

2021/05/04 20:16
★リソースの差し替え
　ステージ１～３のBGM
・チュートリアルステージ用のシーンへBGMをセット
main_scene\CalamariTape\Assets\Scenes\main.unity

2021/05/04 18:22
★リソースの差し替え
・メニュー画面、クリア画面のUI差し替え
main_scene\CalamariTape\Assets\Scenes\main.unity
・メニュー画面内の遊び方を確認する項目の画像を追加
main_scene\CalamariTape\Assets\Images\joystick_manual_icon.png
main_scene\CalamariTape\Assets\Images\joystick_manual_table.png
main_scene\CalamariTape\Assets\Images\joystick_manual_title.png
main_scene\CalamariTape\Assets\Images\keybord_mouse_manual_icon.png
main_scene\CalamariTape\Assets\Images\keybord_mouse_manual_table.png
main_scene\CalamariTape\Assets\Images\keybord_mouse_manual_title.png
・メニュー画面内の選択アイコン画像を追加
main_scene\CalamariTape\Assets\Textures\morumotto.png

2021/05/03 13:47
★不具合修正　シーン遷移先の指定とフェード演出をセレクト画面に合わせる
・フェード演出用のUIオブジェクトの作成と不要になったフェードUIを削除
main_scene\CalamariTape\Assets\Scenes\main.unity
・ステージをやり直すUIへフェード背景の変更
main_scene\CalamariTape\Assets\Scripts\GameRedo.cs
・他のステージを選ぶUIへフェード背景の変更
main_scene\CalamariTape\Assets\Scripts\GameSelect.cs
・（削除）フェードイン演出スクリプトクラスを削除
main_scene\CalamariTape\Assets\Scripts\UIFadeIn.cs
・（削除）フェードアウト演出スクリプトクラスを削除
main_scene\CalamariTape\Assets\Scripts\UIFadeOut.cs
・フェードインアウト演出スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\ScreenDirectInOut.cs
・ロード画面のイメージを追加
main_scene\CalamariTape\Assets\Textures\eye_chaching.png

2021/05/02 13:21
★不具合修正　カメラ操作の追従
・不要なCinamechineを削除。プレイヤーオブジェクトの設定を変更。
main_scene\CalamariTape\Assets\Scenes\main.unity
・モード切り替えの際に追従カメラ対象を切り替える処理を追加
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・Cinamechineへ追従カメラ対象を切り替えるスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\CameraPointMove.cs

2021/05/02 10:36
★不具合修正　壁昇り操作
・カラマリモード操作にて、壁（縦）と壁（横）に対して昇り降りの挙動時の不具合修正
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ネンチャクモード操作にて、壁（縦）と壁（横）に対して昇り降りの挙動時の不具合修正
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・InputManagerにてメニュー操作時の決定（Decition）項目を追加
main_scene\CalamariTape\ProjectSettings\InputManager.asset
・タグマネージャーにてWallタグを追加
main_scene\CalamariTape\ProjectSettings\TagManager.asset
・タグ定義クラスを作成
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
・壁判定用のパブリックenumを作成
main_scene\CalamariTape\Assets\Scripts\Common\WallRunHorizontalMode.cs

2021/05/01 14:03
★マージ前の対応

2021/04/25 15:06
★ステージ
チュートリアル
　モルモットのアドバイス中はボタン入力を行ってもプレイヤーが動かないようにする
　モルモットのアドバイス中はボタン入力を行うとアドバイスを全て表示させる
　アドバイスが全て表示されたらプレイヤーが動けるようにする

・チュートリアル画面のUIを配置
main_scene\CalamariTape\Assets\Scenes\main.unity
・プレイヤー管理スクリプトへ各モード操作情報を定義
main_scene\CalamariTape\Assets\Scripts\PlayerManager.cs
・メッセージ1（サンプル）のアニメーションを作成
main_scene\CalamariTape\Assets\Animations\Message001.anim
main_scene\CalamariTape\Assets\Animations\Message001.controller
main_scene\CalamariTape\Assets\Animations\MessageComplete001.anim
・チュートリアルメッセージとメッセージ枠とメッセージ1（サンプル）の画像ファイルを作成
main_scene\CalamariTape\Assets\Images\game_tutorial_message.png
main_scene\CalamariTape\Assets\Images\game_tutorial_serit.png
main_scene\CalamariTape\Assets\Images\game_tutorial_serit001.png
・プレイヤー接触時のメッセージ制御スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\MessageManager.cs
・メッセージスクロールアニメーション制御スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\MessageScroll.cs

2021/04/24 21:37
★クリア画面
３秒後
　上下入力で選択
　Bボタン入力で決定
　決定するとフェードイン
　もう一度遊ぶで同じステージ
　次のステージを遊ぶで次のステージ（※mainシーン遷移で仮対応）
　他のステージを遊ぶでステージセレクトレベルに遷移（※mainシーン遷移で仮対応）

・クリア画面にて各メニュー設定を変更
main_scene\CalamariTape\Assets\Scenes\main.unity
・クリア画面の各メニュー選択制御を修正
main_scene\CalamariTape\Assets\Scripts\ClearManager.cs
・不要な処理を削除
main_scene\CalamariTape\Assets\Scripts\GameRedo.cs
・メニュー画面の各メニュー選択制御を修正
main_scene\CalamariTape\Assets\Scripts\Menu.cs

2021/04/24 17:25
★ポーズ画面
ゲームクリアロゴ表示
３秒後
　もう一度遊ぶ表示
　他のステージを遊ぶ表示
　最終ステージ以外の時は次のステージを遊ぶを表示

・クリア画面にて、各メニュー表示UIを追加
main_scene\CalamariTape\Assets\Scenes\main.unity
・ゴール判定処理にて、プレイヤー操作を禁止にしてクリア画面を表示する処理を追加
main_scene\CalamariTape\Assets\Scripts\GoalEvent.cs
・クリア画面のメニューUI画像を追加
main_scene\CalamariTape\Assets\Images\game_clear_logo.png
main_scene\CalamariTape\Assets\Images\game_proceed.png
main_scene\CalamariTape\Assets\Images\game_retry.png
・メニュー画面とクリア画面で使用するUIをPrefab化
main_scene\CalamariTape\Assets\Prefabs\ClearScreen.prefab
main_scene\CalamariTape\Assets\Prefabs\GameRedo.prefab
main_scene\CalamariTape\Assets\Prefabs\GameSelect.prefab
・クリア画面のメニューUI表示制御用のスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\ClearManager.cs

2021/04/24 5:30
★ポーズ画面
　ゲームに戻るを選択で0.5秒後（要調整）にゲームプレイに戻る
　Aボタン入力で0.5秒後（要調整）にゲームプレイに戻る
　ステージセレクトに戻るを選択でフェードアウト（※シーン先をmainで仮実装）
　フェードアウト終了後ステージセレクトレベルに遷移（※シーン先をmainで仮実装）

・やりなおすとゲームに戻るとステージセレクトに戻るのUI設定変更、フェードの設定変更
main_scene\CalamariTape\Assets\Scenes\main.unity
・ゲームに戻るが何度も実行されないように修正
main_scene\CalamariTape\Assets\Scripts\GameBack.cs
・ステージをやりなおすでmainシーンを再読み込み処理追加
main_scene\CalamariTape\Assets\Scripts\GameRedo.cs
・他のステージを遊ぶを選択する処理にて、mainシーンを再読み込み処理追加
main_scene\CalamariTape\Assets\Scripts\GameSelect.cs
・ゲームに戻る際に0.5秒の待機時間を用意するよう処理を追加
main_scene\CalamariTape\Assets\Scripts\PauseWindowManager.cs
・フェードアウト処理内からシーン制御を切り離し
main_scene\CalamariTape\Assets\Scripts\UIFadeOut.cs
・シーン制御用のスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\SceneMove.cs

2021/04/24 3:52
★ポーズ画面
　遊び方確認を選択で遊び方を確認する表を表示
　遊び方を確認する表が表示されているときにAボタン入力で表示を消す
　遊び方を確認する表が表示されているときにAボタン入力でキャンセルSE再生

・遊び方を確認の際に表示される操作方法UIオブジェクトを追加
main_scene\CalamariTape\Assets\Scenes\main.unity
・遊び方マニュアルの表示と非表示切り替えるよう変更
main_scene\CalamariTape\Assets\Scripts\GameCheck.cs
・PC操作方法とコントーラー操作方法の仮画像
main_scene\CalamariTape\Assets\Images\game_manual_pc.png
main_scene\CalamariTape\Assets\Images\game_manual_xbox.png

2021/04/24 1:53
★ポーズ画面
　やりなおすを選択でフェードアウト
　フェードアウト終了後現在のステージを初期状態に戻す
　ステージ初期状態に戻した後フェードイン開始

・フェード処理用のCanvas配置など
main_scene\CalamariTape\Assets\Scenes\main.unity
・やりなおすメニュー選択時にフェードアウト演出開始処理を追加
main_scene\CalamariTape\Assets\Scripts\GameRedo.cs
・UIフェードイン演出スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\UIFadeIn.cs
・UIフェードアウト演出スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\UIFadeOut.cs

2021/04/17 14:19
★ポーズ画面
　STARTボタン入力でポーズ画面を表示
　ゲームに戻るのロゴ表示
　ポーズのロゴ表示
　やりなおすのロゴ表示
　遊び方確認のロゴ表示
　ステージセレクトに戻るのロゴ表示
　上下入力で選択
　上下入力でゲーム中のメニューSE再生
　Bボタン入力で決定
　Bボタン入力時に決定SE再生

・ポーズ画面用のUIオブジェクト配置
main_scene\CalamariTape\Assets\Scenes\main.unity
・決定音、ゲーム中のメニューの音、ゲーム中のメニューを閉じる音を再生する処理を追加
main_scene\CalamariTape\Assets\Scripts\SfxPlay.cs
・2D Sprite設定を追加
main_scene\CalamariTape\Packages\manifest.json
main_scene\CalamariTape\Packages\packages-lock.json
・メニュー内の操作にて上下選択、決定の設定を追加と変更
main_scene\CalamariTape\ProjectSettings\InputManager.asset
・UIオブジェクトへの2D Sprite仮素材
main_scene\CalamariTape\Assets\Images\game_back_logo.png
main_scene\CalamariTape\Assets\Images\game_check_logo.png
main_scene\CalamariTape\Assets\Images\game_frame.png
main_scene\CalamariTape\Assets\Images\game_pause_logo.png
main_scene\CalamariTape\Assets\Images\game_redo_logo.png
main_scene\CalamariTape\Assets\Images\game_select_logo.png
・ポーズ画面にてメニュー選択枠オブジェクトのPrefabを作成
main_scene\CalamariTape\Assets\Prefabs\GameFrame.prefab
・ゲームに戻る実行スクリプトクラスを仮作成
main_scene\CalamariTape\Assets\Scripts\GameBack.cs
・遊び方の確認実行スクリプトクラスを仮作成
main_scene\CalamariTape\Assets\Scripts\GameCheck.cs
・ステージをやりなおす実行スクリプトクラスを仮作成
main_scene\CalamariTape\Assets\Scripts\GameRedo.cs
・他のステージを選ぶ実行スクリプトクラスを仮作成
main_scene\CalamariTape\Assets\Scripts\GameSelect.cs
・メニュー画面制御スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\Menu.cs
・ポーズ画面全体制御のスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\PauseWindowManager.cs
・UI操作のスクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\UIController.cs

2021/04/17 4:15
★移動(カラマリモード)の実装
ジャンプの実装
　ジャンプした時のSEを再生
★ツルツルモードの実装
ジャンプの実装
　ジャンプした時のSEを再生

・効果音制御ゲームオブジェクトの作成やプレイヤーへコンポネントセットなど
main_scene\CalamariTape\Assets\Scenes\main.unity
・カラマリモードにてジャンプ（サイズ小～中）SE再生とハイジャンプ（サイズ中～最大）SE再生処理を追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ツルツルモードにてジャンプ（サイズ小～中）SE再生とハイジャンプ（サイズ中～最大）SE再生処理を追加かつ、
サイズ調整に合わせてジャンプ力調整の不具合修正。
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・ジャンプ（サイズ小～中）SEファイルを追加
main_scene\CalamariTape\Assets\Audio\SFX\jump_1.wav
・ハイジャンプ（サイズ中～最大）SEファイルを追加
main_scene\CalamariTape\Assets\Audio\SFX\jump_2.wav
・効果音再生スクリプトクラスを作成
main_scene\CalamariTape\Assets\Scripts\SfxPlay.cs

2021/04/17 2:40
★移動(カラマリモード)の実装
空中移動の実装
　スティック入力を取得
　入力された方向に速さ100で設定 等速で2マス/1秒移動[要調整]
　スティックで入力される割合（-1~1）
他、移動処理にて、移動←→止まるのサイクル基準を距離にするよう修正。

・カラマリモードゲームオブジェクトへ耐久ゲージゲームオブジェクトをサブオブジェクトとして配置
main_scene\CalamariTape\Assets\Scenes\main.unity
・2点間の距離を測りながら、移動←→止まるを繰り返す処理を追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・耐久ゲージゲームオブジェクトのPrefab作成
main_scene\CalamariTape\Assets\Prefabs\DurableValue.prefab

2021/04/13 21:57
★ネンチャクモードの実装
テープ長さに関する縮小・拡大処理の実装
　L2ボタン長押しでテープのサイズを縮小
　最小のテープサイズを設定（要調整）

・ネンチャクモードのスクリプトクラスにて拡大する時に壁に埋もれる不具合修正
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs

2021/04/13 21:25
★移動(カラマリモード)の実装
ジャンプの実装
　Ａ/スペースの入力の取得
　高さ(プレイヤー最小サイズ)２.５マス分（２マス登れる分）移動
　空中にいる場合はジャンプ不可

2021/04/13 21:15
★移動(カラマリモード)の実装
移動の実装
　スティック入力を取得
　自動でプレイヤーが移動と停止を繰り返す処理。（速さ100で設定 等速で3マス/1秒移動）
　スティックで入力される割合（-1~1）
　加速度「スティックで入力される割合×速さ×deltatime」を毎フレーム加算

・カラマリモードのゲームオブジェクトのスクリプトコンポーネント修正
main_scene\CalamariTape\Assets\Scenes\main.unity
・ゲーム起動時にカラマリモード切り替えのデバッグ出力追加
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs
・カラマリモードの移動操作スクリプトクラス作成（※MoveController.cs削除予定）
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs

2021/04/13 20:09
★ネンチャクモードの実装
移動の実装
　地上での移動不可の処理
　壁との当たり判定処理
　壁とくっついた後の移動処理
　（※壁斜めの挙動については要調整）
　壁とくっついた時の耐久ゲージ処理
　耐久ゲージ０になった際の落下処理

・壁用のオブジェクトをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\CubeWall.prefab
・壁移動の検証用に壁オブジェクト配置
main_scene\CalamariTape\Assets\Scenes\main.unity
・耐久値オブジェクト（耐久値デバッグ表示）
main_scene\CalamariTape\Assets\Scripts\DurableValue.cs
・ネンチャクモードの移動操作スクリプトクラス作成
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・ツルツルモードの移動操作スクリプトクラス作成（※MoveController.csからの派生）
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs

2021/04/11 23:00
★ツルツルモードの実装（ジャンプの調整）
ジャンプの実装後の調整
　高さ(プレイヤー最小サイズ)２.５マス分（２マス登れる分）移動
　高さ(プレイヤー最大サイズ)４.５マス分（４マス登れる分）移動
　ジャンプ中の最大横移動幅は４マス先までにする
移動の実装
　テープ拡大時の最高速の設定　4００で２マス/１秒で動く→4で4マス/１秒で動く
　ジャンプ中の最大横移動幅は４マス先までにする（※2マス動けず要調整）
main_scene\CalamariTape\Assets\Scripts\MoveController.cs

・プレイヤーオブジェクトの調整
main_scene\CalamariTape\Assets\Scenes\main.unity

2021/04/11 20:30
★ネンチャクモードの実装
切り替えの実装
　Lボタンでネンチャクモードに切り替え
　再度Lボタン押下でネンチャクモード解除の処理。（カラマリモードに切り替え）
main_scene\CalamariTape\Assets\Scripts\ModeChanger.cs

★ツルツルモードの実装（速度・重力の調整）
移動の実装後の調整
　自動でプレイヤーが移動する処理。（速さ100で設定 等速で3マス/1秒移動　→　速さ3で設定 等速で3マス/1秒移動）
　テープ拡大時の最高速の設定　（4００で２マス/１秒で動く　→　2で２マス/１秒で動く）
空中移動の実装後の調整
　入力された方向に速さ100で設定 等速で2マス/1秒移動[要調整]　→　速さ2で設定 等速で2マス/1秒移動
　最高速の設定　４００で２マス/１秒で動く　→　2で２マス/１秒で動く
main_scene\CalamariTape\Assets\Scripts\MoveController.cs

・プレイヤーオブジェクトへ位置情報を追加
main_scene\CalamariTape\Assets\Scenes\main.unity
・InputManagerにてネンチャクモード切り替えのキー設定
main_scene\CalamariTape\ProjectSettings\InputManager.asset

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
