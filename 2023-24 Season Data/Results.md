# 2023-24 Season Data

## Training the model

The model used to generate these findings was as followed:

Linear(19, 100)
ReLu()
Linear(100, 50)
ReLu()
Linear(50, 24)
ReLu()
Linear(24,1)

This model was trained over 3000 epochs with a learning rate of 0.01, a loss function of MSELoss and an Adam optimizer.
todo: try other loss functions 

The model achieved a best MSE of 7.17 and an RMSE of 2.68.

The model would predict values no higher than ~5, indicating that it struggled to predict when a player was going to have a high scoring week. This makes sense as it is a difficult metric to predict with the given data and a player may look as though they are going to perform really well in a week but they don't. Therefore, it makes sense that the model is conservative in its predictions. 
One idea is to instead not take this number as expected points but rather a players anticipated performance in a gameweek compared to other players. I would select my team by selecting the players with the highest output value even though they wouldn't necessarily have a high expected points. 
For example, if the model predicted Mo Salah to earn 4.2 points, and that was the highest number in the prediction, you would really expect a haul of 10-15 points as 4 points will not be the highest score in a gameweek. Instead Salah may just be the most likely player to haul but is not realistic to predict 10 points as you are more likely to be incorrect instead of being conservative in your guessing. This doesn't allow you 

Metrics from training model on last seasons data:
9825 rows of training data
19 columns in each row
We count that 87.8% of all rows had the player "blank. (score < 5 points)
Therefore 12.2% of all rows had the player return.
If we consider the model predicting a blank to be a prediction of < 3 points, we correctly predicted a blank 95.3% of the time.
We only correctly predicted a return 14.2% of the time.

Another way to view this data is that we had a "bad guess" or a predicted value > 5 from actual value 165 times.
The other differences are seen in this array [(0, 861), (1, 918), (2, 561), (3, 259), (4, 123), (5, 61)]
Overall, the model is predicting well most of the time, but as FPL is an unpredictable game, it is hard to always be correct.