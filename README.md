# BirdJump
Game made for my practice and learning. Goal: Similar to flapppy birds.

## Problems faced and their solutions
### Making sure that the game is infinite
Making an infinite scroller for the first time. So I looked at how others do it and then learnt that they "instantiate" the same prefabs again and again. We can add a randomized component to keep things fresh. This was used in two places. One was in the moving background (with different layers to give the illusion of depth) and the other was the randomised obstacle generation.

```
float yPos = randomize ? randomY : transform.position.y;
Instantiate(gameObject, new Vector3(newPosX, yPos, transform.position.z), Quaternion.identity);
instances.Add(this);
hasSpawned = true;
```

### When pausing, making sure everthing actually paused
I had added a pause button but was facing this issue that the game was playing in the background like nothing had happened. This was solved in a weird way (idk if I was right). Basically anything that was moving, I would freeze their movment. For the player this was done by freezing their Y axis. For the background, I had to make a list of all the items in the background. Then I would make their movement speed equal to 0. Same was done for any obstacles on the screen. (For reference the player can only go up and down, the background and the obstacles only right to left)

```
public void pauseMenu()
{
    playerRB.constraints = RigidbodyConstraints2D.FreezePositionY;
    movnigObjects = FindObjectsByType<BackgroundMoveScript>(sortMode: FindObjectsSortMode.None);
    savedSpeeds.Clear();
    foreach (BackgroundMoveScript instance in movnigObjects)
    {
        savedSpeeds[instance] = instance.speed;
        instance.speed = 0f;
    }
    pausemenu.gameObject.SetActive(true);

}
```

### The player-bird was a little too sensitive
This was an easy solution. I just played around with the gravity affecting it and set it to a value that felt like the right balance.

### Difficulty scaling according to score
So as in the original game, I decided that the difficulty should be easy at the beginning of the level and once the player reaches higher and higher levels, it should increase. This was a lazy solution where I just checked whether the player had corssed a certain score threshold and if so, I would just increase the randomness. (For reference the difficulty change only affects the randomness of the height of the obstacles)

```
void levelDecider()
{
    if (gameLogic.score <= 20)
    {
        level = level1;
    }
    else if (gameLogic.score <= 50)
    {
        level = level2;
    }
    else
    {
        level = level3;
    }
}
```
## Stuff I enjoyed
### Making assets
Making the assets using a simple pixel art program like Krita or just using a website for this was fun because it was something I had never tried. Learning how to use pixel art in Unity and making sure it shows up properly was a new learling experience.

### Having my friends play-test it
The jank version of this game was fun to test around because it would be actually challenging to overcome the jankness. Watching them struggle to even go past the score of 10 was really ammusing.
