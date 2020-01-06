## Description
This is a backend system to showcase 

## Prerequisites
1. Install .net core 2.2
2. Prepare database 

## Run the application
1. The easiest is to run it using Jetbrains Rider or Visual Studio
2. *NOTE* Unfortunately the TimeSeries estimator is currently only runs on Windows [link](https://github.com/dotnet/machinelearning/issues/3903)
3. In order to test this on Mac, for now, please return an arbitrary number in SsaEstimator.cs
    ```c#
    public static float GetEstimate(ProductWithData product)
    {
        return 100f;
    }
    ```
4. To access the application: `http://localhost:5000/api/{controller}/{productName}`  

## Structure of the application
- The main entry point is in WebApplicationAttempt project. 
- During the presentation, the EstimateController was used as an entry point.
- There are 2 domain logic projects: 
  - DomainLogic: This is the one used during the demo
  - DomainLogicExt: This is a complete implementation using Either<E, T> approach
- The final working application can be accessed from the ReportController