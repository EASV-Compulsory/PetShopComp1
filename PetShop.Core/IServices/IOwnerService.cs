using System.Collections;
using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IOwnerService
    {
        List<Owner> GetOwners();
        Owner GetOwnerById(int selectedNumber);
    }
}