INCLUDE globals.ink

{CHOICES_TEST has Z0: -> AfterJeanneGrigri}

{CHOICES_TEST has Z1: -> AfterMartinYes}
{CHOICES_TEST has Z2: -> AfterMartinNo}

{CHOICES_TEST has Z3: -> AfterZoeYes}
{CHOICES_TEST has Z4: -> AfterZoeNo}

~grigriLives = 0

A travers la vitre je regarde les montagnes de mon enfance disparaitre peu à peu au loin alors que la voiture s'éloigne d'Adonis. #name:nothing #bg:menu #music:Happy/GREG
"Comment s'est passé ta semaine chérie ?" #name:Jeanne #transbg:transbg_neutral
Je tourne ma tête en direction de ma mère au volant. #name:nothing
"Bien maman." #name:Arianne
Elle possède une expression bien plus sereine qu'en début de semaine. #name:nothing #trans:trans_center

La mort de Mamie Nat nous a pris au dépourvu. #bg:bedroom
Comme papa travail au restaurant, seul maman et moi sommes descendu afin de s'occuper du déménagement.
Et c'est en rangeant dans une chambre que j'ai découvert un bracelet très particulié.
"Qu'est-ce que c'est..." #name:Arianne #sprite:ari_neutral/2
C'est à l'intérieur d'une petite boite qu'il était caché. #name:nothing
Grand-mère Nat était connue pour ses bijoux, mais je ne l'ai jamais vu porter quelque chose ressemblant à ça.
L'apparence singulière de celui-ci et la curiosité me l'ont fait garder sur moi. #trans:trans_center

Plus tard dans la soirée, j'ai découvert que ce bracelet renfermait un pouvoir qui me dépasse. #bg:kitchen #music:nothing/GREG
"Arianne, tu ne vois pas que je suis fatiguée ?" #name:Jeanne #music:Pensive/Solitude #sprite:ari_sad/1  #sprite:jea_neutral/3
"Oui, mais-" #name:Arianne #sprite:ari_shocked/1
"Ce rendez-vous était exténuant, et je n'ai vraiment pas envie d'en reparler." #name:Jeanne #sprite:ari_sad/1
"Remonte dans ta chambre s'il te plait." #name:Jeanne
Maman venait de rentrer du serfice funéraire, et j'ai probablement parler trop vite. #name:nothing
Mais alors que j'allais monter dans ma chambre, l'une des perle du bracelet s'est mise à briller fortement, et la pièce se remplis d'une lumière blanche. #next:Z_JeaGri/now

===AfterJeanneGrigri===
Sur le moment, j'étais confuse, persuadé que ce n'était qu'une illusion ou un rêve. #name:nothing #bg:kitchen
Mais je compris rapidement que ce que j'avais vu était bien réel, et qu'il me permettait d'entendre la pensée des autres. #music:Pensive/Solitude
J'ai pu parler avec ma mère du stress qu'elle éprouvait, et je penses qu'on en avait toute les deux besoin. #trans:trans_center

"Je crois que tu as raison, revenir m'a aidé à accepter pour Yann." #name:Arianne
Elle pose sa main sur mes cheveux, les ébourrifant un peu avant de reprendre le volant. #name:nothing #bg:menu
"Je suis contente pour toi." #name:Jeanne
Je souris doucement. #name:nothing
Puis je reçois un message sur mon téléphone. #music:nothing/Solitude
"Martin et Zoé te disent au revoir." #name:Arianne 
"Tu peux les remercier." #name:Jeanne #music:Happy/GREG
"Comment vont-il d'ailleurs ?"
"J'ai passé tellement de temps dans cette maison, j'aurais dû sortir un peu plus."
"Très bien." #name:Arianne
"Zoé est toujours la même." #trans:trans_center
-> BeforeZoe

===BeforeZoe===
"Energique et heureuse en permanence." #bg:square
"Hey regardez !" #name:Zoé #sprite:zoe_neutral/4
"Je suis la plus grande !" #name:Zoé #music:Happy/Enfant 
"Zoé.. fait attention s'il te plait..." #name:Martin #sprite:mar_awkward/1 #sprite:ari_neutral/2
"Ne t'inquiète pas ! Je suis Miss Mouse ! Voleuse extra-ordinaire !" #name:Zoé
"Je suis aussi agile que le vent et aussi discrète que la nuit." #name:Zoé
"Elle nous a refait jouer à Detective Wolf tous ensemble." #name:Arianne
"Detective Wolf ? La BD ?" #name:Jeanne #clear:all
"J'ai pas entendu parler de ça depuis si longtemps."
"C'est sur. Mais c'était sympa, ça nous a replongé un peu dans un temps plus simple." #name:Arianne
"Sans problèmes.." #trans:trans_center

