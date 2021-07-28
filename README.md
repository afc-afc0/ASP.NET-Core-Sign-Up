# ASP.Net client Sign Up  

We are building a trading game with my friend. Currently, I am developing the server-side in ASP.NET and, my friend is developing the client-side in Unity. Game's purpose is getting resources from different places such as Mines or the Market then selling these resources through the Market, also we can generate a variety of items with these resources. Then we can use the items for selling or building other items. The first client who reaches a certain amount of money wins the game. Our goal is to develop a minimum viable product.
 
- Client sends json data through Unitywebrequest.
- Asp.Net Validates user information through Fluent Validation Library(https://fluentvalidation.net). 
- After validation. We send the data to DataLibrary.
- DataLibrary hashes the password through BCrypt.Net-Next(https://www.nuget.org/packages/BCrypt.Net-Next) library. 
- DataLibrary uses Dapper(https://dapper-tutorial.net) to access the postgreSQL. Checks if the client is already registered.
- If client is not registered. We register the client.

# UML Sequence Diagram of Sign Up
![Sequence Diagram sign up](https://user-images.githubusercontent.com/37782582/125366266-c4437000-e343-11eb-8b12-9090dbda3bf5.PNG)








