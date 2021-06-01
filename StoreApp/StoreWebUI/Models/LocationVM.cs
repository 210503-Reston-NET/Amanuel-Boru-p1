using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StoreModels;

namespace StoreWebUI.Models
{
    public class LocationVM
    {
        public LocationVM()
        {
        }

        public LocationVM(Location location)
        {
            LocationID = location.LocationID;
            Address = location.Address;
            LocationName = location.LocationName;
        }


        public int LocationID;

        [Required]
        public string Address { get; set; }

        [Required]
        public string LocationName { get; set; }
    }
}
