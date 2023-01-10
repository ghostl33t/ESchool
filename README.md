# ESCHOOL - ASP NET CORE API

API namijenjen za evidenciju rada učenika u školama. <br />

#### TEHNOLOGIJE
C# - ASP NET CORE 6.0 <br />
MSSQL SERVER 
ANGULAR

#### NUGET PAKETI
-Microsoft.EntityFrameworkCore.SqlServer <br />
-Microsoft.EntityFrameworkCore.Tools <br />
-Microsoft.AspNetCore.Authentication.JwtBearer <br />
-Microsoft.IdenityModel.Tokens <br />
-System.IdentityModel.Tokens.Jwt <br />
-AutoMapper <br />
-AutoMapper.Extension.Microsoft.DependencyInjection <br />

# Struktura 
### MODELI
###### BAZA: DBREGISTRIES
1. Subjects - Tabela zaduzena za spremanje svih predmeta u školama.<br />
2. SchoolList - Tabela zaduzena za spremanje svih vrsta škola.

###### BAZA: DBMAIN
1. Users - Tabela zaduzena za spremanje svih korisnika aplikacije<br />
2. tempEmail - Tabela zaduzena za spremanje temp podataka o emailovima koje ce servis slati (WIP).<br />
3. StudentGrades - Tabela zaduzena za spremanje ocjena učenika.<br />
4. StudentDetails - Tabela zaduzena za spremanje detaljnih podataka učenika.<br />
5.  ClassDepartment - Tabela svih odjeljenja u školi.<br />
6. ProfessorSubjects - Tabela veze između profesora i predmeta koje određeni profesori predaju.<br />
7. ClassSubjects - Tabela veze između odjeljenja i predmeta koje odjeljenja imaju.<br />
8. ClassProfessors - Tabela veze između odjeljenja i profesora koji predaju odjeljenjima.<br />

### KONTROLERI
Kontroleri ako su validacije zadovoljene pozivaju repozitorij. <br />

1. UserController<br />
2. SubjectController<br />
3. StudentGradesController<br />
4. StudentDetailsController<br />
5. SchoolListController<br />
6. ProfessorSubjectsController<br />
7. LoginController<br />
8. ClassSubjectsController<br />
9. ClassProfessorsController<br />
10. ClassDepartmentController<br />

### REPOZITORIJI
Svaki repozitorij izvršava osnovne CRUD operacije. 

##### Prikaz "složenijih podataka"

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

### VALIDACIJE
Sve validacije se nalaze u folderu: '  Validations ' .
Validacije se pozivaju iz kontrolera aplikacije. Svaka validacija ima sljedeće objekte:
1. code - Zasluzan da spremi na osnovu validacije StatusCode response-a https://en.wikipedia.org/wiki/List_of_HTTP_status_codes
2. validationMessage - Ovisno od objekta 'code' sprema poruku koja će biti vraćena kao response.

###### VALIDACIJE KORISNIKA (IUserValidations.cs)
1. `ValidateUserNameLength(username)` - Validira duzinu polja 'username',
2. `ValidateUserNameUnique(username)` - Validira da li je 'username' unikatan.
3. `ValidateUserPassword(password)` - Validira šifru korisnika.
4. `ValidateCreateUserByType(usertype)` - Validira kreatora novog korisnika (Tip = 0).
5. `ValidateUserNameAndLastName(imePrezime)` - Validira ime i prezime korisnika.
6. `ValidateUserOIB(oib)`- Validira OIB korisnika.
7. `ValidateUserOIBUnique(OIB)` - Validira unikatnost OIB-a.
8. `ValidateUserPhone(phone)` - Validira broj korisnika. 
9. `ValidateUserPhoneUnique(phone)` - Validira unikatnost broja korisnika.

###### VALIDACIJE DETALJA STUDENTA (IStudentDetailsValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateStudent(studentId)` - Validira da li je korisnik student.
3. `ValidateClassDepartment(classDepId)` - Validira da li je proslijeđeni razred validan.
4. `ValidateParent(usertype)` - Validacija korisnika da li je roditelj.
5. `ValidateDiscipline(discipline)` - Validira vladanje korisnika (1-5).

###### VALIDACIJE PREDMETA (ISubjectValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateSerialUnique(serialNumber)` - Validira unikatnost rednog broja.
3. `ValidateSerialNumberLength(serialNumber)` - Validira dužinu rednog broja (3-5).
4. `ValidateSubjectName(usertype)` - Validira unikatnost naziva predmeta.

###### VALIDACIJE OCJENA (IStudentGradesValidations.cs)
1. `ValidateGrade(grade)` - Validira ocjenu (1-5),
2. `ValidateDescription(description)` - Validira opis ocjene.
3. `Validate(object)` - Ovisno o tipu requesta prosljedjuje se razlicit objekat, validira vezu izmedju studenta, predmeta i profesora. 

###### VALIDACIJE VRSTE ŠKOLE (ISchoolListValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateSerialUnique(serialNumber)` - Validira unikatnost rednog broja.
3. `ValidateSerialNumberLength(serialNumber)` - Validira dužinu rednog broja (3-5).
4. `ValidateSchoolName(schoolName)` - Validira unikatnost naziva predmeta.
5. `ValidateSchoolType(schoolType)` - Validira tip škole (0 - četverogodišnja, 1 - trogodišnja).

###### VALIDACIJE ODJELJENJA (IClassDepartmentValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0),
2. `ValidateSerialUnique(serialNumber)` - Validira unikatnost rednog broja.
3. `ValidateSerialNumberLength(serialNumber)` - Validira dužinu rednog broja (3-5).
4. `ValidateSchoolName(schoolName)` - Validira unikatnost naziva predmeta.
5. `ValidateSchoolListId(schoolType)` - Validira da li proslijeđena vrsta škole postoji.

###### VALIDACIJE PROFESOR-PREDMET (IProfessorSubjectsValidation.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0).
2. `ValidateProfessorType(UserType)` - Validira tip usera koji je proslijeđen kao profesor.
3. `ValidateProfessorRepeating(profSubjId,professorId,subjectId)` - Validira da se veza između profesora i predmeta ne upiše 2 puta. 
4. `ValidateSubject(subjectId) - ` Validira da li je predmet validan

###### VALIDACIJE ODJELJENJE-PREDMET (IClassSubjectsValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0).
2. `ValidateClassDepartment(classDepartmentId)` -Validira da li je proslijeđeno odjeljenje validno
3. `ValidateSubject(subjectId) - ` Validira da li je predmet validan

###### VALIDACIJE ODJELJENJE-PROFESOR (IClassProfessorsValidations.cs)
1. `ValidateCreator(CreatedById)` - Validira kreatora 'UserType' korisnika koji dodaje novi predmet (Tip = 0).
2. `ValidateClassDepartment(classDepartmentId)` -Validira da li je proslijeđeno odjeljenje validno
3. `ValidateProfessor(professorId) - ` Validira da li je profesor validan.


### SERVISI
###### LOGIN SERVIS
Validira da li je user kreiran u bazi podataka. Ako jeste vraća JWTBearer token.
###### RESPONSE SERVICE 
Na osnovu proslijeđenog status code-a vraća response. (koristi se u kontrolerima).
###### AEMAIL
/-----------------------------------/

