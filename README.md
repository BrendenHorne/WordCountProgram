# WordCountProgram

## Overview
This is a multithreaded word count program. It sends data between two pipes and displays the word count of the inputted text.

## Installation Requirements
For this project you will need to download a recent version of .NET SDK.\
You can download a version [here](https://dotnet.microsoft.com/en-us/download)

## Run Requirements
After downloading and extracting the files locate the WordCount folder in your file explorer. Left click on the folder to select it. Then, right click and select Copy as Path.

Windows:
Next open the command terminal and run the command "cd path\to\WordCount". Replace path\to\WordCount with the path you copied.\
Next, run the command "dotnet build" and "dotnet run"

Linux:
Run the command "cd /mnt/c/path/to/WordCount". Replace path/to/WordCount with the path you copied.\
Make sure to change the "\" to "/" if neccesary.\
Next, run the command "dotnet build" and "dotnet run"

You will have to do this in two separate command terminals, one for the Producer and one for the Consumer.
