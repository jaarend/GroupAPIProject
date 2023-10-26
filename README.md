# GroupAPIProject

## Arcane Anvil : A character creator

### By: Zach, Jordan, Jayson, Alex

This application will help users create a new character completely from scratch for an adventure role-playing game.
It should make it easy for users to create and update not only a character, but brand new races, classes, and gear of their character(s) in an all-in-one app.

---
Several steps need to be completed in order to run this application yourself.
1. Clone the code
2. Have appropriately credentialed access to a running server
3. Create or access secrets.json and enter the following code: (replace the asterisks with your relevant connection string)
```
{
    "ConnectionStrings:DefaultConnection" : "*******"
    ,
    "Jwt:Key" : "SuperSecretSecurityKeyGoesHere12345!",
    "Jwt:Issuer" : "localhostServer",
    "Jwt:Audience" : "localhostClient"
}
```
4. Run the command below to migrate the code first database to the above server:
```
dotnet ef database update -p .\GroupApiProject.Data\ -s .\GroupApiProject.WebApi\
```
5. Some initial data should be seeded into the database with the following query:
```
INSERT into charactertypes
    ([Name], [Description], DateCreated)
VALUES
    ('Default', 'This characterType is the default and is not supposed to be used for normal selections', GETDATE())
    ,('Hero', 'the bestest good guy that you always want to win', GETDATE());
    ,('Enemy', 'The one that should always be defeated', GETDATE());
GO

INSERT into geartypes
    ([Name], [Description], DateCreated)
VALUES
    ('Default', 'This gearType is the default and is not supposed to be used for normal selections', GETDATE())
    ,('Armor', 'An important piece of kit that helps reduce incomeing damage', GETDATE());
    ,('Weapon', 'The applier of damage', GETDATE());
GO

INSERT into attacktypes
    ([Name], [Description], DateCreated)
VALUES
    ('Default', 'This attackType is the default and is not supposed to be used for normal selections', GETDATE())
    ,('Physical', 'No nonesense pure and raw damage dealing', GETDATE());
    ,('Magic', 'Uses the elements to apply damage with effects', GETDATE());
GO

INSERT into usertypes
    ([Name], [Description], DateCreated)
VALUES
    ('Default', 'This userType is the default and is not supposed to be used for normal selections', GETDATE())
    ,('Admin', 'All of the access to all of the things', GETDATE());
    ,('Forger', 'The typical user with limited access', GETDATE());
```
6. Run the application with the following command:
```
dotnet run --project .\GroupApiProject.WebApi\
```
---
This application will primarily be accessed via api endpoints with a json body (several via URL route) defined below but due to the database structure and foreign key dependencies, certain functions should be processed before others.
(Be sure to capture your initial connection url in your running code to apply in front of the listed endpoints.)

1. Create a new user (if a user already exists, this step may be skipped).
  
   {userRole is the Id of one of the freshly seeded UserTypes from above}

   `POST` to /api/User
```
{
  "firstName": "string",
  "lastName": "string",
  "email": "user@example.com",
  "userName": "string",
  "userRole": 0,
  "password": "string",
  "confirmPassword": "string"
}
```
2. Login for a bearer token.
   
   `POST` to /api/Token
```
{
  "userName": "string",
  "password": "string"
}
```
3. Gear, Attacks, and Race may be created in any order before Class and then lastly Characters

   Create a piece of gear:

   `POST` to /api/Gear

   {type is the Id of one of the freshly seeded GearTypes from above; value is the amount of armor or damage it deals depending on selected type}
```
{
  "name": "string",
  "description": "string",
  "type": 0,
  "value": 0
}
```
#### Additional Gear endpoints:

Display all gear:

-`GET` to /api/Gear

Display a piece of gear by it's Id:

-`GET` to /api/Gear/{gearId}

Update a piece of gear by it's Id:

-`PUT` to /api/Gear
```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "type": 0,
  "value": 0
}
```
Delete a piece of gear by it's Id:

