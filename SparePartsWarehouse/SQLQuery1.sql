USE [Warhouse]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[sp_EditPurchaseItem]
		@PurchaseDetailId = 26,
		@NewQuantity = 4,
		@NewUnitPrice = 60,
		@Reason = N'''damaged goods''',
		@EditedBy = N'''Ahmed'''

SELECT	@return_value as 'Return Value'

GO
