using Microsoft.EntityFrameworkCore;
using SparePartsWarehouse.CORE.DTO;
using SparePartsWarehouse.CORE.Entities;
using SparePartsWarehouse.CORE.Interfaces;
using SparePartsWarehouse.DATA.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
namespace SparePartsWarehouse.DATA.Service
{


    public class PurchaseEditService : IPurchaseEditService
    {
        private readonly AppDbContext _context;

        public PurchaseEditService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResultDto> EditPurchaseItemAsync(
        int purchaseDetailId,
        decimal newQty,
        decimal newPrice,
        string reason,
        string editedBy)
        {
            var parameters = new[]
            {
        new SqlParameter("@PurchaseDetailId", purchaseDetailId),
        new SqlParameter("@NewQuantity", newQty),
        new SqlParameter("@NewUnitPrice", newPrice),
        new SqlParameter("@Reason", reason ?? ""),
        new SqlParameter("@EditedBy", editedBy)
    };

            try
            {
                var result = _context.Database
                    .SqlQueryRaw<OperationResultDto>(
                        "EXEC sp_EditPurchaseItem @PurchaseDetailId, @NewQuantity, @NewUnitPrice, @Reason, @EditedBy",
                        parameters)
                    .AsEnumerable()
                    .FirstOrDefault();

                return result ?? new OperationResultDto
                {
                    IsSuccess = false,
                    Message = "لم يتم تنفيذ العملية"
                };
            }
            catch (SqlException ex)
            {
                // إذا كانت رسالة SQL تحتوي على نص الرصيد بالسالب
                if (ex.Message.Contains("الرصيد سيصبح بالسالب"))
                {
                    return new OperationResultDto
                    {
                        IsSuccess = false,
                        Message = "لا يمكن تعديل الكمية: الرصيد سيصبح بالسالب بعد التعديل"
                    };
                }

                // لأي خطأ آخر، يمكن إرجاع رسالة عامة أو التفاصيل
                return new OperationResultDto
                {
                    IsSuccess = false,
                    Message = $"حدث خطأ أثناء التعديل: {ex.Message}"
                };
            }
        }

    }


}
