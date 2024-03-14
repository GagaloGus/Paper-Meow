INCLUDE ../globalVars.ink

{beekoTalk == "": -> main}
{beekoTalk == "canGetGlider": -> GiveGlider}
{beekoTalk == "hasGlider": -> PostGlider}

=== main ===
Oye.. bzz bzz #speaker:beeko #emotion:neutral
+[¿Quien eres?]
-Me llamo Beeko.
¿Tu eres el que va a ir a romper la barrera verdad?
+[Sí, supongo]
-Ya veo..
Yo no me fío nada.
No te lo recomiendo, no eres el primero que lo intenta.
+[Tengo que hacerlo]
-Jeje, supongo que no hay nada que pueda hacer para impedirtelo.
Buena suerte, supongo.
Si sobrevives, tengo una cosita para ti al otro lado de la barrera. Así que no mueras :3
~startPatrol("beeko", 0, "false")
~beekoTalk = "canGetGlider"
->END


=== GiveGlider ===
Wow. Realmente lo hiciste.
Bueno, aquí tienes.
~beekoTalk = "hasGlider"
Con este planeador podras volar muy lejos, capaz incluso llegar a zonas antes inaccesibles.
Puedes usarlo pulsando Espacio en el aire
+[¡Gracias!]
bzz bzz
~giveItem("paper_glider", 1)
~ changeMood("beeko", "idle")
->END

=== PostGlider ===
~playEmote("question")
Sigues aqui? #speaker:beeko #emotion:neutral
Recuerda que puedes usar el planeador pulsando Espacio en el aire
bzz bzz
~ changeMood("beeko", "idle")
-> END