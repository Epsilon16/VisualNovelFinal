INCLUDE globals.ink

{ CHOICES_TEST has A1: -> GrigriYES}
{ CHOICES_TEST has A2: -> GrigriNO}

~grigriLives = 8
Hello there ! #next:nothing/nothing #bg:grigri #name:nothing #clear:all #sprite:jes_murder/2 #music:goldnoct
Or more like howdy there ! #trans:trans_center
-> main 

=== main ===
 
Test1 #name:Arianne #bg:forest #clear:all #sprite:jes_murder/2 #music:goldslaughter
~grigriLives = 4
Test2 #next:Grigri_Test/true #audio:celeste_low
Test3 #item:sakutarou
Test4 #item:nothing
Test5 #next:nothing/false
~grigriLives = 2
How are you feeling today ? #name:Arianne #clear:all #sprite:jes_murder/2 #music:goldnoct #audio:nothing
+[Happy]
    That make me feel <color=\#F8FF30>happy</color> as well! #name:Zoé #clear:2 #sprite:jes_murder/3
    I'm so <color=\#F8FF30>happy</color> !
+[Sad]
    oh well, that makes me <color=\#5B81FF>sad</color> to! #name:Aloïs #clear:2 #sprite:jes_murder/3
    Very <color=\#5B81FF>sad</color> actually...

- Don't trust him, he's <b><color=\#FF1E35>not</color></b> a real doctor! #name:Sakutarou #sprite:sak_happy/4
-> GrigriNO

    
     === GrigriYES ===
    The Grigri was very successful ! #next:nothing/nothing #bg:forest
    Thank you !
    Do you have any more questions ?
+[Yes]
    ->main
+[No]
    Goodbye then! #name:Zoé #clear:all #bg:bedroom
    -> END
    
    === GrigriNO ===
    ~grigriLives = 0
    The Grigri was unsuccessful... #next:nothing/nothing #bg:forest
    Goodbye then! #name:Zoé #clear:all #bg:bedroom
    -> END