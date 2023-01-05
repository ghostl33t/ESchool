#ESCHOOL - ASP NET CORE API

API namijenjen za evidenciju rada učenika u školama. 

#### TEHNOLOGIJE
C# - ASP NET CORE 6.0
MSSQL SERVER


####NUGET PAKETI
-Microsoft.EntityFrameworkCore.SqlServer
-Microsoft.EntityFrameworkCore.Tools
-Microsoft.AspNetCore.Authentication.JwtBearer
-Microsoft.IdenityModel.Tokens
-System.IdentityModel.Tokens.Jwt
-AutoMapper
-AutoMapper.Extension.Microsoft.DependencyInjection

**Dijelovi aplikacije**

[TOCM]

[TOC]
#Baza podataka
Aplikacija koristi 2 baze podataka. 
1.Glavna baza u kojoj će se upisivati ocjene, odjeljenja, korisnici...
2.Baza registara u kojoj se nalazi tabela predmeta, vrsta škola.
###Podešavanje baze
1. Pokrenuti komande u "Package Manager" konzoli:
`add-migration NazivMigracije --context DBRegistries`
na primjer:
`add-migration CreateDBRegistries --context DBRegistries`
Uraditi update baze registara
`update-database --context DBRegistries`
2. Pokrenuti komande u "Package Manager" konzoli:
`add-migration NazivMigracije --context DBMain`
na primjer:
`add-migration CreateDBMain --context DBMain`
Uraditi update glavne baze
`update-database --context DBMain`
3. Pokrenuti komandu u "Package Manager" konzoli za kreiranje pogleda iz DBRegistries u DBMain (pogledi na predmete i vrste skola):
`update-database -target AddViewSchoolList --context DBMain`
`update-database -target AddViewSubjects --context DBMain`

#Struktura aplikacije
###MODELI
######BAZA: DBREGISTRIES
1. Subjects - Tabela zaduzena za spremanje svih predmeta u školama.
2. SchoolList - Tabela zaduzena za spremanje svih vrsta škola.

######BAZA: DBMAIN
1. Users - Tabela zaduzena za spremanje svih korisnika aplikacije
2. tempEmail - Tabela zaduzena za spremanje temp podataka o emailovima koje ce servis slati (WIP).
3. StudentGrades - Tabela zaduzena za spremanje ocjena učenika.
4. StudentDetails - Tabela zaduzena za spremanje detaljnih podataka učenika.
5.  ClassDepartment - Tabela svih odjeljenja u školi.
6. ProfessorSubjects - Tabela veze između profesora i predmeta koje određeni profesori predaju.
7. ClassSubjects - Tabela veze između odjeljenja i predmeta koje odjeljenja imaju.
8. ClassProfessors - Tabela veze između odjeljenja i profesora koji predaju odjeljenjima.

###KONTROLERI
Kontroleri ako su validacije zadovoljene pozivaju repozitorij. 

1. UserController
2. SubjectController
3. StudentGradesController
4. StudentDetailsController
5. SchoolListController
6. ProfessorSubjectsController
7. LoginController
8. ClassSubjectsController
9. ClassProfessorsController
10. ClassDepartmentController

###REPOZITORIJI
Repozitoriji su zaduženi za komunikaciju sa bazom podataka.
Svaki repozitorij sadrži osnovne CRUD operacije. 

#####Prikaz "složenijih podataka"

###### IProfessorSubjectsRepository.cs
`GetProfessorsAndSubjects()` - Vraća listu objekata tipa **GetprofessorSubjects** (putanja: Models.DTOs.ProfessorSubjects). 
1.Ime profesora 
2.Predmet koji profesor predaje.

###### IClassSubjects.cs
`GetSubjectsPerClass(classDepartmentId)` - Vraća listu objekata tipa **GetClassSubject** (Putanja Models.DTOs.ClassSubjects).
U listi se nalaze predmeti koje određeno odjeljenje ima. 

###### IClassDepartment.cs
`GetStudentsPerClassDetailsAsync(classDepartmentId)` - Vraća listu objekata tipa **GetStudentDetails**(putanja: Models.DTOs.StudentDetails).
1.  Ime i prezime studenta
2. Vrsta škole
3. Razred

###VALIDACIJE
Sve validacije se nalaze u folderu: '  Validations ' .
Validacije se pozivaju iz kontrolera aplikacije. Svaka validacija ima sljedeće objekte:
1. code - Zasluzan da spremi na osnovu validacije StatusCode response-a https://en.wikipedia.org/wiki/List_of_HTTP_status_codes
2. validationMessage - Ovisno od objekta 'code' sprema poruku koja će biti vraćena kao response.

