INCLUDE globals.ink

~grigriLives--
J'en ai marre. #next:Jour_2/false #bg:grigri_eli #gsprite:eli_grigri #name:nothing #clear:all #music:Stupefaction
Marre, marre, marre, marre,
MARRE!!
Dégage de ma vu.
Vous m'enragez, m'enragez!
+[Calme toi, ca ne sert à rien.]
    ~CHOICES_TEST = GAlo0
    Je peux pas me calmer.
    Je veux pas me calmer!
    Juste...
    Juste part!
    -> END
+[Juste ecoute moi!]
    ~CHOICES_TEST = GAlo1
    Pourquoi je t'ecouterais?
    Tu ne sais rien!
    -> Puzzle_Moment

=== Puzzle_Moment ====
Comme si tu pouvais me comprendre. #puzzle:puzzle_1

Rien que voir vos visage niais et heureux.
J'ai impression de le voir lui.
Ca me rappelle ce foutu halloween.
Ce putain d'halloween.
J'aimerais l'oublier mais vous existez toujours pour me le rappeler.
Si seulement vous pouviez tous disparaitre.
Pour toujours.
-> END