Hello there ! #clear:all #place:2 #sprite:jes_murder
-> main 

=== main ===

How are you feeling today ? #clear:all #place:2 #sprite:jes_murder
+[Happy]
    That make me feel <color=\#F8FF30>happy</color> as well!
+[Sad]
    oh well, that makes me <color=\#5B81FF>sad</color> to! #clear:2 #place:3 #sprite:jes_murder

- Don't trust him, he's <b><color=\#FF1E35>not</color></b> a real doctor! #place:4 #sprite:sak_happy

Well, do you have any more questions ? #clear:3 #place:2 #sprite:jes_murder
+[Yes]
    ->main
+[No]
    Goodbye then! #clear:all
    -> END