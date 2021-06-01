using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public interface ILocationBL
    {

        Location AddLocation(Location newLocation); 

        List<Location> GetAllLocations();

        Location GetLocation(int id);

        bool LocationExists(Location newLocation);

        void AddItemToLocation(int locationId, Inventory newItem);

        List<Inventory> GetInventory(Location location);

        List<Inventory> GetInventory(int location);

        Inventory UpdateInventory(Inventory inventory);

        void changeInventory(Location location, Inventory item, int amount);

        Inventory DeleteItem(int id);

        Inventory GetInventoryItem(int id);

        Inventory GetInventoryItemFromLocation(int locationID, int productId);

        Location UpdateLocation(Location location);

        Location DeleteLocation(Location location);
    }
}
