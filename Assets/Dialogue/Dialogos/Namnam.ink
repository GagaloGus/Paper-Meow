INCLUDE ../globalVars.ink

{trama == "1-2 preguntaPueblo" : -> npcTalk}

.... #speaker:namnam #emotion:neutral

== npcTalk ==
¿Hmmm..? #speaker:namnam #emotion:neutral
¿Quién eres?
Hola, yo soy KittyBot. Estamos buscando información sobre donde estamos #speaker:kittybot #emotion:neutral
+[Yo me llamo Sko]
-Hmm... Deberíais hablar con Misheow #speaker:namnam #emotion:neutral
Él es el que más sabe sobre este lugar..
Con que un tal Misheow... #speaker:kittybot #emotion:think
+[¿Sabes quién es?]
-Nop, vamos a seguir buscando. #speaker:kittybot #emotion:neutral
~finishQuest("PlotQuest_1-2")
~changeMood("namnam","idle")
->END
