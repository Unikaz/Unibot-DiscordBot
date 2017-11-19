# Unibot - DiscordBot
##### A bot for the Uniserv's Discord server
_(and a way for me to learn C# ^^)_

NB: If you want to try this bot, please rename AppExemple.config into App.config, and enter your token inside

### Commands
- `!hello` say hello to the bot
- `!help` display commands
- `!role <add|remove> <roleName>` add or remove the named role
- `!whois <Pseudo>` to learn more about someone
- `!challenge` ask for a challenge
- `!rep #<id> <answer>` answer to a challenge

### Challenge implemented (Server side)
- `math` an easy operation with only two operandes

### Challenge inplemented (Client Side)
- `math`

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

