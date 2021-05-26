using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// This class should contain all the fields and properties that define a store location.
    /// </summary>
    public class Location
    {
        public Location()
        {
        }
        public Location(string locationName , string address){
            Address = address;
            LocationName = locationName;
            Inventories = new List<Inventory>();
        }

        public Location(string locationName , string address, int locationID){
            Address = address;
            LocationName = locationName;
            Inventories = new List<Inventory>();
            LocationID = locationID;
        }

        public Location(int locationID)
        {
            LocationID = locationID;
            Address = null;
            LocationName = null;
            Inventories = new List<Inventory>();
        }

        public int LocationID;
        public string Address { get; set; }
        public string LocationName { get; set; }

        public List<Inventory> Inventories { get; set;}
        //TODO: add some property for the location inventory

        public bool Equals(Location newLocation){
            return this.Address.Equals(newLocation.Address) && this.LocationName.Equals(newLocation.LocationName);
        }

        public Inventory GetItem(Inventory newItem){
            foreach(Inventory item in Inventories){
                if (item.Equals(newItem)) return item;
            }

            return null;
        }

        public int GetItemIndex(Inventory newItem){
            for(int i =0; i < Inventories.Count; i++){
                if (Inventories[i].Equals(newItem)){
                    return i;
                }
            }
            return -1;
        }

        public override string ToString()
        {
            return $" Location name: {LocationName} \n\t Address: {Address}";
        }
    }
}