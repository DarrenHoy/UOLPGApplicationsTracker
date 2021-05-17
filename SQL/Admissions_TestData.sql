use PGProgrammeApplications
go

delete from Application;
delete from Student;
delete from ProgrammeOfStudy;
delete from AdmissionTerm;
delete from ApplicationStatus;
delete from AppUserRoleMember
delete from AppUser;
delete from UserRole;
delete from ModeOfStudy;



insert into AdmissionTerm(Description, StartDate,EndDate)
select '2020-21','01-SEP-2020','31-AUG-2021'
union all select '2021-22','01-SEP-2021','31-AUG-2022'
union all select '2022-23','01-SEP-2022','31-AUG-2023'
union all select '2023-24','01-SEP-2023','31-AUG-2024';

--I am doing a terrible thing and putting passwords into the database in plain text!
insert into AppUser(Username, DisplayName, UserPassword)
select 'AdminBob','Bob','adminbob'
union all select 'AdminSarah','Sarah','adminsarah'
union all select 'AdminJane', 'Jane', 'adminjane';

insert into UserRole(Description)
values ('Administrator');

insert into AppUserRoleMember(AppUserId, UserRoleId)
select AppUserId, UserRoleId 
from
(
	select Id AppUserId, (select Id from UserRole where Description='Administrator') UserRoleId
	from AppUser
) as AppRoleMembers;

insert into ApplicationStatus(Id, Description)
select 1, 'Submitted'
union all select 2, 'Under review'
union all select 3, 'Approved'
union all select 4, 'Rejected'

insert into ModeOfStudy(Id, Description)
select 1, 'Full time'
union all select 2, 'Part time'

