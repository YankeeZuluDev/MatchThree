# MatchThree
A repository for "Match Three" game

[:camera: **See Screenshots**](#screenshots)

[:video_game: **Play**](https://play.google.com/store/apps/details?id=com.YankeeZulu.MatchTiles)

[:100: **Best code practices**](#best-code-practices-in-this-project)

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
This project uses [GridMatchCalculator class](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/MatchThreeGrid/GridMatchCalculator.cs) to calculate matches in the tile grid. [The algorithm for calculating the matches in the grid](https://github.com/YankeeZuluDev/MatchThree/blob/0cfa622b094a41b9b341866270ac7b3d3b0b4475/Assets/Scripts/MatchThreeGrid/GridMatchCalculator.cs#L114C42-L114C42) is using sliding-window technique. Here is short breakdown of the algorithm that was used:

  Calculate matching tiles algorithm:
  1. Iterate through every row to calculate horizontal matches
  1. Declare left and right pointers that will be iterating through the current row
  2. Shift window right if left tile and right tiles are matching
  3. Add matching tiels in window to HashSet if number of matching tiles more or equal to minTilesToMatch value that is set in inspector
  4. Reset window before going to the next row
  5. Iterate untill all rows are checked for matches
  6. Repeat again but now iterate through every column to find vertical matches
  7. Return HashSet* of unique matches
     
**Time complexity: O(n^2)** worst and average case

*HashSet is created to be filled with matches. Particularly, HashSet data structure is used to guarantee, that matches are unique to avoid counting twice both horizontally and vertically matching tiles.

### Split responsibilities
Class responsibilities in this project are well defined and separated. Each class is responsible for only one thing. Match-3 board consists of the following parts:
* [MatchThreeGrid](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/MatchThreeGrid/MatchThreeGrid.cs)
* [GridMatchCalculator](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/MatchThreeGrid/GridMatchCalculator.cs)
* [GridTileSwapper](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/MatchThreeGrid/GridTileSwapper.cs)
* [GridItemsManager](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/MatchThreeGrid/GridItemsManager.cs)

Each part has it's own corresponding prefab, that is instantiated and has it's dependencies resolved by Zenject`s installer, allowing for great aggregation relations between these parts.

### Game event system
This game uses an event system to handle in-game events such as LevelCompleteEvent or StartNewLevelEvent. The event system is implemented uisng ScriptableObjects, making it simple, convenient and extendible. The event system consists of 2 classes: GameEvent and GameEventListener. [GameEvent class](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/Events/GameEvent.cs) provides a way to create custom game events that can be triggered by different components. It allows for flexible event handling and communication between different parts of the game. [GameEventListener class](https://github.com/YankeeZuluDev/MatchThree/blob/main/Assets/Scripts/Events/GameEventListener.cs) is responsible for listening to a specific GameEvent and triggering a UnityEvent response when that event is raised. GameEventListener can be attached to any gameobject. Event system is implemented using [observer pattern](https://en.wikipedia.org/wiki/Observer_pattern).

### Prefab-based project architecture
This project uses prefab-based design approach, utilizing the power of prefabs to create and organize all game parts. Prefab-based approach allows for reusability, efficiency, easier testing, better collaboration and significant time and effort savings when implementing new features to some game parts. Additionally it allows for more convenient game initialization and dependency resolving.
