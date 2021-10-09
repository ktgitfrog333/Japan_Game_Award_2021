@echo off

rem ########## 定数 ##########
rem ※ユーザごとに差異アリ※　リポジトリと紐づくエクスプローラーの保存先の親
set USER_DIRECTORY="C:\Users\kzpaqu\Documents"
rem ※ユーザごとに差異アリ※　リポジトリと紐づくフォルダ名
set GIT_DIRECTORY="Japan_Game_Award_2021"
rem \
set EN="\\"
rem movie_senceディレクトリ
set MOVIE_SENCE_DIRECTORY="Morumotto_ Wheerun_Movie\Assets"
rem title_senceディレクトリ
set TITLE_SENCE_DIRECTORY="Morumotto_ Wheerun_Title\Assets"
rem select_senceディレクトリ
set SELECT_SENCE_DIRECTORY="Morumotto_ Wheerun_Select\Assets"
rem main_senceディレクトリ
set MAIN_SENCE_DIRECTORY="main_scene\CalamariTape\Assets"
rem main_senceディレクトリ(新)
set NEW_MAIN_SENCE_DIRECTORY="main_scene\Assets"
rem Morumotto_Wheerunディレクトリ
set MORUMOTTO_WHEERUN_DIRECTORY="Morumotto_Wheerun\Assets"
rem 不要ディレクトリ
set SCENES_DIRECTORY="Scenes"
rem 不要ファイル
set SCENES_DIRECTORY_FILE="Scenes.meta"

rem キーワードを入力
set keyword=
set /P keyword="キーワードを入力(entaro-):"

rem キーワードをチェック
if "%keyword%"=="entaro-" goto correctCase
if not "%keyword%"=="entaro-" goto notCorrectCase

rem キーワードが正しい場合
:correctCase

echo "リポジトリのディレクトリチェック"
set targetUserGitDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1%

rem ディレクトリが存在するかチェックする
if exist %targetUserGitDirectory% goto findUserDirectorySuccessCase
if not exist %targetUserGitDirectory% goto findUserDirectoryErrorCase

rem ディレクトリが存在する場合
:findUserDirectorySuccessCase

echo "movie_senceのディレクトリチェック"
set targetUserGitMovieSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%MOVIE_SENCE_DIRECTORY:~1%
echo %targetUserGitMovieSenceDirectory%

rem ディレクトリが存在するかチェックする
if exist %targetUserGitMovieSenceDirectory% goto findUserMovieSenceDirectorySuccessCase
if not exist %targetUserGitMovieSenceDirectory% goto findUserMovieSenceDirectoryErrorCase

rem ディレクトリが存在する場合
:findUserMovieSenceDirectorySuccessCase

echo "title_senceのディレクトリチェック"
set targetUserGitTitleSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%TITLE_SENCE_DIRECTORY:~1%
echo %targetUserGitTitleSenceDirectory%

rem ディレクトリが存在するかチェックする
if exist %targetUserGitTitleSenceDirectory% goto findUserTitleSenceDirectorySuccessCase
if not exist %targetUserGitTitleSenceDirectory% goto findUserTitleSenceDirectoryErrorCase

rem ディレクトリが存在する場合
:findUserTitleSenceDirectorySuccessCase

echo "select_senceのディレクトリチェック"
set targetUserGitSelectSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%SELECT_SENCE_DIRECTORY:~1%
echo %targetUserGitSelectSenceDirectory%

rem ディレクトリが存在するかチェックする
if exist %targetUserGitSelectSenceDirectory% goto findUserSelectSenceDirectorySuccessCase
if not exist %targetUserGitSelectSenceDirectory% goto findUserSelectSenceDirectoryErrorCase

rem ディレクトリが存在する場合
:findUserSelectSenceDirectorySuccessCase

echo "main_senceのディレクトリチェック"
set targetUserGitMainSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%MAIN_SENCE_DIRECTORY:~1%
echo %targetUserGitMainSenceDirectory%

rem ディレクトリが存在するかチェックする
if exist %targetUserGitMainSenceDirectory% goto findUserMainSenceDirectorySuccessCase
if not exist %targetUserGitMainSenceDirectory% goto findUserMainSenceDirectoryErrorCase

rem ディレクトリが存在する場合
:findUserMainSenceDirectorySuccessCase

