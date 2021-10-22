2021/10/22 21:02
★セレクトシーンへシーン名を渡す実装
・namespace名の変更による影響
main_scene\CalamariTape\Assets\Scripts\SaveControllerScene.cs
main_scene\CalamariTape\Assets\Scripts\Common\StageClearNumber.cs
main_scene\CalamariTape\Assets\Scripts\Common\StageNameManager.cs
・シーン遷移処理へ遷移元シーン情報を渡す処理を追加
main_scene\CalamariTape\Assets\Scripts\SceneMove.cs
・オブジェクト名管理スクリプトへシーン名を管理するオブジェクトを追加
main_scene\CalamariTape\Assets\Scripts\Common\NameManager.cs
・タグ名管理スクリプトへシーン名を管理するオブジェクトを追加
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
・次のシーンへシーン情報を渡す処理を作成
main_scene\CalamariTape\Assets\Scripts\ResponseSceneInfo.cs

2021/10/22 18:06
★不具合修正　InputManagerの移動とジャンプ修正
・コントローラーの十字入力による移動とジャンプボタンをBボタンへ変更
main_scene\CalamariTape\ProjectSettings\InputManager.asset

2021/10/22 15:28
★不具合修正　BGMの差し替えと音量調整
・ステージ2～5のBGM音量を30％下げる
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
・ステージ7～9のBGMを変更、BGM音量を30％下げる
main_scene\CalamariTape\Assets\Scenes\Stage7_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage8_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage9_Scene.unity
【参考】
・ステージ7～8
　⇒「bgm_section_02_03.mp3」のBGM(※変更)
・ステージ9～10
　⇒「bgm_section_0405.mp3」のBGM(※変更)

2021/10/22 15:03
★不具合修正　UI外観と挙動の不備
・ステージ1～9にてポーズ画面、クリア画面の選択アイコンを変更
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage7_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage8_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage9_Scene.unity
・ポーズ画面、クリア画面用のアイコン画像ファイルを追加
main_scene\CalamariTape\Assets\Textures\pause_pencil.png

2021/10/22 09:42
★不具合修正　進行不能バグ
・ステージ1の一部チュートリアル表示後に操作不能になる不具合を修正
main_scene\CalamariTape\Assets\Scenes\main.unity
・ステージ4、6、7にて落下箇所によって復帰されない不具合を修正
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage7_Scene.unity

2021/10/16 18:58
★レベルデザイン　ステージ6の修正
・ステージ6の見た目（マテリアル）の調整
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene\*

2021/10/16 11:57
★レベルデザイン　ステージ1～6の実装、7～9追加
・ステージ1から9を修正（プレイヤーのモルモットのモーション参照エラー解消済み）
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage7_Sence.meta
main_scene\CalamariTape\Assets\Scenes\Stage8_Sence.meta
main_scene\CalamariTape\Assets\Scenes\Stage9_Sence.meta
・テンプレートシーンを更新
main_scene\CalamariTape\Assets\Scenes\main 3.scenetemplate

2021/10/06 22:18
★新ギミック　ギミック強制解除の実装
・ステージ6へギミック強制解除のサンプルエリアを追加
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・ギミック停止処理へギミック強制解除を追加
main_scene\CalamariTape\Assets\Scripts\StopGimmick.cs
main_scene\CalamariTape\Assets\Scripts\NotAttached\TippedSawActiveManager.cs
・タグ定義へギミック強制解除を追加
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
・コンポネント参照例外回避へ下記を追加
　カラマリ耐久値
　ネンチャク耐久値
　チップソー
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
・チップソーのモデルデータを追加
main_scene\CalamariTape\Assets\Models\Tipped_saw.fbx
・チップソーのプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\TippedSaw.prefab
・チップソーの処理を作成
main_scene\CalamariTape\Assets\Scripts\TippedSaw.cs

