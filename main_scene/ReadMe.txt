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