insert into ProgrammeOfStudy(Description)
SELECT 'Town and Regional Planning - Master of Arts (MA)'
 UNION ALL SELECT 'Thoroughbred Horseracing Industries - Master of Business Admin'
 UNION ALL SELECT 'Theoretical Computer Science with a Year in Industry - Master of Science (MSc)'
 UNION ALL SELECT 'Theoretical Computer Science - Master of Science (MSc)'
 UNION ALL SELECT 'Telecommunications and Wireless Systems with a Year in Industry - Master of Science (Eng)'
 UNION ALL SELECT 'Telecommunications and Wireless Systems - Master of Science (Eng)'
 UNION ALL SELECT 'Teaching English to Speakers of Other Languages - Master of Arts (MA)'
 UNION ALL SELECT 'Sustainable Heritage Management - Master of Arts (MA)'
 UNION ALL SELECT 'Sustainable Civil and Structural Engineering - Master of Science (Eng)'
 UNION ALL SELECT 'Strategic Communication - Master of Science (MSc)'
 UNION ALL SELECT 'Sports Business and Management - Master of Science (MSc)'
 UNION ALL SELECT 'Social Research Methods - Master of Arts (MA)'
 UNION ALL SELECT 'Social Research - Master of Research (MRes)'
 UNION ALL SELECT 'Sensor Technologies and Enterprise - Master of Science (MSc)'
 UNION ALL SELECT 'Research Methods in Psychology - Master of Science (MSc)'
 UNION ALL SELECT 'Radiotherapy - PG Diploma in Radiotherapy (PGDip)'
 UNION ALL SELECT 'Radiotherapy - Master of Science (MSc)'
 UNION ALL SELECT 'Radiometrics: Instrumentation and Modelling - Master of Science (MSc)'
 UNION ALL SELECT 'Public Health - Master of Public Health (MPH)'
 UNION ALL SELECT 'Psychology (Conversion) - Master of Science (MSc)'
 UNION ALL SELECT 'Project Management - Master of Science (MSc)'
 UNION ALL SELECT 'Product Design and Management - Master of Science (Eng)'
 UNION ALL SELECT 'Physiotherapy (Pre-Reg) - Master of Science (MSc)'
 UNION ALL SELECT 'Philosophy - Master of Arts (MA)'
 UNION ALL SELECT 'Performance - Master of Music (MMus)'
 UNION ALL SELECT 'Palaeoanthropology - Master of Science (MSc)'
 UNION ALL SELECT 'Palaeoanthropology - Master of Research (MRes)'
 UNION ALL SELECT 'Organ Transplantation - Master of Science (MSc)'
 UNION ALL SELECT 'Operations and Supply Chain Management - Master of Science (MSc)'
 UNION ALL SELECT 'Occupational Therapy (Pre-Reg) - Master of Science (MSc)'
 UNION ALL SELECT 'Occupational and Organisational Psychology - Master of Science (MSc)'
 UNION ALL SELECT 'Nursing - Master of Science (MSc)'
 UNION ALL SELECT 'Nuclear Science and Technology - Master of Science (MSc)'
 UNION ALL SELECT 'Music Industry Studies - Master of Arts (MA)'
 UNION ALL SELECT 'Music - Master of Research (MRes) (subject to approval)'
 UNION ALL SELECT 'Musculoskeletal Ageing - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Translation Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Spanish Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Sociolinguistics) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Portuguese Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Italian Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Hispanic Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (German Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (French Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Film Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Chinese Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Catalan Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Modern Languages and Cultures (Basque Studies) - Master of Research (MRes)'
 UNION ALL SELECT 'Microelectronic Systems (with a Year in Industry) - Master of Science (Eng)'
 UNION ALL SELECT 'Microelectronic Systems - Master of Science (Eng)'
 UNION ALL SELECT 'Mental Health Nursing (Pre-Reg) - Master of Science (MSc)'
 UNION ALL SELECT 'Medical Education - Postgraduate Diploma (PGDip)'
 UNION ALL SELECT 'Media, Culture and Everyday Life - Master of Arts (MA)'
 UNION ALL SELECT 'Media and Politics - Master of Arts (MA)'
 UNION ALL SELECT 'Media and Communication (Media and Politics) - Master of Arts (MA)'
 UNION ALL SELECT 'Media and Communication (Digital Culture and Communication) - Master of Arts (MA)'
 UNION ALL SELECT 'Mechanical Engineering with Management - Master of Science (MSc)'
 UNION ALL SELECT 'Mechanical Engineering Design with Management - Master of Science (MSc)'
 UNION ALL SELECT 'Mathematical Sciences - Master of Science (MSc)'
 UNION ALL SELECT 'Masters in Management - (MIM)'
 UNION ALL SELECT 'Master of Business Admin - Master of Business Admin'
 UNION ALL SELECT 'Marketing - Master of Science (MSc)'
 UNION ALL SELECT 'Management - Master of Research (MRes)'
 UNION ALL SELECT 'Law Medicine and Health Care - Master of Laws (LLM)'
 UNION ALL SELECT 'Law - Master of Laws (LLM)'
 UNION ALL SELECT 'Latin American Studies - Master of Research (MRes)'
 UNION ALL SELECT 'Irish Studies - Master of Research (MRes)'
 UNION ALL SELECT 'Irish Studies - Master of Arts (MA)'
 UNION ALL SELECT 'Investigative and Forensic Psychology - Master of Science (MSc)'
 UNION ALL SELECT 'International Relations and Security - Master of Research (MRes)'
 UNION ALL SELECT 'International Relations and Security - Master of Arts (MA)'
 UNION ALL SELECT 'International Human Rights Law - Master of Laws (LLM)'
 UNION ALL SELECT 'International Economic Law - Master of Laws (LLM)'
 UNION ALL SELECT 'International Business - Master of Science (MSc)'
 UNION ALL SELECT 'Infection and Immunity - Master of Science (MSc)'
 UNION ALL SELECT 'Human Resource Management - Master of Science (MSc)'
 UNION ALL SELECT 'Housing and Community Planning - Master of Arts (MA)'
 UNION ALL SELECT 'History - Master of Research (MRes)'
 UNION ALL SELECT 'History - Master of Arts (MA)'
 UNION ALL SELECT 'Geographic Data Science - Master of Science (MSc)'
 UNION ALL SELECT 'Football Industries - Master of Business Admin'
 UNION ALL SELECT 'Financial Technology - Master of Science (MSc)'
 UNION ALL SELECT 'Financial Mathematics - Master of Science (MSc)'
 UNION ALL SELECT 'Finance and Investment Management - Master of Science (MSc)'
 UNION ALL SELECT 'Finance - Master of Science (MSc)'
 UNION ALL SELECT 'Environmental Science - Master of Science'
 UNION ALL SELECT 'Environmental Assessment and Management - Master of Science (MSc)'
 UNION ALL SELECT 'Environment and Climate Change - Master of Science (MSc)'
 UNION ALL SELECT 'Entrepreneurship and Innovation Management - Master of Science (MSc)'
 UNION ALL SELECT 'English Literature - Master of Arts (MA)'
 UNION ALL SELECT 'Energy and Power Systems (with a Year in Industry) - Master of Science (Eng)'
 UNION ALL SELECT 'Energy and Power Systems - Master of Science (Eng)'
 UNION ALL SELECT 'Egyptology - Master of Research (MRes)'
 UNION ALL SELECT 'Egyptology - Master of Arts (MA)'
 UNION ALL SELECT 'Economics - Master of Science (MSc)'
 UNION ALL SELECT 'Diagnostic Radiography (Pre-Reg) - Master of Science (MSc)'
 UNION ALL SELECT 'Data Science and Artificial Intelligence with a Year in Industry - Master of Science (MSc)'
 UNION ALL SELECT 'Data Science and Artificial Intelligence - Master of Science (MSc)'
 UNION ALL SELECT 'Criminological Research - Master of Research (MRes)'
 UNION ALL SELECT 'Contemporary Human Geography (Research Methods) - Master of Arts (MA)'
 UNION ALL SELECT 'Contemporary Europe - Master of Arts (MA)'
 UNION ALL SELECT 'Conflict Transformation and Social Justice - Master of Arts (MA)'
 UNION ALL SELECT 'Computer Science - Master of Science (MSc)'
 UNION ALL SELECT 'Clinical Sciences (Medical Physics) - Master of Science (MSc)'
 UNION ALL SELECT 'Clinical Sciences - Master of Research (MRes)'
 UNION ALL SELECT 'Climate Resilience and Environmental Sustainability in Architecture - Master of Science (MSc)'
 UNION ALL SELECT 'Classics and Ancient History - Master of Research (MRes)'
 UNION ALL SELECT 'Classics and Ancient History - Master of Arts (MA)'
 UNION ALL SELECT 'Classical Music Industry - Master of Arts (MA)'
 UNION ALL SELECT 'Chinese-English Translation and Interpreting - Master of Arts (MA)'
 UNION ALL SELECT 'Chemistry - Master of Science (MSc)'
 UNION ALL SELECT 'Cancer Biology and Therapy - Master of Science (MSc)'
 UNION ALL SELECT 'Business Analytics and Big Data - Master of Science (MSc)'
 UNION ALL SELECT 'Building Information Modelling - Master of Science (MSc)'
 UNION ALL SELECT 'Bovine Reproduction - Postgraduate Diploma'
 UNION ALL SELECT 'Biotechnology - Master of Science (MSc)'
 UNION ALL SELECT 'Biomedical Science and Translational Medicine - Master of Research (MRes)'
 UNION ALL SELECT 'Biomedical Engineering with Management (Healthcare) - Master of Science (Eng)'
 UNION ALL SELECT 'Biomedical Engineering with Management - Master of Science (Eng)'
 UNION ALL SELECT 'Biomedical Engineering (Healthcare) - Master of Science (Eng)'
 UNION ALL SELECT 'Biomedical Engineering - Master of Science (Eng)'
 UNION ALL SELECT 'Bioinformatics - Master of Science (MSc)'
 UNION ALL SELECT 'Big Data and High Performance Computing with a Year in Industry - Master of Science (MSc)'
 UNION ALL SELECT 'Big Data and High Performance Computing - Master of Science (MSc)'
 UNION ALL SELECT 'Beatles, Music Industry and Heritage - Master of Arts (MA)'
 UNION ALL SELECT 'Arts - Master of Research (MRes)'
 UNION ALL SELECT 'Art Philosophy and Cultural Institutions - Master of Arts (MA)'
 UNION ALL SELECT 'Archives and Records Management - Master of Research (MRes)'
 UNION ALL SELECT 'Archives and Records Management - International - Master of Arts (MA)'
 UNION ALL SELECT 'Archives and Record Management - Master of Arts (MA)'
 UNION ALL SELECT 'Architecture - Master of Arts (MA)'
 UNION ALL SELECT 'Architecture - Master of Architecture (MArch)'
 UNION ALL SELECT 'Archaeology - Master of Science (MSc)'
 UNION ALL SELECT 'Archaeology - Master of Research (MRes)'
 UNION ALL SELECT 'Archaeology - Master of Arts (MA)'
 UNION ALL SELECT 'Applied Linguistics - Master of Arts (MA)'
 UNION ALL SELECT 'Advanced Practice in Healthcare - Masters of Science (MSc)'
 UNION ALL SELECT 'Advanced Mechanical Engineering - Master of Science (Eng)'
 UNION ALL SELECT 'Advanced Mechanical Engineering  - Master of Science (Eng)'
 UNION ALL SELECT 'Advanced Manufacturing Systems and Technology - Master of Science (Eng)'
 UNION ALL SELECT 'Advanced Computer Science with Internet Economics with a Year in Industry - Master of Science (MSc)'
 UNION ALL SELECT 'Advanced Computer Science with Internet Economics - Master of Science (MSc)'
 UNION ALL SELECT 'Advanced Computer Science with a Year in Industry - Master of Science (MSc)'
 UNION ALL SELECT 'Advanced Computer Science - Master of Science (MSc)'
 UNION ALL SELECT 'Advanced Chemical Sciences - Master of Science (MSc)'
 UNION ALL SELECT 'Advanced Biological Sciences - Master of Science (MSc)'
 UNION ALL SELECT 'Advanced Biological Sciences - Master of Research (MRes)'
 UNION ALL SELECT 'Advanced Aerospace Engineering - Master of Science (Eng)'
 UNION ALL SELECT 'Accounting and Finance - Master of Science (MSc)'
 UNION ALL SELECT 'Accounting and Finance - Master of Science (MSc)'



