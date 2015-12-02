C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc /optimize /unsafe /t:exe /out:RunUO.exe /win32icon:Server\runuo.ico /recurse:Server\*.cs
copy RunUO.exe.config.Windows RunUO.exe.config
pause