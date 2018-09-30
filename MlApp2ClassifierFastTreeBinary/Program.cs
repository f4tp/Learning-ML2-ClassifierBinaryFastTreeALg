using System;
using System.IO;

namespace MlApp2ClassifierFastTreeBinary
{
    class Program
    {
        #region
            // needed imports, introduce as and when
            //using System;
            //using System.Collections.Generic;
            //using System.IO; - Path.Combine
            //using System.Linq;
            //using System.Threading.Tasks;
            //using Microsoft.ML;
            //using Microsoft.ML.Data;
            //using Microsoft.ML.Models;
            //using Microsoft.ML.Runtime.Api;
            //using Microsoft.ML.Trainers;
            //using Microsoft.ML.Transforms;
            #endregion
        #region
        //_dataPath has the path to the dataset used to train the model.
        //_testDataPath has the path to the dataset used to evaluate the model.
        //_modelPath has the path where the trained model is saved.
        #endregion
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "wikipedia-detox-250-line-data.tsv");
        static readonly string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "wikipedia-detox-250-line-test.tsv");
        static readonly string _modelpath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");


        static void Main(string[] args)
        {
            
        }
    }
}
