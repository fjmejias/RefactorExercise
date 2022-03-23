# RefactorExercise

## Question 1

I have included a *ConsoleApp* project with the code received as input. Then I have created a Library project, with a *EncodeService* class that includes the business logic.
And finally, I created a net5 Test Project that contains some unit tests to validate the logic included in the EncodeService.

## Question 2

To improve the *HighCard* game I have created several projects.
* **HighCard.Contracts:** This is a Library .netCore project that includes models and interfaces for a cards game. 
In this project I have included the models *Card* and *Player* that will be used in the implementation for the HighCard game. Also I have added two Enums to get a more understandable values for *GameResult* and *Suits* per deck.
Finally, I added 3 interfaces, the first one for a generic *CardGame* that contains properties for players and *GameResult* and methods to create players and play the cards game, 
the second interface is a *CardSelector* that will be used for initialize the cards collection used in the game and to draw cards for the players. And finally I added an interface
for some settings that will be used in the *CardSelector* implementation.
* **HighCard:** This is a Library .netCore project that includes the implementation of the interfaces included in the HighCard.Contracts project. This implementation is specific for the HighCard game.
This HighCard game contains a dependency to a service *CardSelector* that will be used to create the cards collection (depending on the number of cards per suit, the number of decks used in the game and also if the deck contains a Joker card, that are included in the HighCardSettings) and to get cards for the players.
* **HighCard.UnitTest:** This is a Test .netCore project that contains unit tests for the HighCardGame and CardSelector classes. We have covered all the requirements described in the exercise for the HighCard game.
* **Question2.ConsoleApp:** This is a ConsoleApp project for the HighCard game that print all the information received when running the game.
