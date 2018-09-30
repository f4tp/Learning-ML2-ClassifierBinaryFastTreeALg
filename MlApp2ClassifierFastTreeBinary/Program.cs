using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using System.Linq;
using Microsoft.ML.Models;

namespace MlApp2ClassifierFastTreeBinary
{
    class Program
    {
        #region
        //https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/sentiment-analysis
        // needed imports, introduce as and when
        //using System;
        //using System.Collections.Generic;
        //using System.IO; - Path.Combine
        //using System.Linq; - Zip() method
        //using System.Threading.Tasks; - async task
        //using Microsoft.ML; - PredictionModel
        //using Microsoft.ML.Data; - TextLoader
        //using Microsoft.ML.Models; - BinaryClassificationEvaluator
        //using Microsoft.ML.Runtime.Api;
        //using Microsoft.ML.Trainers; - FastTreeBinaryClassifier
        //using Microsoft.ML.Transforms; - TextFeaturizer
        #endregion
        #region
        //_dataPath has the path to the dataset used to train the model.
        //_testDataPath has the path to the dataset used to evaluate the model.
        //_modelPath has the path where the trained model is saved.
        #endregion
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "wikipedia-detox-250-line-data.tsv");
        static readonly string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "wikipedia-detox-250-line-test.tsv");
        static readonly string _modelpath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");

        #region
        //main method altered
        //You add async to Main with a Task return type because you're saving the model to a zip file later, and the program needs to wait until that external task completes.
        //An async main method enables you to use await in your Main method.
        //asyn called from System.Threading.Tasks
        //the await object is awaited in the Main method, before main is executed
        //the above means the computation will execute before Main is called, and just returns the model as a zip file later
        #endregion

            //train
            //evaluate
            //predict
        static async Task Main(string[] args)
        {
            var model = await Train(); //train
            Evaluate(model); //evaluate performance
            Predict(model);//predict
            Console.ReadLine();
        }
        #region
//setup the training algorithm and start training
        //ingest the data
        //pipeline is a structure containign all data needed for machien learnign algorithm
        //async Task is a threaded task
        //clean the data / pre-process the data / featurize the data
        //Textfeaturizer - creates a numeric vector from the SentimentText colum - given by the Column attribute added in SentimentData - to preprocess /featurize the data to clean it so it doesn't give inaccurate results - see below
        //Pre-processing and cleaning data are important tasks that occur before a dataset is used effectively for machine learning. Raw data is often noisy and unreliable, and may be missing values. Using data without these modeling tasks can produce misleading results. ML.NET's transform pipelines allow you to compose a custom set of transforms that are applied to your data before training or testing. The transforms' primary purpose is for data featurization. A transform pipeline's advantage is that after transform pipeline definition, save the pipeline to apply it to test data.
        //set / refine the training algorithm
        //FastTreeBinaryClassifier - the selected classifier algorithm we are using
        //change the paramters passed to the algorithm can help refine the results
        //NumLeaves etc are called hyper parameters
        //train on the data
        //PredictionModel model then starts trainign teh model
        //await model.WriteAsync(_modelpath) / return then passes to the Main method
        #endregion

        public static async Task<PredictionModel<SentimentData, SentimentPrediction>> Train()
        {
            var pipeline = new LearningPipeline();
            pipeline.Add(new TextLoader(_dataPath).CreateFrom<SentimentData>());
            pipeline.Add(new TextFeaturizer("Features", "SentimentText"));
            pipeline.Add(new FastTreeBinaryClassifier(){ NumLeaves = 20, NumTrees = 20, MinDocumentsInLeafs = 10});
            PredictionModel<SentimentData, SentimentPrediction> model = pipeline.Train<SentimentData, SentimentPrediction>();
            await model.WriteAsync(_modelpath);
            return model;
        }
        #region
        //evaluate the model's performance
        //load the test data
        //TextLoader

        //create a binary evaluator
        //BinaryClassificationEvaluator


        //Evaluate the model and generate metrics for evaluation
        //BinaryClassificationMetrics


        //output metrics
        //cw tab

        #endregion

        public static void Evaluate(PredictionModel<SentimentData, SentimentPrediction> model)
        {
            var testData = new TextLoader(_testDataPath).CreateFrom<SentimentData>();
            var evaluator = new BinaryClassificationEvaluator();
            BinaryClassificationMetrics metrics = evaluator.Evaluate(model, testData);
            Console.WriteLine("PredictionModel quality metrics evaluation");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.Auc:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");


        }
        #region
        //predict using new data
        //creates test data
        //IENumerable loaded with 2 different pieces of text


        // predicts sentiment based on data
        //IEnumerable<SentimentPrediction> predicitions = model.Predict(sentiments);



        //Combines test data and predictions for reporting
        //Zip() method


        //displays the predicted results
        //foreachloop


        //change language to c# 7.1 or higher
        //Because inferred tuple element names are a new feature in C# 7.1 and the default language version of the project is C# 7.0, you need to change the language version to C# 7.1 or higher. To do that, right-click on the project node in Solution Explorer and select Properties. Select the Build tab and select the Advanced button. In the dropdown, select C# 7.1 (or a higher version). Select the OK button.
        //without doing teh above, the sentimentsAndPredictions line will have syntax errors / won't compile
        #endregion

        public static void Predict(PredictionModel<SentimentData, SentimentPrediction> model)
        {
            IEnumerable<SentimentData> sentiments = new[]
            {
                new SentimentData
                {
                    SentimentText = "Please refrain from adding nonsense to Wikipedia."
                },
                new SentimentData
                {
                    SentimentText = "He is the best, and the article should say that."
                },
                new SentimentData
                {
                    SentimentText = "This guy is talking nonsense he is terrible and bad at the same time."
                }
            };

            IEnumerable<SentimentPrediction> predictions = model.Predict(sentiments);

            Console.WriteLine();
            Console.WriteLine("Sentiment Predictions");
            Console.WriteLine("---------------------");

            var sentimentsAndPredictions = sentiments.Zip(predictions, (sentiment, prediction) => (sentiment, prediction));

            foreach (var item in sentimentsAndPredictions)
            {
                Console.WriteLine($"Sentiment: {item.sentiment.SentimentText} | Prediction: {(item.prediction.Sentiment ? "Positive" : "Negative")}");
            }


        }

    }
}
