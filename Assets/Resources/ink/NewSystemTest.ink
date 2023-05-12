Hello there ! #bg:forest #name:Jessica #clear:all #place:2 #sprite:jes_murder
-> main 

=== main ===

How are you feeling today ? #clear:all #place:2 #sprite:jes_murder
+[Happy]
    That make me feel <color=\#F8FF30>happy</color> as well! #clear:2 #place:3 #sprite:jes_murder
+[Sad]
    oh well, that makes me <color=\#5B81FF>sad</color> to! #clear:2 #place:3 #sprite:jes_murder

- Don't trust him, he's <b><color=\#FF1E35>not</color></b> a real doctor! #name:Sakutarou #place:4 #sprite:sak_happy

Well, do you have any more questions ? #name:Jessica #clear:3 #place:2 #sprite:jes_murder
+[Yes]
    ->main
+[No]
    Goodbye then! #clear:all #bg:bedroom
    -> END