-`DELETE` to /api/Gear
```
{
  "id": 0
}
```
---
Create an attack

`POST` to /api/Attack 

{type is the Id of one of the freshly seeded attackTypes from above}
```
{
  "name": "string",
  "description": "string",
  "type": 0,
  "hitValue": 0,
  "apCost": 0
}
```
#### Additional Attack endpoints:

Display all attacks:

-`GET` to /api/Attack

Display an attack by it's Id:

-`GET` to /api/Attack/{attackId}

Update an attack by it's Id:

-`PUT` to /api/Attack
```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "type": 0,
  "hitValue": 0,
  "apCost": 0
}
```
Delete an attack by it's Id:

-`DELETE` to /api/Attack
```
{
  "id": 0
}
```
---
Create a race

`POST` to /api/Race
```
{
  "name": "string",
  "description": "string",
  "strengthModifier": 0,
  "constitutionModifier": 0,
  "intelligenceModifier": 0
}
```
#### Additional Race endpoints:

Display a race by it's Id:

-`GET` to /api/Race

-`GET` to /api/Race/{raceId}

Update a race by it's Id:

-`PUT` to /api/Race
```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "strengthModifier": 0,
  "constitutionModifier": 0,
  "intelligenceModifier": 0
}
```
Delete a race by it's Id:

-`DELETE` to /api/Race
```
{
  "id": 0
}
```
---
4. Class should be created next:

   `POST` to /api/Class

   {weaponId and armorId are Id's from the Gear table; attackSlot_1 and attackSlot_2 are Id's from the Attack table}
```
{
  "name": "string",
  "description": "string",
  "attackSlot_1": 0,
  "attackSlot_2": 0,
  "weaponId": 0,
  "armorId": 0
}
```
#### Additional Class endpoints:

Display a class by it's Id:

-`GET` to /api/Class/{classId}

Update a class by it's Id:

-`PUT` to /api/Class
```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "attackSlot_1": 0,
  "attackSlot_2": 0,
  "weaponId": 0,
  "armorId": 0
}
```
Delete a class by it's Id:

-`DELETE` to /api/Class
```
{
  "id": 0
}
```
---
5. Finally, with the other information created, a Character may be forged!

   `POST` to /api/Character

{type is the Id from characterTypes; raceId is the Id from Race; classId is the Id from Class}   
```
{
  "name": "string",
  "description": "string",
  "type": 0,
  "raceId": 0,
  "classId": 0,
  "armor": 0,
  "strength": 0,
  "constitution": 0,
  "intelligence": 0
}
```
#### Additional Character endpoints:

Display all characters (without being logged in):

-`GET` to /api/Character/All

Display a character by it's Id (without being logged in):

-`GET` to /api/Character/Find/{characterId}

Display only characters with an ownerId matching the logged in user:

-`GET` to /api/Character

Update a character by it's Id:

-`PUT` to /api/Character
```
{
  "id": 0,
  "name": "string",
  "description": "string",
  "type": 0,
  "raceId": 0,
  "classId": 0,
  "armor": 0,
  "strength": 0,
  "constitution": 0,
  "intelligence": 0,
  "ownerId": 0
}
```
Delete a character by it's Id:

-`DELETE` to /api/Character
```
{
  "id": 0
}
```
---
Additional functionality and endpoints exist and may be processed as described below:
---
One of the "type" tables may be interacted with via the api as well:

Display all attack types:

-`GET` to /api/AttackType

Display an attack type by it's Id:

-`GET` to /api/AttackType/{attackTypeId}

Update an attack type by it's Id:

-`PUT` to /api/AttackType
```
{
  "id": 0,
  "name": "string",
  "description": "string"
}
```
Delete an attack type by it's Id:

-`DELETE` to /api/AttackType
```
{
  "id": 0
}
```
---
A specific user may be looked up via:

`GET` to /api/User/{userId}

---
Enjoy the endless possibilities you can make!