insert into Student(FirstName, LastName, DateOfBirth, EmailAddress, IsUkResident, Username, UserPassword)
select 'STEVEN','LAWSON',CONVERT(datetime, '16/12/1977', 103),'steven.lawson@anemailprovider.com','Y','steven.lawson@anemailprovider.com','password123'
union all select 'KAREN','DE-PETRO',CONVERT(datetime, '05/02/1956', 103),'karen.de-petro@anemailprovider.com','Y','karen.de-petro@anemailprovider.com','password123'
union all select 'KATE','WHITEING',CONVERT(datetime, '16/06/1972', 103),'kate.whiteing@barfoo.co.uk','N','kate.whiteing@barfoo.co.uk', 'password123'
union all select 'CLARE','MANN',CONVERT(datetime, '03/04/1986', 103),'clare.mann@foobar.ac.uk','Y','clare.mann@foobar.ac.uk','password123'
union all select 'TIANNA','VILLA',CONVERT(datetime, '30/09/1994', 103),'tianna.villa@foobar.ac.uk','Y','tianna.villa@foobar.ac.uk','password123'
union all select 'AMBER','HART',CONVERT(datetime, '05/04/1997', 103),'amber.hart@foobar.ac.uk','Y','amber.hart@foobar.ac.uk','password123'
union all select 'BRETT','ROY', CONVERT(datetime, '24/06/1968', 103),'brett.roy@foobar.ac.uk','Y','brett.roy@foobar.ac.uk','password123'


