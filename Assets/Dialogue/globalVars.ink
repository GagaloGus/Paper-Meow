VAR trama = "1-1 start"
VAR beekoTalk = ""

EXTERNAL playEmote(emoteName)
EXTERNAL changeMood(npcId,emoteName)
EXTERNAL giveQuest(questID)
EXTERNAL finishQuest(questID)
EXTERNAL giveItem(itemID,amount)
EXTERNAL getPegatina(number)
EXTERNAL startPatrol(listNumber)

=== function playEmote(emoteName) ===
~return "question"

=== function changeMood(npcId,emoteName)===
~return "talk"

=== function giveQuest(questID) ===
~return "temp"

=== function finishQuest(questID) ===
~return "temp"

=== function giveItem(itemID,amount)===
~return "apple"

=== function getPegatina(number) ===
~return 1

=== function startPatrol(listNumber) ===
~return 1