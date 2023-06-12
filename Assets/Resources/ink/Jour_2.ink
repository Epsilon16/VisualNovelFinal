INCLUDE globals.ink

{CHOICES_TEST has GEli0: -> AloisNoGrigri}
{CHOICES_TEST has GEli1: -> AloisYesGrigri}


{CHOICES_TEST has GEli0: -> EliseNoGrigri}
{CHOICES_TEST has GEli1: -> EliseYesGrigri}

VAR Coffee = ""

"Mince." #name:Jeanne
Je tourne la tête vers ma mère entrain de frénétiquement ouvrir les armoires de la cuisine. #name:nothing
"Qu'est-ce qui se passe ?". #name:Arianne
"J'ai oublié de prendre du café, et visiblement Nat en est à court." #name:Jeanne
"Tu peux aller en acheter chez Greg ?" #name:Jeanne
"Tu penses qu'il est toujours ouvert ?" #name:Arianne
"Je le connais, ce magasin fermera le jour où il passera l'arme à gauche." #name:Jeanne
Greg est le gérant d'une superette de village ainsi qu'un ancien ami de mes parents.. #name:nothing
Lorsqu'on était petit Yann, Martin, Zoé et moi allions souvent prendre des friandise chez lui.
C'est un chic type, j'espère qu'il va toujours aussi bien.
Ma mère sort un billet de dix de son porte monnais qu'elle me tend.
"Et essaye de prendre du extra-fort s'il te plait." #name:Jeanne
"Okay." #name:Arianne
J'enfile mon manteau avant de sortir.. #name:nothing
"A toute à l'heure." #name:Jeanne
"A toute." #name:Arianne

En descendant la rue jusqu'au magasin, je repense aux évènements d'hier. #name:nothing
J'ai peut-être été trop dure avec eux.
Je revois mes amis après tout ce temps et j'ai tout ruiné.
Mais leurs réactions...
C'est juste...
Juste...
Aaaaaahhh !
Je devrais penser à autour chose.
Pas comme si j'allais les revoirs.

Le magasin est toujours au même endroit, mais il semble fermé. #name:nothing
Je m'approche et vois un petit carton derrière l'une des fenêtres.
Ouvre à 9h
Je regarde mon téléphone.
8h50
J'aurais pas à attendre énormément, c'est déjà ça.
Je recule un peu, et rentre accidentellement dans quelqu'un.
"Ah !" #name:Aloïs
"Ah !" #name:nothing
Le jeune homme tombe en arrière.
"Vous allez bien ?" #name:Arianne
Je lui tend ma main pour l'aider à se relever, quelque chose semble familier chez lui. #name:nothing
"Oui... Désolé je vous ais fait peur..." #name:Aloïs
"C'est ma faute j'aurais dû faire plus attention." #name:Arianne
Mais il n'a vraiment fait aucun bruit, je ne l'ai pas vu venir du tout. #name:nothing
Mais...
Ces cheveux... Et cette voix...
"Aloïs..?" #name:Aloïs
"A-Arianne !?" #name:Arianne
Il me regarde avec des yeux écarquillé. J'ai probablement l'air très différente de la dernière fois qu'on s'est vu. #name:nothing
"Whoa. J'aurais jamais cru te revoir un jour." #name:Arianne
"M-Moi non plus..." #name:Aloïs
Aloïs était dans la même classe que Yann, donc je le voyais de temps en temps. #name:nothing
Comme Adonis n'est pas massif, tout les enfants se connaisse plus ou moins.
Il était assez timide et réservé mais c'était pas un type méchant.
De souvenir il avait sauté une classe, donc c'était probablement pour ça qu'il passait moins de temps avec les autres élèves.
Et c'était aussi quelqu'un de très intelligent, j'imagine qu'il doit faire des études sur des sujets qui me dépasse.
"Comment tu vas depuis le temps ?" #name:Arianne
"Bien je suppose ?" #name:Aloïs
"Enfin tu sais... Il se passe plein de choses..." #name:Aloïs
Je sens alors une sensation dans ma poche. #name:nothing
Je sors ce qu'il y a dedans et...
Le bracelet ?
Je l'avais oublié, mais l'une des perles semble briller légèrement.
Je pourrais l'utiliser comme hier ?
"Arianne ?" #name:Aloïs
Aloïs me regarde bizarrement. En même temps je regarde un bracelet depuis tout à l'heure. #name:nothing
"Désolé. Je t'écoutes." #name:Arianne
"..." #name:Aloïs
"Tu disais ?" #name:Arianne
"C'est euh... Rien d'important." #name:Aloïs
"Si tu le dis." #name:Arianne
->discu_no_grigri

