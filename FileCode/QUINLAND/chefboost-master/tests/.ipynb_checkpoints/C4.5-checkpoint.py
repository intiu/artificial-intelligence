from chefboost import Chefboost as chef
import pandas as pd

df = pd.read_csv("dataset/golf.txt")
df.head()
config = {'algorithm': 'C4.5'}
model = chef.fit(df, config)
