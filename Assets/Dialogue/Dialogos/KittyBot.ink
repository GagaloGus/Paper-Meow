INCLUDE ../globalVars.ink

{trama == "1-1 start": -> main} //nada mas empezar
{trama == "1-1 waitPueblo": ->ir_al_pueblo} //espera a que digas que si al ir al pueblo
{trama == "1-1 goToPueblo" : -> preguntaPueblo} //Ya volando hacia el pueblo
{trama == "1-2 preguntaPueblo" : -> preguntaaAlguien} //espera a que preguntes a alguien

{trama == "1-5 esdras" : -> preEsdras} //Antes de pegarte con los esdras
{trama == "1-6 barreraCaida" : -> postEsdras } //Tras pegarte con los esdras

== main ==
~ playEmote("exclamation")
¡Sko, despertaste! #speaker:kittybot #emotion:happy 
    +[¿Que? ¿Dónde meow estamos?]
    -Parece que estamos en otra dimensión.. #speaker:kittybot #emotion:sad 
    Una dimensión de papel.
        + [¡¿Por que soy de papel?!]
        - Mientras estabas durmiendo he ido recopilando información del entorno #speaker:kittybot #emotion:think
        Según mi base de datos, es una dimensión desconocida. A si que no te sé decir por qué.
        Perdón :( #speaker:kittybot #emotion:sad
            +[Un momento.. ¿Y Rufus?]
            ~ playEmote("question")
            - No lo sé, creo que él cayó con nosotros por el agujero. #speaker:kittybot #emotion:neutral
            He visto un pueblo cerca de aquí. ¿Vamos a preguntar?
                +[Esta bién, vamos]
                ->setupPueblo
                
                +[Quiero mirar una cosa antes.]
                Vale. Cuando quieras ir al pueblo dímelo.
                ~ trama = "1-1 waitPueblo"
                ~ changeMood("kittybot", "idle")
                -> END
            
== ir_al_pueblo ==            
¿Quieres ir ya para el pueblo? #speaker:kittybot #emotion:neutral
+[Sí, vamos]
-> setupPueblo
+[Quiero mirar una cosa antes.]
Vale. Yo me quedaré por aquí mientras tanto.
~ changeMood("kittybot", "idle")
-> END
                

== setupPueblo ==
¡Vale! #speaker:kittybot #emotion:happy
He visto un camino para llegar al pueblo, sigueme! #speaker:kittybot #emotion:map
~ giveQuest("PlotQuest_1-1")
~ changeMood("kittybot", "idle")
~ startPatrol(0)
->END

== preguntaPueblo ==
Aqui estamos. #speaker:kittybot #emotion:neutral
¿Por qué no pruebas a preguntar a alguien?
~giveQuest("PlotQuest_1-2")
~trama = "1-2 preguntaPueblo"
->END

== preguntaaAlguien==
¿Por qué no pruebas a preguntar a alguien? #speaker:kittybot #emotion:neutral
->END

== preEsdras ==
Sko. #speaker:kittybot #emotion:neutral
¿Estas seguro de que quieres ir?
No sabemos cuantos son, ni como de fuertes son. #speaker:kittybot #emotion:sad
    +[Estoy seguro]
    Hmmm...
    Bueno, confío en ti. #speaker:kittybot #emotion:neutral
    ¡Tu puedes! #speaker:kittybot #emotion:happy
    ->END

== postEsdras ==
¡Sko, lo hicíste! #speaker:kittybot #emotion:happy
¡Ahora podremos ir a Paper Town sin problema!
Wow, realmente lo hicíste. #speaker:misheow #emotion:neutral
+[¿Misheow?]
-Yuju
¡Misheow, lo logramos! #speaker:kittybot #emotion:happy
Ya veo ya veo.. #speaker:misheow #emotion:neutral
Sinceramente no me esperaba mucho, pero me has sorprendido.
Bien hecho Sko #speaker:misheow #emotion:happy
+[:3]
-Supongo que ahora ireís a por vuestro amigo. #speaker:misheow #emotion:
Sí, hay que ir a Paper Town ahora #speaker:kittybot #emotion:neutral
Hm, yo tambien tendria que ir a visitar a un amigo ahora que ha caido la barrera. #speaker:misheow #emotion:neutral
Bueno, mucha suerte vosotros dos.
~changeMood("misheow","give")
Ah si, tambien tomad esto, como muestra de mi gratitud.
¡Muchas gracias! #speaker:kittybot #emotion:happy
Chau. #speaker:misheow #emotion:happy
~giveQuest("PlotQuest_1-5")
~getPegatina(1)
->END