===AloisYesGrigri===
Qu'est-ce qu'il vient de dire ? #name:nothing
"Si tu voulais que je part dis le tout de suite." #name:Arianne
Aloïs recule de quelque pas. #name:nothing
"Qu'est-ce qui..." #name:Aloïs
"J'ai compris, je te mets mal." #name:Arianne
"Hein non je.." #name:Aloïs
"Désolé d'être moi Aloïs." #name:Arianne
Il me regarde avec un air terrifié et part presque en courant. #name:nothing
Je ne sais pas si c'était une bonne idée d'utiliser le bracelet.
J'ai l'impression que ça brouille mon cerveau, comme si je fusionnais avec celui de l'autre personne.
C'est peut-être ce qui se passe.
Mais il avait une telle peur. J'en ais eu peur moi même.
Je devrais peut-être pas rentrer dans la tête des gens tout court.
La porte du magasin s'ouvre alors et un hommme à la large posture en sort.
"On est en service !" #name:Greg
-> greg_intro

===AloisNoGrigri===
Ah! #name:nothing
J'ai l'impression d'avoir été explusé de sa tête...
Il faut que je fasse attention la prochaine fois.
Il n'a pas l'air d'avoir vu quoi que ce soit...
-> discu_no_grigri 

=== discu_no_grigri ===
"Sinon, qu'est-ce que tu fais là ?" #name:Arianne
"Je dois récupérer un colis pour mes parents." #name:Aloïs
"Je vois. Ils vont bien ?" #name:Arianne
"Oui." #name:Aloïs
Je n'ai jamais vraiment cotoyé les parents d'Aloïs. #name:nothing
Je les voyais à certaines occasions, mais je crois pas leurs avoir parler en dehors de 'Bonjours' et 'Au-revoirs'.
Ils étaient gentils mais ils m'ont toujours fait un peu peur je crois.
Probablement car ce sont des professeurs.
Enfin, ils vont bien, c'est le principal.
"Cool." #name:Arianne
La discussion arrive à un point mort. Mais heureusement la porte du magasin s'ouvre et un hommme à la large posture en sort. #name:nothing
"On est en service !" #name:Greg
Aloïs passe rapidement devant moi. #name:nothing
"Bonjour. Euh... Est-ce que le colis pour mes parents est arrivé ?" #name:Aloïs
"Ah Aloïs ! Il est derrière le contoire tu peux le prendre." #name:Greg
"Merci!" #name:Aloïs
Il rentre dans le magasin en pas de course et ressort tout aussi rapidement. #name:nothing
Notre discussion à dû le mettre mal à l'aise j'ai l'impression.
-> greg_intro

=== greg_intro ===
L'homme me dévisage un peu. #name:nothing
"Hmm... Ton visage me dit quelque chose." #name:Greg
"Hey Greg, je pensais pas que tu m'oublierais si facilement." #name:Arianne
"Arianne !? Bon dieu ce que tu as grandis !" #name:Greg
"La dernière fois que je t'ai vu tu faisais cette taille !" #name:Greg
Il fait un geste exagéré qui ne dépasse pas les un mètre cinquante. #name:nothing
"Je sais que j'ai grandis mais tu exagère un peu." #name:Arianne
"Hahaha ! Toujours." #name:Greg
Lui en tout cas, n'a pas changé d'un poil. Peut-être les cheveux qui grisonne, mais c'est toujours le même homme souriant dont je me souviens. #name:nothing
"Qu'est-ce qui t'amène ici ?" #name:Greg
"Ah oui, je dois acheter..." #name:Arianne
+[Café Mega-Fort]
    ~Coffee = "CMF"
    "Du Café Mega-Fort." #name:Arianne
+[Thé Extra-Fort]
    ~Coffee = "TEF"
    "Du Thé Extra-Fort." #name:Arianne
