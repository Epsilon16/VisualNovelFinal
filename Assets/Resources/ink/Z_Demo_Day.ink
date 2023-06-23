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
Elle possède une expression bien plus sereine qu'il y a quelques jours. #name:nothing #trans:trans_center

La mort de Mamie Nat nous a pris au dépourvu. #bg:bedroom
Comme papa travail au restaurant, seul maman et moi sommes descendu afin de s'occuper du déménagement.
Et c'est en rangeant dans une chambre que j'ai découvert un bracelet très particulié.
"Qu'est-ce que c'est..." #name:Arianne #sprite:ari_neutral/2
C'est à l'intérieur d'une petite boite qu'il était caché. #name:nothing #item:grigri_8
Grand-mère Nat était connue pour ses bijoux, mais je ne l'ai jamais vu porter quelque chose ressemblant à ça.
L'apparence singulière de celui-ci et la curiosité me l'ont fait garder sur moi. #trans:trans_center #item:nothing

Plus tard dans la soirée, j'ai découvert que ce bracelet renfermait un pouvoir qui me dépasse. #bg:kitchen  #music:Pensive/Solitude
"Arianne, ne vois-tu pas que je suis fatiguée ?" #name:Jeanne #sprite:ari_sad/1  #sprite:jea_tired/3
"Oui, mais-" #name:Arianne #sprite:ari_shocked/1
"Ce rendez-vous était exténuant, et je n'ai vraiment pas envie d'en reparler." #name:Jeanne #sprite:ari_sad/1 #sprite:jea_angry/3
"Remonte dans ta chambre s'il te plait." #name:Jeanne
Maman venait de rentrer du service funéraire, et j'ai probablement parler trop vite. #name:nothing
Mais alors que j'allais monter dans ma chambre, l'une des perle du bracelet s'est mise à briller fortement, et la pièce se remplis d'une lumière blanche. #next:Z_JeaGri/now

===AfterJeanneGrigri===
Sur le moment, j'étais confuse, persuadé que ce n'était qu'une illusion ou un rêve. #name:nothing #bg:kitchen #music:Pensive/Solitude
Mais je compris rapidement que ce que j'avais vu était bien réel, et qu'il me permettait d'entendre la pensée des autres.
Il me suffisait de toucher le bracelet dés qu'il s'illuminait, et j'arrivais à lire les esprits.
J'ai pu parler avec ma mère du stress qu'elle éprouvait, et je penses qu'on en avait toute les deux besoin. #trans:trans_center

"Je crois que tu as raison, revenir m'a aidé à accepter pour Yann." #name:Arianne #bg:menu
Elle pose sa main sur mes cheveux, les ébourrifant un peu avant de reprendre le volant. #name:nothing #music:Happy/GREG
"Je suis contente pour toi." #name:Jeanne
Je souris doucement. #name:nothing
Puis je reçois un message sur mon téléphone. 
"Martin et Zoé te disent au revoir." #name:Arianne 
"Tu peux les remercier." #name:Jeanne
"Comment vont-ils d'ailleurs ?"
"J'ai passé tellement de temps à faire des cartons, j'aurais dû sortir un peu plus."
"Ils vont bien." #name:Arianne
"Zoé est toujours la même." #trans:trans_center
-> BeforeZoe

===BeforeZoe===
"Energique et heureuse en permanence." #bg:square
"Hey regardez !" #name:Zoé #sprite:zoe_neutral/4 #music:Happy/Enfant
"Je suis la plus grande !" #name:Zoé #sprite:zoe_happy/4
"Zoé.. fait attention s'il te plait..." #name:Martin #sprite:mar_awkward/1/flip #sprite:ari_neutral/2
"Ne t'inquiète pas ! Je suis Miss Mouse ! Voleuse extra-ordinaire !" #name:Zoé
"Je suis aussi agile que le vent et aussi discrète que la nuit." #name:Zoé #sprite:mar_neutral/1/flip
"Elle nous a refait jouer à Detective Wolf tous ensemble." #name:Arianne #clear:all
"Detective Wolf ? La BD ?" #name:Jeanne
"J'ai pas entendu parler de ça depuis un bon moment."
"C'est sur. Mais c'était sympa, ça nous a replongé un peu dans un temps plus simple." #name:Arianne
"Sans problèmes.." #trans:trans_center

"J'ai pu lui parler un peu seule à seule." #name:Arianne #music:nothing/Enfant
"Je suis désolé d'avoir gâché nos retrouvailles. Je sais que tu avais hâte qu'on se revoir." #name:Arianne #music:Pensive/Dialogue #sprite:zoe_sad/1 #sprite:ari_sad/2
"Dis pas ça, c'est de la faute d'Elise." #name:Zoé #next:Z_ZoeGri/true #sprite:zoe_awkward/1
"Elle s'est invité toute seule. C'est à elle de s'excuser."
"Non, j'ai mal réagis hier. Et on n'en serait pas là sinon." #name:Arianne
"..." #name:Zoé #sprite:zoe_sad/1
"C'est pas..." #sprite:zoe_awkward/1
"C'est pas vrai." #name:Zoé #sprite:zoe_sad/1
"Si, j'ai été méchante." #name:Arianne #sprite:ari_neutral/2
"M-Mais tu n'as pas été méchante ! Juste un peu..." #name:Zoé
"Comment dire..." #next:Z_ZoeGri/false #sprite:zoe_awkward/1
-> AfterZoeNo

