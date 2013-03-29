dds.net
=======

The .net wrapper around Bo Haglund  bridge double dummy solver. http://privat.bahnhof.se/wb758135/

api link http://dds-2.apphb.com/metadata

Bridge.Domain project - incapsulate bridge game logic and supposed to be used to with another bridge based applications.

Dds.Net project -- integration with dds.dll

Dds.Net.Sample project -- sample of game based on pbn code. 

You can download sample project there -> https://github.com/anorsich/dds.net/raw/master/builds/dds.net-0.0.1.zip

Following dds.dll methods implemented:

1. CalcDDtablePBN
2. SolveBoardPb


Notes:
All apps who reference this Dds.Net should x86 based, otherwise you will recieve following exception:

http://stackoverflow.com/questions/2023766/an-attempt-was-made-to-load-a-program-with-an-incorrect-format-even-when-the-p
