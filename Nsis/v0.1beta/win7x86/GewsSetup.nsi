; example2.nsi
;
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;
; It will install example2.nsi into a directory that the user selects,

;--------------------------------

; The name of the installer
Name "Gews"

; The file to write
OutFile "Setup.exe"

; The default installation directory
InstallDir $PROGRAMFILES\Gews

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\Gews" "Install_Dir"

; Request application privileges for Windows Vista
RequestExecutionLevel admin

;--------------------------------

; Pages

Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

;--------------------------------

; The stuff to install
; Section "工作面瓦斯涌出动态特征管理系统 (required)" 

Section "核心文件 (必需)" 

  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\bin
  
  File /r "bin\*.*"
  
  ExecWait "$INSTDIR\bin\register.bat"
  ; Show .bat windows
  ; nsExec::Exec "$INSTDIR\bin\register.bat"
  ; Put file there

  ; Write the installation path into the registry
  ; WriteRegStr HKLM SOFTWARE\Gews "Install_Dir" "$INSTDIR\gews"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "DisplayName" "Gews"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "工作面瓦斯涌出动态特征管理系统" 

  ;SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\sys1
  
  ; Put file there
  File /r "rsys1\*.*"
  
  ; Write the installation path into the registry
  ; WriteRegStr HKLM SOFTWARE\Gews\sys1 "Install_Dir" "$INSTDIR\sys1"
  
  ; Write the uninstall keys for Windows
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "DisplayName" "Gews"
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "UninstallString" '"$INSTDIR\uninstall.exe"'
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoModify" 1
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "工作面采掘进度管理系统帮助文件" 

  ;SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\sys2
  
  ; Put file there
  File /r "rsys2\*.*"
  
  ; Write the installation path into the registry
  ; WriteRegStr HKLM SOFTWARE\Gews\rsys1 "Install_Dir" "$INSTDIR\rsys1"
  
  ; Write the uninstall keys for Windows
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "DisplayName" "Gews"
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "UninstallString" '"$INSTDIR\uninstall.exe"'
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoModify" 1
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "工作面地质测量管理系统" 

  ;SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\sys3
  
  ; Put file there
  File /r "rsys3\*.*"
  
  ; Write the installation path into the registry
  ; WriteRegStr HKLM SOFTWARE\Gews\sys1 "Install_Dir" "$INSTDIR\sys1"
  
  ; Write the uninstall keys for Windows
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "DisplayName" "Gews"
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "UninstallString" '"$INSTDIR\uninstall.exe"'
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoModify" 1
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "工作面动态防突管理系统帮助文件" 

  ;SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\sys4
  
  ; Put file there
  File /r "rsys4\*.*"
  
  ; Write the installation path into the registry
  ; WriteRegStr HKLM SOFTWARE\Gews\sys1 "Install_Dir" "$INSTDIR\sys1"
  
  ; Write the uninstall keys for Windows
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "DisplayName" "Gews"
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "UninstallString" '"$INSTDIR\uninstall.exe"'
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoModify" 1
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

Section "瓦斯预警系统管理平台" 

  ;SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\sys5
  
  ; Put file there
  File /r "rsys5\*.*"
  
  ; Write the installation path into the registry
  ; WriteRegStr HKLM SOFTWARE\Gews\sys1 "Install_Dir" "$INSTDIR\sys1"
  
  ; Write the uninstall keys for Windows
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "DisplayName" "Gews"
  ; WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "UninstallString" '"$INSTDIR\uninstall.exe"'
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoModify" 1
  ; WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

; Optional section (can be disabled by the user)
Section "桌面快捷方式"

  CreateDirectory "$SMPROGRAMS\Gews"
  CreateShortCut "$SMPROGRAMS\Gews\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\Gews\工作面瓦斯涌出动态特征管理系统.lnk" "$INSTDIR\sys1\1.GasEmission.exe" "" "$INSTDIR\sys1\1.GasEmission.exe" 0
  CreateShortCut "$SMPROGRAMS\Gews\工作面采掘进度管理系统.lnk" "$INSTDIR\sys2\2.MiningScheduling.exe" "" "$INSTDIR\sys2\2.MiningScheduling.exe" 0
  CreateShortCut "$SMPROGRAMS\Gews\工作面地质测量管理系统.lnk" "$INSTDIR\sys3\3.GeologyMeasure.exe" "" "$INSTDIR\sys3\3.GeologyMeasure.exe" 0
  CreateShortCut "$SMPROGRAMS\Gews\工作面动态防突管理系统.lnk" "$INSTDIR\sys4\4.OutburstPrevention.exe" "" "$INSTDIR\sys1\4.OutburstPrevention.exe" 0
  CreateShortCut "$SMPROGRAMS\Gews\瓦斯预警系统管理平台.lnk" "$INSTDIR\sys5\5.WarningManagement.exe" "" "$INSTDIR\sys1\5.WarningManagement.exe" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews"
  DeleteRegKey HKLM SOFTWARE\Gews

  ; Remove files and uninstaller
  Delete $INSTDIR\sys1\*.*
  Delete $INSTDIR\sys2\*.*
  Delete $INSTDIR\sys3\*.*
  Delete $INSTDIR\sys4\*.*
  Delete $INSTDIR\sys5\*.*
  Delete $INSTDIR\uninstall.exe
  Delete $INSTDIR\*.*
  Delete $INSTDIR\sys1
  Delete $INSTDIR\sys2
  Delete $INSTDIR\sys3
  Delete $INSTDIR\sys4
  Delete $INSTDIR\sys5

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\Gews\sys1\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\Gews\sys1"
  RMDir "$INSTDIR\Gews\sys1"

SectionEnd
