SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TEDU
-- Create date: 18/12/2018
-- Description:	Get all product with paging
-- =============================================
CREATE PROCEDURE Get_Product_AllPaging
	@keyword nvarchar(50),
	@categoryId int,
	@pageIndex int,
	@pageSize int,
	@totalRow int output
AS
BEGIN
	SET NOCOUNT ON;

	select @totalRow = count(*) from Products p 
	where (@keyword is null or p.Sku like @keyword +'%') and p.IsActive = 1

	select p.Id,
		p.Sku,p.Price,
		p.DiscountPrice,
		p.ImageUrl
	from Products p where (@keyword is null or p.Sku like @keyword +'%')
	and p.IsActive = 1
	order by p.CreatedAt desc
	offset (@pageIndex - 1) * @pageSize rows
	fetch next @pageSize row only

END
GO
