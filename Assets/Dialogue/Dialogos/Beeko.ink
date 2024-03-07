INCLUDE ../globalVars.ink

{beekoTalk == "": -> main}
{beekoTalk == "hasGlider": -> PostGlider}

=== main ===
bzz bzz bzz #speaker:beeko #emotion:neutral
Quieres volar conmigo?
+[¿Como?]
   ->GiveGlider 

+[No, estoy bien]
Bueno, tu mismo, bzz bzz
~ changeMood("beeko", "idle")
-> END

=== GiveGlider ===
Toma
~beekoTalk = "hasGlider"
Con este planeador podras volar muy lejos
+[Ooo vale gracias]
bzz bzz
~giveItem("paper_glider", 1)
~ changeMood("beeko", "idle")
->END

=== PostGlider ===
~playEmote("question")
Sigues aqui? #speaker:beeko #emotion:neutral
bzz bzz
~ changeMood("beeko", "idle")
-> END