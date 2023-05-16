INCLUDE globals.ink

{ CHOICES_TEST has A1: -> GrigriYES}
{ CHOICES_TEST has A2: -> GrigriNO}

Hello there ! #next:nothing/nothing #bg:forest #name:nothing #clear:all #place:2 #sprite:jes_murder #music:goldnoct
Or more like howdy there !
-> main 

=== main ===
 
Test1 #name:Arianne #clear:all #place:2 #sprite:jes_murder #music:nothing
Test2 #next:Grigri_Test/true #audio:celeste_low
Test3
Test4
Test5 #next:nothing/false
How are you feeling today ? #name:Arianne #clear:all #place:2 #sprite:jes_murder #music:goldnoct #audio:nothing
+[Happy]
    That make me feel <color=\#F8FF30>happy</color> as well! #name:Zoé #clear:2 #place:3 #sprite:jes_murder
    I'm so <color=\#F8FF30>happy</color> !
+[Sad]
    oh well, that makes me <color=\#5B81FF>sad</color> to! #name:Aloïs #clear:2 #place:3 #sprite:jes_murder
    Very <color=\#5B81FF>sad</color> actually...

- Don't trust him, he's <b><color=\#FF1E35>not</color></b> a real doctor! #name:Sakutarou #place:4 #sprite:sak_happy
-> GrigriNO

    
     === GrigriYES ===
    The Grigri was very siccessful ! #next:nothing/nothing
    Thank you !
    Do you have any more questions ?
+[Yes]
    ->main
+[No]
    Goodbye then! #name:Zoé #clear:all #bg:bedroom
    -> END
    
    === GrigriNO ===
    The Grigri was unsuccessful... #next:nothing/nothing
    Goodbye then! #name:Zoé #clear:all #bg:bedroom
    -> END