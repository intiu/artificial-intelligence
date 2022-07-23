alarm_beeps.
hot.
fire :- hot, smoky.
smoky :- alarm_beeps.
switch_on_sprinklers :- fire.
go(X):-member(sprinklers_on,X).
go(X):-member(fire,X), write([switching,sprinklers,on]),
go([sprinklers_on | X]).
go(X):-member(hot,X), member(smoky,X), go([fire | X]).
go(X):-member(alarm_beeps,X), go([smoky | X]).
