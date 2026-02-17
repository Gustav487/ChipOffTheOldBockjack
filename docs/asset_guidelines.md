# Introduction
The game is designed with asset substitution in mind. Player's may purchase for in game currently alternative looks and cosmetics. To allow for substitution each alternative style must follow a few formatting restrictions.

# Directory Structure
To allow the game to find the appropriate asset it's important to follow the directory structure below precisely. Any missing assets will be substituted by the asset in the default asset set.

```
[unique_style_identifier]
└── textures
    ├── cards
    |   ├── ace_of_clubs
    |   ├── two_of_clubs
    |   ├── three_of_clubs
    |   ├── four_of_clubs
    |   ├── five_of_clubs
    |   ├── six_of_clubs
    |   ├── seven_of_clubs
    |   ├── eight_of_clubs
    |   ├── nine_of_clubs
    |   ├── ten_of_clubs
    |   ├── jack_of_clubs
    |   ├── king_of_clubs
    |   ├── queen_of_clubs
    |   ├── ace_of_diamonds
    |   ├── two_of_diamonds
    |   ├── three_of_diamonds
    |   ├── four_of_diamonds
    |   ├── five_of_diamonds
    |   ├── six_of_diamonds
    |   ├── seven_of_diamonds
    |   ├── eight_of_diamonds
    |   ├── nine_of_diamonds
    |   ├── ten_of_diamonds
    |   ├── jack_of_diamonds
    |   ├── king_of_diamonds
    |   ├── queen_of_diamonds
    |   ├── ace_of_hearts
    |   ├── two_of_hearts
    |   ├── three_of_hearts
    |   ├── four_of_hearts
    |   ├── five_of_hearts
    |   ├── six_of_hearts
    |   ├── seven_of_hearts
    |   ├── eight_of_hearts
    |   ├── nine_of_hearts
    |   ├── ten_of_hearts
    |   ├── jack_of_hearts
    |   ├── king_of_hearts
    |   ├── queen_of_hearts
    |   ├── ace_of_spades
    |   ├── two_of_spades
    |   ├── three_of_spades
    |   ├── four_of_spades
    |   ├── five_of_spades
    |   ├── six_of_spades
    |   ├── seven_of_spades
    |   ├── eight_of_spades
    |   ├── nine_of_spades
    |   ├── ten_of_spades
    |   ├── jack_of_spades
    |   ├── king_of_spades
    |   ├── queen_of_spades
    |   ├── card_front
    |   └── card_back
    ├── guis
    |   ├── game_bg
    |   ├── game_overlay
    |   ├── hit_btn
    |   ├── hit_btn_disabled
    |   ├── hit_btn_hovered
    |   ├── menu_bg
    |   ├── pause_btn
    |   ├── pause_btn_hovered
    |   ├── stand_btn
    |   ├── stand_btn_disabled
    |   ├── stand_btn_hovered
    |   ├── title_bg
    |   └── title_logo
    └── match_result
        ├── loss
        ├── tie
        └── you_win
```

# Types
## Textures
All the assets in the 'textures' directory must be one of the following texture types.
### Static
Textures may be a single static image. In this case the asset should be provided as a png named *[asset path].png*, where *[asset path]* is the asset's path
### Animated
Textures may be a animation created from a looped sequence of frames. In this case the asset should be provided as a folder at the asset path. Inside the folder should be a sequence of pngs named *0.png* through *[n - 1].png*, where 'n' is the number of frames. Inside the folder may optionally also be an *anim.json* file that defines the frame positionings and timings, if this file is not provided the loop will consist of each frame in order lasting .1 seconds each - **it is highly preferred to place an animation file even if the default would work, in the future they may not still be optional**.

The *anim.json* file should contain an object with the following key value pairs.
- "frame_duration": a positive number indicating the amount of time each frame lasts on screen.
- "frame_sequence": a list of frame indices, frames will be displayed in the order they're listed.

# Aspect Ratios/Scales
In order to correctly fit, each asset must have a fixed aspect ratio(width:height) otherwise it'll be warped/stretched. Further, it is generally advised that all aspect ratios for a given style be scaled be the same factor, so on screen pixel sizing is consistent.

## General
- Windows: 16:9 (1152x648)
- Title Logo: 50:9 (100x18)
- Cards: 9:14

## Gameplay screen
- Play area: 10:9 (720x648)
- Info margin left: 3:9 (216x648)
- Info margin right: 3:9 (216x648)
- Card 9:14 (135x210)
- Hit/Stand buttons 3:1.5 108