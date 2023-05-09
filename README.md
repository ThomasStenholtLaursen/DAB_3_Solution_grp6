# DAB_3_Solution_grp6

---
> It is recommended to run Microsoft SQL Server with Docker, to do this you can [follow this guide](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&preserve-view=true&pivots=cs1-powershell) and to run MongoDB with Docker, to do this you can [follow this guide](https://hub.docker.com/_/mongo/). You don't have to run the databases in Docker, but it's <b>important</b> to use Microsoft SQL Server as this application uses the NuGet-package [Microsoft.EntityFrameworkCore.SqlServer](https://learn.microsoft.com/en-us/ef/core/providers/sql-server/?tabs=dotnet-core-cli) and use MongoDb as this application uses the [MongoDB.Driver](https://www.mongodb.com/docs/drivers/csharp/current).
## Quick Guide

1. Connect to your SQL Server.
2. Get the `ConnectionString` of your database.
3. In the VS-solution navigate to `appsettings.json`.
4. Replace the current `ConnectionString` with your personal `ConnectionString`.
5. In Visual Studio go to <b>Tools</b> -> <b>NuGet Package Manager</b> -> <b>Package Manager Console.</b>
6. The PMC terminal will open.
7. Type the following: `Update-Database`.
8. Refresh your database.
10. You should now see that tables have been added.
11. In the VS-solution navigate to `appsettings.json`.
12. Replace the current `MongoDbDab3:ConnectionString` with your MongoDb ConnectionString, add authentication if your MongoDb uses it.
13. You can now start the solution (Press <kbd>F5</kbd>).
14. If the database does not contain any data, the program will Seed some dummy-data automatically the first time you launch the program. Otherwise you can use the "Reset" endpoints in the SwaggerUI, both for MSSQL and MongoDB
15. Try out the different queries in the SwaggerUI.

## Step 4 and step 12:
> `appsettings.json` looks something like this, insert your personal connectionstring as shown.
```Json 
{
  "MongoDbDab3": {
    "ConnectionString": "mongodb://localhost:27017", // replace with YOUR MongoDB ConnectionString
    "DatabaseName": "DAB3",
    "CanteenCollectionName": "Canteen",
    "CustomerCollectionName": "Customer",
    "MealCollectionName": "Meal",
    "RatingCollectionName": "Rating",
    "ReservationCollectionName": "Reservation",
    "MenuCollectionName": "Menu"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Database": "Data Source=localhost;User ID=sa;Password=Sqlpassword1;Initial Catalog=DAB_Assignment3_au637137_au597196_au635831;Encrypt=False;Trust Server Certificate=True;"
    // replace with YOUR MSSQL ConnectionString
  }
}
```
If your `ConnectionString` for MSSQL does not contain the following settings: `Encrypt=False;Trust Server Certificate=False;` then please add them. A full `ConnectionString` would look something like this: `"Data Source=localhost;User ID=<SOME_USERID>;Password=<SOME_PASSWORD>;Initial Catalog=<YOUR_CHOSEN_DATABASE_NAME>;Encrypt=False;Trust Server Certificate=False;"`
A typical `ConnectionString` for MongoDB with Docker would look something like this `"mongodb://localhost:27017"`
