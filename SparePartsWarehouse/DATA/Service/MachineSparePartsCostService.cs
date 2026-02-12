using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SparePartsWarehouse.DATA.Service
{
    public class MachineSparePartsCostService : IMachineSparePartsCostService
    {
        private readonly AppDbContext _context;

        public MachineSparePartsCostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MachineSparePartsCostDetailDto>> GetDetailsAsync(
    int factoryDepartmentId = 0,
    int machineId = 0,
    DateTime? fromDate = null,
    DateTime? toDate = null)
        {
            var factoryParam = new SqlParameter("@FactoryDepartmentId", factoryDepartmentId);
            var machineParam = new SqlParameter("@MachineId", machineId);
            var fromDateParam = new SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value);
            var toDateParam = new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value);

            return await _context.Set<MachineSparePartsCostDetailDto>()
                .FromSqlRaw(
                    "EXEC sp_MachineSparePartsCost_Details @FactoryDepartmentId, @MachineId, @FromDate, @ToDate",
                    factoryParam, machineParam, fromDateParam, toDateParam
                )
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<MachineSparePartsCostDto>> GetAsync(
        int factoryDepartmentId = 0,
        int machineId = 0,
        DateTime? fromDate = null,
        DateTime? toDate = null)
        {
            var factoryParam = new SqlParameter("@FactoryDepartmentId", factoryDepartmentId);
            var machineParam = new SqlParameter("@MachineId", machineId);
            var fromDateParam = new SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value);
            var toDateParam = new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value);
            return await _context.Set<MachineSparePartsCostDto>()
                .FromSqlRaw(
                    "EXEC sp_MachineSparePartsCost @FactoryDepartmentId, @MachineId, @FromDate, @ToDate",
                    factoryParam, machineParam, fromDateParam, toDateParam
                )
                .AsNoTracking()
                .ToListAsync();
        }
    }

}
