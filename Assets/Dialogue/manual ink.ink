INCLUDE globalVars.ink

//salto a un apartado en concreto nada mas empezar el dialogo
->main

//indicador de apartado
== main ==
Minituto de ink
Esto es texto normal
    +[Respuesta 1]
     hola! //necesitas ahora o seguir escribiendo texto, o saltar a otro apartado, o terminar con ->END
     ->apartado2
    +[Respuesta 2]
    yuhuu
        ++[subpregunta]
        los dos ++ indican que es otra pregunta distina, imaginalo como una lista
        ->END
    //Como maximo 4 respuestas
    
== apartado2 ==
Esto son tags! #speaker:kittybot #emotion:happy

Hay 2 tipos de tags: speaker, emotion
Speaker es el nombre del npc en cuestion (siempre en minusculas!)
Emotion es la emocion del icono del npc (hay veces que un npc puede o no tener algunas emociones)
Siempre si se escribe el tag speaker tambien se tiene que escribir el tag de emotion
El tag speaker tiene que coincidir con el id_name que tiene cada NPC en su NPC Behaviour

~changeMood("id_name","emocion")
~playEmote("question")

->END