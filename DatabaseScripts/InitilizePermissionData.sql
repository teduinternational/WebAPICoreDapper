delete from Actions
select * from Actions
select * from Functions
select * from ActionInFunctions
select * from AspNetUsers
select * from AspNetRoles
select * from AspNetUserRoles
select * from Permissions
--Add user to role


--Insert to Actions

insert into Actions(Id,Name,SortOrder,IsActive) values('CREATE',N'Tạo mới',1,1)
insert into Actions(Id,Name,SortOrder,IsActive) values('UPDATE',N'Cập nhật',2,1)
insert into Actions(Id,Name,SortOrder,IsActive) values('DELETE',N'Xóa',3,1)
insert into Actions(Id,Name,SortOrder,IsActive) values('VIEW',N'Truy cập',4,1)
insert into Actions(Id,Name,SortOrder,IsActive) values('IMPORT',N'Nhập',5,1)
insert into Actions(Id,Name,SortOrder,IsActive) values('EXPORT',N'Xuất',6,1)
insert into Actions(Id,Name,SortOrder,IsActive) values('APPROVE',N'Duyệt',7,1)

--Insert to Function
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SYSTEM',N'Hệ thống','/admin/system',NULL,1,'icon-system',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SYSTEM.ROLE',N'Nhóm người dùng','/admin/system/role','SYSTEM',1,'icon-role',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SYSTEM.USER',N'Người dùng','/admin/system/user','SYSTEM',2,'icon-user',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SYSTEM.SETTING',N'Cấu hình','/admin/system/setting','SYSTEM',3,'icon-setting',1)

INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SALES',N'Kinh doanh','/admin/sales',NULL,2,'icon-sales',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SALES.CATALOG',N'Nhóm sản phẩm','/admin/sales/catalog','SALES',1,'icon-catalog',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SALES.PRODUCT',N'Sản phẩm','/admin/sales/product','SALES',2,'icon-product',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SALES.ORDER',N'Hóa đơn','/admin/sales/order','SALES',3,'icon-order',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('SALES.ATTRIBUTE',N'Thuộc tính','/admin/sales/attribute','SALES',4,'icon-attribute',1)

INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('REPORT',N'Báo cáo','/admin/report',NULL,3,'icon-system',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('REPORT.REVENUE',N'Doanh thu','/admin/report/revenue','REPORT',1,'icon-revenue',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('REPORT.INVENTORY',N'Tồn kho','/admin/report/inventory','REPORT',2,'icon-inventory',1)
INSERT INTO [dbo].[Functions]([Id],[Name],[Url],[ParentId],[SortOrder],[CssClass],[IsActive]) VALUES('REPORT.VISITOR',N'Truy cập','/admin/report/visitor','REPORT',3,'icon-visitor',1)

--Add action to function

INSERT INTO [dbo].[ActionInFunctions]([FunctionId],[ActionId])VALUES('SYSTEM.USER','CREATE')
INSERT INTO [dbo].[ActionInFunctions]([FunctionId],[ActionId])VALUES('SYSTEM.USER','UPDATE')
INSERT INTO [dbo].[ActionInFunctions]([FunctionId],[ActionId])VALUES('SYSTEM.USER','DELETE')
INSERT INTO [dbo].[ActionInFunctions]([FunctionId],[ActionId])VALUES('SYSTEM.USER','VIEW')
INSERT INTO [dbo].[ActionInFunctions]([FunctionId],[ActionId])VALUES('SYSTEM.USER','IMPORT')