+[Café Extra-Fort]
    ~Coffee = "CEF"
    "Du Café Extra-Fort." #name:Arianne
-"Bien sur ! Il y en a dans l'îlo à droite." #name:Greg
"Merci." #name:Arianne
Je vais dans le dit îlot, prend un paquet du produit puis va payer à la caisse. #name:nothing
"Du coup qu'est-ce que tu deviens ?" #name:Greg
"Moi pas grand chose." #name:Arianne
"Naaan, je te crois pas. L'Arianne que je connais n'a pas une vie barbante." #name:Greg
Je souris face à la remarque. #name:nothing
"Je fais des études en ingénieurie." #name:Arianne
"Vraiment ? Quoique ça m'étonnes pas tant que ça." #name:Greg
"En tout cas je pourrais pas faire ça. Trop de machination cérébrale." #name:Greg
"C'est sur que tenir un magasin doit être bien plus relaxant pour tes neurones." #name:Arianne
"Hahaha !" #name:Greg
"J'en ri mais tu n'as pas tout à fait tord."
"Ca devient de plus en plus difficile de rester au dessus de l'eau."
"Les grandes surface en ville sont plus pratiques, plus rapide et ont plus de choix."
"Même avec Elise pour m'aider ici je peux pas vraiment combattre le progrès."
"C'est... Dommage." #name:Arianne
"Oooh, me fait pas cette tête là. Les choses change, c'est normal." #name:Greg
"De mon époque on avait des systèmes de locations de casette de films !"
"Ca n'existe plus car on n'en a plus besoin, c'est comme ça."
"Tu sais Greg. Je suis assez grande pour avoir connu les locations de films." #name:Arianne
"..." #name:Greg
"HAHAHAHA !!"
"J'oublis tellement de choses desfois."
"Mais j'oublis pas le plus important !"
"Le café ?" #name:Arianne
"Non, toi bien sur !" #name:Greg
"D'ailleurs, t'as pu voir Zoé et Martin ?" #name:Greg
"Eh bien... En parlant de ça..." #name:Arianne
"Un problème ?" #name:Greg
"..." #name:Arianne
"Si tu veux m'en parler, tu sais que tu peux compter sur Greg !"
Malgré le fait qu'il soit presque du même âge que ma mère, pour nous, Greg était plus proche d'un tonton sympa qu'une véritable figure d'autorité. #name:nothing
"Je les ais vu hier, mais ça s'est mal passé." #name:Arianne
"C'est probablement de ma faute également."
"Est-ce que tu voulais que ça se passe mal ?" #name:Greg
"Non évidemment." #name:Arianne
"Donc tout va bien !" #name:Greg
"Si tu leur explique ton point de vu ils comprendront."
"Tu les connais depuis la maternelle, y'a pas de raison."
Pas sur de vraiment les connaitres maintenant. #name:nothing
"Tu n'as pas envie de finir sur une mauvaise note, si ?" #name:Greg
"Tu as raison, merci Greg." #name:Arianne
"Haha, de rien." #name:Greg
Je lui fait un signe de la main avant de repartir. #name:nothing

Il a probablement raison, je devrais les revoirs et parler. #name:nothing
C'est probablement la dernière fois qu'on se verra. Et je n'ai pas envie qu'on se quitte sur de mauvais termes.

Une fois arrivé à la maison, je pose les courses sur la table. #name:nothing
"J'ai ce que tu voulais !" #name:Arianne
Maman rentre dans la cuisine et regarde les courses. #name:nothing
{Coffee == "CMF": -> MegaCoffee}
{Coffee == "CEF": -> ExtraCoffee}
{Coffee == "TEF": -> ExtraTea}

===ExtraCoffee===
"Merci Arianne, c'est parfait." #name:Jeanne
"Viens m'aider dans le garage, on dirait qu'une tornarde est passée par là..." #name:Jeanne
"Okay j'arrive." #name:Arianne
-> afternoon

