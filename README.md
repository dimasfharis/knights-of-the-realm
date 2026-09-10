# Knights of the Realm

## Game Overview
This game genre is 2D Turn-Based Deckbuilder. It's about you fight enemies along the stages until you win the game.
Each stage, you will be battled by a few of enemy unit and you can attack, defend or spells magic, depend on what cards you pick.
Each turn, you will be given the 7 cards and 3 energy to pick the cards you think worth, each card can do something and cost energy you have.
You win the battle if you defeat all the enemies in the battle.

## How to Run
Step-by-step instructions to run your build.
- Open the google drive and download the rar file in the link below
- Extract the rar
- Open folder \Knights of the Realm\
- Run KotR.exe

- Engine & version used: Unity 6000.0.75f1
- Build location: Google Drive link [https://drive.google.com/drive/folders/1n2SROB3QeKNcEpznuNHivRUHCHqFaN6Z?usp=sharing]

## Technical Decisions
1. State machine pattern to manage turn system between player and enemy. This system is keeping the turn logic isolated and sequenced
2. For the relation between UI and logic, I mostly use Observer pattern to keep the code separated and adopt MVC(Model-View-Controller) system.
3. For the mostly of the class system, I usually make 3 hierarchy, ex; GameManager, BattleManager, PlayerInstance (player & enemy). This way, I can allocate the task scope efficiently
4. Since the main mechanic is in the card action, I plan to use Command pattern to keep every card picked will be stored to the CommandHistory.
5. For the separation of the game data and battle data, I make a instance/clone of the game data when the player is in a battle, so changes that made (characters, cards) in the battle doesn't affect the main stats/data.

## What I Would Do With More Time
### The effect of cards action
- When it's player turn in the FSM system, player can pick card in hand pile
- Every cards will have data of method they send as a command instance
- And it will choose target if the card is "selectTarget" type card
- It will generate Command instance from the BattleManager and stack it at a list
- BattleManager use Update() to check if there is command in that list, if yes it will executed
- FSM system at the currentState will wait for the command list to be empty, they can't continue the logic sequence if the list is not empty
- BattleManager has PlayerInstance of player and enemy. The command will address at the relevant target
### End Turn button
- When end turn button clicked, it trigger BattleUIController to invoke a method to ChangeState to enemy's turn
### Enemy decision making
- Enemy in a battle has a same structure of script as player, unless the UI
- It has PlayerInstance that keep energy, character data. and BattleCardModel that keep draw, hand & discard pile of cards
- For basic functionality, it will randomly pick a card when enemy's turn
- But for the advanced plan, I think of Personality-based decision, for ex. enemy with aggressive personality, will prefer cards with attack type rather than defend or casting spells
### FSM system
- The outline of the system is already being scripted, but it has to be more optimized
- With the FSM system run, the battle scene can be a finished game loop in the stage
### Customize Menu (after a battle finished)
- BattleManager sends command history to the CommandManager, and data to the GameManager
- The StageManager read the BattleManager to get the currentStage and generate the pre-defined enemy according to the current stage
- And player can press Next Stage button to trigger the GameManager to move to the BattleScene and execute the process again, but with different enemy
### Unprioritized appealing features
- Reward system, after each stage completed
- Customize the deck master of cards
- More characters and cards ScriptableObjects

## Known Issues
- In customize scene, the button of add card to the master deck and remove card from it, is sometimes work
- When user click the move/add/remove button, it will appears strange behavior of cards is gone
- In battle scene, if user pick a card, strange behavior of cards like it's gone
- and unfinished features is mentioned in section above
