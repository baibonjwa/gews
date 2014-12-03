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
OutFile "GewsSetup.exe"

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
Section "工作面瓦斯涌出动态特征管理系统 (required)" 


  SectionIn RO
  
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR\sys1
  
  ; Put file there
  File "GewsSetup.nsi"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\Gews\sys1 "Install_Dir" "$INSTDIR\sys1"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "DisplayName" "NSIS Example2"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Gews" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts"

  CreateDirectory "$SMPROGRAMS\Example2"
  CreateShortCut "$SMPROGRAMS\Example2\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\Example2\Example2 (MakeNSISW).lnk" "$INSTDIR\example2.nsi" "" "$INSTDIR\example2.nsi" 0
  
SectionEnd

;--------------------------------

; Uninstaller

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Example2"
  DeleteRegKey HKLM SOFTWARE\NSIS_Example2

  ; Remove files and uninstaller
  Delete $INSTDIR\example2.nsi
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\Example2\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\Example2"
  RMDir "$INSTDIR"

SectionEnd
