create proc Get_Role_AllPaging
	@keyword nvarchar(50),
	@pageIndex int,
	@pageSize int,
	@totalRow int output
as
begin
	set nocount on;
	select @totalRow = count(*) from AspNetRoles r
	where (@keyword is null or r.Name like @keyword +'%')

	select Id,Name from AspNetRoles r
	where (@keyword is null or r.Name like @keyword +'%')
	order by r.Name
	offset (@pageIndex - 1) * @pageSize rows
	fetch next @pageSize row only
end