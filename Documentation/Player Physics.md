# MEGA MAN MOVEMENT

This is all assuming you're using 60 fps for the game.

## Standing

While standing still, Mega Man retains his normal position for 90 frames, then blinks for 9 frames.

When he begins moving, then he initiates his stepping sprite for 5 frames.

## Movement

There are 3 walk cycles, in the order of 1 - 2 - 3 - 2. Mega Man transitions cycles each 7 frames.

Mega Man moves at 1.3 pixels per frame, except when stepping. When initializing stepping, he advances 1 pixel in whatever direction he's facing. If this is not possible to achieve, then he advances at 1/6.5 of his speed.

When the player releases left or right, then Mega Man decelerates, by initiating the stepping sprite again for 5 frames, but not moving at all, then revert to standing and blinking.

### Sliding:

While Mega Man slides, he does so for 25 frames, and travels 65 pixels in that time, at 2.5 pixels per second. If Mega Man is under a 1-tile wide gap, then he keeps sliding until there is clear space Mega Man can safely revert to standing.

Mega Man can slide across 1 tile wide holes in the ground, but not more. If there's a gap in the ground that would prevent him from being able to slide, and he has enough slide time left to be able to make it to the hole, then he clears the entire block with his hitbox, then reverts to his standing position.

### Climbing:

Mega Man can climb a ladder by pressing up or down. When he gets near the top of a ladder, he shifts his sprite to the one where he's ready to get back on ground. 

Mega Man can't fall down a ladder unless he presses down, but he can get off a ladder by reaching either end of it, or by jumping off of it. His climbing speed is the same as his walking speed.

### Jumping:

Mega Man can jump (very surprising). His initial velocity is 4.75 pixels per frame upwards, while it decreases by 0.25 each frame. If he keeps falling, then he cannot fall any faster than -7 pixels per frame.

Mega Man can let go of the jump button, and this will set his velocity to 0, while still decreasing.

Underwater, Mega Man's velocity decrease only appears every 3 frames, but because this is easier said than done, the underwater decrease is 0.08333 in Y+1, in other words, 0.25/3

When jumping from a ladder, his initial velocity is 0.

## Shooting

When Mega Man shoots, but does not charge, he stays in his shooting position for 16 frames after shooting.

When he does charge on the other hand, he progresses through 4 stages. The first 3 are similar, it affects Mega Man's outline by changing it to 3 different color depending on the stage. It takes 48 frames to get him fully charged up

First 16 frames: His outline changes to a dark magenta color but flashes back to black, in the following pattern (black is 0, magenta is 1):

0 0 1 1 (repeated 4 times)

Next 16 frames: His outline changes to a lighter magenta color and flashes back to black, with the same pattern

The final 16 frames has the magenta be much lighter. Same pattern.

### Fully charged palette:

Black Outline is 0, Blue is 1, Cyan is 2

| Index | Outline | Blue | Cyan |
| ----- | ------- | ---- | ---- |
| No.0  | 0       | 1    | 2    |
| No.1  | 2       | 0    | 1    |
| No.2  | 1       | 2    | 0    |

The palette index changes every 2 frames (0 - 1 - 2 pattern loop), and it keeps shifting until you release the charge. After that he retains his 16 frame cooldown.

