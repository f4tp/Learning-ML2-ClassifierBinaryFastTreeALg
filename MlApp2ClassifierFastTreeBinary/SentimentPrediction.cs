using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Runtime.Api;

namespace MlApp2ClassifierFastTreeBinary
{
    #region
    //SentimentPrediction is the class used for prediction after the model has been trained.
    //It has a single boolean (Sentiment) and a PredictedLabel ColumnName attribute. 
    //The Label is used to create and train the model, and it's also used with a second dataset to evaluate the model. The PredictedLabel is used during prediction and evaluation. For evaluation, an input with training data, the predicted values, and the model are used.
    #endregion
    class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Sentiment;
    }
}
