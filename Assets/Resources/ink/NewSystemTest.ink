INCLUDE globals.ink

{ CHOICES_TEST has A1: -> GrigriYES}
{ CHOICES_TEST has A2: -> GrigriNO}

~grigriLives = 8
Test #bg:grigri #name:nothing #trans:trans_intro
Hello there ! #sprite:eli_neutral/2
Or more like howdy there ! #trans:trans_center #transbg:transbg_neutral
-> main 

=== main ===

Test1 #name:Arianne #bg:forest #sprite:eli_neutral/2
Nouveau Test #sprite:jea_neutral/2
Nouveau CHANGEMENT #sprite:ari_neutral/2
Nouveau TWIST ? #sprite:zoe_neutral/2
~grigriLives = 4
Test2 #next:Grigri_Test/true #audio:celeste_low
Test3 #item:sakutarou
Test4 #item:nothing
Test5 #next:nothing/false
~grigriLives = 2
How are you feeling today ? #name:Arianne #clear:all #sprite:eli_neutral/2 #audio:nothing
+[Happy]
    That make me feel <color=\#F8FF30>happy</color> as well! #name:Zoé #clear:2 #sprite:eli_neutral/3
    I'm so <color=\#F8FF30>happy</color> !
+[Sad]
    oh well, that makes me <color=\#5B81FF>sad</color> to! #name:Aloïs #clear:2 #sprite:eli_neutral/3
    Very <color=\#5B81FF>sad</color> actually...

- Don't trust him, he's <b><color=\#FF1E35>not</color></b> a real doctor! #name:Sakutarou #sprite:zoe_neutral/4
-> GrigriNO

    
     === GrigriYES ===
    The Grigri was very successful ! #next:nothing/nothing #bg:forest
    Thank you !
    Do you have any more questions ? #trans:trans_neutral
+[Yes]
    ->main
+[No]
    Goodbye then! #name:Zoé #clear:all #bg:bedroom
    -> END
    
    === GrigriNO ===
    ~grigriLives = 0
    The Grigri was unsuccessful... #next:Jour_1/nothing #bg:forest
    Goodbye then! #name:Zoé #clear:all #bg:bedroom #transbg:transbg_1
    -> END