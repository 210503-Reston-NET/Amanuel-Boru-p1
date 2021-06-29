# Store App
The store app is a software that helps customers purchase products from your business. Designed with functionality that would make virtual shopping much simpler! Customer features include: account creation, order placements, and order history. It also comes with admin functionality that let's store owners replenish inventory and view the specific store's order history!

# Start up
To start this application on your own local machine follow this steps.
  1. clone this Repasitory to your local machine
  2. Create a Database and get the connection string for the database
  3. Add an appsetting.json file to the storeWebUi folder and add the connection string in the file with the key "StoreDB".
  4. run the command `dotnet restore` in the storeApp directory
  5. run the command `dotnet build` and `dotnet run` to run the aplication
