select top 100 u.Username, u.Email, r.Name from AspNetUsers u
inner join AspNetUserRoles ur on ur.UserId = u.Id
inner join AspNetRoles r on r.Id = ur.RoleId