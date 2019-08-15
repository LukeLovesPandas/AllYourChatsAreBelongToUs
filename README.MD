# AEP Charge Code Kata - AllYouChatsAreBelongToUs

## Installation and Running
1) install dotnet core from here: https://dotnet.microsoft.com/download
2) pull down this project and run 'dotnet restore' and 'dotnet build'
3) once those are successful, use 'dotnet run' to run the service

There is a postman collection included with this project to hit it with. There is also an environment. 
Take your slack token and slack user name and set them in the environment variables.

Run the 'Get All Users with Integrations Info' to set the defaultUserId environment variable and then the other calls should work for you. 
I included a direct call to the slack API for comparison which you can also set with the environment variables


## Design overview
This project is brokwn down into a few parts
Contracts - shared DTOs, used for integrations(slack and the 'frontend/rest api')
Controllers - Where the simple UsersController lives
Database - In memory database where the user entity, sub entities and context are stored
Services - Code that consolidates and operates on the data

This project has a user concept, with a list of integrations that the user can be part of. The list of integrations have a 
base type of ChatIntegration, and a derived type of SlackIntegration. The user can have multiple instances of slack or any other
chat integration and have those be operated on.

We have two different ways of calling the users, one to get the user details with the integrations details and one to get user details
with the profile details of the given integration. These are retrieved by calling the GetUserInfo method on the IUserClient interface
for the integration specific http client. 

To add a new integration for users, you would need to implement content in a few spots:
1) In the contracts, add an integration to the constants with the ID.
2) Implement the DTOs for the UI contracts and the API contracts, using the Base ChatUser, ChatIntegration for the ViewModels contracts
3) Implement a Integration Entity using the base ChatIntegrationEntity, and add the type to the 'HasBaseType' modelBuilder section of the UserContext
4) Implement an Integration Client that implements the IUserClient to retrieve info.
5) Update the derived logic in the MappingExtensions and ChatIntegrationConverter

After those five steps anyone should be able to call and use your integration