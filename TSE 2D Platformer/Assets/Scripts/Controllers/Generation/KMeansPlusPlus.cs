using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ML;
using System.IO;
using K_Means_Plus_Plus;
public class KMeansPlusPlus : MonoBehaviour
{
    int goodClusterID;
    int badClusterID;

    // Start is called before the first frame update
    void Start()
    {      
        //File path to file where trained model is stored
        string _modelPath = Path.Combine(Directory.GetCurrentDirectory(), "Model", "KMeansClusteringModel.zip");

        //Sets up ML Context: allows useful mechanisms like logging and entry points
        var mlContext = new MLContext(seed: 0);

        //For the data preperation pipeline
        DataViewSchema modelSchema;

        //Loads trained model
        var trainedModel = mlContext.Model.Load(_modelPath, out modelSchema);
        Debug.Log(trainedModel);
        //Using the model to make a prediction (Note - not thread safe)
        //The Prediction Enginge class is an API to make running predictions simpler
        var predictor = mlContext.Model.CreatePredictionEngine<PlayerData, ClusterPrediction>(trainedModel);
        Debug.Log("TESTING END");
        //Loads cluster context
        string[] goodClusterLine;
        string[] badClusterLine;

        using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "Model", "ClusterContext.txt")))
        {
            string goodCluster = sr.ReadLine();
            goodClusterLine = goodCluster.Split(",");

            string badCluster = sr.ReadLine();
            badClusterLine = badCluster.Split(",");
        }
        //Checks expeted data found
        if (badClusterLine[0] != "Bad" | goodClusterLine[0] != "Good")
        {
            Debug.Log("INCORRECT CLUSTER ANALYSIS");
        }

        goodClusterID = int.Parse(goodClusterLine[1]);
        badClusterID = int.Parse(badClusterLine[1]);

        //Prediction for TestIrisData.cs
        var prediction = predictor.Predict(TestPlayerData.BadPlayer);
        Debug.Log($"Cluster: {prediction.PredictedClusterId}");
        Debug.Log($"Distances: {string.Join(" ", prediction.Distances)}");
        PrintClusterContext(prediction.PredictedClusterId);

        var prediction2 = predictor.Predict(TestPlayerData.AveragePlayer);
        Debug.Log($"Cluster: {prediction2.PredictedClusterId}");
        Debug.Log($"Distances: {string.Join(" ", prediction2.Distances)}");
        PrintClusterContext(prediction2.PredictedClusterId);

        var prediction3 = predictor.Predict(TestPlayerData.GoodPlayer);
        Debug.Log($"Cluster: {prediction3.PredictedClusterId}");
        Debug.Log($"Distances: {string.Join(" ", prediction3.Distances)}");
        PrintClusterContext(prediction3.PredictedClusterId);

    }

    void PrintClusterContext(uint clusterID)
    {
        if (clusterID == goodClusterID)
        {
            Debug.Log("Good Player");
        }
        else if (clusterID == badClusterID)
        {
            Debug.Log("Bad Player");
        }
        else
        {
            Debug.Log("Average Player");
        }
    }


}
