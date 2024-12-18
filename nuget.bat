@REM Directory: D:\Study\FaceMan.Common\src

@REM Mode                 LastWriteTime         Length Name
@REM ----                 -------------         ------ ----
@REM d----          2024/11/27    12:58                FaceMan.DynamicAPI
@REM d----          2024/11/27    10:54                FaceMan.EntityFrameworkCore
@REM d----          2024/11/26    15:21                FaceMan.PublicLibrary

@echo off
REM 设置源目录和目标目录
set SRC_DIR=D:\Study\FaceMan.Common\src
set NUGET_OUTPUT_DIR=D:\Study\FaceMan.Common\nuget

REM 检查nuget文件夹是否存在，不存在则创建
if not exist "%NUGET_OUTPUT_DIR%" (
    mkdir "%NUGET_OUTPUT_DIR%"
)

REM 进入源目录
cd /d "%SRC_DIR%"

REM 遍历每个子目录并使用MSBuild打包
for /d %%i in (*) do (
    echo 正在打包项目 %%i...
    REM 使用MSBuild进行打包
    pushd "%%i"
    for %%j in (*.csproj) do (
        call "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" "%%j" /t:pack /p:Configuration=Release /p:OutputPath="%NUGET_OUTPUT_DIR%"
    )
    popd
)

echo 所有项目已打包完成。
pause

