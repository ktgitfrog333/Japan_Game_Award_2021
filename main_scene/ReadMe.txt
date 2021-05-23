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
