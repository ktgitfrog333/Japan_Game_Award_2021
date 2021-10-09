@echo off

rem ########## 定数 ##########
rem ※ユーザごとに差異アリ※　リポジトリと紐づくエクスプローラーの保存先の親
set USER_DIRECTORY="C:\Users\kzpaqu\Documents"
rem ※ユーザごとに差異アリ※　リポジトリと紐づくフォルダ名
set GIT_DIRECTORY="Japan_Game_Award_2021"
rem \
set EN="\\"
rem tmpディレクトリ
set TMP_DIRECTORY="C:\tmp"
rem movie_senceディレクトリ
set MOVIE_SENCE_DIRECTORY="Morumotto_ Wheerun_Movie\Assets"
rem title_senceディレクトリ
set TITLE_SENCE_DIRECTORY="Morumotto_ Wheerun_Title\Assets"
rem select_senceディレクトリ
set SELECT_SENCE_DIRECTORY="Morumotto_ Wheerun_Select\Assets"
rem main_senceディレクトリ
set MAIN_SENCE_DIRECTORY="main_scene\CalamariTape\Assets"
rem ガントチャートディレクトリ
set GANTT_CHART_DIRECTORY="ガントチャート"
rem 仕様書ディレクトリ
set DOCUMENT_DIRECTORY="仕様書"
rem ui_uxディレクトリ
set UI_UX_DIRECTORY="ui_ux"

rem movie_senceブランチ
set PROGRAMMER_MOVIE_SENCE_BRANCH="programmer/movie_sence"
rem title_senceブランチ
set PROGRAMMER_TITLE_SENCE_BRANCH="programmer/title_sence"
rem select_senceブランチ
set PROGRAMMER_SELECT_SENCE_BRANCH="programmer/select_sence"
rem main_senceブランチ
set PROGRAMMER_MAIN_SENCE_BRANCH="programmer/main_sence"
rem ガントチャートブランチ
set PLANNER_GANTT_CHART_BRANCH="planner/ガントチャート"
rem 仕様書ブランチ
set PLANNER_DOCUMENT_BRANCH="planner/仕様書"
rem ui_uxブランチ
set DESIGNER_UI_UX_BRANCH="designer/ui_ux"

rem キーワードを入力
set keyword=
set /P keyword="キーワードを入力(entaro-):"

rem キーワードをチェック
if "%keyword%"=="entaro-" goto correctCase
if not "%keyword%"=="entaro-" goto notCorrectCase

rem ディレクトリが存在する場合
:correctCase

echo "リポジトリのディレクトリチェック"
set targetUserGitDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1%

rem ディレクトリが存在するかチェックする
if exist %targetUserGitDirectory% goto findUserDirectorySuccessCase
if not exist %targetUserGitDirectory% goto findUserDirectoryErrorCase

rem ディレクトリが存在する場合
:findUserDirectorySuccessCase

echo "tmpのディレクトリチェック"
set targetTmpDirectory=%TMP_DIRECTORY%

rem ディレクトリが存在するかチェックする
if exist %targetTmpDirectory% goto findTmpDirectorySuccessCase
if not exist %targetTmpDirectory% goto findTmpDirectoryErrorCase

rem ディレクトリが存在しない場合
:findTmpDirectoryErrorCase

echo "【1】movie_senceのコピーを開始"

rem tmpフォルダをC:\へ作成
set tmpDirectoryMovieSenceDirectory=%TMP_DIRECTORY:~0,-1%%EN:~2,-1%%MOVIE_SENCE_DIRECTORY:~1%
mkdir %tmpDirectoryMovieSenceDirectory%

rem movie_senceチェックアウト
set userDirectoryGitDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1%
cd %userDirectoryGitDirectory%
git checkout %PROGRAMMER_MOVIE_SENCE_BRANCH%

rem movie_senceディレクトリをtmpへコピー
set userGitMovieSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%MOVIE_SENCE_DIRECTORY:~1%
xcopy /s %userGitMovieSenceDirectory% %tmpDirectoryMovieSenceDirectory%

echo "movie_senceのコピーを終了"
rem pause

echo "【2】title_senceのコピーを開始"

rem tmpフォルダをC:\へ作成
set tmpDirectoryTitleSenceDirectory=%TMP_DIRECTORY:~0,-1%%EN:~2,-1%%TITLE_SENCE_DIRECTORY:~1%
mkdir %tmpDirectoryTitleSenceDirectory%

rem title_senceチェックアウト
git checkout %PROGRAMMER_TITLE_SENCE_BRANCH%