===AfterZoeYes===
"C'est pas grave Zoé.." #name:Arianne #music:Pensive/Solitude #sprite:zoe_sad/1 #sprite:ari_neutral/2 #bg:square
"Si jamais tu as trop mal, tu n'es pas obligé de rejeter sur quelqu'un d'autre."
"Et si tu ne veux pas en parler tu peux juste me faire un câlin."
"Mais tu déteste mes câlins." #name:Zoé
"C'est faux, je les ais toujours aimé." #name:Arianne #sprite:zoe_happy/1 #sprite:ari_happy/2
->BeforeMartin

===AfterZoeNo===
"On a tous des mauvais souvenir, et j'oublie que je ne suis pas seule pafois." #name:Arianne #music:Pensive/Solitude #sprite:zoe_sad/1 #sprite:ari_neutral/2 #bg:square
"Je suis désolé Zoé."
"Je ne sais pas comment tu fais pour toujours dire ce que tu penses." #name:Zoé #sprite:zoe_neutral/1
"C'est pas toujours une bonne chose. Ton optimisme l'est bien plus." #name:Arianne
"J'insiste, c'est vraiment une bonne chose." #name:Zoé #sprite:ari_happy/2
->BeforeMartin


===BeforeMartin===
"Je suis sûr qu'elle s'en tire bien." #name:Arianne #clear:all
"Et Martin ?" #name:Jeanne
"Il a un peu changé, un peu plus posé qu'avant." #name:Arianne #trans:trans_center

"Au départ il avait un peu de mal à me parler." #name:Arianne #bg:rugby #music:Pensive/Dialogue
"Le départ de Yann l'a beaucoup affecté je penses."
"Tu..." #name:Martin #sprite:mar_sad/2 #sprite:ari_neutral/1
"Tu penses que c'est possible pour nous de redevenir comme avant ?" #next:Z_MarGri/true
"Avant... Avant que tout change ?" #sprite:mar_awkward/2
"Martin je..." #name:Arianne #sprite:ari_sad/1
"Je n'avais pas envie de partir tu sais..." #name:Martin #sprite:mar_sad/2
"Martin..." #name:Arianne
"C'était juste..." #name:Martin
"..." #name:Arianne
"Pourquoi c'est si dur de parler ?" #name:Martin #next:Z_MarGri/false #sprite:mar_awkward/2
->AfterMartinNo

===AfterMartinYes===
"Je peux comprendre que tu te sente perdu." #name:Arianne #music:Pensive/Solitude #sprite:mar_awkward/2 #sprite:ari_neutral/1 #bg:rugby
"Je me suis sentis comme ça aussi." #sprite:ari_sad/1
"..."  #name:Martin #sprite:mar_sad/2
"Je vois maintenant à quel point tu l'admirais." #name:Arianne #sprite:ari_neutral/1
"Je..." #name:Martin #sprite:mar_sad/2
"Je ne savais plus quoi faire."
"Je l'ai perdu lui mais je vous ais perdu vous." #name:Martin #sprite:mar_awkward/2
"..." #name:Arianne #sprite:ari_sad/1
"Tu veux en parler un peu ?" #sprite:ari_neutral/1
"Ouais..." #name:Martin #sprite:mar_neutral/2
->EliAlo

===AfterMartinNo===
"C'est pas..." #name:Martin #sprite:mar_awkward/2 #sprite:ari_neutral/1 #bg:rugby #music:Pensive/Solitude
"C'est pas grave." #sprite:mar_sad/2
"On est pas là pour parler de tout ça."
"Martin si." #name:Arianne #sprite:ari_sad/1
"Je n'ai pas..." #name:Martin
"Je n'ai plus vraiment envie."
"Désolé." #sprite:mar_awkward/2
"C'est pas grave." #name:Arianne #sprite:ari_neutral/1 #sprite:mar_sad/2
"Tu m'en parleras quand tu le voudra."
"Okay..." #name:Martin
"Merci Arianne." #sprite:mar_neutral/2
->EliAlo

===EliAlo===
"Je pense qu'il est sur le bon chemin." #name:Arianne #trans:trans_center #music:nothing/nothing #clear:all

"Et j'ai appris une ou deux choses sur lui dont je te parlerais un autre jour..." #bg:menu
"Oh et évidemment j'ui allé voir Greg." #name:Arianne #music:Happy/GREG
"Il ne doit pas avoir changé d'un pouce." #name:Jeanne
"Tu serais surprise du nombre de cheveux blanc visibles." #name:Arianne
"Arrête, tu me rappel que je suis sensé être son ainée." #name:Jeanne #trans:trans_center

