Last Wish
=======

Last Wish is an open source Ultima Online Freeshard running on RunUO, aiming to realize one last wish from all players who have played on "Hege" Shard some years ago.

It runs over default Ultima Online concept, with just some custom additions to make the shard more friendly. Anyone is free to join and even run his own Last Wish version.

We use UO Client 7.0.15.1 (Publish 51, June 15 2011), which can be downloaded from <http://sourceforge.net/projects/ultimaonlineclient70151>

To Run on Windows: Just download the repository and run `CompileServerWindows.bat` to compile the server.

Complete Installation on Ubuntu:

    apt-get update
    apt-get install mono-complete git
    git clone --depth 1 https://github.com/felladrin/last-wish.git
    cd last-wish
    dmcs -optimize+ -unsafe -t:exe -out:RunUO.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:MONO -recurse:Server/*.cs
    cp RunUO.exe.config.Linux RunUO.exe.config