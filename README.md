Job test tasks
---
####Trinetix:

Просканировать папку на предмет наличия текстовых файлов, файлы разделить на слова, разработать БД для хранения следующей информации - имя файла, его метоположение на диске, слово, информация о том в каком файле это слово находится и в какой позиции (номер строки, номер колонки)

Требования к выполнению задания:

Задание должно быть реализовано с максимальным использование ООП. Суть задания не в том чтобы убедиться что кандидат знает инструмент, а в том, чтобы увидеть, как он им умеет пользоваться. Должно быть заимплеменчено WEB.API используя методы которого можно получить информацию типа:
* Список просканированных файлов (+ paging)
* Список слов внутри одного файла (+ paging)
* Список позиций конретного слова в конкретном файле (+ paging)

База данных MSSQL, должны присутствовать модульные тесты. База данных должна создаваться скриптом - это для того чтобы ее можно было легко восcоздать при проверке тестового задания, или должен быть приложен bak базы.



Scan a folder for the presence of text files, devide files into words, develop  database to store the following information - name, location on the disk,  word, the information in any file that word is and what position (line number, column number)

The requirements:

The job must be done with the maximum use of the OOP. The essence of the job is not to make sure that the candidate knows the tool, and to see how he knows how to use them. Must be implemented WEB.API using methods which you can get information such as:
* The list of scanned files (+ paging)
* A list of words within a single file (+ paging)
* List each words in a particular file (+ paging)

Database MSSQL, unit tests must be present. The database must be created script - this in order that it can be easily recreated on checking tests, or shall be accompanied bak base.

---
####Daxx:

You need to create a simple web page which should contain a registration wizard. The registration wizard
contains two steps:

#####Step 1: (all fields are required)
* Login valid email.
* Password must contain min 1 digit and min 1 letter.
* Confirm password must be the same with the field “password”.
* I agree checkbox is a required checkbox.
* Button next should validate all fields on the step and show validation errors (under the fields) or go to next step.

#####Step 2: (all fields are required)
* Country drop down list which contains list of counties.
* Province contains list of provinces for selected country. The list of provinces should be loaded by AJAX if country is  changed.
* Button save should should validate all fields on the step and show validation errors (under the fields) or save the data from the wizard to the database using AJAX call.

#####Tech requirements:
* Use ASP.NET MVC + ASP.NET MVC WebAPI as platform 
* Use client side MVVM framework (KnockoutJS, AngularJS, etc.)
* Use Bootstrap as design framework
* Use MS SQL (localDb) and Entity Framework + Migrations as DB and ORM
* The result should be presented as working Visual Studio solution / project. Run F5 to get working.
* Beside this, feel free to choose any other libraries, frameworks, patterns, test (fake) data, etc. For example you database with country/province can contain only 2 test countries and 23 test provinces for each country (Country 1, Province 1.1, Province 1.2, is also okay). 

If you miss some information, do not ask do it in the way you think it should be done correctly .
