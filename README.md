1 - sql запросы для создания таблиц и создания пользователя (для создания бд использовал sqllocal create)
CREATE TABLE Institute
(
	 ID INT PRIMARY KEY,
        Name VARCHAR(255),
	 CountryName VARCHAR(255),
	 WebPage VARCHAR(255)
);

CREATE LOGIN you_username WITH PASSWORD = 'your_password';
CREATE USER you_usernaFOR LOGIN you_username;
EXEC sp_addrolemember 'db_owner', ' you_username '

2 - для заполнения бд используется проект etl (решение настроено таким образом, чтобы при переключении с проекта на проект запускался нужный)
для заполнения базы данных можно вручную вводить страны в консоль или нажать enter для рандомного выбора стран и заполнения данными бд
