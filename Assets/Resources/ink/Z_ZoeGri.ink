INCLUDE globals.ink

Je n'arrive pas a parler...#next:Z_Demo_Day/false #bg:grigri_neutral #gsprite:zoe_grigri #name:nothing #clear:all #music:Spirit/Distortion #audio:Zoe
J'ai l'impression d'etre bloque.
Non...
Rien ne me bloque..
Et pourtant...
Les mots ne sortent pas.
+[Tout va bien se passer.]
    Je ne peux pas vraiment en etre sur...
    Et si ça tourne mal ?
    Je ne pourrais pas réessayer...
    -> Puzzle_Moment
+[Prends ton temps.]
    Mais je ne peux pas...
    Tu parts bientot...
    C'est maintenant ou jamais.
    -> Puzzle_Moment

=== Puzzle_Moment ====
Je ne suis juste pas sur de quoi faire... #puzzle:puzzle_2

Maintenant qu'il n'est plus la.
Plus rien n'est pareil.
Il me guidait, mais maintenant je ne sais plus quoi faire.
J'aimerais qu'il soit toujours ici.
J'ai peur que ça se passe mal.

+[Nous somme là pour toi.]
    ~CHOICES_TEST = Z3
    Je pense...
    Que si c'est vous...
    Tout se passera bien.
    -> END
+[Il est temps d'être fort.]
    ~CHOICES_TEST = Z4
    Mais j'ai...
    Je ne suis pas pret...
    Désolé...
    -> END