union all select 'ABDIEL','WOODARD',CONVERT(datetime, '22/06/1993', 103),'abdiel.woodard@foobar.ac.uk','Y','password123'
union all select 'LAWSON','WONG',CONVERT(datetime, '28/02/2003', 103),'lawson.wong@anemailprovider.com','Y','password123'
union all select 'KENDALL','INGRAM',CONVERT(datetime, '15/07/1978', 103),'kendall.ingram@foobar.ac.uk','Y','password123'
union all select 'CECILIA','CHEN',CONVERT(datetime, '09/05/1987', 103),'cecilia.chen@foobar.ac.uk','Y','password123'
union all select 'VALERIA','GILLESPIE',CONVERT(datetime, '15/03/1999', 103),'valeria.gillespie@foobar.ac.uk','Y','password123'
union all select 'JACQUELYN','KANE',CONVERT(datetime, '14/10/1996', 103),'jacquelyn.kane@anemailprovider.com','Y','password123'
union all select 'SARAHI','REILLY',CONVERT(datetime, '10/03/1980', 103),'sarahi.reilly@barfoo.co.uk','Y','password123'
union all select 'CARSON','GREER',CONVERT(datetime, '17/09/1994', 103),'carson.greer@barfoo.co.uk','Y','password123'
union all select 'LEON','SINGH',CONVERT(datetime, '14/09/1983', 103),'leon.singh@anemailprovider.com','Y','password123'
union all select 'BECKETT','FRANK',CONVERT(datetime, '09/08/1991', 103),'beckett.frank@foobar.ac.uk','Y','password123'
union all select 'ANYA','DUKE',CONVERT(datetime, '04/04/1995', 103),'anya.duke@barfoo.co.uk','Y','password123'
union all select 'MADELINE','KEY',CONVERT(datetime, '07/04/1999', 103),'madeline.key@anemailprovider.com','Y','password123'
union all select 'BRIANNA','OWENS',CONVERT(datetime, '03/11/1985', 103),'brianna.owens@anemailprovider.com','Y','password123'

