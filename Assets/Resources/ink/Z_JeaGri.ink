INCLUDE globals.ink

J'ai l'impression de vivre dans un cauchemar. #next:Z_Demo_Day/false #bg:grigri_jea #gsprite:jea_grigri #name:nothing #clear:all #music:Spirit/Distortion #audio:Jeanne
Tout revient pour me hanter.
Pourquoi...
Pourquoi est-ce que tout me fait si mal ?
Apres tout ce temps,
Je pensais que c'etait fini...
+[Maman ?]
    Arianne ma cherie...
    Non je n'ai pas a te meler a tout ca.
    A mes problemes...
+[Ou suis-je ?]
    Je ne sais pas...
    Mais laisse moi a mes problemes...
    Je ne veux pas t'ennuyer avec.
-Je ne veux pas que vous me parliez de cela.
J'en ais deja trop entendu.
+[Tu peux m'en parler.]
    Tu es sur...?
    -> Puzzle_Moment
+[Je suis la pour toi maman.]
    Ma cherie...
    -> Puzzle_Moment

=== Puzzle_Moment ====
~grigriLives = 8
Tout est trop confus dans ma tete... #puzzle:puzzle_0
~CHOICES_TEST = Z0

Aller au service funeraire...
C'est comme si je revivais tout.
Encore.
Et encore...
Et encore.....
En boucle.
Sans echappatoire.
Les memes discussions...
Qu'allons nous faire du corps...
Comment organiser l'enterrement... 
-> END