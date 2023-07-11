# MatchThree
A repository for "Match Three" game

[:camera: **See Screenshots**](#screenshots)

[:video_game: **Play**](https://play.google.com/store/apps/details?id=com.YankeeZulu.MatchTiles)

## About the game
Genre: casual, puzzle

Mechanics: matching

Unity version: 2021.3.18f1 (LTS)

Accessibility: Project can be freely explored in unity

## Screenshots
<div style="display:flex;">
  <img src="https://github.com/YankeeZuluDev/MatchThree/assets/129124150/50b11fa5-cabb-4e56-b7cf-821a61914569" alt="screenshot_1" width="270" height="480">
  <img src="https://github.com/YankeeZuluDev/MatchThree/assets/129124150/20dfb460-4715-49f1-ad81-9143bc0ce792" alt="screenshot_2" width="270" height="480">
  <img src="https://github.com/YankeeZuluDev/MatchThree/assets/129124150/a992fbad-6951-4cf4-b893-ad35f541d9d4" alt="screenshot_3" width="270" height="480">
</div>

## Best сode practices in this project

### Dependency injection using Zenject framework
This project uses Zenject dependency injection framework to resolve dependencies in the code. Useage of this framework is helpflul to follow Dependency Inversion and Single Responsibility Principles. Zenject allows the project to turn into a collection of loosely coupled parts with highly segmented responsibilities. In this project all the game parts are created and their dependenices resolved scene-wide using [MatchThreeInstaller class](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/Installers/MatchThreeInstaller.cs).

### Great sliding window technique inplementation
This project uses [MatchThreeCalculator class]() to calculate matches in the tile grid. [The algorithm for calculating the matches in the grid]() is using sliding-window technique. Here is short breakdown of the algorithm that was used:

  Calculate horizontally matching tiles:
  1. Iterate through every row to calculate horizontal matches
  1. Declare left and right pointers that will be iterating through the current row
  2. Shift window right if left tile and right tiles are matching
  3. Add matching tiels in window to HashSet if number of matching tiles more or equal to minTilesToMatch value that is set in inspector
  4. Reset window before going to the next row
  5. Iterate untill all rows are checked for matches
  6. Repeat again but now iterate through every column to find vertical matches
  7. Return HashSet* of unique matches
     
Time complexity: O(n^2) worst and average case

*HashSet is created to be filled with matches. Particularly, HashSet data structure is used to guarantee, that matches are unique

### Split responsibilities

### Game event system

### Prefab-based project architecture
