using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class LocationBL : ILocationBL
    {
        private LocationDB _locationDB;

        public LocationBL(LocationDB newLocationRepo){
            _locationDB = newLocationRepo;
        }

        public Location AddLocation(Location newLocation){
            if (LocationExists(newLocation)) {
                throw new Exception("Location already exists");
            }
            return _locationDB.AddLocation(newLocation);
        }

        public List<Location> GetAllLocations(){
            return _locationDB.GetAllLocations();
        }

        public Location GetLocation(int id)
        {
            return _locationDB.GetLocationById(id);
        }

        public bool LocationExists(Location newLocation){
            if (_locationDB.GetLocation(newLocation) != null) return true;

            return false;
        }
        
        public void AddItemToLocation(int locationId, Inventory newItem){
            Location existingLocation = _locationDB.GetLocationById(locationId);
            if (existingLocation == null){ 
                throw new Exception("Location does not exist");
            }
            else{
                _locationDB.AddItemToLocation(existingLocation, newItem);
            }
        }

        public List<Inventory> GetInventory(Location location){
            if (LocationExists( location) == false){
                throw new Exception("Location does not exist");
            }
            return _locationDB.GetInventory(location);
        }

        public List<Inventory> GetInventory(int location)
        { 
            return _locationDB.GetInventory(location);
        }

        public Inventory UpdateInventory(Inventory inventory)
        {
            return _locationDB.UpdateInventory(inventory);
        }

        public void changeInventory(Location location, Inventory item, int amount){
            _locationDB.changeInventory(location.LocationID, item, amount);
        }

        public Inventory DeleteItem(int id) {
            return _locationDB.DeleteItem(id);
        }

        public Inventory GetInventoryItem(int id)
        {
            return _locationDB.GetInventoryItem(id);
        }

        public Inventory GetInventoryItemFromLocation(int locationID, int productId)
        {
            return _locationDB.GetInventoryItemFromLocation(locationID, productId);
        }

        public Location UpdateLocation(Location location)
        {
            return _locationDB.UpdateLocation(location);
        }

        public Location DeleteLocation(Location location)
        {
            return _locationDB.DeleteLocation(location);
        }
    }
}