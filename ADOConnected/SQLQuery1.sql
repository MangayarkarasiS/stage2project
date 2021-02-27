USE [Library]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[Book_ins]
		@id = 888,
		@bnm = N'MVC',
		@pri = 120,
		@qty = 12

SELECT	@return_value as 'Return Value'

GO
