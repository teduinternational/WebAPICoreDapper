USE [RestAPIDapper]
GO
/****** Object:  StoredProcedure [dbo].[Get_Product_AllPaging]    Script Date: 12/19/2018 10:38:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TEDU
-- Create date: 18/12/2018
-- Description:	Get all product with paging
-- =============================================
ALTER PROCEDURE [dbo].[Get_Product_AllPaging]
	@keyword nvarchar(50),
	@categoryId int,
	@pageIndex int,
	@pageSize int,
	@language varchar(5),
	@totalRow int output
AS
BEGIN
	SET NOCOUNT ON;

	select @totalRow = count(*) from Products p 
	inner join ProductTranslations pt on p.Id = pt.ProductId
	left join ProductInCategories pic on p.Id = pic.ProductId
	left join Categories c on c.Id = pic.CategoryId
	where (@keyword is null or p.Sku like @keyword +'%') and p.IsActive = 1
	and pic.CategoryId = @categoryId

	select p.Id,
		p.Sku,
		p.Price,
		p.DiscountPrice,
		p.ImageUrl,
		p.CreatedAt,
		p.IsActive,
		p.ViewCount,
		pt.Name,
		pt.Content,
		pt.Description,
		pt.SeoAlias,
		pt.SeoDescription,
		pt.SeoKeyword,
		pt.SeoTitle,
		pt.LanguageId,
		c.Name as CategoryName
	from Products p 
	inner join ProductTranslations pt on p.Id = pt.ProductId 
	left join ProductInCategories pic on p.Id = pic.ProductId
	left join Categories c on c.Id = pic.CategoryId
	where (@keyword is null or p.Sku like @keyword +'%')
	and pt.LanguageId = @language
	and p.IsActive = 1
	and pic.CategoryId = @categoryId
	order by p.CreatedAt desc
	offset (@pageIndex - 1) * @pageSize rows
	fetch next @pageSize row only

END