2021/10/02 21:52
★新ギミック　トランポリン、プレイヤーの空中速度の修正
・トランポリン検証エリアを追加
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・カラマリモードにて下記を実装
　空中速度を地上と同様にする修正
　一時停止処理を地上のみにする修正
　外部オブジェクトから重力変更を可能にする
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・ネンチャクモードにて外部オブジェクトから重力変更を可能にする
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
main_scene\CalamariTape\Assets\Scripts\NenchakScaler.cs
・ツルツルモードにて下記を実装
　空中速度を地上と同様にする修正
　外部オブジェクトから重力変更を可能にする
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruScaler.cs
・トランポリンのモデルデータを追加
main_scene\CalamariTape\Assets\Models\Trampoline.fbx
・斜めトランポリンのモデルデータを追加
main_scene\CalamariTape\Assets\Models\Trampoline2.fbx
・トランポリンをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\Trampoline.prefab
・斜めトランポリンをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\TrampolineSlope.prefab
・トランポリンの処理を作成
main_scene\CalamariTape\Assets\Scripts\Trampoline.cs

2021/09/30
★新ギミック　ダッシュパネル（※シーンごとの変更あり）
・シーンテンプレートを更新
main_scene\CalamariTape\Assets\Scenes\main 3.scenetemplate
・各シーンごとのプレイヤー設定を反映
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・プレイヤー管理スクリプトへ、壁との距離判定スクリプトを追加
main_scene\CalamariTape\Assets\Scripts\PlayerManager.cs
・ギミック停止処理へダッシュパネルを追加
main_scene\CalamariTape\Assets\Scripts\StopGimmick.cs
・タグ管理スクリプトへダッシュパネルのタグ情報を追加
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
・全モード判定スクリプトへ壁の衝突判定を追加（衝突エフェクト用）
main_scene\CalamariTape\Assets\Scripts\NotAttached\AllmodeStateConf.cs
・コンポネント参照例外回避処理へダッシュパネルの情報を追加
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
・ダッシュパネルのモデルを追加
main_scene\CalamariTape\Assets\Models\dashpanel.fbx
・ダッシュパネルをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\DashPanel.prefab
・ダッシュパネルの処理を作成
main_scene\CalamariTape\Assets\Scripts\DashPanel.cs
・ダッシュ中のプレイヤーを判定する処理を作成
main_scene\CalamariTape\Assets\Scripts\DashPlayer.cs
・衝突エフェクト実行スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\PlayerEffectController.cs
・ギミック停止用のダッシュパネルを作成
main_scene\CalamariTape\Assets\Scripts\NotAttached\DashPanelActiveManager.cs
・壁の衝突情報の構造体を作成
main_scene\CalamariTape\Assets\Scripts\Struct\FieldWalled.cs

2021/09/26
★新ギミック　ターザンの実装、ゲームスタート時にヒエラルキーにゲームオブジェクトが大量生成される不具合を修正
・ターザンの動作確認エリアの作成
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・滑る床の上判定にて、不備のあった処理を修正
main_scene\CalamariTape\Assets\Scripts\NotAttached\AllmodeStateConf.cs
・コンポーネント例外参照回避処理にて一時的にデバッグをコメントアウト
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
・ギミック側からプレイヤーを参照する処理にて、ターザンの条件を追加
main_scene\CalamariTape\Assets\Scripts\NotAttached\GimmicksDecision.cs
・ロープのマテリアルを追加
main_scene\CalamariTape\Assets\Materials\Rope.mat
・ロープのモデルデータを追加
main_scene\CalamariTape\Assets\Models\rope.fbx
・ターザンのオブジェクトをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\Tarzan.prefab
・ロープ（ターザンの子オブジェクト）の処理を作成
main_scene\CalamariTape\Assets\Scripts\Rope.cs
・ターザンの処理を作成
main_scene\CalamariTape\Assets\Scripts\Tarzan.cs

