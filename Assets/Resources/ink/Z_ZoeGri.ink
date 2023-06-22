INCLUDE globals.ink

Je ne dois pas perdre face! #next:Z_Demo_Day/false #bg:grigri_neutral #gsprite:zoe_grigri #name:nothing #clear:all #music:Spirit/Distortion #audio:Zoe
~CHOICES_TEST = Z4
La situation est deja assez complexe.
Si moi j'arrete tout le monde va désespere.
C'est mon devoir.
Je dois le faire.
Pour le bien de mes amis.
+[De quoi est-ce que tu parle ?]
    Je dois t'aider à etre heureuse.
    Tout le monde sera content comme ça.
    -> Puzzle_Moment
+[Zoe c'est n'importe quoi...]
    Ne me juge pas comme ça!
    C'est mon devoir!
    Si tu n'aimes pas ça, ce n'est pas mon probleme.
    -> END

=== Puzzle_Moment ====
Même si c'est un petit mensonge blanc au début..
On finira par vraiment l'etre!
C'est un mal pour un bien. #puzzle:puzzle_1

Enfin...
Je crois...

+[Tu te fais du mal...]
    ~CHOICES_TEST = Z3
    Je ne me fais pas mal..
    Je repousse le mal.
    Je le repousse loin de moi.
    N'est-ce pas?
    N'est-ce pas...?
    -> END
+[Laisse moi t'aider.]
    Je n'ai pas besoin d'aide.
    Je suis l'aide.
    -> END