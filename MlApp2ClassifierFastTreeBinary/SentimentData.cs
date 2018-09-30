using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace MlApp2ClassifierFastTreeBinary
{
    #region
    //SentimentData is the input dataset class and has a float (Sentiment) that has a value for sentiment of either positive or negative, and a string for the comment (SentimentText). 
    //Both fields have Column attributes attached to them. This attribute describes the order of each field in the data file, and which is the Label field.
    #endregion
    class SentimentData
    {
        [Column(ordinal: "0", name: "Label")]
        public float Sentiment;
        [Column(ordinal: "1", name: "SentimentText")]
        public string SentimentText;
    }
}