2021/09/23 15:24
★UI差し替え　ポーズ画面、クリア画面、mainシーンテンプレート更新、チュートリアルへフォント適用
・コングラチュレーションロゴを更新
main_scene\CalamariTape\Assets\Images\game_allclear_logo.png
・ゲームに戻るロゴを更新
main_scene\CalamariTape\Assets\Images\game_back_logo.png
・遊び方の確認ロゴを更新
main_scene\CalamariTape\Assets\Images\game_check_logo.png
・ステージクリアロゴを更新
main_scene\CalamariTape\Assets\Images\game_clear_logo.png
・ポーズロゴを更新
main_scene\CalamariTape\Assets\Images\game_pause_logo.png
・次のステージを選ぶロゴを更新
main_scene\CalamariTape\Assets\Images\game_proceed.png
・もう一度遊ぶロゴを更新
main_scene\CalamariTape\Assets\Images\game_redo_logo.png
・他のステージを選ぶロゴを更新
main_scene\CalamariTape\Assets\Images\game_select_logo.png
・各シーンにてクリア画面プレハブ情報を更新
main_scene\CalamariTape\Assets\Prefabs\ClearScreen.prefab
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene\ClearScreen.prefab
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene\ClearScreen.prefab
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene\ClearScreen.prefab
・mainシーンのシーンテンプレートを更新
main_scene\CalamariTape\Assets\Scenes\main 3.scenetemplate
・各シーンにてポーズ画面、クリア画面のUIを更新
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・統一フォントの登録
main_scene\CalamariTape\Assets\Fonts\rounded-mplus-2m-medium.ttf
・ロゴ画像のバックアップ
main_scene\CalamariTape\Assets\Images\game_allclear_logo_bk.png
main_scene\CalamariTape\Assets\Images\game_back_logo_bk.png
main_scene\CalamariTape\Assets\Images\game_back_logo_bk2.png
main_scene\CalamariTape\Assets\Images\game_check_logo_bk2.png
main_scene\CalamariTape\Assets\Images\game_clear_logo_bk.png
main_scene\CalamariTape\Assets\Images\game_pause_logo_bk2.png
main_scene\CalamariTape\Assets\Images\game_proceed_bk2.png
main_scene\CalamariTape\Assets\Images\game_redo_logo_bk2.png
main_scene\CalamariTape\Assets\Images\game_select_logo_bk2.png

2021/09/23 12:36
★新ギミック　滑る床の実装
・滑る床のエリアを作成
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・カラマリモードにて、滑る床ギミックで参照する情報を追加
　コンベア判定を共通ロジックから呼ぶように変更
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
main_scene\CalamariTape\Assets\Scripts\CircleRing.cs
main_scene\CalamariTape\Assets\Scripts\MarmotWheel.cs
・ネンチャクモードにて、滑る床ギミックで参照する情報を追加
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・ギミック停止処理にて、各ギミックをオブジェクト化して管理する方向へ修正
main_scene\CalamariTape\Assets\Scripts\StopGimmick.cs
・ツルツルモードにて、滑る床ギミックで参照する情報を追加
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・コンポーネント名管理スクリプトへ滑る床の情報を追加
main_scene\CalamariTape\Assets\Scripts\Common\ComponentManager.cs
・タグ名管理スクリプトへ滑る床の情報を追加
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
・全モード共通ロジックへコンベアと滑る床の判定を追加
main_scene\CalamariTape\Assets\Scripts\NotAttached\AllmodeStateConf.cs
・例外無効処理へ滑る床の判定を追加
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
・ギミック判定へ滑る床の判定を追加
main_scene\CalamariTape\Assets\Scripts\NotAttached\GimmicksDecision.cs
・滑る床のマテリアルを作成
main_scene\CalamariTape\Assets\Materials\IcePlane.mat
・滑る床オブジェクトをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\IcePlane.prefab
・滑る床の処理を作成
main_scene\CalamariTape\Assets\Scripts\IcePlane.cs
・ギミック停止処理にて使用するインターフェースを作成
main_scene\CalamariTape\Assets\Scripts\Interface\GimmkckActiveManager.cs
・ベルトコンベアのギミック停止・再生クラスを作成
main_scene\CalamariTape\Assets\Scripts\NotAttached\ConveyorMoveCharacterActiveManager.cs
・ステージ内のギミック情報を管理するスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\NotAttached\GimmickDifferent.cs
・回し車のギミック停止・再生クラスを作成
main_scene\CalamariTape\Assets\Scripts\NotAttached\MarmotHealthActiveManager.cs
・動く壁のギミック停止・再生クラスを作成
main_scene\CalamariTape\Assets\Scripts\NotAttached\MoveWallsActiveManager.cs

