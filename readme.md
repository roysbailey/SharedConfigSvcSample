# Sample shared config implementation
Provides a basic (not production strength) shared configuration service implementation to demonstrate the concept of providing centralised configuration to different domains within a microservices architecture.  

Specifically, it allows within a domain, for configuration to be segmented on a different aspect - lets look at an example.  Imagine a scenario, where behaviour within a domain differed by the *training type* of a particular training record.  For example, if the contracting domain had to enforce a different minimum contract duration based upon the *training type*, or where the recruit domain needed to hide a particular feature for a specific *training type*.  We would not want to litter the code with lots of `if trainingType == "X" then showFeatureX` it would be hard to maintain and cause coplexity.  Instead, you would want to load the configuration for that domain / training type combination and then ask this question... `if config.ShowFeatureX then showFeatureX()`   Using this approach, the domains behaviour is controlled by the configuration, allowing new training types to be added via configuration only (with no need for new code, if they used the existing behaviours)

Context diagram shown below.

![Training Configuration Diagram](assets/TrainingConfigC4.png)

## Training Type Config API
Stores the configuration for each combination of Domain and Training type.  Allows this to be access via an API endpoint.  A http file is included for endpoint testing, ensure you install the *Rest Client* extension is using Visual Studio Code.

## Training Type Shared Models
Assembly storing the c# models for each configuration set.

## Training Type Config Client
API client used by domain processes to simplify access to the config API.

## Consuming Domain
Represents a sample domain consuming the config service

## Running locally
Just run the *run.cmd* file within the root folder.  If you are on mac, i am sure you will work it out!