"J'ai pu lui parler un peu seule à seule." #name:Arianne
"Je suis désolé d'avoir gâché nos retrouvailles. Je sais que tu avais hâte qu'on se revois." #name:Arianne #music:Pensive/Dialogue #sprite:zoe_neutral/1 #sprite:ari_neutral/2
"Dis pas ça, c'est de la faute d'Elise." #name:Zoé #next:Z_ZoeGri/true
"Elle s'est invité toute seule. C'est à elle de s'excuser."
"Non non, j'ai mal réagis hier. Et on n'en serait pas là sinon." #name:Arianne
"..." #name:Zoé
"C'est pas..." #name:Arianne
"..."
"C'est pas vrai." #name:Zoé
"Si, j'ai été méchante." #name:Arianne
"M-Mais tu n'as pas été méchante ! Juste un peu..." #name:Zoé
"Comment dire..." #next:Z_ZoeGri/false
-> AfterZoeNo

===AfterZoeYes===
"Si jamais tu as trop mal, tu n'es pas obligé de rejeter sur quelqu'un d'autre." #name:Arianne #music:Pensive/Solitude #sprite:zoe_neutral/1 #sprite:ari_neutral/2
"Et si tu ne veux pas en parler tu peux juste me faire un câlin."
"Mais tu déteste mes câlins." #name:Zoé
"C'est faux, je les ais toujours aimé." #name:Arianne
->BeforeMartin

===AfterZoeNo===
"On a tous des mauvais souvenir, et j'oublie que je ne suis pas seule pafois." #name:Arianne #music:Pensive/Solitude #sprite:zoe_neutral/1 #sprite:ari_neutral/2
"Je suis désolé Zoé."
Zoé souris et se sèche une petite larme. #name:nothing
"Je ne sais pas comment tu fais pour toujours dire ce que tu penses." #name:Zoé
"C'est pas toujours une bonne chose. Ton optimisme l'est bien plus." #name:Arianne
"J'insiste, c'est vraiment une bonne chose." #name:Zoé
->BeforeMartin


===BeforeMartin===
"Je suis sûr qu'elle s'en tire bien." #name:Arianne #clear:all
"Et Martin ?" #name:Jeanne
"Il a un peu changé, un peu plus posé qu'avant." #name:Arianne #trans:trans_center

"Au départ il avait un peu de mal à me parler." #name:Arianne #bg:rugby #music:Pensive/Dialogue
"Le départ de Yann l'a beaucoup affecté je penses."
"Tu..." #name:Martin #sprite:mar_neutral/2 #sprite:ari_neutral/1
"Tu penses que c'est possible pour nous de redevenir comme avant ?" #next:Z_MarGri/true
"Avant... Avant que tout change ?"
"Martin je..." #name:Arianne
"Je n'avais pas envie de partir tu sais..." #name:Martin
"Martin..." #name:Arianne
"C'était juste..." #name:Martin
"..." #name:Arianne
"Pourquoi c'est si dur de parler ?" #name:Martin #next:Z_MarGri/false
->AfterMartinNo

===AfterMartinYes===
"Je peux comprendre que tu te sente perdu." #name:Arianne #music:Pensive/Dialogue #sprite:mar_neutral/2 #sprite:ari_neutral/1
"Je me suis sentis comme ça aussi."
"..."  #name:Martin
"Je vois maintenant à quel point tu l'admirais." #name:Arianne
"Je..." #name:Martin
"Je ne savais plus quoi faire."
"Je l'ai perdu lui mais je vois ais perdu vous." #name:Martin
"..." #name:Arianne
"Tu veux en parler un peu ?"
"Ouais..." #name:Martin
->EliAlo

===AfterMartinNo===
"C'est pas..." #name:Martin #music:Pensive/Dialogue #sprite:mar_neutral/2 #sprite:ari_neutral/1
"C'est pas grave."
"On est pas là pour parler de tout ça."
"Martin si." #name:Arianne
"Je n'ai pas..." #name:Martin
"Je n'ai plus vraiment envie."
"Désolé."
"C'est pas grave." #name:Arianne
"Prend ton temps."
"Merci..." #name:Martin
->EliAlo

===EliAlo===
"Je pense qu'il est sur le bon chemin." #name:Arianne #trans:trans_center #music:nothing/Dialogue #clear:all

"Et j'ai appris une ou deux choses sur lui dont je te parlerais un autre jour..." #bg:menu
"Oh et évidemment j'ui allé voir Greg." #name:Arianne #music:Happy/GREG
"Il ne doit pas avoir vieillis d'un pouce." #name:Jeanne
"Tu serais surprise du nombre de cheveux blanc visibles." #name:Arianne
"Arrête, tu me rappel que je suis sensé être son ainée." #name:Jeanne #trans:trans_center

