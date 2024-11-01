@echo off
REM Navigate to the TrainingTypeConfigAPI folder
cd TrainingTypeConfigAPI

REM Start the API project (runs in the background)
start /B dotnet run

REM Wait a few seconds to ensure the API is up and running
timeout /t 5 /nobreak > nul

REM Navigate to the ProvideFeedbackSample folder
cd ..\ProvideFeedbackSample

REM Run the console application
dotnet run

REM Optional: Kill the API process once the console app completes
taskkill /F /IM dotnet.exe
