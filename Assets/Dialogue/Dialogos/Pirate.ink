INCLUDE ../globalVars.ink

{pirataTalk == "" : ->main}
{pirataTalk == "giveSword" : -> damemiespada}
{pirataTalk == "noSword" : -> enfadao}

== main ==
Hola #speaker:pirate #emotion:happy
¿Te importaria sujetarme mi espada un segundo?
+[Wtf]
    ¿Por qué me miras así?
    Bueno, tu mismo.
    ->END
+[Ehhh.. vale supongo]
    Garcias :3
    ~pirataTalk = "giveSword"
    ~giveItem("pirate_sword",1)
    ~changeMood("pirate", "idle")
->END

== damemiespada ==
Oye #speaker:pirate #emotion:happy
¿Me puedes devolver la espada?
+[Me la dejas un ratito más]
Vaale, pero solo un ratito.
->END
+[Nel]
    Huh.
    ¿Como que nel? #speaker:pirate #emotion:angry
    ++[Nel]
    ->ladronculiao

==ladronculiao==
~changeMood("pirate","angry")
¡Ey!#speaker:pirate #emotion:angry
Grrrrrrrrr >:(
~pirataTalk = "noSword"
~changeMood("pirate","idle")
->END
== enfadao ==
~changeMood("pirate","angry")
>:( #speaker:pirate #emotion:angry
+[Vale te la devuelvo]
    ~changeMood("pirate","talk")
    ¿Oh, en serio? #speaker:pirate #emotion:angry
    ++[No ajsjajs pringao]
    ->ladronculiao
->END