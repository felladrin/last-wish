![Last Wish](http://i.imgur.com/EaqBWW9.png)

**An open source RunUO Freeshard hoping to fulfill one last wish.**

Live Shard Website: <https://felladrin.github.io/last-wish/>

Anyone is free to join and even run their own version of Last Wish.

It follows the original Ultima Online concept, with just some special scripts to make it a little more friendly.

The shard targets UO Client 7.0.15.1 (Download: [Installer](http://felladrin.com/download/UOClassicSetup_7_0_15_1.exe) or [Portable](http://felladrin.com/download/UOClassicSetup_7_0_15_1.zip)). And we recommend [UOS](http://felladrin.com/download/UOS_1_0_5.exe) for connecting.

## Running the server on Windows

1. [Download this repository](https://github.com/felladrin/last-wish/archive/master.zip) or clone it.
2. Execute `CompileServerWindows.bat` to generate *RunUO.exe* and *RunUO.exe.config* files on the folder
3. Launch RunUO.exe

## Running the server on Ubuntu

Here's all commands you need to execute in order to have the server ready:

    apt-get update
    apt-get install mono-complete git
    git clone --depth 1 https://github.com/felladrin/last-wish.git
    cd last-wish
    dmcs -optimize+ -unsafe -t:exe -out:RunUO.exe -win32icon:Server/runuo.ico -nowarn:219,414 -d:MONO -recurse:Server/*.cs
    cp RunUO.exe.config.Linux RunUO.exe.config
    mono RunUO.exe

If you want to run it as a background task use `nohup mono RunUO.exe >> console.log &` . Then, before logging out, type: `disown` to detach the proccess from the terminal.

## First administrative actions to take in-game

On the first launch, RunUO will ask you to create an administrator account. Do it, then login. Once there, the first thing you'll want to do is to decorate and populate the world.

### Decorating the World

Type `[admin` to open the shard admin panel. Navigate to *Administer >> World Building*, then generate: Teleporters, Doors, Moongates, Decoration, Signs.

### Populating the World

Type `[spawnmaps` to open the spawner panel. Select the places you want to populate then click *Apply* on the second page.

That's it! Now your shard is ready to receive players!
