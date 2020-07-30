#Example: scikit-learn and Swagger
import json
import numpy as np
import os
from sklearn.externals import joblib
from sklearn.linear_model import Ridge

from inference_schema.schema_decorators import input_schema, output_schema
from inference_schema.parameter_types.numpy_parameter_type import NumpyParameterType

import ast
import pandas as pd

def init():
    global model
    # AZUREML_MODEL_DIR is an environment variable created during deployment. Join this path with the filename of the model file.
    # It holds the path to the directory that contains the deployed model (./azureml-models/$MODEL_NAME/$VERSION).
    # If there are multiple models, this value is the path to the directory containing all deployed models (./azureml-models).
    # Alternatively: model_path = Model.get_model_path('sklearn_mnist')
    model_path = os.path.join(os.getenv('AZUREML_MODEL_DIR'), 'lgbModel.pkl')
    # Deserialize the model file back into a sklearn model
    model = joblib.load(model_path)

#@input_schema('data', NumpyParameterType(input_sample))
#@output_schema(NumpyParameterType(output_sample))
def run(data):
    try:
    	json_dictionary = ast.literal_eval(data)
        df = pd.DataFrame.from_dict(d)
        df = prepareDataset(df)
        result = model.predict(df, num_iteration=model.best_iteration)
        # You can return any data type, as long as it is JSON serializable.
        return result.tolist()
    except Exception as e:
        error = str(e)
        return error

def prepareDataset(df):
    
    # Clean up the 'Name' column.
    df.loc[df['Name'].isna(), 'Name'] = ""
    
    unnamedForms = set(['Lost Dog', 'No Name', '9 Puppies For Adoption!', 'No Name Yet', 'Not Named', '(No Name)', '[No Name]', '$ To Be Named $', 'Noname', 'Unamed Yet 2',\
               'Unamed', 'Unnamed', 'No Names Yet', 'Not Named Yet', 'Unnamed 3 Kittens ( By Dani)', 'No Name Kitten', 'Nameless', '(no Name)', 'Name Them & Love Them', \
               'Not Name Yet', 'No Names Yet', '*No Name*', '"no Name"', '(No Names Yet)', '* To Be Named *', 'Unnamed.', 'NO NAME', 'Not Yet Name', 'No Name Kitties', \
               'Waiting For You To Give Him A Name', 'No Names Yet', '*please Name Us*', 'Newborn *no Name', '- To Be Named -', 'No Name Yet, It\'s Up To The Owner', \
               'Name Them & Love Them 3', 'NO NAME YET', '(No Name - She Is Just A Stray)', 'Cream Cat (unnamed)', '(no Name)', 'Wait For The Real Owner To Name It', \
               '4 Kittens Open For Adoption (no Name)', 'Need You Giving  A Name', 'No Name 2', 'UNNAMED', 'Unamed Yet', 'No Name Yet....', 'Kitten....no Name', \
               'Name Less Kitten', 'Haven\'t Named Them', 'No Name Yet (Must Neuter)', 'Haven\'t Name Yet', 'Haven\'t Been Named', 'Not Yet Named'])

    df.loc[trainData['Name'].isin(unnamedForms), 'Name'] = ""
    
    codeNames = set()
    for name in df['Name']:
        strName = str(name).lower()
        if len(strName) < 3 or ('a' not in strName and 'e' not in strName and 'i' not in strName and 'o' not in strName and 'u' not in strName and 'y' not in strName):
            codeNames.add(strName)
    
    df.loc[df['Name'].isin(codeNames), 'Name'] = ""
    
    
    # Create new features.
    categorical_features = ['Type', 'Breed1', 'Breed2', 'Gender', 'Color1', 'Color2', 'Color3', 'Health', 'MaturitySize', 'FurLength', 'Vaccinated', 'Dewormed', \
                           'Sterilized']
    
    bool_features = ['HasPhoto', 'IsSinglePet', 'IsFree', 'IsPureBreed', 'HasVideo']

    numerical_features = ['Age', 'PhotoAmt', 'Age_Years', 'Fee', 'SentimentScore', 'SentimentMagnitude', 'Name_Length', 'SentimentMultiplier', \
                          'DescriptionLength', 'Quantity', 'VideoAmt']
    
    df['Name_Length'] = df['Name'].map(str).apply(len)
    df['Age_Years'] = df['Age'] // 12
    df['IsPureBreed'] = (df['Breed1'] == 0) | (df['Breed2'] == 0) | (df['Breed1'] == df['Breed2'])
    df['HasPhoto'] = df['PhotoAmt'] > 0
    df['SentimentMultiplier'] = df['SentimentScore'] * df['SentimentMagnitude']
    df['DescriptionLength'] = df['Description'].map(str).apply(len)
    df['IsSinglePet'] = df['Quantity'] == 1
    df['IsFree'] = df['Fee'] == 0
    df['HasVideo'] = df['VideoAmt'] > 0
    
    # Transform features into categorical.
    for c in categorical_features:
        df[c] = df[c].astype('int')
        
    # Transform features into float.
    for c in numerical_features:
        df[c] = df[c].astype('float')
        
    # Transform features into bool.
    for c in bool_features:
        df[c] = df[c].astype('bool')
        
    #Filter out columns.
    allFeatures = ['Type', 'Age', 'Breed1', 'Breed2', 'Gender', 'Color1', 'Color2', \
       'Color3', 'MaturitySize', 'FurLength', 'Vaccinated', 'Dewormed', \
       'Sterilized', 'Health', 'Fee', 'SentimentScore', 'SentimentMagnitude', \
       'Name_Length', 'Age_Years', 'IsPureBreed', 'HasPhoto', 'SentimentMultiplier', \
       'PhotoAmt', 'DescriptionLength', 'Quantity', 'IsSinglePet', 'IsFree', 'VideoAmt', 'HasVideo']
    
    #Filter out columns
    return df[allFeatures]