union all select 'JAYLEN','HART',CONVERT(datetime, '05/07/1984', 103),'jaylen.hart@foobar.ac.uk','Y','password123'
union all select 'CHRISTINE','KRISTJANSSON',CONVERT(datetime, '18/08/1997', 103),'christine.kristjansson@barfoo.co.uk','N','password123'
union all select 'SHELBY','LOGAN',CONVERT(datetime, '02/02/2005', 103),'shelby.logan@foobar.ac.uk,57563','Y','password123'



declare @studentId uniqueidentifier, @courseId uniqueidentifier, @admissionTermId uniqueidentifier;

begin	
	select @admissionTermId = Id from AdmissionTerm where Description='2021-22'
	select @studentId = Id from Student where EmailAddress = 'steven.lawson@anemailprovider.com'
	select @courseId = Id from 
		(
			select Id, row_number() over (partition by Id order by Id) rw 
			from ProgrammeOfStudy 
			where Description='Accounting and Finance - Master of Science (MSc)'
		) a where a.rw=1

	insert into Application(AdmissionTermId, ProgrammeOfStudyId, StudentId, ModeOfStudyId, ApplicationStatusId)
	values (@admissionTermId, @courseId, @studentId, 1, 1);
end;

begin
	select @admissionTermId = Id from AdmissionTerm where Description='2022-23'
	select @studentId = Id from Student where EmailAddress = 'steven.lawson@anemailprovider.com'
	select @courseId = Id from 
		(
			select Id, row_number() over (partition by Id order by Id) rw 
			from ProgrammeOfStudy 
			where Description='Arts - Master of Research (MRes)'
		) a where a.rw=1

	insert into Application(AdmissionTermId, ProgrammeOfStudyId, StudentId, ModeOfStudyId, ApplicationStatusId)
	values (@admissionTermId, @courseId, @studentId, 2, 1);
end;


begin
	select @admissionTermId = Id from AdmissionTerm where Description='2020-21'
	select @studentId = Id from Student where EmailAddress = 'kate.whiteing@barfoo.co.uk'
	select @courseId = Id from 
		(
			select Id, row_number() over (partition by Id order by Id) rw 
			from ProgrammeOfStudy 
			where Description='Arts - Master of Research (MRes)'
		) a where a.rw=1

	insert into Application(AdmissionTermId, ProgrammeOfStudyId, StudentId, ModeOfStudyId, ApplicationStatusId)
	values (@admissionTermId, @courseId, @studentId, 2, 1);
end;

begin
	select @admissionTermId = Id from AdmissionTerm where Description='2023-24'
	select @studentId = Id from Student where EmailAddress = 'tianna.villa@foobar.ac.uk'
	select @courseId = Id from 
		(
			select Id, row_number() over (partition by Id order by Id) rw 
			from ProgrammeOfStudy 
			where Description='International Human Rights Law - Master of Laws (LLM)'
		) a where a.rw=1

	insert into Application(AdmissionTermId, ProgrammeOfStudyId, StudentId, ModeOfStudyId, ApplicationStatusId)
	values (@admissionTermId, @courseId, @studentId, 2, 2);
end;