rem title_senceディレクトリをtmpへコピー
set userGitTitleSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%TITLE_SENCE_DIRECTORY:~1%
xcopy /s %userGitTitleSenceDirectory% %tmpDirectoryTitleSenceDirectory%

echo "title_senceのコピーを終了"
rem pause

echo "【3】select_senceのコピーを開始"

rem tmpフォルダをC:\へ作成
set tmpDirectorySelectSenceDirectory=%TMP_DIRECTORY:~0,-1%%EN:~2,-1%%SELECT_SENCE_DIRECTORY:~1%
mkdir %tmpDirectorySelectSenceDirectory%

rem select_senceチェックアウト
git checkout %PROGRAMMER_SELECT_SENCE_BRANCH%

rem select_senceディレクトリをtmpへコピー
set userGitSelectSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%SELECT_SENCE_DIRECTORY:~1%
xcopy /s %userGitSelectSenceDirectory% %tmpDirectorySelectSenceDirectory%

echo "select_senceのコピーを終了"
rem pause

echo "【4】main_senceのコピーを開始"

rem tmpフォルダをC:\へ作成
set tmpDirectoryMainSenceDirectory=%TMP_DIRECTORY:~0,-1%%EN:~2,-1%%MAIN_SENCE_DIRECTORY:~1%
mkdir %tmpDirectoryMainSenceDirectory%

rem main_senceチェックアウト
git checkout %PROGRAMMER_MAIN_SENCE_BRANCH%

rem main_senceディレクトリをtmpへコピー
set userGitMainSenceDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%MAIN_SENCE_DIRECTORY:~1%
xcopy /s %userGitMainSenceDirectory% %tmpDirectoryMainSenceDirectory%

echo "main_senceのコピーを終了"
rem pause

echo "【5】ガントチャートのコピーを開始"

rem tmpフォルダをC:\へ作成
set tmpDirectoryGanttChartDirectory=%TMP_DIRECTORY:~0,-1%%EN:~2,-1%%GANTT_CHART_DIRECTORY:~1%
mkdir %tmpDirectoryGanttChartDirectory%

rem ガントチャートチェックアウト
git checkout %PLANNER_GANTT_CHART_BRANCH%

rem ガントチャートディレクトリをtmpへコピー
set userGitGanttChartDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%GANTT_CHART_DIRECTORY:~1%
xcopy /s %userGitGanttChartDirectory% %tmpDirectoryGanttChartDirectory%

echo "ガントチャートのコピーを終了"
rem pause

echo "【5】仕様書のコピーを開始"

rem tmpフォルダをC:\へ作成
set tmpDirectoryDocumentDirectory=%TMP_DIRECTORY:~0,-1%%EN:~2,-1%%DOCUMENT_DIRECTORY:~1%
mkdir %tmpDirectoryDocumentDirectory%

rem 仕様書チェックアウト
git checkout %PLANNER_DOCUMENT_BRANCH%

rem 仕様書ディレクトリをtmpへコピー
set userGitDocumentDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%DOCUMENT_DIRECTORY:~1%
xcopy /s %userGitDocumentDirectory% %tmpDirectoryDocumentDirectory%

echo "仕様書のコピーを終了"
rem pause

echo "【6】ui_uxのコピーを開始"

rem tmpフォルダをC:\へ作成
set tmpDirectoryUiUxDirectory=%TMP_DIRECTORY:~0,-1%%EN:~2,-1%%UI_UX_DIRECTORY:~1%
mkdir %tmpDirectoryUiUxDirectory%

rem ui_uxチェックアウト
git checkout %DESIGNER_UI_UX_BRANCH%

rem ui_uxディレクトリをtmpへコピー
set userGitUiUxDirectory=%USER_DIRECTORY:~0,-1%%EN:~2,-1%%GIT_DIRECTORY:~1,-1%%EN:~2,-1%%UI_UX_DIRECTORY:~1%
xcopy /s %userGitUiUxDirectory% %tmpDirectoryUiUxDirectory%

echo "ui_uxのコピーを終了"
pause

explorer %TMP_DIRECTORY%
exit /b

rem ディレクトリが存在しない場合
:findUserDirectoryErrorCase
echo "ディレクトリが存在しませんでした。"
pause
exit /b

rem ディレクトリが存在する場合
:findTmpDirectorySuccessCase
echo "既にディレクトリが存在します。以下のディレクトリを削除して下さい。"
echo %targetTmpDirectory%
pause
exit /b

rem ディレクトリが存在しない場合
:notCorrectCase
echo "間違ったキーワード。"
pause
