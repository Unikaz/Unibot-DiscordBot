# Unibot - DiscordBot
##### A bot for the Uniserv's Discord server
_(and a way for me to learn C# ^^)_

NB: If you want to try this bot, please rename AppExemple.config into App.config, and enter your token inside

### Commands
- `!hello` say hello to the bot
- `!help` display commands
- `!role <add|remove> <roleName>` add or remove the named role
- `!whois <Pseudo>` to learn more about someone
- `!challenge [challengeNumber|challengeType]` ask for a challenge
- `!rep #<id> <answer>` answer to a challenge

### Challenge implemented (Server side)
- `math` an easy operation with only two operandes
- `sudoku` a (temporary harcoded) sudoku to resolve
- `pathfinder` generate a random grid and validate the users results

### Challenge implemented (Client Side)
- `math`
- `sudoku` a small implementation, can only solve really easy sudoku (not on git to avoid cheat)
- `pathfinder` solve a simple pathinder based on Dijkstra's algorithm (not on git to avoid cheat)

### Users
- add User entity
- first implementation of User in a MySQL database



### ToDo
- `!vote \"<description>\" <option1, option2, option3, ...>` ask for a vote
- auto register challenges players in database
- count players/bots scores
- add a timeout on challenges
- register challenges and bots answers in database 


## ChangeLog
##### 171124.0:
- Add challenge sudoku
- Add challenge pathfinder
- Client side of sudoku and pathfinder
- Rework challenge manager

##### 171119.0:
- Add command challenge
- Add command rep
- Add user entity
- Add database
- Add users' table
- Add challenge math: an easy addition
- Add the client side of challenge math

##### 171118.0:
- Start the Unibot-DiscordBot project
- Config handling
- Add command hello
- Add command help
- Add command role
- Add command whois