===ExtraTea===
"Arianne..." #name:Jeanne
"J'avais demandé du Café Extra. Pas du Thé Extra." #name:Jeanne
"Ah merde." #name:Arianne
"J'me suis emmèler les pinceaux." #name:Arianne
"C'est pas grave, j'irais en acheter cet aprem." #name:Jeanne
"Par contre tu vas devoir m'aider toute la matinée au garage." #name:Jeanne
"On dirait qu'une tornade est passée là dedans." #name:Jeanne
"Okay maman." #name:Arianne
-> afternoon

===MegaCoffee===
"Arianne..."
"J'avais demandé du Café Extra. Pas du Café Méga." #name:Jeanne
"C'est ton père qui le préfère comme ça" #name:Jeanne
"Ah merde, j'ai pas fait attention." #name:Arianne
"Désolé maman." #name:Arianne
"C'est pas grave, ça en fera plus pour chez nous." #name:Jeanne
"Par contre tu vas devoir m'aider toute la matinée au garage." #name:Jeanne
"On dirait qu'une tornade est passée là dedans." #name:Jeanne
"Okay maman." #name:Arianne
-> afternoon


=== afternoon ===
Après avoir passé la matinée à ranger les affaires de Mamie et mangé un coup, je me dirige en direction de chez Zoé. #name:nothing
Martin avait l'air bien moins enclin à la discussion hier. Et je penses que Zoé voudra reparler.
Du moins j'espère...

Je continue à descendre la rue jusqu'à arriver devant sa maison. #name:nothing
Le doute me prend alors.
Devrais-je vraiment sonner ?
C'est probablement un peu trop tôt vu ce qu'il s'est passé.
Mais si je ne fait rien maintenant ça s'empirera.
Je n'ai juste pas envie de la déranger comme ça.
Que faire ?
"Tout va bien jeune fille ?" #name:Mère de Zoé
Une voix féminine me bloque dans mon monologue intérieur. #name:nothing
"Vous avez besoin de direction ?" #name:Mère de Zoé
Je lève la tête et remarque que quelqu'un se tient devant la porte à côté de moi. #name:nothing
C'est Maria, la mère de Zoé.
"Oh non non, je suis là où je veux." #name:Arianne
"Attendez... Arianne c'est toi ? Qu'est-ce que tu as grandis !" #name:Mère de Zoé
J'ai une sensation de déjà-vu.
"Bonjour Maria. Désolé de venir à l'improviste." #name:Arianne
"Oh mais nulle besoin de t'excuser, tu es et sera toujours la bienvenue chez nous." #name:Mère de Zoé
"Qu'est-ce qui t'amène ?"
"Je me demandais si Zoé était ici." #name:Arianne
"Oh tu viens juste de la rater. Elle est partie voir Martin il n'y a pas si longtemps.""Qu'est-ce qui t'amène ?"
"Ah ça rapelle le bon vieux temps de quand vous jouiez tout les quatres."
"..." #name:Arianne
"Merci, je vais y aller."
"Passez une bonne journée.""Qu'est-ce qui t'amène ?"
"Vous aussi." #name:Arianne

Je me dirige alors vers chez Martin. #name:nothing
Malgré les années, je retrouves facilement le chemin, et fini par arriver chez lui.

Arrivant devant sa grande maison, j'entends Zoé parlant à la porte. #name:nothing
"Aller !" #name:Zoé
"Je te dis que je me sens pas bien.." #name:Martin
"Arrête avec tes excuses ! C'est aujourd'hui ou jamais !" #name:Zoé
"Si on fait rien on le regrettera !"
"Je n'ai pas envie de..." #name:Martin
Martin lève la tête, ses mots tombant alors qu'il me remarque. #name:nothing
"De ?" #name:Zoé
Elle tourne la tête dans la direction de Martin, affichant du mécontentement. #name:nothing
Puis elle me remarque, et son visage change du tout au tout, reprenant son sourir habituel.
"Arianne !!" #name:Zoé
Elle descend les marches qui mène à la porte d'entrée pour venir me voir. #name:nothing
"Contente de te revoir !" #name:Zoé
"Salut Zoé. De même." #name:Arianne
Je donne un faible sourire et penche la tête en direction de Martin qui reste sans un mot sur le pas de sa maison. #name:nothing
Je respire un coup, tournant les mots de Greg dans ma tête une fois de plus.
"Pour hier..." #name:Arianne
"Maintenant qu'on est tous là, allons au square !" #name:Zoé
"Attends Zoé je.." #name:Arianne
"Nan nan, on y va maintenant." #name:Zoé
Elle appuie le 'maintenant' en tournant sa tête vers Martin. #name:nothing
Celui ci, après avoir fait une expression difficile, ferme la porte derrière lui et s'approche de nous.
"Allons-y !" #name:Zoé
Zoé commence alors à marcher sans faire attention à Martin ou moi. #name:nothing