echo "Morumotto_Wheerunのディレクトリチェック"
set targetUserGitMorumottoWheerunDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%MORUMOTTO_WHEERUN_DIRECTORY:~1%
echo %targetUserGitMorumottoWheerunDirectory%

rem ディレクトリが存在するかチェックする
if exist %targetUserGitMorumottoWheerunDirectory% goto findUserMorumottoWheerunDirectorySuccessCase
if not exist %targetUserGitMorumottoWheerunDirectory% goto findUserMorumottoWheerunDirectoryErrorCase

rem ディレクトリが存在する場合
:findUserMorumottoWheerunDirectorySuccessCase

rem Morumotto_WheerunのAssetディレクトリ内にmovie_senceディレクトリを作成
set mergeMovieSenceDirectory=%targetUserGitMorumottoWheerunDirectory:~0,-1%%EN:~2,-1%%MOVIE_SENCE_DIRECTORY:~1%
mkdir %mergeMovieSenceDirectory%

rem Morumotto_WheerunのAssetディレクトリ内にtitle_senceディレクトリを作成
set mergeTitleSenceDirectory=%targetUserGitMorumottoWheerunDirectory:~0,-1%%EN:~2,-1%%TITLE_SENCE_DIRECTORY:~1%
mkdir %mergeTitleSenceDirectory%

rem Morumotto_WheerunのAssetディレクトリ内にselect_senceディレクトリを作成
set mergeSelectSenceDirectory=%targetUserGitMorumottoWheerunDirectory:~0,-1%%EN:~2,-1%%SELECT_SENCE_DIRECTORY:~1%
mkdir %mergeSelectSenceDirectory%

rem Morumotto_WheerunのAssetディレクトリ内にmain_senceディレクトリを作成
set mergeMainSenceDirectory=%targetUserGitMorumottoWheerunDirectory:~0,-1%%EN:~2,-1%%NEW_MAIN_SENCE_DIRECTORY:~1%
mkdir %mergeMainSenceDirectory%

rem movie_senceディレクトリをMorumotto_Wheerunへコピー
xcopy /s %targetUserGitMovieSenceDirectory% %mergeMovieSenceDirectory%

rem title_senceディレクトリをMorumotto_Wheerunへコピー
xcopy /s %targetUserGitTitleSenceDirectory% %mergeTitleSenceDirectory%

rem select_senceディレクトリをMorumotto_Wheerunへコピー
xcopy /s %targetUserGitSelectSenceDirectory% %mergeSelectSenceDirectory%

rem main_senceディレクトリをMorumotto_Wheerunへコピー
xcopy /s %targetUserGitMainSenceDirectory% %mergeMainSenceDirectory%

rem 不要ディレクトリを削除
echo "不要ディレクトリを削除"
set deleteScenesDirectory=%targetUserGitMorumottoWheerunDirectory:~0,-1%%EN:~2,-1%%SCENES_DIRECTORY:~1%
echo %deleteScenesDirectory%
rmdir /s %deleteScenesDirectory%
echo "不要ファイルを削除"
set deleteScenesFile=%targetUserGitMorumottoWheerunDirectory:~0,-1%%EN:~2,-1%%SCENES_DIRECTORY_FILE:~1%
echo %deleteScenesFile%
del %deleteScenesFile%

pause
exit /b

rem ディレクトリが存在しない場合
:findUserDirectoryErrorCase
echo "ディレクトリが存在しませんでした。"
pause
exit /b

rem ディレクトリが存在しない場合
:findUserMovieSenceDirectoryErrorCase
echo "movie_senceのディレクトリが存在しませんでした。"
pause
exit /b

rem ディレクトリが存在しない場合
:findUserTitleSenceDirectoryErrorCase
echo "title_senceのディレクトリが存在しませんでした。"
pause
exit /b

rem ディレクトリが存在しない場合
:findUserSelectSenceDirectoryErrorCase
echo "select_senceのディレクトリが存在しませんでした。"
pause
exit /b

rem ディレクトリが存在しない場合
:findUserMainSenceDirectoryErrorCase
echo "main_senceのディレクトリが存在しませんでした。"
pause
exit /b

rem ディレクトリが存在しない場合
:findUserMorumottoWheerunDirectoryErrorCase
echo "Morumotto_Wheerunのディレクトリが存在しませんでした。"
pause
exit /b

rem キーワードが正しくない場合
:notCorrectCase
echo "間違ったキーワード。"
pause
