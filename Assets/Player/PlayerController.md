# Player Controller Concepts
This controller needs to be able to switch on or off many different abilities. To help break it down, see the concepts and abilities below.
SE - Self Explanatory

## Concepts
These are frame by frame ideas that need to happen to make a fun, snappy character controller.
- Moving Left and Right
    - Fairly simple, move at constant velocity in either direction with arrow keys.
- Jumping
    - variables maxJumpCount and currentJumpCount SE
    - Holding the jump button should result in a longer jump.
        - jumpTimer is a timer that is reset every time a new jump is started or the player is grounded.
        - jumpStrength is the duration a decreasing velocity will be added while the jump key is held down
        - 