######VALIDACIJE KORISNIKA (IUserValidations.cs)
1. `ValidateUserNameLength(username)` - Validira duzinu polja 'username',
2. `ValidateUserNameUnique(username)` - Validira da li je 'username' unikatan.
3. `ValidateUserPassword(password)` - Validira šifru korisnika.
4. `ValidateCreateUserByType(usertype)` - Validira kreatora novog korisnika (Tip = 0).
5. `ValidateUserNameAndLastName(imePrezime)` - Validira ime i prezime korisnika.
6. `ValidateUserOIB(oib)`- Validira OIB korisnika.
7. `ValidateUserOIBUnique(OIB)` - Validira unikatnost OIB-a.
8. `ValidateUserPhone(phone)` - Validira broj korisnika. 
9. `ValidateUserPhoneUnique(phone)` - Validira unikatnost broja korisnika.

######VALIDACIJE DETALJA STUDENTA (IStudentDetailsValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateStudent(studentId)` - Validira da li je korisnik student.
3. `ValidateClassDepartment(classDepId)` - Validira da li je proslijeđeni razred validan.
4. `ValidateParent(usertype)` - Validacija korisnika da li je roditelj.
5. `ValidateDiscipline(discipline)` - Validira vladanje korisnika (1-5).

######VALIDACIJE PREDMETA (ISubjectValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateSerialUnique(serialNumber)` - Validira unikatnost rednog broja.
3. `ValidateSerialNumberLength(serialNumber)` - Validira dužinu rednog broja (3-5).
4. `ValidateSubjectName(usertype)` - Validira unikatnost naziva predmeta.

######VALIDACIJE OCJENA (IStudentGradesValidations.cs)
1. `ValidateGrade(grade)` - Validira ocjenu (1-5),
2. `ValidateDescription(description)` - Validira opis ocjene.
3. `Validate(object)` - Ovisno o tipu requesta prosljedjuje se razlicit objekat, validira vezu izmedju studenta, predmeta i profesora. 

######VALIDACIJE VRSTE ŠKOLE (ISchoolListValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateSerialUnique(serialNumber)` - Validira unikatnost rednog broja.
3. `ValidateSerialNumberLength(serialNumber)` - Validira dužinu rednog broja (3-5).
4. `ValidateSchoolName(schoolName)` - Validira unikatnost naziva predmeta.
5. `ValidateSchoolType(schoolType)` - Validira tip škole (0 - četverogodišnja, 1 - trogodišnja).

######VALIDACIJE ODJELJENJA (IClassDepartmentValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateSerialUnique(serialNumber)` - Validira unikatnost rednog broja.
3. `ValidateSerialNumberLength(serialNumber)` - Validira dužinu rednog broja (3-5).
4. `ValidateSchoolName(schoolName)` - Validira unikatnost naziva predmeta.
5. `ValidateSchoolListId(schoolType)` - Validira da li proslijeđena vrsta škole postoji.

######VALIDACIJE PROFESOR-PREDMET (IProfessorSubjectsValidation.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0).
2. `ValidateProfessorType(UserType)` - Validira tip usera koji je proslijeđen kao profesor.
3. `ValidateProfessorRepeating(profSubjId,professorId,subjectId)` - Validira da se veza između profesora i predmeta ne upiše 2 puta. 
4. `ValidateSubject(subjectId) - ` Validira da li je predmet validan

######VALIDACIJE ODJELJENJE-PREDMET (IClassSubjectsValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0).
2. `ValidateClassDepartment(classDepartmentId)` -Validira da li je proslijeđeno odjeljenje validno
3. `ValidateSubject(subjectId) - ` Validira da li je predmet validan

######VALIDACIJE ODJELJENJE-PROFESOR (IClassProfessorsValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0).
2. `ValidateClassDepartment(classDepartmentId)` -Validira da li je proslijeđeno odjeljenje validno
3. `ValidateProfessor(professorId) - ` Validira da li je profesor validan.


###SERVISI
###### LOGIN SERVIS
Validira da li je user kreiran u bazi podataka. Ako jeste vraća JWTBearer token.
###### RESPONSE SERVICE 
Na osnovu proslijeđenog status code-a vraća response. (koristi se u kontrolerima).
###### AEMAIL
/-----------------------------------/