Le chemin en direction du square est pour le moins malaisant. #name:nothing
Zoé est en tête et Martin n'ose pas me regarder.
J'ai envie de dire quelque chose mais je n'ose pas faire grand chose.
Je vois pas comment Zoé peut garder son sourire. Je pourrais couper la tension avec un couteau.
"Oh on est bientôt arrivé !" #name:Zoé
Elle se tourne et prend mon poignet, me tirant vers l'entrée du square. #name:nothing

Le lieu n'a pas changé. #name:nothing
Les mêmes chemins de terre et graviers parcourant le parc, les arbres que nous escaladions et l'air de jeu dans laquelle on a passé tant de temps.
Milles et une histoires ont été raconté, joué et oublié ici.
La nostalgie me pince légèrement le coeur, mais au vu de sa réaction hier, ce doit être pire pour Martin.
"Hey regardez !" #name:Zoé
Zoé est monté sur le toit d'un des jeux en bois. #name:nothing
"Je suis la plus grande !" #name:Zoé
"Zoé.. fait attention s'il te plait..." #name:Martin
C'est la première chose que je l'entends dire depuis tout à l'heure, il a l'air vraiment inquiet. #name:nothing
"Ne t'inquiète pas ! Je suis Miss Mouse ! Voleuse extra-ordinaire !" #name:Zoé
"Je suis aussi agile que le vent et aussi discrète que la nuit."
Zoé glisse le long du toit avant d'atterir sur une petit plateforme. #name:nothing
Miss Mouse.
Je n'ai pas entendu ce nom prononcé depuis longtemps.
C'est la méchante d'une BD que nous lisions tous ensemble, et Zoé l'adore.
Martin et Zoé finissait souvent dans des arguments, lui étant un grand fan du personnage principal, Detective Wolf.
Mais c'était aussi la source de nombre de nos jeux.
"Oh mais qui va donc me poursuivre ? Je suis sur le point de voler un précieux diamant !" #name:Zoé
"Zoé descend..." #name:Martin
"Non ! Je suis Miss Mouse." #name:Zoé
"Miss M..." #name:Martin
"Zoé c'est pas drôle.."
"Alerte police. Nous avons besoin de renfort. Le diamant du musée vient d'être volé." #name:Arianne
Zoé se tourne vers moi, affichant un grand sourire. #name:nothing
"Jamais vous ne m'attraperez." #name:Zoé
"J'avais un plan de secours depuis le début."
Elle se retourne et descend le long du tobbogan. #name:nothing
"La cible est perdue de vu ! Nous avons besoin d'un détective pour retrouver sa trace." #name:Arianne
Je tourne mon regard vers Martin qui n'a toujours pas bougé. #name:nothing
"Seul mon arch-nemesis saurait me tenir tête." #name:Zoé
"Mais il semblerait qu'il ne soit pas là aujourd'hui."
"..." #name:Martin
"Appeler tout le monde, que quelqu'un trouve Mr.Wolf." #name:Arianne
"..." #name:Martin
"Si je m'échappe, c'est la fin ! On ne me reverra jamais !" #name:Zoé
"..." #name:Martin
"Arrête toi là..."
"Hm ?" #name:Zoé
"Arrête toi là, Miss." #name:Martin
"Je crois avoir un signal !" #name:Arianne
"Arrête toi là Miss Mouse !" #name:Martin
"Car je suis le grand Détective Wolf et rien ne saura me stopper dans ma poursuite de la justice !"
Martin se place devant Zoé, étant à présent entièrement dans la fantasy. #name:nothing
"Rend toi maintenant !" #name:Martin
"Damned ! Je suis faite avoir comme un rat !" #name:Zoé
"C'est la fin de tes activités, tu as ma parole." #name:Martin
"Jamais !" #name:Zoé
Zoé commence à courir dans le square. #name:nothing
"Arrêtez-vous !" #name:Arianne
"Reviens là Miss Mouse !" #name:Martin
Martin et moi commençons alors à la courser. #name:nothing
J'ai l'impression d'être revenu dans le temps.
On pouvait passer des journées tout les quatres à prétendre être ce qu'on voulait et imaginer des histoires.
Après une dizaine de minutes, nous nous ponsons sur un banc.
"Haha, j'ai pas autant couru depuis si longtemps."
"Vraiment ? Tu m'étonnes."
"Je sens plus mes jambes c'est horrible."
"Ca m'avait manqué nos jeux de Detective Wolf."
On reste comme ça un petit moment, reprenant notre souffle.
Puis je me lève, faisant face au banc.
"Hey, à propos d'hier..."
"Hm ?"
Martin baisse un peu le regard, n'ayant visiblement pas envie d'y repenser.
"C'est juste... On a besoin de parler de Yann."
Les deux reste silencieux.
"On peut pas ne rien faire. Il était une partie centrale de nos vie !"
"Le fait qu'il soit plus là.."
"C'est..."
Je perds un peu mes mots, je sais ce que je veux dire, mais je trouve pas les bonnes paroles.
"Arianne..."
"Hey !"
Je lève les yeux et remarque qu'une jeune femme est entrain de nous interpeller.
Elle s'avance vers notre direction d'un pas décidé.
"Vous arrivez pas à lire ? Réservé aux moins de dix ans."
"Non c'est juste que..."
Martin et Zoé se lève
"Attends.."
"J'te reconnais."
Elle ecarquille les yeux, et je me souviens également de la personne en face de moi.
C'est la fille de Greg, Elise.
"Qu'est-ce que tu fais ici Arianne ?"
"On va partir d'ici, okay ?"
"Je te parle pas la barbe à papa."
"Qu'est-ce que tu me veux."
"Répond à ma question."
"Je ne suis pas sur que ça te regarde."
"Plus que tu ne le crois."
"Vous tous d'ailleurs !"
"Vous agissez comme des gosses ignorant."
"Mais je sais que vous ne l'êtes pas."
Elise a toujours été comme ça, brute de décofrage, disant tout ce qu'elle pense et hâpte aux crises de colère.
On ne s'est jamais vraiment lié d'amitié enfants, mais quelque chose à profondément changé chez elle depuis que je l'ai vu pour la dernière fois.
"Vous êtes qu'une bande de lâche-"
"Elise stop.."
"Quoi tu veux te battre ?"
Martin s'approche d'elle, et elle fait de même.
Elise pousse alors Martin qui fini a terre.
"Bah alors ? C'est tout ce qu'il te reste capitaine ?"
"Elise arrête !"
"Pourquoi tu nous ennuie comme ça !?"
Elle allait pour nous répondre puis s'arrête dans son élan et frappe l'échelle en bois à côté d'elle.
"Allez vous faire."
Elle se retourne furieuse et sort du parc.
"Martin ça va ?"
Celui-ci se relève, le regard lourd.
"J'aurais pas dû venir."
Et il s'en va également, allant en direction de la forêt.
"Zoé ?"
Celle-ci reste immobile déviant un peu le regard.
Qu'est-ce que je devrais faire...
+[Suivre Elise]
    ~CHOICES_TEST = J2C1
    ->ChoiceElise
