INCLUDE ../globalVars.ink

{trama == "1-1 start": -> main}
{trama == "1-1 waitPueblo": ->ir_al_pueblo}

== main ==
~ playEmote("exclamation")
¡Sko, despertaste! #speaker:kittybot #emotion:happy 
    +[¿Que? ¿Dónde meow estamos?]
    -Parece que estamos en otra dimensión.. #speaker:kittybot #emotion:sad 
    Una dimensión de papel. #speaker:pirate #emotion:happy
        + [¡¿Por que soy de papel?!]
        - Mientras estabas durmiendo he ido recopilando información del entorno #speaker:kittybot #emotion:think
        Según mi base de datos, es una dimensión desconocida. A si que no te sé decir por qué.
        Perdón :( #speaker:kittybot #emotion:sad
            +[Un momento.. ¿Y Rufus?]
            ~ playEmote("question")
            - No lo sé, creo que él cayó con nosotros por el agujero. #speaker:kittybot #emotion:neutral
            He visto un pueblo cerca de aquí. ¿Vamos a preguntar?
                +[Esta bién, vamos]
                ¡Vale! #speaker:kittybot #emotion:happy
                ~ giveQuest("PlotQuest_0-1")
                ~ trama = "1-1 goToPueblo"
                ->END
                +[Quiero mirar una cosa antes.]
                ~ trama = "1-1 waitPueblo"
                Vale. Cuando quieras ir al pueblo dímelo.
                -> END
                
            
== ir_al_pueblo ==            
¿Quieres ir ya para el pueblo? #speaker:kittybot #emotion:neutral
+[Sí, vamos]
¡Vale! #speaker:kittybot #emotion:happy
~ giveQuest("PlotQuest_0-1")
~ trama = "1-1 goToPueblo"
->END

+[Quiero mirar una cosa antes.]
Vale. Yo me quedaré por aquí mientras tanto.
-> END