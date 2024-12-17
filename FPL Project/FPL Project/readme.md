Gameweek 34: Fix goals scored and conceded in last five

Gameweek 35 Ideas: Change goals scored etc to per game stats



How to start a new season:

Generate a new set of PlayerDetails.csv using players you want.
You can get a list of players from the DataGrabber.ipynb and filter that list
to the players you want to keep data on. You need to add an id column 
filled with random, unique numbers and make sure the data is in the right format.

Next, "reloadalldata" to load the correct ids from the Fantasy API and load gameweek data

From there, you can "generatetrainingdata" or "generatetestingdata", and proceed 
to train your model