"Il continue de gérer son petit magasin." #name:Arianne #bg:market
"Arianne !? Bon dieu, ce que tu as grandis !" #name:Greg #music:nothing/GREG #sprite:greg_neutral/3 #sprite:ari_neutral/1
"La dernière fois que je t'ai vu, tu faisais cette taille !" #name:Greg
"Je sais que j'ai grandis mais tu exagère un peu." #name:Arianne
"Hahaha ! Toujours." #name:Greg
"Et Elise l'aide sur le côté." #name:Arianne #clear:all
"Comment elle se porte ?" #name:Jeanne #trans:trans_center

"Elise elle est..." #bg:square
"Qu'est-ce que tu fais ici Arianne ?" #name:Elise #music:Fear/Colère
"On va partir d'ici, okay ?" #name:Zoé
"Je te parle pas, la barbe à papa." #name:Elise
"Qu'est-ce que tu me veux?" #name:Arianne
"Réponds à ma question." #name:Elise
"Je ne suis pas sur que ça te regarde." #name:Arianne
"Plus que tu ne le crois." #name:Elise
"Vous tous d'ailleurs !"
"Dense ?" #name:Jeanne #trans:trans_center #clear:all

"Ouais c'est ça..." #name:Arianne #bg:forest
"Vous ne pouvez pas me comprendre !" #name:Elise #sprite:eli_neutral/2
"Vous me regardez comme si j'étais un monstre mais vous ne savez rien !"
"Alors arrête de nous faire mal !" #name:Arianne
"Vous êtes bien pire !" #name:Elise
"Elle est dense." #name:Arianne #clear:all
"Mais..."
"..." #name:Elise #sprite:eli_neutral/2
"Je pense que c'est fini maintenant." #name:Arianne #trans:trans_center #clear:all

"J'ai aussi rencontré Aloïs." #name:Arianne #bg:mall_cultur #music:nothing/Colère
"Aloïs..." #name:Jeanne
"Ah oui ! Le petit génie."
"Je m'en souviens, il était de la même année que ton frère."
"On a eu du mal à parler." #name:Arianne
"Qu'est-ce que tu fais ici ?" #name:Arianne #sprite:alo_neutral/3 #sprite:ari_neutral/2
"C'est heum..." #name:Aloïs
"Pour mon costume d'Halloween..."
"..." #name:Arianne
"..." #name:Aloïs
"..." #name:Arianne
"Je euh..." #name:Aloïs
"P-Pardon..."
"Pas besoin." #name:Arianne
"Mais évite la prochaine fois. Je sais que tu sais." #name:Arianne
"O-Oui..." #name:Aloïs #clear:3
"Alors Arianne tu trouves ton..." #name:Zoé #sprite:zoe_neutral/3
"Bonheur ?"
"C'était qui ce type qui part en courant?"
"Aloïs." #name:Arianne
"Pourquoi est-ce qu'il est là ?" #name:Zoé
"Costume." #name:Arianne
"Oh je vois..." #name:Zoé
->BeforeEnd

===BeforeEnd===
"Quand au reste..." #name:Arianne #trans:trans_center #clear:all

Tu n'aurais jamais dû revenir à Adonis. #name:nothing #bg:grigri_neutral #music:Fear/Menace
Restes terrée dans ta maison. #trans:trans_center

"Passe la balle Zoé !" #name:Martin #bg:rugby
"Aaah trop loin !" #name:Zoé
"Je l'ai !" #name:Arianne #trans:trans_center

"Il est ruiné..." #name:Zoé #bg:kitchen #sprite:zoe_neutral/2
"Et c'est de ta faute !" #trans:trans_center

"Tu fais peur." #name:Elise #bg:mall_sport #sprite:eli_neutral/3 #sprite:ari_neutral/1
"Depuis quand ça t'intéresse le base-ball ?"
"On se découvre tes passions en cinq ans." #name:Arianne
"J'ui pas de la dernière pluie Arianne." #name:Elise #trans:trans_center

"..." #name:??? #bg:classroom #sprite:mon_neutral/2 #trans:trans_center

"Je préfère garder ça pour une prochaine fois." #name:Arianne #bg:menu #music:nothing/Menace #clear:all
"Le plus important c'est que tu te sois amusée." #name:Jeanne #music:Happy/Camaraderie
"Oui..." #name:Arianne
"On peut dire ça..."
Je deviens silencieuse en regardant la fenêtre. #name:nothing
Je ne sais pas si amuser sois le mot.
Mais je suis heureuse d'être revenu.
Au revoir Adonis.
Au revoir Yann.
Je sais enfin pourquoi tu es mort.
A la prochaine fois tout le monde.
A la prochaine fois... #next:nothing/false
-> END