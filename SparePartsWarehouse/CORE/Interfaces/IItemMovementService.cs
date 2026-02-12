using SparePartsWarehouse.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface IItemMovementService
    {
        Task<List<ItemMovement>> GetAsync(
            int? itemId,
            int? itemDescriptionId);
    }


}