+[Suivre Martin]
    Je décide de suivre Martin, je m'approche de l'entrée de la forêt.
    "Martin-"
    Il remarque que je suis là et commence à partir en courant.
    J'accélère pour le rattraper, mais rien à faire. Martin est bien plus athlétique que moi.
    Au bout d'un moment, je fini par le perdre et je me retrouve seule dans les bois.
    ~CHOICES_TEST = J2C2
    ->ChoiceMartin
+[Rester avec Zoé]
    Je décide de rester avec Zoé.
    Elle est resté sur le côté sans rien dire tout le long de la conversation.
    Sans un mot, elle escalade doucement l'une des structures en bois avant de s'assoir sur le bord.
    J'en fais de même et me mets à côté d'elle.
    Le temps passe sans que l'une d'entre nous ne dise grand chose.
    Puis au bout d'un moment, j'entame la conversation.
    ~CHOICES_TEST = J2C3
    ->ChoiceZoe

===ChoiceElise===
Je décide de suivre Elise, toute cette situation est de sa faute.
Elle a des excuses à donner à Martin et Zoé.
"Elise !"
Malgré mes appels, elle continue de me tourner le dos.
"ELISE !"
"QUOI ?"
Elle se stop et se retourne d'un mouvement sec, une expression de rage sur le visage.
"Pourquoi est-ce que tu fais ça ?"
Je sens le bracelet s'allumer.
"On t'a rien fait ! On était entrain de parler et t'es juste..."
"Venu nous faire chier sans raison !"
"Oh ferme la."
"Quoi ?"
"Juste.. ferme la ! Tu sais rien de moi !"
"Et tu sais rien de moi non plus ! Pourquoi est-ce que tu es autant en colère tout le temps ??"
->EliseNoGrigri

