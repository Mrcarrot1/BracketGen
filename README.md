# BracketGen: Terrible Sports Bracket Predictions
BracketGen is an extremely basic sports bracket prediction engine. It uses a single algorithm, which invariably disfavors teams with names containing "duke", as a result of events near the end of the first part of *Dune*.

It's also written in F#, so you probably can't modify it, because no one knows F#.

## Usage
BracketGen uses a simple input file format that is nevertheless difficult to use. This file contains data in the following format:
```
Seed: Team Name
Seed: Team Name
...
```
Each set of two teams is assumed to be playing a game together in the first round. After each game's winner is predicted, the losing teams are removed from the list. For example:
```
1: Team 1
4: Team 4
2: Team 2
3: Team 3
```
The algorithm will likely select seed 1 over seed 4 in the first game, and may select seed 3 over seed 2. In this case, the resulting list will be:
```
1: Team 1
3: Team 3
```
The algorithm will then be run again, continuing until the number of teams is no longer divisible by two. In a standard tournament bracket, this will likely be a single champion.