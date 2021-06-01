using System.Collections.Generic;
using StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace StoreDL
{
    public class LocationDB
    {
        private StoreDBContext _context;

        public LocationDB(StoreDBContext context)
        {
            _context = context;
        }

        public Location AddLocation(Location location){
            _context.Locations.Add(
                new Location(
                    location.LocationName,
                    location.Address
                )
            );

            _context.SaveChanges();
            return location;
        }
        public List<Location> GetAllLocations(){
            return _context.Locations.Select( location => new Location(location.LocationName, location.Address, location.LocationID)).ToList();
        }

        public Location GetLocationById(int id)
        {
            return _context.Locations.FirstOrDefault(location => location.LocationID == id);
        }

        public Location GetLocation(Location location){
            Location found = _context.Locations.FirstOrDefault(local =>  local.LocationID == location.LocationID);

            if (found == null) return null;
            else{
                return found;
            }
        }

        public List<Inventory> GetInventory(Location location){
            Location found = _context.Locations.FirstOrDefault(local =>  local.LocationID == location.LocationID);

            if (found == null){
                throw new Exception("Location does not exist");
            }

            List<Inventory> inventory = _context.Inventories.Where(
                item => item.LocationId == found.LocationID)
                .Select(
                    item => new Inventory(item.InventoryId, item.LocationId, item.ProductId, item.Quantity)
                ).ToList();

            return inventory;
        }

        public List<Inventory> GetInventory(int location)
        {
            Location found = _context.Locations.FirstOrDefault(local => local.LocationID == location);

            if (found == null)
            {
                throw new Exception("Location does not exist");
            }

            List<Inventory> inventory = _context.Inventories.Where(
                item => item.LocationId == found.LocationID)
                .Select(
                    item => new Inventory(item.InventoryId, item.LocationId, item.ProductId, item.Quantity)
                ).ToList();

            return inventory;
        }

        public Inventory GetInventoryItemFromLocation(int locationID, int productId)
        {
            return _context.Inventories.FirstOrDefault(item => item.LocationId == locationID && item.ProductId == productId);
        }

        public Inventory UpdateInventory(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            _context.SaveChanges();
            return inventory;
        }

        public Inventory DeleteItem(int id)
        {
            Inventory toBeDeleted = _context.Inventories.FirstOrDefault(item => item.InventoryId == id);
            _context.Inventories.Remove(toBeDeleted);
            _context.SaveChanges();
            return toBeDeleted;
        }

        public Inventory GetInventoryItem(int id)
        {
            return _context.Inventories.FirstOrDefault(item => item.InventoryId == id);
        }

        public Location UpdateLocation(Location location)
        {
            _context.Locations.Update(location);
            _context.SaveChanges();
            return location;
        }

        public Location DeleteLocation(Location location)
        {
            Location toBeDeleted = _context.Locations.FirstOrDefault(loca => loca.LocationID == location.LocationID);
            _context.Locations.Remove(toBeDeleted);
            _context.SaveChanges();
            return location;
        }

        public void changeInventory(int location, Inventory item, int amount){
            
                Inventory Eitem = _context.Inventories.
                FirstOrDefault( itm => itm.ProductId == item.ProductId && itm.LocationId == location);

                Eitem.Quantity += amount;

                _context.SaveChanges();
        }

        public Inventory AddItemToLocation(Location location, Inventory item){
            _context.Inventories.Add(
                new Inventory(item.LocationId, item.ProductId, item.Quantity)
            );
            _context.SaveChanges();
            return item;
        }
    }
}