===EliseYesGrigri===
...
Les poils de mes bras se sont redressé, j'ai l'impression d'avoir fait face à un prédateur enragé.
J'ais vraiment cru que j'allais mourir à tout moment là dedans.
"Qu'est-ce qui se passe ? Perdue ta langue ?"
Je suis figée sur place, Elise me fixe avec des yeux de tueur.
"Alors !?"
Je n'ose rien dire et part en courant, revenant sur mes pas.
Qu'est-ce que c'était que ça ?
C'était terrifiant !
Je repense à la lettre d'hier soir et de sa menace.
Ce serait elle ? Mais pourquoi ?
Une fois devant l'entrée de square, je me demande qui aller voir en premier.
+[Chercher Martin]
    Je vais en direction de la forêt pour trouver Martin.
    Il doit bien être quelque part.
    ~CHOICES_TEST = J2C2
    ->ChoiceMartin
+[Chercher Zoé]
    Je rentre à l'intérieur du square en direction des jeux.
    "Zoé ?"
    Aucune réponse, mais je la trouve assise sur l'une des structures en bois, balançant ses jambes d'avant en arrière.
    Je monte et m'assois à côté d'elle.
    ~CHOICES_TEST = J2C3
    ->ChoiceZoe

===EliseNoGrigri===
~RS_Martin -= 1
~RS_Zoe -= 1
"Mon dieu juste dégage de ma vu !"
"Arrête de me suivre comme si t'étais un chien !"
"J'ui une attraction pour toi ??"
"Juste dégage !"
"Elise.."
"DEGAGE !!"
Elle me fixe, essoufflé, j'ai l'impression qu'elle est presque sur le point de pleurer de colère.
On reste une vingtaine de seconde à se regarder sans un mot, puis elle se retourne et part.
Je n'ose pas continuer à la suivre.
Je reste planter là plusieurs dizaine de seconde avant de me retourner en direction du square.

Une fois arrivé là bas, et malgré mes efforts, aucune trace de Zoé ni de Martin.

Je tente également la forêt, avec tout autant de résultat.

Je fini par rentrer chez moi, la tête lourde.
Je n'aurais probablement pas dû suivre Elise...
Sur le moment ça semblait être une bonne idée, mais j'aurais dû rester vers Martin de Zoé au lieu de ne rien faire.

Je passe la soirée de ma chambre à ruminer.
J'ai gâché nos retrouvailles, encore et encore.
Ils ont tous raisons.
Je n'aurais pas dû revenir à Adonis.
->END


