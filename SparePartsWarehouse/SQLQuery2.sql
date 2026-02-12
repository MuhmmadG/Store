USE [Warhouse]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[sp_EditStockOutQuantity]
		@StockOutDetailId = 23,
		@NewQuantity = 0,
		@EditedBy = N'Ahmed'

SELECT	@return_value as 'Return Value'

GO
