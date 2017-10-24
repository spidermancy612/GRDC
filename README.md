# Strikedown Protocol

## GAME Project

GRDC Game Project for the 2017 year

A turn-based space combat game played from a top-down perspective.

- Turns are resolved simutaneously
- Networked multiplayer between 2 to 4 players
- Actions available to a player will be determined randomly through a card draw system

***

- [Gameplay](#gameplay)
- [Card Draw](#card-draw)
- [Movement](#movement)
- [Attack](#attack)
- [Gameplay (cont)](#gameplay-cont)

## Gameplay

In each round, each player will draw seven cards. Each round has three phases, with three cards being played in the first phase, three cards being played in the second phase, and the final card being saved for the last phase. Card chances are reset after each round.

Each player plans their turn and then lets the next player plan their turn. When all players have finished, their turns will all play out at the same time. It is important to note that the turns are simply pauses in a perpetual environment. This will be explained further after card types are discussed.

## Card Draw

The actions available to each player are determined by a card draw system. The card types are as follows: Move; Weak Move; Strong Move; Attack; Shield. The planned odds for these cards are as follows: 12/42; 6/42; 8/42; 10/42; 6/42.

## Movement

The movement cards provide the player with Thrust, allowing the player to move their ship. Thrust can be assigned to the six thrusters on the ship. The thrusters allow movement forward and backward, as well as turning and strafing. A Move gives the player four Thrust, a Weak Move give two less, a Strong Move gives two more.

## Attack

The Attack card allows a player to fire their cannon. The projectile travels a certain distance every card slot, and does so until it had left the playing area or hits something. The Shield card prevents a player from taking damage if it is hit during that card slot. Each player ship can take two hits before being destroyed, though this may change based on the practical difficulty of hitting an enemy in testing.

## Gameplay (cont)

Having discussed the types of cards, we return to the idea of the perpetual game environment. Here are the basic principles behind the perpetual game environment:

- Any Thrust that is applied to a ship will carry on into future turns and rounds. This means that if a player uses Thrust to move forward, they will continue going forward until they use more Thrust to counteract that original Thrust.
- Any shots that are fired will continue to move in the direction they were fired until they collide with an object or leave the playing area. This is important for the use of Shields: if one player Attacks and the other Shields in the same slot, but the shot hits during the next slot due to travel time, the Shield will have no effect.
- Each card slot will take the same amount of time to play out. No type of card takes any more time to take effect than any other, so all players will always play seven cards.
