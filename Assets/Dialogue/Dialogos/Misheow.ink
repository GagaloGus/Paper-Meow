INCLUDE ../globalVars.ink

{trama == "1-3 goToMisheow" : -> busquedaMisheow}
{trama == "1-4 entrenarStart" : -> pegateconmaniqui}
{trama == "1-4 postEntrenar" : -> despuesDeEntrenar}
{trama == "1-5 aPorLosEsdras" : ->aporEsdras}

//Principal sin desbloquear
~changeMood("misheow", "punch")
....  #speaker:misheow #emotion:neutral
(Se le ve muy concentrado.. #speaker:kittybot #emotion:suspicious
mejor no le molestemos..) #speaker:kittybot #emotion:neutral
->END

== busquedaMisheow ==
 ~startPatrol("kittybot", 1, "true")
~changeMood("misheow", "punch talk")
Hola.#speaker:misheow #emotion:neutral
+[Hola. ¿Conoces a un tal Misheow?]
Pues si. Justo estas hablando con él jeje #speaker:misheow #emotion:happy
-> pregunta
+[Hola ¿eres Misheow?]
Si, soy yo
-> pregunta

== pregunta ==
~playEmote("question")
~changeMood("misheow", "talk")
¿Qué necesitais? #speaker:misheow #emotion:neutral
 Estamos buscando a un amigo llamado Rufus, es un gato bajito, con cara de pocos amigos y serio. #speaker:kittybot #emotion:think
 Vi a alguien con esa descripción hace un rato, iba camino a Paper Town. #speaker:misheow #emotion:neutral
 Si queréis encontrarle tenéis que ir por esa zona. 
 +[Muchas gracias Misheow.]
 De nada, no sois de por aquí ¿Verdad? #speaker:misheow #emotion:neutral
 No, hemos aparecido en este mundo sin saber por qué. #speaker:kittybot #emotion:neutral
 Hmmm que raro… #speaker:misheow #emotion:neutral
 Pues os tengo que advertir, este no es un lugar muy seguro, #speaker:misheow #emotion:angry
 Hace un tiempo empezaron a invadirnos unos pulpos llamados Esdras.
 Han tomado el control de casi toda la isla.
 ¿Son muy peligrosos? #speaker:kittybot #emotion:neutral
 Sí, pero con el arma adecuada podréis avanzar. #speaker:misheow #emotion:happy
 Venid conmigo, os voy a entrenar para que podáis defenderos.
 ~finishQuest("PlotQuest_1-3")
 ~startPatrol("misheow",0,"false")
 -> END
 
 ==pegateconmaniqui==
 ~startPatrol("kittybot", 2, "true")
 Peléate un poco con el maniquí, quiero ver que puedes hacer #speaker:misheow #emotion:happy
 ->END
 
 == despuesDeEntrenar ==
  ~changeMood("misheow", "talk")
 Ya estáis listos para ir en busca de vuestro amigo. #speaker:misheow #emotion:neutral
 Tened mucho cuidado y estar siempre alerta, os podrían atacar en cualquier momento.
 Muchas gracias por ayudarnos. #speaker:kittybot #emotion:happy
 De nada. #speaker:misheow #emotion:happy
 ~changeMood("misheow", "give")
 Antes de que os vayáis tomad, os será de ayuda en vuestra aventura.
 ¡Meow! Muchas gracias, has sido muy amable ayudándonos. #speaker:kittybot #emotion:happy
 +[¡Adios!]
 ~playEmote("question")
 ~changeMood("misheow", "talk")
 -¿Vais entonces a Paper Town? #speaker:misheow #emotion:neutral
 Esa es la intención, sí. #speaker:kittybot #emotion:neutral
 Va a ser algo difícil… #speaker:misheow #emotion:angry
 Hace un tiempo los Esdras nos alejaron del resto de la isla,
 y han construido una barrera que que nos aísla completamente de los otros pueblos.
 Hemos intentado cruzarla varias veces, pero no lo hemos conseguido. Son muchos y muy fuertes.
 +[Nos encargaremos de ellos.]
 -Ya veo.
 ~changeMood("misheow", "give")
 entonces necesitarás esto. #speaker:misheow #emotion:neutral
 +[¡Meow! ¿Y esto?]
 ~changeMood("misheow", "talk")
 -Os servirá para abriros paso entre los Esdras. #speaker:misheow #emotion:happy
 Buena suerte, ¡Y a por ellos!
 ~trama = "1-5 aPorLosEsdras"
 ~giveItem("money", 250)
 ~giveItem("cutter_sword", 1)
 ~giveQuest("PlotQuest_1-5")
 ~startPatrol("misheow", 1, "true")
  ~startPatrol("kittybot", 3, "true")
 -> END
 
 ==aporEsdras==
 Mucha suerte. #speaker:misheow #emotion:neutral
Ten en cuenta que los esdras estan guardando la entrada.
Tendras que vencerlos a todos antes de poder romper la barrera. #speaker:misheow #emotion:angry
¡Mucha suerte Sko! #speaker:kittybot #emotion:happy
 ->END