2021/09/17 09:07
★新ギミック　回転リングの実装
・回転リングのサンプルエリアを作成
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・新ギミックの一部ロジック共通化により、回し車の一部ロジックを削除
main_scene\CalamariTape\Assets\Scripts\MarmotWheel.cs
・コンポーネント定義へ回転リングを追加
main_scene\CalamariTape\Assets\Scripts\Common\ComponentManager.cs
・(削除)新ギミック用のプレハブ格納先を変更
main_scene\CalamariTape\Assets\Prefabs\DoorWheel.prefab
main_scene\CalamariTape\Assets\Prefabs\MarmotHealth.prefab
main_scene\CalamariTape\Assets\Prefabs\MarmotWheel.prefab
main_scene\CalamariTape\Assets\Prefabs\MoveHorizontalWall.prefab
main_scene\CalamariTape\Assets\Prefabs\MoveVerticalWall.prefab
・回転リングのモデルを追加
main_scene\CalamariTape\Assets\Models\circle_ring.fbx
・回転リングのマテリアル（外側）を追加
main_scene\CalamariTape\Assets\Models\Materials\circle_material.mat
・回転リングのマテリアル（内側）を追加
main_scene\CalamariTape\Assets\Models\Materials\hashira.mat
・回転リング、回転床のプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\CircleRing.prefab
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\CircleScaffold.prefab
・新ギミック用のフォルダを作成してプレハブを移動
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\DoorWheel.prefab
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\MarmotHealth.prefab
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\MarmotWheel.prefab
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\MoveHorizontalWall.prefab
main_scene\CalamariTape\Assets\Prefabs\Gimmicks\MoveVerticalWall.prefab
・回転リング処理の作成
main_scene\CalamariTape\Assets\Scripts\CircleRing.cs
・回転床処理の作成
main_scene\CalamariTape\Assets\Scripts\CircleScaffold.cs
・新ギミックのプレイヤー判定の共通化モジュール作成（回し車、回転リング）
main_scene\CalamariTape\Assets\Scripts\NotAttached\GimmicksDecision.cs

2021/09/14 09:02
★壁、床の上で大きさを変更する際に空中に浮く挙動を修正
・クリア画面プレハブにてオブジェクト無効化を初期値に設定
main_scene\CalamariTape\Assets\Prefabs\ClearScreen.prefab
・選択アイコンのプレハブにて画像を変更
main_scene\CalamariTape\Assets\Prefabs\GameFrame.prefab
・ゲームオーバーエリアのプレハブにて位置情報を変更
main_scene\CalamariTape\Assets\Prefabs\GameOverLine.prefab
・縦向きの壁にてレイヤー情報を変更
main_scene\CalamariTape\Assets\Prefabs\HorizontalWall.prefab
・横向きの壁にてレイヤー情報を変更
main_scene\CalamariTape\Assets\Prefabs\VerticalWall.prefab
・各ステージにてネンチャク、カラマリモード設定を変更
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・カラマリモードにて大きさ変更中に重力発生させる処理を追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
main_scene\CalamariTape\Assets\Scripts\CalamariScaler.cs
main_scene\CalamariTape\Assets\Scripts\CalamariState.cs
main_scene\CalamariTape\Assets\Scripts\CalamariWallMove.cs
・ゲームオーバーエリア処理にてプレイヤーコンポーネントにプライベート追加
main_scene\CalamariTape\Assets\Scripts\GameOverLine.cs
・ネンチャクモードにて大きさ変更中に重力発生させる処理を追加
main_scene\CalamariTape\Assets\Scripts\NenchakScaler.cs
main_scene\CalamariTape\Assets\Scripts\NenchakState.cs
・メニュープレハブ化を解除
※各シーンのヒエラルキーに依存、及びプレハブ化により既存設定が初期化された？為
（削除）main_scene\CalamariTape\Assets\Prefabs\Menu.prefab
・全モード使用スクリプトの名前を変更
（削除）main_scene\CalamariTape\Assets\Scripts\NotAttached\CalamariStateConf.cs
（作成）main_scene\CalamariTape\Assets\Scripts\NotAttached\AllmodeStateConf.cs
・ステージ1をベースにテンプレートシーンを作成
main_scene\CalamariTape\Assets\Scenes\main 3.scenetemplate
・重力方向を定義するスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\Common\GravityDirection.cs

