using SparePartsWarehouse.CORE.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.CORE.Interfaces
{
    public interface IMachineSparePartsCostService
    {
        Task<List<MachineSparePartsCostDetailDto>> GetDetailsAsync(
            int factoryDepartmentId = 0,
            int machineId = 0,
            DateTime? fromDate = null,
            DateTime? toDate = null);

        Task<List<MachineSparePartsCostDto>> GetAsync(
        int factoryDepartmentId = 0,
        int machineId = 0,
        DateTime? fromDate = null,
        DateTime? toDate = null);
    }
}
