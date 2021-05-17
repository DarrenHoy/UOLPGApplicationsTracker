use PGProgrammeApplications
go

drop table if exists Application;
drop table if exists ProgrammeOfStudy;
drop table if exists AdmissionTerm;
drop table if exists ModeOfStudy;
drop table if exists AppUserRoleMember;
drop table if exists AppUser;
drop table if exists UserRole;
drop table if exists Student;
drop table if exists ApplicationStatus;
drop sequence if exists SQ_ModeOfStudyEnum;
drop sequence if exists SQ_ApplicationStatusEnum;




create table Student(
	Id uniqueidentifier default newid() primary key,
	FirstName nvarchar(200) not null,
	LastName nvarchar(200) not null,
	EmailAddress nvarchar(500) not null,
	DateOfBirth date not null,
	IsUkResident char not null,
	Username nvarchar(100) not null,
	UserPassword nvarchar(100) not null,

	constraint CK_Student_IsUKResident check (IsUkResident IN ('Y','N')),
	constraint UQ_Student_EmailAddress unique (EmailAddress)
);

create table ProgrammeOfStudy(
	Id uniqueidentifier default newid() primary key,
	Description nvarchar(500) not null
);

create table AdmissionTerm(
	Id uniqueidentifier default newid() primary key,
	Description nvarchar(20) not null,
	StartDate date not null,
	EndDate date not null
);

create table AppUser(
	Id uniqueidentifier default newid() primary key,
	Username nvarchar(200),
	DisplayName nvarchar(200),
	UserPassword nvarchar(100) not null
);


create table UserRole(
	Id uniqueidentifier default newid() primary key,
	Description nvarchar(100)
);

create table AppUserRoleMember(
	Id uniqueidentifier default newid() primary key,
	AppUserId uniqueidentifier not null,
	UserRoleId uniqueidentifier not null,

	constraint FK_AppUserRoleMember_AppUserId foreign key (AppUserId) references AppUser (ID),
	constraint FK_AppUserRoleMember_UserRoleId foreign key (UserRoleId) references UserRole (ID)
);


create table ModeOfStudy(
	Id int not null,
	Description nvarchar(20),

	constraint PK_ModeOfStudy_Id primary key (Id),
	constraint UQ_ModeOfStudy_Description unique (Description),
);

create table ApplicationStatus(
	Id int not null,
	Description nvarchar(50) not null,

	constraint PK_ApplicationStatus_ID primary key (Id),
	constraint UQ_ApplicationStatus_Description unique (Description)
);

--ASSUMPTION - all Programmes of Study are available for all Admission Terms and all Modes of Study
create table Application(
	Id uniqueidentifier default newid() primary key,
	AdmissionTermId uniqueidentifier not null,
	ProgrammeOfStudyId uniqueidentifier not null,
	StudentId uniqueidentifier not null,
	ModeOfStudyId int not null,
	ApplicationStatusId int not null,
	Comments nvarchar(max),
	ApplicationTimestamp datetime not null default current_timestamp,

	constraint FK_Application_ApplicationTerm foreign key (AdmissionTermId) references AdmissionTerm(Id),
	constraint FK_Application_ProgrammeOfStudyId foreign key (ProgrammeOfStudyId) references ProgrammeOfStudy(Id),
	constraint FK_Application_StudentId foreign key (StudentId) references Student(Id),
	constraint FK_Application_ModeOfStudyId foreign key (ModeOfStudyId) references ModeOfStudy(Id),
	constraint FK_Application_ApplicationStatusId foreign key (ApplicationStatusId) references ApplicationStatus(Id)
);


--begin
--	create view vUsers as
--	select EmailAddress Username, FirstName + ' ' + LastName DisplayName, UserPassword, 'Student' UserRole 
--	from Student
--	union all
--	select Username, DisplayName, UserPassword, r.Description RoleName
--	from AppUser u
--		inner join AppUserRoleMember m on u.Id=m.AppUserId
--		inner join UserRole r on r.Id=m.UserRoleId;
--end;