2021/09/11 23:05
★ポーズ画面、クリア画面のクリック動作で非選択になる不具合を修正
・右クリック、マウスホイール押下で非選択になる不具合を修正
main_scene\CalamariTape\Assets\Scripts\ClearManager.cs
main_scene\CalamariTape\Assets\Scripts\MenuManager.cs

2021/09/11 17:51
★モルモット3Dモデルリソース差し替え
・新モルモット歩行アニメーション追加
main_scene\CalamariTape\Assets\Animations\Morumotto.controller
・各ステージのモルモット3Dモデルリソース差し替え
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・旧歩行アニメーションの名前を変更
main_scene\CalamariTape\Assets\Animations\Morumotto_bk.controller
・新モルモット3Dモデルを追加
main_scene\CalamariTape\Assets\Models\morumotto 1.fbx

2021/09/11 15:02
★ポーズ画面、クリア画面(1ステージ)リソース差し替え
・ポーズ画面、クリア画面のUI素材を追加
main_scene\CalamariTape\Assets\Images\game_back_logo.png
main_scene\CalamariTape\Assets\Images\game_check_logo.png
main_scene\CalamariTape\Assets\Images\game_pause_logo.png
main_scene\CalamariTape\Assets\Images\game_proceed.png
main_scene\CalamariTape\Assets\Images\game_redo_logo.png
main_scene\CalamariTape\Assets\Images\game_select_logo.png
・ゲームに戻るUIのプレハブ情報変更
main_scene\CalamariTape\Assets\Prefabs\GameRedo.prefab
・ステージを選ぶUIのプレハブ情報変更
main_scene\CalamariTape\Assets\Prefabs\GameSelect.prefab
・ステージ1のUIを変更
main_scene\CalamariTape\Assets\Scenes\main.unity
・ステージ1のUIを変更（途中）
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
・UI処理にて各UIが選択された時に手前に表示する処理を追加
main_scene\CalamariTape\Assets\Scripts\UIController.cs
・旧ポーズ画面、クリア画面のUI素材の名前を変更
main_scene\CalamariTape\Assets\Images\game_back_logo_.png
main_scene\CalamariTape\Assets\Images\game_check_logo_bk.png
main_scene\CalamariTape\Assets\Images\game_pause_logo_bk.png
main_scene\CalamariTape\Assets\Images\game_proceed_bk.png
main_scene\CalamariTape\Assets\Images\game_redo_logo_bk.png
main_scene\CalamariTape\Assets\Images\game_select_logo_bk.png
・ポーズ画面UIをプレハブ化
main_scene\CalamariTape\Assets\Prefabs\Menu.prefab

