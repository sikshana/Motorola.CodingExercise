# Motorola.CodingExercise
https://SwapnaAnnapureddy@dev.azure.com/SwapnaAnnapureddy/Motorola.CodingExercise/_git/Motorola.CodingExercise

Motorola Coding exercise:
-------------------------------------------
Coding exercise for Motorola technical screening.

Projects:
--------
Motorola.CodingExercise.Repository
Motorola.CodingExercise.Service
Motorola.CodingExercise.UI

Project for UnitTest:
----------------------
Motorola.CodingExercise.Repository.Test
Motorola.CodingExercise.Service.Test


Motorola.CodingExercise.Repository:
-------------------------------------
Data/Reposiry Layer of application.
Models, constants and other Resources/context (Ex: Database, cache management, etc) will be defined and implemented.
Purpose of this to communicate with Database and/or remote APIs, implement any business logic, validations.


Motorola.CodingExercise.Service:
------------------------------------
Place where API Controller are developed.
MarsRoverPhotoController -> 
GetMarsRoverPhotos GET API ->
	Read the text file in "Resources/dates.txt"
	Read each line and parse the string to DataTime.
	If valid date, call the repository method to get Mars Rover Photos for the date.
	Add them to list.
	Download the phototos to "Downloads/<Earth Data>/<photo id>.JPG"
	If photos are downloaded previous run, they will not be downloaded again. 
	If you want to test, please delete the "Downloads" folder and run the application.
	Docker container was added for this project.
	Please install "Docker Desktop" for windows and make sure it is running before running the application.
	
Motorola.CodingExercise.UI:
------------------------------------
ASP.Net Core Angular Application.
Calls API from "Motorola.CodingExercise.Service", get the Mars Rover Photos and displays them in tabular
format with columns - Earth date, Photo Id and actual image on page load.

Note: Make sure "Motorola.CodingExercise.Service" and "Motorola.CodingExercise.UI" are set as 
StartUp Projects. To consume API from Angular application, the service has to be up and running.

	
Motorola.CodingExercise.Repository.Test
-----------------------------------------
Unit Test project for Repository classes.


Motorola.CodingExercise.Service.Test	
------------------------------------
Unit Test project for Service (APIs)
	
	
	
	
	