"Il continue de gérer son petit magasin." #name:Arianne #bg:market
"Arianne !? Bon dieu, ce que tu as grandis !" #name:Greg #sprite:greg_neutral/3 #sprite:ari_neutral/1
"La dernière fois que je t'ai vu, tu faisais cette taille !" #name:Greg
"Je sais que j'ai grandis mais tu exagère un peu." #name:Arianne #sprite:ari_happy/1
"Hahaha ! Toujours." #name:Greg #sprite:greg_laugh/3
"Et Elise l'aide sur le côté." #name:Arianne #clear:all
"Comment elle se porte ?" #name:Jeanne #trans:trans_center

"Elise elle est..." #bg:square
"Qu'est-ce que tu fais ici Arianne ?" #name:Elise #music:Fear/Colère #sprite:eli_neutral/0/flip #sprite:ari_neutral/2/flip #sprite:zoe_sad/3 #sprite:mar_awkward/4
"On va partir d'ici, okay ?" #name:Zoé
"Je te parle pas, la barbe à papa." #name:Elise #sprite:zoe_awkward/3
"Qu'est-ce que tu me veux?" #name:Arianne #sprite:ari_angry/2/flip
"Réponds à ma question." #name:Elise
"Je ne suis pas sur que ça te regarde." #name:Arianne
"Plus que tu ne le crois." #name:Elise #sprite:eli_angry/0/flip
"Vous tous d'ailleurs !"
"Dense ?" #name:Jeanne #trans:trans_center #clear:all

"Ouais c'est ça..." #name:Arianne #bg:forest
"Vous ne pouvez pas me comprendre !" #name:Elise #sprite:eli_angry/2
"Vous me regardez comme si j'étais un monstre mais vous ne savez rien !"
"Alors arrête de nous ennuyer !" #name:Arianne
"Vous êtes bien pire !" #name:Elise
"Elle est dense." #name:Arianne #clear:all
"Mais..."
"..." #name:Elise #sprite:eli_angry2/2
"Je pense la comprendre un peu mieux maintenant." #name:Arianne #trans:trans_center #clear:all

"J'ai aussi vu Aloïs." #name:Arianne #bg:mall_cultur #music:nothing/nothing
"Aloïs..." #name:Jeanne
"Ah oui ! Le petit génie." #music:Wind/Wind1
"Il était de la même classe que ton frère si je me souviens bien."
"On a eu du mal à parler." #name:Arianne
"Qu'est-ce que tu fais ici ?" #name:Arianne #sprite:alo_neutral/1/flip #sprite:ari_neutral/2/flip
"C'est heum..." #name:Aloïs #sprite:alo_awkward2/1/flip
"Pour mon costume d'Halloween..."
"..." #name:Arianne #name:Arianne #sprite:ari_angry2/2/flip
"..." #name:Aloïs #sprite:alo_awkward/1/flip
"..." #name:Arianne
"Je euh..." #name:Aloïs #sprite:alo_awkward2/1/flip
"P-Pardon..."
"Pas besoin." #name:Arianne #sprite:ari_neutral/2/flip
"Mais évite la prochaine fois. Je sais que tu sais." #name:Arianne #sprite:ari_angry2/2/flip
"O-Oui..." #name:Aloïs #sprite:alo_awkward/1/flip
"Alors Arianne tu trouves ton..." #name:Zoé #sprite:zoe_neutral/3 #clear:1
"Bonheur ?"
"C'était qui ce type qui part en courant?"
"Aloïs." #name:Arianne #sprite:ari_neutral/2
"Pourquoi est-ce qu'il est là ?" #name:Zoé
"Costume d'Halloween." #name:Arianne #sprite:ari_angry2/2
"Oh je vois..." #name:Zoé
->BeforeEnd

===BeforeEnd===
"Quand au reste..." #name:Arianne #trans:trans_center #clear:all

Tu n'aurais jamais dû revenir à Adonis. #name:nothing #bg:bedroom #music:Fear/Menace
Restes terrée dans ta maison. #trans:trans_center

"Passe la balle Zoé !" #name:Martin #bg:rugby #sprite:mar_happy/2
"Aaah trop loin !" #name:Zoé #sprite:zoe_happy/2
"Je l'ai !" #name:Arianne #trans:trans_center #sprite:ari_happy/2

"Il est ruiné..." #name:Zoé #bg:kitchen #sprite:zoe_angry2/2
"Et c'est de ta faute !" #trans:trans_center #sprite:zoe_angry/2

"Qu'est-ce que tu fais ici ?" #name:Elise #bg:mall_sport #sprite:eli_neutral/3 #sprite:ari_neutral/1
"Tu t'intéresse au base-ball maintenant ?" #sprite:eli_smirk/3
"On se découvre des passions en cinq ans." #name:Arianne
"J'ui pas de la dernière pluie Arianne." #name:Elise #trans:trans_center

"..." #name:??? #bg:classroom #sprite:mon_neutral/2
"....." #sprite:mon_knife/2 #trans:trans_center

"Je préfère garder ça pour une prochaine fois." #name:Arianne #bg:menu #music:nothing/nothing #clear:all
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