===ChoiceMartin===
~RS_Martin += 1
"Martin ?"
J'appel, sans recevoir de réponse.
Au bout d'un moment je fini par le trouver derrière un rocher, assis dans l'herbe.
"Hey Martin."
"Ah !"
"Trouvé."
Il ne dit rien, me laissant une place à côté de lui, que je viens prendre.
On reste un moment assit sans rien dire.
Je n'ai pas vraiment envie de reparler d'hier.
Mais je ne sais pas vraiment de quoi parler non plus.
Ce que je sais, c'est que je ne veux pas le laisser seul non plus.
"Dit..."
"Pourquoi est-ce que t'es revenu ?"
"..."
Je ne pensais pas que ça pourrait faire si mal de la part de Martin.
"Tu n'avais pas l'air de vouloir nous revoir hier. Alors pourquoi est-ce que tu venu chez moi ?"
"Oh ça.."
"Eh bien.. je n'avais pas envie de finir sur une mauvaise note."
"Je sais que ça fait longtemps."
"Mais je vous aimes toujours."
"Moi aussi..."
"Désolé pour ce que j'ai dit tout à l'heure."
"Je suis content d'être venu, c'était cool."
"Ton impression de Detective Wolf n'a pas changé."
"Arrête, c'était embarrassant.."
"Mais c'était cool."
"Haha ouais."
"..."
"Merci Arianne."
"Et je suis désolé pour hier."
"..."
"Je voulais pas..."
"C'est juste..."
"C'est dur tu sais."
Martin ne dit rien, mais il n'a pas l'air de mal le prendre non plus.
"On va voir Zoé ?"
"Okay."
Je me relève et lui tend ma main pour l'aider à se mettre debout.

Une fois au parc, nous ne trouvons aucune trace de Zoé.
"On devrait aller voir chez elle."
"Tu devrais aller voir chez elle."
"Tu ne viens pas ?"
"Je penses pas qu'elle ait envie de me voir."
"Je suis qur qu'elle ne t'en veux pas. Mais je vais pas te forcer."
"Rentre bien."
"Merci."

Sur le chemin retour, je me demande si je n'aurais pas dû rester avec Zoé.
Elle qui était si contente qu'on soit de nouveau ensemble.
Elle doit penser que je n'en ais pas grand chose à faire.
J'espère pouvoir en reparler un jour.
->END

===ChoiceZoe===
~RS_Zoe += 1
On reste un moment assise toute les deux sans rien dire.
Puis j'entamme la conversation.
"A quoi est-ce que tu penses ?"
"Elise."
Dur de penser à autre chose en même temps.
"Qu'est-ce qu'on lui a fait pour qu'elle nous haïsse comme ça."
"Pourquoi est-ce qu'elle est aussi.. explosive envers nous ?"
"Mais elle a l'air de m'en vouloir à moi. Et elle était pas comme ça avant."
"Tu crois que c'est parce que tu es partie ?"
"Nos familles se connaissait c'est vrai. Mais pas proches au point que ça l'affecte comme ça."
"Je penses pas en tout cas."
"Et je vois pas pourquoi moi spécifiquement."
Le message d'hier me revient en tête... Ce serait elle ?
"On ne saura probablement jamais..."
Zoé est inhabituellement très neutre.
Elle qui est toujours positive et enjouée, la voir aussi sérieuse est assez étrange.
J'ai vraiment raté beaucoup de chose, j'aurais dû faire de mon mieux pour garder contacte...
"Je suis désolé d'avoir gâché nos retrouvailles. Je sais que tu avais hâte qu'on se revois."
"Dis pas ça, c'est de la faute d'Elise."
"Elle s'est invité toute seule. C'est à elle de s'excuser."
"Non non, j'ai mal réagis hier. Et on n'en serait pas là sinon."
"..."
"C'est pas..."
"On a tous des mauvais souvenir, et j'oublie que je ne suis pas seule pafois."
Zoé souris et se sèche une petite larme.
"Je ne sais pas comment tu fais pour toujours dire ce que tu penses."
"C'est pas toujours une bonne chose. Ton optimisme l'est bien plus."
"J'insiste, c'est vraiment une bonne chose."
Je ne suis pas sur de pourquoi elle penses ça, mais je l'accepte et lui fais un câlin.
Après une dizaine de seconde on se sépare et on descend de la structure de jeu.
"On devrait aller voir si Martin va bien."
"Enfin, tu devrais."
"Pourquoi tu dis ça ?"
"Je penses pas qu'il ait envie de me reparler."
"Tu es sur ?"
J'acquiesce.
"Et puis, tu as toujours eu une connaissance encyclopédique de ces bois."
"Je ne peux pas argumenter contre ça."
"Rentre bien Arianne."
"Merci Zoé."

Sur le chemin retour, je me demande si je n'aurais pas dû rester avec Martin.
Il avait probablement plus besoin de moi que Zoé tout à l'heure.
Et il doit penser qu'on l'a abandonné...
J'espère pouvoir en reparler un jour.
->END