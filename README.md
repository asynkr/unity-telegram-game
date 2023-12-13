# Unity Telegram Game
 
 This package enables you to create a WebGL Telegram Game using Unity game engine.
 
 > For the Node.js bot server, see [unity-telegram-game-backend](https://github.com/asynkr/unity-telegram-game-backend)

## Overview

 Telegram gives the possibility to connect HTML5 games to a bot. See [Telegram documentation](https://core.telegram.org/bots/games) for more information.
 This feature provides a way to create a game that can be played directly embedded in Telegram mobile apps, and a built-in score feature, that allows users to see a leaderboard specific to each group chat.

 Moreover, games created with this feature can easily be shared on group chats, providing a unique way to share your games.

This package provides simple scripts to create a WebGL Game in Unity that can be connected to a bot.

This package is only half of the solution. You still need to create a bot that serves the game and receives score from the client. You will find the other half in the [unity-telegram-game-backend](https://github.com/asynkr/unity-telegram-game-backend) repo.

## Installation

### Unity

* Download this package from the package manager. Select "from git url" and enter:
``` 
https://github.com/asynkr/unity-telegram-game.git
```
* Add a GameObject with the `ConnexionManager` script attached.
* Setup the `ConnexionManager` variables in the inspector:
  * Enter the right url (the endpoint that you will catch in the bot server)
  * Setup the obfuscation. If you want to use the default obfuscation, add 9 big prime numbers in the array. If you want to use your own obfuscation, create a script that implements `IObfuscation`.
* Call the `sendScore` method wherever and whenever you want (it does not need to be at the end of the game, nor when the user quit your app).
* Make sure you're building for WebGL.

WebGL can be hard to build sometimes, new problems can arise from compression/optimization. To make sure the build is supposed to work, independently of the node app, you can use "Build and Run" in Unity, or upload it on a separated host (like heroku or itch.io). In particular, most common problems are with compression. See [Unity documentation on deploying with WebGL](https://docs.unity3d.com/Manual/webgl-deploying.html) and [the necessary server configs provided by Unity](https://docs.unity3d.com/Manual/webgl-server-configuration-code-samples.html). In the server config, you'll also have to properly declare the folder where your `index.html` will be. Finally, some compilation/compression options won't work. By trial and error, for example, I found that I needed to **enable the `Decompression fallback`option** in the Player Settings.

### Node.js server

See [unity-telegram-game-backend](https://github.com/asynkr/unity-telegram-game-backend) for complete instructions.

## Complementary Info

### Sequence Diagram

The process and the different back-and-forth interactions can be hard to follow. Here is a sequence diagram that shows the complete process.

![Sequence Diagram](http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/asynkr/unity-telegram-game/main/sequence.puml)

Note that "Telegram Backend" refers to both the app and the bot API.

### Obfuscation

When sending the score from the HTML game to the backend, the simplest way to do it would be to use a request that looks like `https://www.my-website.com/score?id=123456&score=543`.
Problem is a geeky user can easily find the score in the request, and therefore send a request with the same player id and a score only limited by the 32/64 bit size of the score in the backend.
Note that **it is NOT a big deal**. We're talking about a highscore in a private group chat, not your banking card number.

However, this package provides a way to obfuscate the score.
This is a very poor security measure, as it doesn't follow [Kerckhoffs's principle](https://en.wikipedia.org/wiki/Kerckhoffs%27s_principle). But this is good enough to stop the geeks. Note that it does not protect you from the hardcore nerds.

You can use the given obfuscation script, or you can create your own.
Using the default obfuscation script means the user can know your algorithm by finding this repo (if you're currently trying to do such a thing: hello there).

Again, since this is not about any banking card number, it isn't a big deal. Your main goal is to make your game fun for everyone, not to make sure every point is deserved.
And if you're giving cashprizes to the players, please reconsider your choice to do your competition on Telegram (and to use this package).