2021/09/11 10:21
★ベルトコンベアの実装
・デモシーンにてベルトコンベアの配置
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・カラマリモードにてベルトコンベア用判定を追加
main_scene\CalamariTape\Assets\Scripts\CalamariMoveController.cs
・関数名の命名規則にあわせた修正
main_scene\CalamariTape\Assets\Scripts\MarmotHealth.cs
・ネンチャクモードにてベルトコンベア用判定を追加
main_scene\CalamariTape\Assets\Scripts\NenchakMoveController.cs
・ギミック停止処理へベルトコンベアを追加
　＋関数名の命名規則にあわせた修正
main_scene\CalamariTape\Assets\Scripts\StopGimmick.cs
・ツルツルモードにてベルトコンベア用判定を追加
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
・コンポーネント例外判定の定義を追加
main_scene\CalamariTape\Assets\Scripts\Common\ComponentManager.cs
・レイヤー定義へベルトコンベアを追加
main_scene\CalamariTape\Assets\Scripts\Common\LayerManager.cs
・タグ定義へベルトコンベアを追加
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
・CharacterController、各モードの振る舞い制御スクリプトコンポーネントの例外回避処理を追加
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
・ベルトコンベアのアセット追加
main_scene\CalamariTape\Assets\ModularConveyorTools\*
・ベルトコンベア上でプレイヤーを動かすスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\ConveyorMoveCharacter.cs
・ベルトコンベアの速度定義スクリプトを作成
main_scene\CalamariTape\Assets\Scripts\Common\ConveyorManager.cs
・ベルトコンベアの移動方向を定義するスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\Common\ConveyorMoveMode.cs

2021/09/03 08:57
★ケージで回転の実装
・各ステージのツルツルモード変更適用
main_scene\CalamariTape\Assets\Scenes\main.unity
main_scene\CalamariTape\Assets\Scenes\Stage2_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage3_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage4_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage5_Scene.unity
main_scene\CalamariTape\Assets\Scenes\Stage6_Scene.unity
・メニュー画面から再びステージを始めからやり直すとスクリプトが参照不可になるバグを修正
main_scene\CalamariTape\Assets\Scripts\CalamariScaler.cs
・カラマリモードにてステージを読み込む際にコンポネント参照チェック処理を追加
main_scene\CalamariTape\Assets\Scripts\CalamariWallMove.cs
・プレイヤー管理スクリプトへ大きさ変更制御を追加
main_scene\CalamariTape\Assets\Scripts\PlayerManager.cs
・ギミック停止処理へケージで回転を追加
main_scene\CalamariTape\Assets\Scripts\StopGimmick.cs
main_scene\CalamariTape\Assets\Scripts\Common\ComponentManager.cs
main_scene\CalamariTape\Assets\Scripts\Common\TagManager.cs
main_scene\CalamariTape\Assets\Scripts\NotAttached\DeadNullReference.cs
・ツルツルモードにてアニメーション、大きさ変更ごとにコンポーネント分け
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruAnimation.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruMoveController.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruState.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruGroundMove.cs
main_scene\CalamariTape\Assets\Scripts\TsuruTsuruScaler.cs
・メニューのスクリプト名とクラス名を変更
main_scene\CalamariTape\Assets\Scripts\UIController.cs
main_scene\CalamariTape\Assets\Scripts\MenuManager.cs
・（削除）メニューのスクリプト名とクラス名を変更
main_scene\CalamariTape\Assets\Scripts\Menu.cs
・大きさ変更禁止エリア用のオブジェクトを作成
main_scene\CalamariTape\Assets\Prefabs\BanScaleChangeErea.prefab
・ケージで回転（ドア）のプレハブ化
main_scene\CalamariTape\Assets\Prefabs\DoorWheel.prefab
・ケージで回転（制御値）のプレハブ化
main_scene\CalamariTape\Assets\Prefabs\MarmotHealth.prefab
・ケージで回転（回し車）のプレハブ化
main_scene\CalamariTape\Assets\Prefabs\MarmotWheel.prefab
・大きさ変更禁止エリアのスクリプトを作成
main_scene\CalamariTape\Assets\Scripts\BanScaleChange.cs
・ケージで回転（ドア）のスクリプト作成
main_scene\CalamariTape\Assets\Scripts\DoorOpener.cs
・ケージで回転（制御値）のスクリプト作成
main_scene\CalamariTape\Assets\Scripts\MarmotHealth.cs
・ケージで回転（回し車）のスクリプト作成
main_scene\CalamariTape\Assets\Scripts\MarmotWheel.cs
・ケージで回転のアニメーションのトランザクション名を定義
main_scene\CalamariTape\Assets\Scripts\Common\AnimatorControllerManager.cs
・回し車モデル追加
main_scene\CalamariTape\Assets\Wheel\*

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
