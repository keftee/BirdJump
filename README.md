# BirdJump
Game made for my practice and learning. Goal: Similar to flapppy birds.

## Problems faced and their solutions
### Making sure that the game is infinite
Making an infinite scroller for the first time. So I looked at how others do it and then learnt that they "instantiate" the same prefabs again and again. We can add a randomized component to keep things fresh. This was used in two places. One was in the moving background (with different layers to give the illusion of depth) and the other was the randomised obstacle generation.

### When pausing, making sure everthing actually paused
I had added a pause button but was facing this issue that the game was playing in the background like nothing had happened. This was solved in a weird way (idk if I was right). Basically anything that was moving, I would freeze their movment. For the player this was done by freezing their Y axis. For the background, I had to make a list of all the items in the background. Then I would make their movement speed equal to 0. Same was done for any obstacles on the screen. (For reference the player can only go up and down, the background and the obstacles only right to left)

### The player-bird was a little too sensitive
This was an easy solution. I just played around with the gravity affecting it and set it to a value that felt like the right balance.

### Difficulty scaling according to score
So as in the original game, I decided that the difficulty should be easy at the beginning of the level and once the player reaches higher and higher levels, it should increase. This was a lazy solution where I just checked whether the player had corssed a certain score threshold and if so, I would just increase the randomness. (For reference the difficulty change only affects the randomness of the height of the obstacles)
