# WeChat_Messenger Web App
A simple messenger web application built with ASP.NET Core 7 for the backend and Angular 14 for the frontend, 
utilizing SignalR(internally uses web-sockets for full duplex communication) for real-time communication between users. 
The application uses dictionary data structures for data storage and manipulation.

Features
Real-time messaging between users
Lightweight data storage with dictionary data structures
Modern frontend built with Angular 14
Backend powered by ASP.NET Core 7
Real-time communication via SignalR

Technologies Used
Backend: ASP.NET Core 7
Frontend: Angular 14
Real-Time Communication: SignalR
Frontend SignalR Package: @microsoft/signalr

Getting Started
Prerequisites
  Make sure you have the following installed:
  .NET SDK 7
  Node.js and npm
  Angular CLI

Installation
Clone the repository: https://github.com/Santos197/WeChat_Messenger.git
git clone 
cd messenger-web-app

Backend Setup:
Navigate to the Backend directory and restore the .NET dependencies.
cd Backend
dotnet restore

Frontend Setup:
Navigate to the Frontend directory and install the npm packages.

cd ../Frontend
npm install

Running the Application
Start the Backend:

cd Backend
dotnet run

Start the Frontend:
cd ../Frontend
ng serve
Open your browser and navigate to http://localhost:4200.

Usage
Once the application is running:

Open the application in multiple browser tabs or windows.
Register or login with different users.
Start chatting in real-time. Messages sent from one user will appear instantly for all other connected users.
